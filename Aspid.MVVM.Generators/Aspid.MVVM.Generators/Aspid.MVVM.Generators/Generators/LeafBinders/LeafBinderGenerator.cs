using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Aspid.MVVM.Generators.Generators.LeafBinders;

/// <summary>
/// Emits the leaf binder pair declared by each <c>[assembly: GenerateBinders(...)]</c>.
/// </summary>
/// <remarks>
/// A leaf binder is four lines of real code wrapped in eighty that are the same for every family. Written by hand, that
/// is where twins drift apart: a guard added to one half and forgotten in the other, a menu path with the wrong dash.
/// This generator emits both halves from one declaration, so drifting is not expressible.
/// <para/>
/// The value type is read from the property rather than declared in the attribute — a family cannot then claim a type
/// its property does not have — and it decides which base pair the classes stand on.
/// </remarks>
[Generator]
public sealed class LeafBinderGenerator : IIncrementalGenerator
{
    private const string AttributeName = "Aspid.MVVM.StarterKit.GenerateBindersAttribute";

    /// <summary>
    /// The bases a value type maps to, keyed by the fully qualified type name.
    /// </summary>
    /// <remarks>
    /// Only the types the package has a base pair for. A property of any other type is reported rather than generated
    /// with a guessed base: the generic <c>ComponentMonoBinder&lt;T, TProperty&gt;</c> would compile and then behave
    /// unlike every other binder of that type, which is worse than a build error naming the gap.
    /// </remarks>
    private static readonly Dictionary<string, (string Target, string Mono, string Keyword)> Bases = new()
    {
        ["bool"] = ("TargetBoolBinder", "ComponentBoolMonoBinder", "bool"),
        ["int"] = ("TargetIntBinder", "ComponentIntMonoBinder", "int"),
        ["float"] = ("TargetFloatBinder", "ComponentFloatMonoBinder", "float"),
        ["string"] = ("TargetStringBinder", "ComponentStringMonoBinder", "string"),
        ["UnityEngine.Color"] = ("TargetColorBinder", "ComponentColorMonoBinder", "UnityEngine.Color"),
        ["UnityEngine.Vector2"] = ("TargetVector2Binder", "ComponentVector2MonoBinder", "UnityEngine.Vector2"),
        ["UnityEngine.Vector3"] = ("TargetVector3Binder", "ComponentVector3MonoBinder", "UnityEngine.Vector3"),
        ["UnityEngine.Quaternion"] = ("TargetQuaternionBinder", "ComponentQuaternionMonoBinder", "UnityEngine.Quaternion"),
    };

