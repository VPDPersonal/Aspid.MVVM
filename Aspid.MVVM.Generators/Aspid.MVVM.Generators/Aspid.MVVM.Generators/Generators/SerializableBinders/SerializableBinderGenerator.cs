using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Generators.Generators.SerializableBinders;

/// <summary>
/// Emits the serializable half of a binder family from the MonoBehaviour half that carries
/// <c>[GenerateSerializableBinder]</c>.
/// </summary>
/// <remarks>
/// The MonoBehaviour half stays hand-written because Unity resolves a component through a MonoScript asset, which
/// exists only for a type declared in a file of its own. The serializable half has no such tie — nothing but
/// <c>[SerializeReference]</c>, which stores a type by name — so it is the half that can be generated.
/// </remarks>
[Generator]
public sealed class SerializableBinderGenerator : IIncrementalGenerator
{
    private const string AttributeName = "GenerateSerializableBinder";

    /// <summary>
    /// The serializable base a MonoBehaviour base maps to when dropping <c>Mono</c> from its name does not name it.
    /// </summary>
    /// <remarks>
    /// Only the component families need an entry: they bind a property of a component the MonoBehaviour finds on its
    /// own, and the serializable half is handed that component as its target instead.
    /// </remarks>
    private static readonly Dictionary<string, string> Bases = new()
    {
        ["ComponentMonoBinder"] = "TargetBinder",
        ["ComponentIntMonoBinder"] = "TargetIntBinder",
        ["ComponentFloatMonoBinder"] = "TargetFloatBinder",
        ["ComponentObjectMonoBinder"] = "TargetObjectBinder",
    };

    /// <summary>Attributes that describe the Inspector and belong to the MonoBehaviour half alone.</summary>
    private static readonly HashSet<string> MonoOnlyAttributes = new()
    {
        "AddComponentMenu", "AddBinderContextMenu", "AddBinderContextMenuByType", "BinderLog", "RequireComponent",
    };

    /// <summary>Members Unity calls on a MonoBehaviour and a serializable binder never has.</summary>
    private static readonly HashSet<string> MonoOnlyMembers = new()
    {
        "OnValidate", "Awake", "OnEnable", "OnDisable", "OnDestroy", "Reset", "Start", "Update",
        "ResolveComponent",
    };

    /// <summary>
    /// Helpers that ping a scene object. The MonoBehaviour half is one itself and leaves the argument out; the
    /// serializable half is not, so the target is passed explicitly or the message arrives without a ping.
    /// </summary>
    private static readonly HashSet<string> ContextHelpers = new()
    {
        "RequireFinite", "SafeClamp", "SafeClamp01", "NonNegative", "LogError", "LogWarning", "Log", "ToKeyName",
    };

