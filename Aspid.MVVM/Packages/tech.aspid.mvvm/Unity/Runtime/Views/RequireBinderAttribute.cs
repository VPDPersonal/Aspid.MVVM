#nullable enable
using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Configures the binding ID and accepted value types for Inspector validation of a serialized binder field.
    /// </summary>
    /// <remarks>Attribute applications are omitted unless <c>DEBUG</c> or <c>UNITY_EDITOR</c> is defined.</remarks>
    /// <example>
    /// <code>
    /// [RequireBinder(nameof(_valueTypeName), Id = nameof(_memberId))]
    /// [UnityEngine.SerializeField] private MonoBinder[] _binders;
    /// </code>
    /// </example>
    [Conditional(conditionString: "DEBUG")]
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Field, AllowMultiple = true)]
    public sealed class RequireBinderAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the binding ID or the name of a string member containing it.
        /// A blank value derives the ID from the decorated field name.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets the accepted type names or the names of string members that supply them.
        /// The parameterless constructor leaves this property <see langword="null"/>.
        /// </summary>
        public string[]? AssemblyQualifiedNames { get; }

        /// <summary>
        /// Creates an attribute without value type restrictions.
        /// </summary>
        public RequireBinderAttribute() { }

        /// <summary>
        /// Creates the attribute for a single value type.
        /// </summary>
        /// <param name="type">The bound value type to accept.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="type"/> has no assembly-qualified name.</exception>
        public RequireBinderAttribute(Type type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            var name = type.AssemblyQualifiedName
                ?? throw new ArgumentException("The value type must have an assembly-qualified name.", nameof(type));
           
            AssemblyQualifiedNames = new[] { name };
        }

        /// <summary>
        /// Creates the attribute for multiple value types.
        /// </summary>
        /// <param name="types">The bound value types to accept. Each binder must support at least one.</param>
        /// <exception cref="ArgumentNullException"><paramref name="types"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">An element of <paramref name="types"/> is <see langword="null"/> or has no assembly-qualified name.</exception>
        public RequireBinderAttribute(params Type[] types)
        {
            if (types is null)
                throw new ArgumentNullException(nameof(types));

            AssemblyQualifiedNames = new string[types.Length];
            for (var i = 0; i < types.Length; i++)
            {
                AssemblyQualifiedNames[i] = types[i].AssemblyQualifiedName
                    ?? throw new ArgumentException("Each value type must have an assembly-qualified name.", nameof(types));
            }
        }

        /// <summary>
        /// Creates the attribute with one value type name or a member supplying it.
        /// </summary>
        /// <param name="assemblyQualifiedName">The assembly-qualified value type name or the name of a string field or property containing it.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assemblyQualifiedName"/> is <see langword="null"/>.</exception>
        public RequireBinderAttribute(string assemblyQualifiedName)
            : this(assemblyQualifiedNames: new[] { assemblyQualifiedName ?? throw new ArgumentNullException(nameof(assemblyQualifiedName)) }) { }

        /// <summary>
        /// Creates the attribute with value type names or members supplying them.
        /// </summary>
        /// <param name="assemblyQualifiedNames">The value type names or names of string fields or properties containing them.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assemblyQualifiedNames"/> is <see langword="null"/>.</exception>
        public RequireBinderAttribute(params string[] assemblyQualifiedNames)
        {
            AssemblyQualifiedNames = assemblyQualifiedNames ?? throw new ArgumentNullException(nameof(assemblyQualifiedNames));
        }
    }
}