    private static readonly DiagnosticDescriptor UnknownProperty = new(
        id: "ASPIDGB001",
        title: "Property to bind was not found",
        messageFormat: "'{0}' has no public instance property named '{1}', so no binder was generated",
        category: "Aspid.MVVM",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor UnsupportedType = new(
        id: "ASPIDGB002",
        title: "No binder base for this property type",
        messageFormat: "'{0}.{1}' is of type '{2}', which has no binder base in the package; write this family by hand",
        category: "Aspid.MVVM",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor NotWritable = new(
        id: "ASPIDGB003",
        title: "Property to bind is read-only",
        messageFormat: "'{0}.{1}' has no setter, so a binder could receive a value and have nowhere to put it",
        category: "Aspid.MVVM",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var declarations = context.CompilationProvider.Select(static (compilation, _) => Read(compilation));

        context.RegisterSourceOutput(declarations, static (production, families) =>
        {
            foreach (var family in families)
            {
                if (family.Diagnostic is not null)
                {
                    production.ReportDiagnostic(family.Diagnostic);
                    continue;
                }

                production.AddSource($"{family.Prefix}Binder.g.cs", family.Source);
            }
        });
    }

    private static ImmutableArray<Family> Read(Compilation compilation)
    {
        var attributeType = compilation.GetTypeByMetadataName(AttributeName);
        if (attributeType is null) return ImmutableArray<Family>.Empty;

        var families = ImmutableArray.CreateBuilder<Family>();
        var existing = new HashSet<string>();

        foreach (var attribute in compilation.Assembly.GetAttributes())
        {
            if (!SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, attributeType)) continue;
            if (attribute.ConstructorArguments.Length < 2) continue;

            var component = attribute.ConstructorArguments[0].Value as INamedTypeSymbol;
            var propertyName = attribute.ConstructorArguments[1].Value as string;

            if (component is null || string.IsNullOrWhiteSpace(propertyName)) continue;

            var named = Named(attribute);
            var prefix = named.Prefix ?? component.Name + Capitalise(propertyName!);

            // Одна и та же семья, объявленная дважды, — это опечатка в одном из двух объявлений;
            // вторая эмиссия дала бы ошибку о дублирующемся типе вместо указания на причину.
            if (!existing.Add(prefix)) continue;

            var property = component.GetMembers(propertyName!).OfType<IPropertySymbol>()
                .FirstOrDefault(candidate => candidate is { IsStatic: false, DeclaredAccessibility: Accessibility.Public });

            if (property is null)
            {
                families.Add(Family.Failed(Diagnostic.Create(UnknownProperty, Location.None, component.ToDisplayString(), propertyName)));
                continue;
            }

            if (property.SetMethod is null || property.SetMethod.DeclaredAccessibility != Accessibility.Public)
            {
                families.Add(Family.Failed(Diagnostic.Create(NotWritable, Location.None, component.ToDisplayString(), propertyName)));
                continue;
            }

            var typeName = property.Type.ToDisplayString();

            if (!Bases.TryGetValue(typeName, out var bases))
            {
                families.Add(Family.Failed(Diagnostic.Create(UnsupportedType, Location.None,
                    component.ToDisplayString(), propertyName, typeName)));

                continue;
            }

            families.Add(new Family(prefix, Emit(component, property, bases, prefix, named)));
        }

        return families.ToImmutable();
    }

    private static (string? Prefix, string? Menu, string? SerializedName) Named(AttributeData attribute)
    {
        string? prefix = null;
        string? menu = null;
        string? serializedName = null;

        foreach (var argument in attribute.NamedArguments)
        {
            var value = argument.Value.Value as string;
            if (string.IsNullOrWhiteSpace(value)) continue;

            switch (argument.Key)
            {
                case "Prefix": prefix = value; break;
                case "Menu": menu = value; break;
                case "SerializedName": serializedName = value; break;
            }
        }

        return (prefix, menu, serializedName);
    }

    private static string Emit(
        INamedTypeSymbol component,
        IPropertySymbol property,
        (string Target, string Mono, string Keyword) bases,
        string prefix,
        (string? Prefix, string? Menu, string? SerializedName) named)
    {
        var componentName = component.ToDisplayString();
        var valueType = bases.Keyword;
        var isBool = valueType is "bool";

        var context = named.SerializedName is null
            ? $"typeof({componentName})"
            : $"typeof({componentName}), serializePropertyNames: \"{named.SerializedName}\"";

        var menu = named.Menu is null
            ? string.Empty
            : $"    [global::UnityEngine.AddComponentMenu(\"{named.Menu}\")]\n";

        var constructorTail = isBool
            ? "bool isInvert = false,\n            global::Aspid.MVVM.BindMode mode = global::Aspid.MVVM.BindMode.OneWay)\n            : base(target, isInvert, mode) { }"
            : $"global::Aspid.MVVM.StarterKit.IConverter<{valueType}, {valueType}>? converter = null,\n            global::Aspid.MVVM.BindMode mode = global::Aspid.MVVM.BindMode.OneWay)\n            : base(target, converter, mode) {{ }}";

        var builder = new StringBuilder();

        builder.AppendLine("// <auto-generated/>");
        builder.AppendLine("#nullable enable");
        builder.AppendLine();
        builder.AppendLine("namespace Aspid.MVVM.StarterKit");
        builder.AppendLine("{");
        builder.AppendLine("    /// <summary>");
        builder.AppendLine($"    /// <see cref=\"{bases.Target}{{T}}\">{bases.Target}&lt;{component.Name}&gt;</see> that binds <see cref=\"{componentName}.{property.Name}\"/>.");
        builder.AppendLine("    /// </summary>");
        builder.AppendLine("    /// <remarks>");
        builder.AppendLine("    /// Generated from a <c>[assembly: GenerateBinders]</c> declaration. Write the family by hand instead when it");
        builder.AppendLine("    /// needs a guard, a mode override or an option of its own — a hand-written class of the same name wins.");
        builder.AppendLine("    /// </remarks>");
        builder.AppendLine("    [global::System.Serializable]");
        builder.AppendLine($"    public class {prefix}Binder : {bases.Target}<{componentName}>");
        builder.AppendLine("    {");
        builder.AppendLine("        /// <inheritdoc/>");
        builder.AppendLine($"        protected sealed override {valueType} Property");
        builder.AppendLine("        {");
        builder.AppendLine($"            get => Target.{property.Name};");
        builder.AppendLine($"            set => Target.{property.Name} = value;");
        builder.AppendLine("        }");
        builder.AppendLine();
        builder.AppendLine("        /// <inheritdoc/>");
        builder.AppendLine($"        public {prefix}Binder(");
        builder.AppendLine($"            {componentName} target,");
        builder.AppendLine($"            {constructorTail}");
        builder.AppendLine("    }");
        builder.AppendLine();
        builder.AppendLine("    /// <summary>");
        builder.AppendLine($"    /// <see cref=\"{bases.Mono}{{T}}\">{bases.Mono}&lt;{component.Name}&gt;</see> that binds <see cref=\"{componentName}.{property.Name}\"/>.");
        builder.AppendLine("    /// </summary>");
        builder.AppendLine("    /// <remarks>");
        builder.AppendLine("    /// Generated from a <c>[assembly: GenerateBinders]</c> declaration, together with its serializable twin — which is");
        builder.AppendLine("    /// what keeps the two from drifting apart.");
        builder.AppendLine("    /// </remarks>");
        builder.AppendLine($"    [global::Aspid.MVVM.AddBinderContextMenu({context})]");
        builder.Append(menu);
        builder.AppendLine($"    public class {prefix}MonoBinder : {bases.Mono}<{componentName}>");
        builder.AppendLine("    {");
        builder.AppendLine("        /// <inheritdoc/>");
        builder.AppendLine($"        protected sealed override {valueType} Property");
        builder.AppendLine("        {");
        builder.AppendLine($"            get => CachedComponent.{property.Name};");
        builder.AppendLine($"            set => CachedComponent.{property.Name} = value;");
        builder.AppendLine("        }");
        builder.AppendLine("    }");
        builder.AppendLine("}");

        return builder.ToString();
    }

    private static string Capitalise(string value) =>
        value.Length is 0 ? value : char.ToUpperInvariant(value[0]) + value.Substring(1);

    private readonly struct Family
    {
        public readonly string Prefix;
        public readonly string Source;
        public readonly Diagnostic? Diagnostic;

        public Family(string prefix, string source)
        {
            Prefix = prefix;
            Source = source;
            Diagnostic = null;
        }

        private Family(Diagnostic diagnostic)
        {
            Prefix = string.Empty;
            Source = string.Empty;
            Diagnostic = diagnostic;
        }

        public static Family Failed(Diagnostic diagnostic) => new(diagnostic);
    }
}