    private static readonly DiagnosticDescriptor UnknownBase = new(
        id: "ASPIDSB001",
        title: "No serializable base for this binder",
        messageFormat: "'{0}' stands on '{1}', which has no serializable counterpart; the twin cannot be generated",
        category: "Aspid.MVVM",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor NoTargetBase = new(
        id: "ASPIDSB004",
        title: "The serializable base was not found",
        messageFormat: "'{0}' maps to '{1}', which this compilation does not contain",
        category: "Aspid.MVVM",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor NameNotMono = new(
        id: "ASPIDSB002",
        title: "Binder name does not end in MonoBinder",
        messageFormat: "'{0}' does not end in 'MonoBinder', so the name of its serializable twin cannot be derived",
        category: "Aspid.MVVM",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor BranchedCondition = new(
        id: "ASPIDSB005",
        title: "The half stands in a branch of conditional compilation",
        messageFormat: "'{0}' stands under an '#if' with an '#elif' or an '#else', which the twin cannot be placed under",
        category: "Aspid.MVVM",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor TwinExists = new(
        id: "ASPIDSB003",
        title: "The serializable twin is written by hand",
        messageFormat: "'{0}' already exists in this compilation, so nothing was generated for '{1}'",
        category: "Aspid.MVVM",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true);

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var candidates = context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, _) => node is ClassDeclarationSyntax { AttributeLists.Count: > 0 },
                static (syntax, _) => (ClassDeclarationSyntax)syntax.Node)
            .Where(static candidate => Marked(candidate))
            .Collect();

        context.RegisterSourceOutput(context.CompilationProvider.Combine(candidates), static (production, pair) =>
        {
            var (compilation, classes) = pair;

            foreach (var declaration in classes)
            {
                var emitted = Build(compilation, declaration, out var diagnostic, out var name);
                if (diagnostic is not null) production.ReportDiagnostic(diagnostic);
                if (emitted is not null) production.AddSource($"{name}.g.cs", emitted);
            }
        });
    }

    private static bool Marked(ClassDeclarationSyntax declaration) =>
        declaration.AttributeLists
            .SelectMany(list => list.Attributes)
            .Any(attribute => Simple(attribute.Name.ToString()) == AttributeName);

    private static string Simple(string name)
    {
        var last = name.LastIndexOf('.');
        if (last >= 0) name = name.Substring(last + 1);
        return name.EndsWith("Attribute") ? name.Substring(0, name.Length - "Attribute".Length) : name;
    }

    private static string? Build(Compilation compilation, ClassDeclarationSyntax declaration,
        out Diagnostic? diagnostic, out string name)
    {
        diagnostic = null;
        var monoName = declaration.Identifier.Text;
        name = monoName;

        if (!monoName.EndsWith("MonoBinder"))
        {
            diagnostic = Diagnostic.Create(NameNotMono, declaration.Identifier.GetLocation(), monoName);
            return null;
        }

        name = monoName.Substring(0, monoName.Length - "MonoBinder".Length) + "Binder";

        var model = compilation.GetSemanticModel(declaration.SyntaxTree);
        if (model.GetDeclaredSymbol(declaration) is not { } symbol) return null;

        if (symbol.BaseType is not { } monoBase || TargetBaseName(monoBase) is not { } targetBaseName)
        {
            diagnostic = Diagnostic.Create(UnknownBase, declaration.Identifier.GetLocation(),
                monoName, symbol.BaseType?.Name ?? "?");

            return null;
        }

        var ns = symbol.ContainingNamespace.ToDisplayString();
        if (compilation.GetTypeByMetadataName($"{ns}.{name}") is not null)
        {
            diagnostic = Diagnostic.Create(TwinExists, declaration.Identifier.GetLocation(), name, monoName);
            return null;
        }

        var arguments = monoBase.TypeArguments.Select(Display).ToArray();
        var found = Resolve(compilation, monoBase, targetBaseName, ns);
        var targetBase = found is null || arguments.Length is 0 ? found : found.Construct(monoBase.TypeArguments.ToArray());

        if (targetBase is null)
        {
            diagnostic = Diagnostic.Create(NoTargetBase, declaration.Identifier.GetLocation(),
                monoName, $"{targetBaseName}`{arguments.Length}");

            return null;
        }

        var interfaces = declaration.BaseList?.Types.Skip(1).Select(type => type.ToString()).ToArray()
            ?? System.Array.Empty<string>();

        var inherited = arguments.Length is 0 ? targetBaseName : $"{targetBaseName}<{string.Join(", ", arguments)}>";
        if (interfaces.Length > 0) inherited += ", " + string.Join(", ", interfaces);

        var unit = declaration.SyntaxTree.GetRoot() as CompilationUnitSyntax;
        var usings = unit?.Usings.Select(directive => directive.ToString()).ToArray()
            ?? System.Array.Empty<string>();

        var conditions = Conditions(unit, declaration, out var branched);

        if (branched is not null)
        {
            diagnostic = branched;
            return null;
        }

        var sealedModifier = declaration.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.SealedKeyword))
            ? "sealed "
            : string.Empty;

