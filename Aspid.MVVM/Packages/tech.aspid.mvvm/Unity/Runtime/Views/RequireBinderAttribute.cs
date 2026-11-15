#nullable enable
using System;
using System.Linq;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Associates a serialized binder field with a binding identifier and accepted value types for Inspector validation.
    /// </summary>
    /// <remarks>Attribute applications are omitted unless <c>DEBUG</c> or <c>UNITY_EDITOR</c> is defined.</remarks>
    /// <example>
    /// <code>
    /// [RequireBinder(typeof(int), Id = "Health")]
    /// [UnityEngine.SerializeField] private MonoBinder[] _health;
    /// </code>
    /// <code>
    /// [RequireBinder(nameof(_valueTypeName), Id = nameof(_memberId))]
    /// [UnityEngine.SerializeField] private MonoBinder[] _binders;
    /// </code>
    /// </example>
    [Conditional(conditionString: "DEBUG")]
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Field, AllowMultiple = true)]
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public sealed class RequireBinderAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the binding identifier or the name of a string field or property containing it.
        /// When absent or blank, the identifier is derived from the decorated field name.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets the accepted value type names or names of string fields or properties containing those names.
        /// <see langword="null"/> when the attribute is used without specifying types.
        /// </summary>
        public string[]? AssemblyQualifiedNames { get; }

        /// <summary>
        /// Creates an attribute without value type restrictions.
        /// </summary>
        public RequireBinderAttribute() { }

        /// <summary>
        /// Creates the attribute for a single value type.
        /// </summary>
        /// <param name="type">The bound value type that assigned binders must support.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">A type is <see langword="null"/> or has no assembly-qualified name.</exception>
        public RequireBinderAttribute(Type type)
            : this(new[] { type ?? throw new ArgumentNullException(nameof(type)) }) { }

        /// <summary>
        /// Creates the attribute for multiple value types.
        /// </summary>
        /// <param name="types">The bound value types that assigned binders must support.</param>
        /// <exception cref="ArgumentNullException"><paramref name="types"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">A type is <see langword="null"/> or has no assembly-qualified name.</exception>
        public RequireBinderAttribute(params Type[] types)
        {
            if (types is null)
                throw new ArgumentNullException(nameof(types));
            if (types.Any(type => type?.AssemblyQualifiedName is null))
                throw new ArgumentException("Each value type must have an assembly-qualified name.", nameof(types));

            AssemblyQualifiedNames = types.Select(type => type.AssemblyQualifiedName!).ToArray();
        }

        /// <summary>
        /// Creates the attribute with a single assembly-qualified type name.
        /// </summary>
        /// <param name="assemblyQualifiedName">The assembly-qualified value type name or the name of a string field or property containing it.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assemblyQualifiedName"/> is <see langword="null"/>.</exception>
        public RequireBinderAttribute(string assemblyQualifiedName)
            : this(assemblyQualifiedNames: new[] { assemblyQualifiedName ?? throw new ArgumentNullException(nameof(assemblyQualifiedName)) }) { }

        /// <summary>
        /// Creates the attribute with multiple assembly-qualified type names.
        /// </summary>
        /// <param name="assemblyQualifiedNames">The value type names or names of string fields or properties containing them.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assemblyQualifiedNames"/> is <see langword="null"/>.</exception>
        public RequireBinderAttribute(params string[] assemblyQualifiedNames)
        {
            AssemblyQualifiedNames = assemblyQualifiedNames
                ?? throw new ArgumentNullException(nameof(assemblyQualifiedNames));
        }
    }
}
