using System.Linq;
using Aspid.MVVM.Generators.Shared;
using System.Text;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Unity.Generators.SerializableBinders;

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

    private static readonly DiagnosticDescriptor InvalidModes = new(
        id: "ASPIDSB006",
        title: "Invalid binding modes",
        messageFormat: "BindModeOverride on '{0}' must contain a non-null array of declared modes other than None",
        category: "Aspid.MVVM",
        defaultSeverity: DiagnosticSeverity.Error,
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

            var byAbstract = classes.ToLookup(declaration => compilation.GetSemanticModel(declaration.SyntaxTree)
                .GetDeclaredSymbol(declaration)?.IsAbstract == true);
            var pending = byAbstract[true].ToList();

            // Add generated bases to the semantic model before resolving derived constructor signatures.
            while (pending.Count > 0)
            {
                var trees = new List<SyntaxTree>();
                var deferred = new List<(ClassDeclarationSyntax Declaration, Diagnostic Diagnostic)>();

                foreach (var declaration in pending)
                {
                    var emitted = Build(compilation, declaration, out var diagnostic, out var identity);
                    if (diagnostic?.Id == NoTargetBase.Id)
                    {
                        deferred.Add((declaration, diagnostic));
                        continue;
                    }

                    if (diagnostic is not null) production.ReportDiagnostic(diagnostic);
                    if (emitted is null) continue;

                    var path = $"{identity}.SerializableBinder.g.cs";
                    production.AddSource(path, emitted);
                    trees.Add(CSharpSyntaxTree.ParseText(emitted, (CSharpParseOptions)declaration.SyntaxTree.Options, path));
                }

                if (trees.Count == 0)
                {
                    foreach (var item in deferred) production.ReportDiagnostic(item.Diagnostic);
                    break;
                }

                compilation = compilation.AddSyntaxTrees(trees);
                pending = deferred.Select(item => item.Declaration).ToList();
            }

            foreach (var declaration in byAbstract[false])
            {
                var emitted = Build(compilation, declaration, out var diagnostic, out var identity);
                if (diagnostic is not null) production.ReportDiagnostic(diagnostic);
                if (emitted is not null) production.AddSource($"{identity}.SerializableBinder.g.cs", emitted);
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
        out Diagnostic? diagnostic, out string identity)
    {
        diagnostic = null;
        var monoName = declaration.Identifier.ValueText;
        identity = monoName;

        if (!monoName.EndsWith("MonoBinder", System.StringComparison.Ordinal)
            || SerializableName(monoName) is not { } name)
        {
            diagnostic = Diagnostic.Create(NameNotMono, declaration.Identifier.GetLocation(), monoName);
            return null;
        }

        var model = compilation.GetSemanticModel(declaration.SyntaxTree);
        if (model.GetDeclaredSymbol(declaration) is not { } symbol) return null;

        identity = AspidTypeDeclaration.Identity(symbol);
        diagnostic = AspidTypeDeclaration.ValidateContainers(symbol);
        if (diagnostic is not null) return null;

        var attribute = symbol.GetAttributes()
            .FirstOrDefault(candidate => candidate.AttributeClass?.Name == AttributeName + "Attribute");
        var targetParameter = symbol.IsAbstract
            && symbol is { Name: "ComponentIntMonoBinder" or "ComponentFloatMonoBinder", Arity: 1 }
                or { Name: "ComponentMonoBinder" or "ComponentObjectMonoBinder" or "SwitcherMonoBinder", Arity: 2 }
            && symbol.ContainingNamespace.ToDisplayString() == "Aspid.MVVM.StarterKit"
            ? symbol.TypeParameters[0]
            : null;
        if (symbol.BaseType is not { } monoBase || SerializableName(monoBase.Name) is not { } targetBaseName)
        {
            diagnostic = Diagnostic.Create(UnknownBase, declaration.Identifier.GetLocation(),
                monoName, symbol.BaseType?.Name ?? "?");

            return null;
        }

        var scope = new AspidTypeDeclaration(symbol);
        var ns = symbol.ContainingNamespace.IsGlobalNamespace ? "" : symbol.ContainingNamespace.ToDisplayString();
        if (symbol.ContainingType is { } parent
                ? parent.GetTypeMembers(name, symbol.Arity).Length > 0
                : symbol.ContainingNamespace.GetTypeMembers(name, symbol.Arity).Length > 0)
        {
            diagnostic = Diagnostic.Create(TwinExists, declaration.Identifier.GetLocation(), name, monoName);
            return null;
        }

        var arguments = monoBase.TypeArguments.Select(argument => Display(argument, targetParameter)).ToArray();
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

        var guard = symbol.IsAbstract ? null : Guard(symbol, out diagnostic);
        if (diagnostic is not null) return null;

        var preserveRequiredBaseParameters = attribute?.NamedArguments.Any(argument =>
            argument.Key is "PreserveRequiredBaseParameters" && argument.Value.Value is true) == true;

        return Emit(scope, name, inherited, monoBase.Name, targetBaseName, targetBase, declaration,
            guard, usings, sealedModifier, conditions, preserveRequiredBaseParameters, model, targetParameter);
    }

    private static string? SerializableName(string monoName)
    {
        var index = monoName.LastIndexOf("Mono", System.StringComparison.Ordinal);
        if (index < 0) return null;

        var name = monoName.Remove(index, "Mono".Length);
        return monoName.StartsWith("Component", System.StringComparison.Ordinal)
            ? "Target" + name.Substring("Component".Length)
            : name;
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

    private static string Display(
        ITypeSymbol type, ITypeParameterSymbol? targetParameter = null, bool includeNullability = false)
    {
        var options = SymbolDisplayMiscellaneousOptions.UseSpecialTypes;
        if (includeNullability)
            options |= SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier;

        var format = SymbolDisplayFormat.FullyQualifiedFormat.WithMiscellaneousOptions(options);
        return targetParameter is null
            ? type.ToDisplayString(format)
            : string.Concat(type.ToDisplayParts(format).Select(part =>
                SymbolEqualityComparer.Default.Equals(part.Symbol, targetParameter) ? "TTarget" : part.ToString()));
    }

    private static string? Guard(INamedTypeSymbol symbol, out Diagnostic? diagnostic)
    {
        diagnostic = null;
        for (var type = symbol; type is not null; type = type.BaseType)
        {
            var attribute = type.GetAttributes()
                .FirstOrDefault(candidate => candidate.AttributeClass?.Name == "BindModeOverrideAttribute");

            if (attribute is null) continue;
            if (attribute.AttributeConstructor?.Parameters.FirstOrDefault()?.Type is not IArrayTypeSymbol array
                || array.ElementType is not INamedTypeSymbol mode) return null;

            var declared = mode.GetMembers().OfType<IFieldSymbol>()
                .Where(field => field.HasConstantValue && !Equals(field.ConstantValue, 0))
                .ToDictionary(field => field.ConstantValue!, field => field.Name);

            var argument = attribute.ConstructorArguments.FirstOrDefault();
            if (argument.Kind is not TypedConstantKind.Array || argument.IsNull
                || argument.Values.Any(value => value.Value is null || !declared.ContainsKey(value.Value)))
            {
                var location = attribute.ApplicationSyntaxReference?.GetSyntax().GetLocation() ?? symbol.Locations.FirstOrDefault();
                diagnostic = Diagnostic.Create(InvalidModes, location, type.Name);
                return null;
            }

            var names = new HashSet<string>(argument.Values.Select(value => declared[value.Value!]));
            var isAll = attribute.NamedArguments.Any(value => value.Key is "IsAll" && value.Value.Value is true);
            var isOne = attribute.NamedArguments.Any(value => value.Key is "IsOne" && value.Value.Value is true);
            var isTwo = attribute.NamedArguments.Any(value => value.Key is "IsTwo" && value.Value.Value is true);

            if (isAll || (!isOne && !isTwo && names.Count is 0)) return null;
            if (isOne)
            {
                names.Add("OneWay");
                names.Add("OneTime");
            }

            if (isTwo)
            {
                names.Add("TwoWay");
                names.Add("OneWayToSource");
            }

            var missing = declared.Values.Where(candidate => !names.Contains(candidate)).ToArray();
            if (missing.Length is 0) return null;

            return string.Join("\n            ",
                missing.Select(item => $"mode.EnsureNot(BindMode.{item});"));
        }

        return null;
    }

    private static string Emit(
        AspidTypeDeclaration scope,
        string name,
        string inherited,
        string monoBaseName,
        string targetBaseName,
        INamedTypeSymbol targetBase,
        ClassDeclarationSyntax declaration,
        string? guard,
        string[] usings,
        string sealedModifier,
        string[] conditions,
        bool preserveRequiredBaseParameters,
        SemanticModel model,
        ITypeParameterSymbol? targetParameter)
    {
        var builder = new StringBuilder();

        WriteOpening(builder, scope, declaration, usings, conditions);

        foreach (var line in Documentation(declaration, monoBaseName, targetBaseName))
        {
            var documentation = targetParameter is not null && line.StartsWith($"/// <typeparam name=\"{targetParameter.Name}\">", System.StringComparison.Ordinal)
                ? "/// <typeparam name=\"TTarget\">The type of the target object that exposes the bound property.</typeparam>"
                : targetParameter is null ? line : line.Replace(targetParameter.Name, "TTarget");
            builder.AppendLine($"    {documentation}");
        }

        builder.AppendLine("    [global::System.Serializable]");
        var frameworkBase = scope.Symbol.IsAbstract
            && scope.Symbol.ContainingNamespace.ToDisplayString() == "Aspid.MVVM.StarterKit";
        var isPropertyBase = frameworkBase
            && scope.Symbol is { Name: "MonoBinder", Arity: 1 } or { Name: "ComponentMonoBinder", Arity: 2 };
        var isSwitcherBase = frameworkBase && scope.Symbol.Name is "SwitcherMonoBinder";
        var valueParameter = isPropertyBase || isSwitcherBase ? scope.Symbol.TypeParameters.Last() : null;
        if (isPropertyBase)
        {
            var mode = declaration.AttributeLists.SelectMany(list => list.Attributes)
                .FirstOrDefault(attribute => Simple(attribute.Name.ToString()) is "BindModeOverride");
            if (mode is not null)
                builder.AppendLine($"    [{mode}]");
        }

        var accessibility = scope.Symbol.ContainingType is null
            ? "public"
            : scope.Symbol.DeclaredAccessibility switch
            {
                Accessibility.Private => "private",
                Accessibility.Protected => "protected",
                Accessibility.Internal => "internal",
                Accessibility.ProtectedAndInternal => "private protected",
                Accessibility.ProtectedOrInternal => "protected internal",
                _ => "public"
            };
        var isAbstract = scope.Symbol.IsAbstract;
        var modifier = isAbstract ? "abstract " : sealedModifier;
        var header = targetParameter is null
            ? AspidTypeDeclaration.Header(scope.Symbol, new[] { inherited }, name, isPartial: false)
            : TargetHeader(scope.Symbol, declaration, name, inherited, targetParameter);
        builder.AppendLine($"    {accessibility} {modifier}{header}");
        builder.AppendLine("    {");

        var members = Members(declaration)
            .Select(member => isAbstract
                ? AdaptAbstractMember(member, model, targetParameter, valueParameter, targetBase)
                : member)
            .ToList();
        var fields = members.Where(member => member is FieldDeclarationSyntax).ToList();
        var rest = members.Where(member => member is not FieldDeclarationSyntax).ToList();

        var hasTarget = HasTarget(targetBase);
        var hasUnityTarget = hasTarget && targetParameter is null;

        if (isAbstract)
            builder.AppendLine("#nullable enable annotations");

        foreach (var field in fields)
        {
            builder.AppendLine(Render(field, hasUnityTarget));
            builder.AppendLine();
        }

        var options = fields.OfType<FieldDeclarationSyntax>().Where(Serialized).ToList();
        builder.AppendLine(Deserialization(name, sealedModifier.Length > 0));
        var constructor = Constructor(
            name, targetBase, options, isSwitcherBase ? "mode.EnsureForwardOnly();" : guard, hasTarget,
            preserveRequiredBaseParameters, isAbstract, targetParameter, isPropertyBase, isSwitcherBase);

        if (constructor.Length > 0)
        {
            builder.AppendLine();
            builder.AppendLine(constructor);
        }

        foreach (var member in rest)
        {
            builder.AppendLine();
            builder.AppendLine(Render(member, hasUnityTarget).TrimStart('\n', '\r'));
        }

        builder.AppendLine("    }");
        WriteClosing(builder, scope, declaration, conditions);

        return builder.ToString();
    }

    private static string TargetHeader(
        INamedTypeSymbol source, ClassDeclarationSyntax declaration, string name, string inherited,
        ITypeParameterSymbol targetParameter)
    {
        var parameters = source.TypeParameters.Select(parameter => parameter.Ordinal == targetParameter.Ordinal
            ? "TTarget"
            : AspidTypeDeclaration.Escape(parameter.Name));
        var constraints = declaration.ConstraintClauses
            .Where(clause => clause.Name.Identifier.ValueText != targetParameter.Name);
        return $"class {name}<{string.Join(", ", parameters)}> : {inherited}"
            + string.Concat(constraints.Select(clause => $" {clause}"));
    }

    private static MemberDeclarationSyntax AdaptAbstractMember(
        MemberDeclarationSyntax member, SemanticModel model, ITypeParameterSymbol? targetParameter,
        ITypeParameterSymbol? valueParameter, INamedTypeSymbol targetBase)
    {
        var rewritten = member;
        if (targetParameter is not null || valueParameter is not null)
        {
            var comparer = SymbolEqualityComparer.Default;
            var identifiers = member.DescendantNodes().OfType<IdentifierNameSyntax>()
                .Select(node => (Node: node, Symbol: model.GetSymbolInfo(node).Symbol))
                .Where(item => item.Symbol is not null
                    && (comparer.Equals(item.Symbol, targetParameter) || comparer.Equals(item.Symbol, valueParameter)))
                .ToDictionary(item => item.Node, item => comparer.Equals(item.Symbol, targetParameter));
            rewritten = member.ReplaceNodes(identifiers.Keys, (original, replacement) => identifiers[original]
                ? replacement.WithIdentifier(SyntaxFactory.Identifier("TTarget").WithTriviaFrom(replacement.Identifier))
                : AsNullable(replacement));
        }

        if (valueParameter is not null)
            return AdaptValueMember(rewritten);

        if (member is not MethodDeclarationSyntax original || rewritten is not MethodDeclarationSyntax method
            || model.GetDeclaredSymbol(original) is not IMethodSymbol { OverriddenMethod: { } overridden })
            return rewritten;

        var inherited = targetBase.GetMembers(overridden.Name).OfType<IMethodSymbol>()
            .FirstOrDefault(candidate => candidate.Arity == overridden.Arity
                && candidate.Parameters.Select(parameter => parameter.Type)
                    .SequenceEqual(overridden.Parameters.Select(parameter => parameter.Type), SymbolEqualityComparer.Default));
        if (inherited is null) return method;

        var parameters = method.ParameterList.Parameters.Select((parameter, index) =>
            parameter.Type is { } type
                && !inherited.Parameters[index].Type.IsValueType
                && inherited.Parameters[index].NullableAnnotation == NullableAnnotation.Annotated
                ? parameter.WithType(AsNullable(type))
                : parameter);
        var list = SyntaxFactory.SeparatedList(parameters, method.ParameterList.Parameters.GetSeparators());
        method = method.WithParameterList(method.ParameterList.WithParameters(list));
        return !inherited.ReturnType.IsValueType && inherited.ReturnNullableAnnotation == NullableAnnotation.Annotated
            ? method.WithReturnType(AsNullable(method.ReturnType))
            : method;
    }

    private static MemberDeclarationSyntax AdaptValueMember(MemberDeclarationSyntax member) => member switch
    {
        FieldDeclarationSyntax field => field.WithDeclaration(
            field.Declaration.WithType(AsNullable(field.Declaration.Type))),
        EventFieldDeclarationSyntax field => field.WithDeclaration(
            field.Declaration.WithType(AsNullable(field.Declaration.Type))),
        MethodDeclarationSyntax method when method.Identifier.ValueText is "RaiseValueChanged" or "SetValue" => method
            .WithAttributeLists(default)
            .WithLeadingTrivia(method.GetLeadingTrivia()),
        _ => member,
    };

    private static TypeSyntax AsNullable(TypeSyntax type) => type is NullableTypeSyntax
        ? type
        : SyntaxFactory.NullableType(type.WithoutTrailingTrivia()).WithTriviaFrom(type);

    private static void WriteOpening(
        StringBuilder builder,
        AspidTypeDeclaration scope,
        ClassDeclarationSyntax declaration,
        string[] usings,
        string[] conditions)
    {
        builder.AppendLine("// <auto-generated/>");

        foreach (var condition in conditions) builder.AppendLine($"#if {condition}");

        // MonoBehaviour member signatures are copied with oblivious reference types.
        builder.AppendLine("#nullable disable");
        builder.AppendLine();

        foreach (var directive in usings) builder.AppendLine(directive);
        if (usings.Length > 0) builder.AppendLine();
        var namespaceDeclarations = declaration.Ancestors().OfType<BaseNamespaceDeclarationSyntax>().Reverse().ToArray();
        foreach (var namespaceDeclaration in namespaceDeclarations)
        {
            builder.AppendLine($"namespace {namespaceDeclaration.Name}");
            builder.AppendLine("{");
            foreach (var directive in namespaceDeclaration.Usings) builder.AppendLine(directive.ToString());
        }
        foreach (var container in scope.Containers)
        {
            builder.AppendLine(AspidTypeDeclaration.Header(container));
            builder.AppendLine("{");
        }
    }

    private static void WriteClosing(
        StringBuilder builder,
        AspidTypeDeclaration scope,
        ClassDeclarationSyntax declaration,
        string[] conditions)
    {
        foreach (var _ in scope.Containers) builder.AppendLine("}");
        foreach (var _ in declaration.Ancestors().OfType<BaseNamespaceDeclarationSyntax>()) builder.AppendLine("}");

        foreach (var _ in conditions) builder.AppendLine("#endif");
    }

    /// <summary>
    /// Reads the conditions the half itself stands under, so the generated one is compiled exactly where its source is.
    /// </summary>
    /// <remarks>
    /// Only the branches that open before the declaration and close after it count. A <c>#if</c> that closes earlier —
    /// the <c>#define ASPID_MVVM_PROFILER</c> header the package puts at the top of a file, say — says nothing about the class,
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
    /// Mirrors the serializable base constructor and inserts this binder's serialized options.
    /// </summary>
    private static string Constructor(
        string name,
        INamedTypeSymbol targetBase,
        List<FieldDeclarationSyntax> fields,
        string? guard,
        bool hasTarget,
        bool preserveRequiredBaseParameters,
        bool isAbstract,
        ITypeParameterSymbol? targetParameter,
        bool requireOptions,
        bool isSwitcherBase)
    {
        var baseConstructor = targetBase.Constructors
            .Where(candidate => candidate.DeclaredAccessibility is not Accessibility.Private)
            .OrderByDescending(candidate => candidate.Parameters.Length)
            .FirstOrDefault();

        if (baseConstructor is null || (baseConstructor.Parameters.Length == 0 && fields.Count == 0))
            return string.Empty;

        var options = fields
            .SelectMany(field => field.Declaration.Variables.Select(variable =>
            {
                var preset = isSwitcherBase && variable.Identifier.ValueText is "_trueValue" or "_falseValue";
                var type = preset && field.Declaration.Type is NullableTypeSyntax nullable
                    ? nullable.ElementType
                    : field.Declaration.Type;
                return (Type: type.ToString(), Field: variable.Identifier.Text,
                    Default: variable.Initializer?.Value.ToString(), Required: requireOptions || preset);
            }))
            .ToList();

        preserveRequiredBaseParameters |= isAbstract;
        var taken = new HashSet<string>(options.Select(option => Parameter(option.Field)));
        var parameters = new List<(string Text, string? Default)>();
        var arguments = new List<string>();

        var optionsAfter = hasTarget ? 0 : -1;
        if (preserveRequiredBaseParameters)
        {
            foreach (var parameter in baseConstructor.Parameters)
            {
                if (!parameter.IsOptional && (parameter.Ordinal is 0 || !taken.Contains(parameter.Name)))
                    optionsAfter = parameter.Ordinal;
            }
        }

        if (optionsAfter < 0)
            parameters.AddRange(options.Select(Option));

        foreach (var parameter in baseConstructor.Parameters)
        {
            var requiredTarget = hasTarget && parameter.Ordinal is 0;
            var requiredBaseParameter = preserveRequiredBaseParameters && !parameter.IsOptional;
            var fallback = requiredTarget || requiredBaseParameter ? null : Default(parameter);

            // Опция, названная как параметр базы, вытесняет его: база получает пустое значение, а поле — параметр.
            if (parameter.Ordinal is not 0 && taken.Contains(parameter.Name)) arguments.Add("default");
            else
            {
                var type = Display(parameter.Type, targetParameter, includeNullability: isAbstract);
                parameters.Add(($"{type} {parameter.Name}", fallback));
                arguments.Add(parameter.Name);
            }

            if (parameter.Ordinal == optionsAfter)
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
        WriteConstructorDocumentation(builder, hasTarget, requireOptions, isSwitcherBase);
        var access = isAbstract ? "protected" : "public";
        builder.AppendLine($"        {access} {name}(");
        builder.AppendLine(string.Join(",\n", parameters
            .Select(parameter => $"            {parameter.Text}{parameter.Default}")) + ")");
        builder.AppendLine($"            : base({string.Join(", ", arguments)})");
        builder.AppendLine("        {");

        if (guard is not null && parameters.Any(parameter => parameter.Text.EndsWith(" mode")))
            builder.AppendLine($"            {guard}");
        foreach (var option in options)
        {
            var fallback = option.Default is not null && !Constant(option.Default) ? $" ?? {option.Default}" : string.Empty;
            builder.AppendLine($"            {option.Field} = {Parameter(option.Field)}{fallback};");
        }

        builder.Append("        }");
        return builder.ToString();
    }

    private static void WriteConstructorDocumentation(
        StringBuilder builder, bool hasTarget, bool isPropertyBase, bool isSwitcherBase)
    {
        if (!isPropertyBase && !isSwitcherBase)
        {
            builder.AppendLine("        /// <inheritdoc/>");
            return;
        }

        var summary = isSwitcherBase
            ? "Creates a binder that selects between two preset values."
            : "Creates a binder with an optional converter and binding mode.";
        builder.AppendLine($"        /// <summary>{summary}</summary>");
        if (hasTarget)
            builder.AppendLine("        /// <param name=\"target\">The target object to bind.</param>");

        if (isSwitcherBase)
        {
            builder.AppendLine("        /// <param name=\"trueValue\">The value applied when the bound value is <see langword=\"true\"/>.</param>");
            builder.AppendLine("        /// <param name=\"falseValue\">The value applied when the bound value is <see langword=\"false\"/>.</param>");
            builder.AppendLine("        /// <param name=\"converter\">The converter used to transform the chosen value, or <see langword=\"null\"/> to use it unchanged.</param>");
            builder.AppendLine("        /// <param name=\"mode\">The binding mode. Must be <see cref=\"BindMode.OneWay\"/> or <see cref=\"BindMode.OneTime\"/>.</param>");
        }
        else
        {
            builder.AppendLine("        /// <param name=\"converter\">");
            builder.AppendLine("        /// The converter used to transform the bound value, or <see langword=\"null\"/> to use it unchanged.");
            builder.AppendLine("        /// Runs in reverse only if it implements <see cref=\"ITwoWayConverter{TFrom, TTo}\"/>.");
            builder.AppendLine("        /// </param>");
            builder.AppendLine("        /// <param name=\"mode\">The binding mode.</param>");
        }

        if (hasTarget)
            builder.AppendLine("        /// <exception cref=\"System.ArgumentNullException\"><paramref name=\"target\"/> is <see langword=\"null\"/>.</exception>");

        if (isSwitcherBase)
            builder.AppendLine("        /// <exception cref=\"System.InvalidOperationException\">The mode is neither <see cref=\"BindMode.OneWay\"/> nor <see cref=\"BindMode.OneTime\"/>.</exception>");
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
    private static (string Text, string? Default) Option(
        (string Type, string Field, string? Default, bool Required) option)
    {
        if (option.Required) return ($"{option.Type} {Parameter(option.Field)}", null);

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