        return Emit(ns, name, inherited, monoBase.Name, targetBaseName, targetBase, declaration,
            Guard(symbol), usings, sealedModifier, conditions);
    }

    /// <summary>
    /// Names the serializable base of a MonoBehaviour one: the pair is spelled the same but for <c>Mono</c>, unless
    /// the family is listed as an exception.
    /// </summary>
    private static string? TargetBaseName(INamedTypeSymbol monoBase)
    {
        if (Bases.TryGetValue(monoBase.Name, out var mapped)) return mapped;

        var index = monoBase.Name.IndexOf("Mono", System.StringComparison.Ordinal);
        return index < 0 ? null : monoBase.Name.Remove(index, "Mono".Length);
    }

    /// <summary>
    /// Finds the serializable base by arity, looking first where the MonoBehaviour base itself lives.
    /// </summary>
    /// <remarks>
    /// The bases are split across two namespaces: the ones that carry a bound property sit in
    /// <c>Aspid.MVVM.StarterKit</c>, the ones that carry only a target in <c>Aspid.MVVM</c>. A pair always shares the
    /// namespace of its MonoBehaviour half, so that is where the search starts.
    /// </remarks>
    private static INamedTypeSymbol? Resolve(
        Compilation compilation, INamedTypeSymbol monoBase, string targetBaseName, string declaringNamespace)
    {
        var arity = monoBase.TypeArguments.Length;
        var suffix = arity is 0 ? string.Empty : $"`{arity}";

        foreach (var candidate in new[] { monoBase.ContainingNamespace.ToDisplayString(), declaringNamespace })
        {
            if (compilation.GetTypeByMetadataName($"{candidate}.{targetBaseName}{suffix}") is { } found) return found;
        }

        return null;
    }

    private static string Display(ITypeSymbol type) =>
        type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat
            .WithMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.UseSpecialTypes));

    /// <summary>
    /// Walks the base chain for the nearest <c>[BindModeOverride]</c> and turns the modes it leaves out into the
    /// guard the serializable half throws from its constructor.
    /// </summary>
    /// <remarks>
    /// The attribute is <c>[Conditional]</c> and never reaches player metadata, but a generator reads syntax, so the
    /// restriction is available here and stays a single declaration.
    /// </remarks>
    private static string? Guard(INamedTypeSymbol symbol)
    {
        for (var type = symbol; type is not null; type = type.BaseType)
        {
            var attribute = type.GetAttributes()
                .FirstOrDefault(candidate => candidate.AttributeClass?.Name == "BindModeOverrideAttribute");

            if (attribute is null) continue;
            if (attribute.NamedArguments.Any(argument => argument.Key is "IsAll" && argument.Value.Value is true)) return null;

            var allowed = attribute.ConstructorArguments.Length is 0
                ? default
                : attribute.ConstructorArguments[0].Values;

            if (allowed.IsDefaultOrEmpty) return null;
            if (allowed[0].Type is not INamedTypeSymbol mode) return null;

            // Имена берутся из самого символа перечисления: порядок его членов — не наше дело.
            var declared = mode.GetMembers().OfType<IFieldSymbol>()
                .Where(field => field.HasConstantValue && !Equals(field.ConstantValue, 0))
                .ToDictionary(field => field.ConstantValue!, field => field.Name);

            var names = allowed
                .Where(argument => argument.Value is not null && declared.ContainsKey(argument.Value))
                .Select(argument => declared[argument.Value!])
                .ToArray();

            var missing = declared.Values.Where(candidate => !names.Contains(candidate)).ToArray();
            if (missing.Length is 0) return null;

            return string.Join("\n            ",
                missing.Select(item => $"mode.ThrowExceptionIfMatches(BindMode.{item});"));
        }

        return null;
    }

    private static string Emit(
        string ns,
        string name,
        string inherited,
        string monoBaseName,
        string targetBaseName,
        INamedTypeSymbol targetBase,
        ClassDeclarationSyntax declaration,
        string? guard,
        string[] usings,
        string sealedModifier,
        string[] conditions)
    {
        var builder = new StringBuilder();

        builder.AppendLine("// <auto-generated/>");

        // Половина, из которой всё взято, может стоять под условной компиляцией; выпущенная стоит под той же.
        foreach (var condition in conditions) builder.AppendLine($"#if {condition}");

        // Тело переносится дословно из половины, которая под nullable-контекстом не стоит: включить его здесь
        // значило бы объявить ненулевым каждый тип, который она не помечала, и разойтись с сигнатурами базы.
        builder.AppendLine("#nullable disable");
        builder.AppendLine();

        // Тело переносится дословно, поэтому ему нужны те же using, что и половине, из которой оно взято.
        foreach (var directive in usings) builder.AppendLine(directive);
        if (usings.Length > 0) builder.AppendLine();
        builder.AppendLine($"namespace {ns}");
        builder.AppendLine("{");

        foreach (var line in Documentation(declaration, monoBaseName, targetBaseName)) builder.AppendLine($"    {line}");

        builder.AppendLine("    [global::System.Serializable]");
        builder.AppendLine($"    public {sealedModifier}class {name} : {inherited}");
        builder.AppendLine("    {");

        var members = Members(declaration).ToList();
        var fields = members.Where(member => member is FieldDeclarationSyntax).ToList();
        var rest = members.Where(member => member is not FieldDeclarationSyntax).ToList();

        var hasTarget = HasTarget(targetBase);

        foreach (var field in fields)
        {
            builder.AppendLine(Render(field, hasTarget));
            builder.AppendLine();
        }

        var options = fields.OfType<FieldDeclarationSyntax>().Where(Serialized).ToList();
        builder.AppendLine(Deserialization(name, sealedModifier.Length > 0));
        builder.AppendLine();
        builder.AppendLine(Constructor(name, targetBase, options, guard, hasTarget));

        foreach (var member in rest)
        {
            builder.AppendLine();
            builder.AppendLine(Render(member, hasTarget).TrimStart('\n', '\r'));
        }

        builder.AppendLine("    }");
        builder.AppendLine("}");

        foreach (var _ in conditions) builder.AppendLine("#endif");

        return builder.ToString();
    }

    /// <summary>
    /// Reads the conditions the half itself stands under, so the generated one is compiled exactly where its source is.
    /// </summary>
    /// <remarks>
    /// Only the branches that open before the declaration and close after it count. A <c>#if</c> that closes earlier —
    /// the <c>#define PROFILER</c> header the package puts at the top of a file, say — says nothing about the class,
    /// and carrying it over would compile the twin out of existence wherever the header's condition is false.
    /// </remarks>
    private static string[] Conditions(
        CompilationUnitSyntax? unit, ClassDeclarationSyntax declaration, out Diagnostic? diagnostic)
    {
        diagnostic = null;

        var conditions = new List<string>();
        if (unit is null) return conditions.ToArray();

        var span = declaration.Span;

        for (var directive = unit.GetFirstDirective(); directive is not null; directive = directive.GetNextDirective())
        {
            if (directive is not IfDirectiveTriviaSyntax branch) continue;
            if (branch.SpanStart > span.Start) break;

            var related = branch.GetRelatedDirectives();
            if (related.Count is 0) continue;
            if (related[related.Count - 1].Span.End < span.End) continue;

            // Ветвление #elif/#else оставляет вопрос, в какой из ветвей стоит класс: под условием #if или под его
            // отрицанием. Ответить на него молча нельзя, а ошибиться — значит собрать половину не там, где вторая.
            if (related.Count > 2)
            {
                diagnostic = Diagnostic.Create(
                    BranchedCondition, declaration.Identifier.GetLocation(), declaration.Identifier.Text);

                return conditions.ToArray();
            }

            conditions.Add(branch.Condition.ToString());
        }

        return conditions.ToArray();
    }

    /// <summary>
    /// Indicates whether a field is one of the binder's Inspector options — a constant or a static is part of the
    /// body, not something the constructor takes.
    /// </summary>
    private static bool Serialized(FieldDeclarationSyntax field)
    {
        if (field.Modifiers.Any(modifier =>
                modifier.IsKind(SyntaxKind.ConstKeyword) ||
                modifier.IsKind(SyntaxKind.StaticKeyword) ||
                modifier.IsKind(SyntaxKind.ReadOnlyKeyword))) return false;

        return field.AttributeLists
            .SelectMany(list => list.Attributes)
            .Any(attribute => Simple(attribute.Name.ToString()) is "SerializeField" or "SerializeReference");
    }

    /// <summary>
    /// Mirrors the serializable base's own constructor, inserting this binder's serialized options right after the
    /// target so the shape matches what the family's hand-written halves take.
    /// </summary>
    private static string Constructor(
        string name, INamedTypeSymbol targetBase, List<FieldDeclarationSyntax> fields, string? guard, bool hasTarget)
    {
        var baseConstructor = targetBase.Constructors
            .Where(candidate => candidate.DeclaredAccessibility is not Accessibility.Private)
            .OrderByDescending(candidate => candidate.Parameters.Length)
            .FirstOrDefault();

        if (baseConstructor is null) return string.Empty;

        var options = fields
            .SelectMany(field => field.Declaration.Variables
                .Select(variable => (
                    Type: field.Declaration.Type.ToString(),
                    Field: variable.Identifier.Text,
                    Default: variable.Initializer?.Value.ToString())))
            .ToList();

        var taken = new HashSet<string>(options.Select(option => Parameter(option.Field)));
        var parameters = new List<(string Text, string? Default)>();
        var arguments = new List<string>();

        // Опции встают сразу за целью; у базы, которая цели не имеет, вставать не за что, и они идут первыми.
        if (!hasTarget) parameters.AddRange(options.Select(Option));

        for (var i = 0; i < baseConstructor.Parameters.Length; i++)
        {
            var parameter = baseConstructor.Parameters[i];

            // Цель обязательна: биндеру без неё некуда писать, и подставлять ей значение по умолчанию нечем.
            // У баз, которые цели не имеют, первый параметр — обычный, и это правило к нему не относится.
            var fallback = i is 0 && hasTarget ? null : Default(parameter);

            // Опция, названная как параметр базы, вытесняет его: база получает пустое значение, а поле — параметр.
            if (i is not 0 && taken.Contains(parameter.Name)) arguments.Add("default");
            else
            {
                parameters.Add(($"{Display(parameter.Type)} {parameter.Name}", fallback));
                arguments.Add(parameter.Name);
            }

            if (i is not 0 || !hasTarget) continue;

            parameters.AddRange(options.Select(Option));
        }

        // Значение по умолчанию держится только там, где оно есть у всех параметров правее.
        var allowed = true;
        for (var i = parameters.Count - 1; i >= 0; i--)
        {
            if (parameters[i].Default is null) allowed = false;
            else if (!allowed) parameters[i] = (parameters[i].Text, null);
        }

        var builder = new StringBuilder();
        builder.AppendLine("        /// <inheritdoc/>");
        builder.AppendLine($"        public {name}(");
        builder.AppendLine(string.Join(",\n", parameters
            .Select(parameter => $"            {parameter.Text}{parameter.Default}")) + ")");
        builder.AppendLine($"            : base({string.Join(", ", arguments)})");
        builder.AppendLine("        {");

        if (guard is not null) builder.AppendLine($"            {guard}");
        foreach (var option in options)
        {
            var fallback = option.Default is not null && !Constant(option.Default) ? $" ?? {option.Default}" : string.Empty;
            builder.AppendLine($"            {option.Field} = {Parameter(option.Field)}{fallback};");
        }

        builder.Append("        }");
        return builder.ToString();
    }

    private static string? Default(IParameterSymbol parameter)
    {
        if (!parameter.HasExplicitDefaultValue)
            return parameter.Type.IsReferenceType || parameter.NullableAnnotation is NullableAnnotation.Annotated
                ? " = null"
                : null;

        var value = parameter.ExplicitDefaultValue;
        if (value is null) return " = null";

        if (parameter.Type is INamedTypeSymbol { TypeKind: TypeKind.Enum } type)
        {
            var member = type.GetMembers().OfType<IFieldSymbol>()
                .FirstOrDefault(field => field.HasConstantValue && Equals(field.ConstantValue, value));

            if (member is not null) return $" = {Display(type)}.{member.Name}";
        }

        return value switch
        {
            bool flag => flag ? " = true" : " = false",
            string text => $" = \"{text}\"",
            float number => $" = {number.ToString(System.Globalization.CultureInfo.InvariantCulture)}f",
            _ => $" = {System.Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture)}",
        };
    }

    /// <summary>
    /// Renders the constructor Unity needs to rebuild a serialized instance.
    /// </summary>
    /// <remarks>
    /// It stays out of reach of calling code: the serialized fields arrive from the asset, and an instance built this
    /// way anywhere else would have neither a target nor a mode.
    /// </remarks>
    private static string Deserialization(string name, bool isSealed)
    {
        var access = isSealed ? "private" : "protected";
        return $"        /// <remarks>For deserialization only.</remarks>\n        {access} {name}() {{ }}";
    }

    /// <summary>
    /// Renders one Inspector option as a constructor parameter. Its default has to be a compile-time constant, so an
    /// initialiser that is computed stays in the body and the parameter offers <see langword="null"/> instead.
    /// </summary>
    private static (string Text, string? Default) Option((string Type, string Field, string? Default) option)
    {
        var constant = option.Default is null || Constant(option.Default);
        var value = option.Default is null ? $"default({option.Type})" : constant ? option.Default : "null";
        return ($"{option.Type} {Parameter(option.Field)}", $" = {value}");
    }

    /// <summary>Indicates whether the serializable base carries a target the ping helpers can be handed.</summary>
    private static bool HasTarget(INamedTypeSymbol targetBase)
    {
        for (var type = targetBase; type is not null; type = type.BaseType)
            if (type.GetMembers("Target").Length > 0) return true;

        return false;
    }

    /// <summary>Indicates whether an initialiser can stand as a parameter's default value.</summary>
    private static bool Constant(string initialiser) =>
        !initialiser.Contains("new ") && !initialiser.Contains("(");

    private static string Parameter(string fieldName) =>
        fieldName.TrimStart('_') is { Length: > 0 } trimmed
            ? char.ToLowerInvariant(trimmed[0]) + trimmed.Substring(1)
            : fieldName;

    /// <summary>Copies the class documentation, dropping the <c>&lt;include&gt;</c> of the MonoBehaviour half.</summary>
    private static IEnumerable<string> Documentation(
        ClassDeclarationSyntax declaration, string monoBaseName, string targetBaseName) =>
        declaration.GetLeadingTrivia().ToFullString()
            .Split('\n')
            .Select(line => line.Trim())
            .Where(line => line.StartsWith("///") && !line.Contains("<include"))
            .Select(line => line.Replace(monoBaseName, targetBaseName));

    private static IEnumerable<MemberDeclarationSyntax> Members(ClassDeclarationSyntax declaration)
    {
        foreach (var member in declaration.Members)
        {
            var name = member switch
            {
                MethodDeclarationSyntax method => method.Identifier.Text,
                PropertyDeclarationSyntax property => property.Identifier.Text,
                _ => null,
            };

            if (name is not null && MonoOnlyMembers.Contains(name)) continue;
            if (member is ConstructorDeclarationSyntax) continue;

            yield return member;
        }
    }

    /// <summary>
    /// Renders a member for the serializable half: the Inspector-only attributes come off, the component field
    /// becomes the target, and the ping helpers are handed that target explicitly.
    /// </summary>
    private static string Render(MemberDeclarationSyntax member, bool hasTarget)
    {
        var kept = SyntaxFactory.List(member.AttributeLists
            .Select(list => list.WithAttributes(SyntaxFactory.SeparatedList(
                list.Attributes.Where(attribute => !MonoOnlyAttributes.Contains(Simple(attribute.Name.ToString()))))))
            .Where(list => list.Attributes.Count > 0));

        var rewritten = new TargetRewriter(hasTarget).Visit(member.WithAttributeLists(kept));
        return rewritten.ToFullString().TrimEnd();
    }

    /// <summary>Rewrites the MonoBehaviour half's body into the serializable half's terms.</summary>
    private sealed class TargetRewriter : CSharpSyntaxRewriter
    {
        private readonly bool _hasTarget;

        public TargetRewriter(bool hasTarget) => _hasTarget = hasTarget;

        public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node) =>
            node.Identifier.Text is "CachedComponent"
                ? node.WithIdentifier(SyntaxFactory.Identifier("Target")).WithTriviaFrom(node)
                : base.VisitIdentifierName(node);

        public override SyntaxNode? VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var visited = (InvocationExpressionSyntax)base.VisitInvocationExpression(node)!;

            if (!_hasTarget) return visited;
            if (visited.Expression is not MemberAccessExpressionSyntax access) return visited;
            if (access.Expression is not ThisExpressionSyntax) return visited;
            if (!ContextHelpers.Contains(access.Name.Identifier.Text)) return visited;

            var target = SyntaxFactory
                .Argument(SyntaxFactory.IdentifierName("Target"))
                .WithLeadingTrivia(SyntaxFactory.Space);

            return visited.WithArgumentList(visited.ArgumentList.AddArguments(target));
        }
    }
}
