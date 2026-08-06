using System;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Identity of a managed-reference type as it is stored in Unity's serialized YAML
    /// (<c>type: {class: …, ns: …, asm: …}</c>). Used to repair a reference whose type went missing by
    /// rewriting that line directly, since Unity's serialization API cannot reassign a missing type.
    /// </summary>
    internal readonly struct ManagedTypeName
    {
        private static readonly char[] _yamlReservedChars = { ',', '[', ']', '{', '}' };

        public readonly string Class;
        public readonly string Assembly;
        public readonly string Namespace;

        /// <summary>
        /// True for the empty type. Computed (not a stored field) so <c>default(ManagedTypeName)</c> — which never
        /// runs the constructor, e.g. <see cref="FromType"/> on a null type — still reports empty instead of a
        /// stale <c>false</c>.
        /// </summary>
        public bool IsEmpty => string.IsNullOrWhiteSpace(Assembly)
            && string.IsNullOrWhiteSpace(Namespace)
            && string.IsNullOrWhiteSpace(Class);

        /// <summary>
        /// Full <c>Namespace.Class, Assembly</c> identity built on top of <see cref="DisplayName"/>, for tooltips that
        /// need the assembly too. Empty for the empty type.
        /// </summary>
        public string FullName => IsEmpty
            ? string.Empty
            : string.IsNullOrWhiteSpace(Assembly) ? DisplayName : $"{DisplayName}, {Assembly}";

        /// <summary>
        /// Human-readable <c>Namespace.Class</c> identity (the class alone when there is no namespace), or an empty
        /// string when this is the empty type. The single source of truth for the missing-type caption shown in the
        /// repair dialog, the project audit list and the graph header, so nested (<c>Outer/Inner</c>) or generic
        /// class-name display fixes land in one place.
        /// </summary>
        public string DisplayName => IsEmpty
            ? string.Empty
            : string.IsNullOrWhiteSpace(Namespace) ? Class : $"{Namespace}.{Class}";

        public ManagedTypeName(string assembly, string @namespace, string className)
        {
            Class = className ?? string.Empty;
            Assembly = assembly ?? string.Empty;
            Namespace = @namespace ?? string.Empty;
        }

        /// <summary>
        /// Builds the YAML type identity for a resolved <see cref="Type"/>, including the
        /// <c>Name`N[[arg, asm],…]</c> shape Unity uses for closed generics.
        /// </summary>
        public static ManagedTypeName FromType(Type type)
        {
            if (type is null) return default;
            var root = type.IsGenericType ? type.GetGenericTypeDefinition() : type;

            // Unity stores a nested type's class identity with its declaring types joined by '/' (Outer/Inner), but
            // Type.Name is only the leaf — mirror of the read side's '/'->'+' mapping in
            // SerializeReferenceHelpers.StoredTypeResolves. Without the prefix a repaired nested reference re-breaks.
            return new ManagedTypeName(
                assembly: root.Assembly.GetName().Name,
                @namespace: root.Namespace,
                className: NestedPrefix(type) + BuildClassName(type));
        }

        // The "Outer/" (or "Outer/Middle/") prefix Unity prepends to a nested type's class identity; empty for a
        // top-level type. Walks the declaring-type chain from the outermost inward.
        private static string NestedPrefix(Type type)
        {
            if (type.DeclaringType is null)
                return string.Empty;

            var prefix = string.Empty;
            for (var declaring = type.DeclaringType; declaring is not null; declaring = declaring.DeclaringType)
            {
                prefix = declaring.Name + "/" + prefix;
            }

            return prefix;
        }

        private static string BuildClassName(Type type)
        {
            if (!type.IsGenericType) return type.Name;

            var definition = type.GetGenericTypeDefinition();
            var arguments = type.GetGenericArguments().Select(BuildGenericArgumentName);

            return $"{definition.Name}[[{string.Join("],[", arguments)}]]";
        }

        private static string BuildGenericArgumentName(Type type) =>
            $"{BuildFullClassName(type)}, {type.Assembly.GetName().Name}";

        private static string BuildFullClassName(Type type)
        {
            if (!type.IsGenericType)
                return type.FullName;

            var definition = type.GetGenericTypeDefinition();
            var prefix = string.IsNullOrEmpty(definition.Namespace) ? string.Empty : $"{definition.Namespace}.";

            return $"{prefix}{BuildClassName(type)}";
        }

        /// <summary>
        /// Renders the inline YAML mapping Unity writes for a managed-reference type entry.
        /// </summary>
        public string ToYamlType() =>
            $"{{class: {EscapeInline(Class)}, ns: {EscapeInline(Namespace)}, asm: {EscapeInline(Assembly)}}}";

        // A flow-scalar containing any of , [ ] { } would break the inline mapping, so single-quote it
        // (doubling embedded quotes) exactly as Unity does for generic class names like Foo`1[[…]].
        private static string EscapeInline(string value)
        {
            if (string.IsNullOrEmpty(value) || value.IndexOfAny(_yamlReservedChars) < 0)
                return value ?? string.Empty;

            return $"'{value.Replace("'", "''")}'";
        }
    }
}
