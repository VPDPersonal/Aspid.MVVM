#nullable enable
using System;
using System.Linq;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Editor-only attribute applied to serialized fields to declare the required binder association
    /// in the Unity Inspector. Enables the editor to validate that a <see cref="MonoBinder"/>
    /// of the expected type is assigned to the field.
    /// Stripped from builds outside of <c>DEBUG</c> and <c>UNITY_EDITOR</c> configurations.
    /// </summary>
    [Conditional(conditionString: "DEBUG")]
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Field, AllowMultiple = true)]
    public sealed class RequireBinderAttribute : Attribute
    {
        /// <summary>
        /// Optional identifier linking this field to a specific bindable member of the ViewModel.
        /// When set, the editor uses this value to match against ViewModel property names.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Assembly-qualified type names of the binder types accepted by this field.
        /// <c>null</c> when the attribute is used without specifying types.
        /// </summary>
        public string[]? AssemblyQualifiedNames { get; }

        /// <summary>
        /// Initializes the attribute without specifying a binder type.
        /// The editor will accept any binder assigned to the field.
        /// </summary>
        public RequireBinderAttribute() { }

        /// <summary>
        /// Initializes the attribute for a single binder type.
        /// </summary>
        /// <param name="type">The binder type that this field requires.</param>
        public RequireBinderAttribute(Type type)
            : this(new[] { type }) { }

        /// <summary>
        /// Initializes the attribute for multiple binder types.
        /// </summary>
        /// <param name="types">The binder types that this field accepts.</param>
        public RequireBinderAttribute(params Type[] types)
        {
            AssemblyQualifiedNames =
                types.Select(type => type.AssemblyQualifiedName).ToArray();
        }

        /// <summary>
        /// Initializes the attribute with a single assembly-qualified type name.
        /// </summary>
        /// <param name="assemblyQualifiedName">The assembly-qualified name of the required binder type.</param>
        public RequireBinderAttribute(string assemblyQualifiedName)
            : this(new[] { assemblyQualifiedName }) { }

        /// <summary>
        /// Initializes the attribute with multiple assembly-qualified type names.
        /// </summary>
        /// <param name="assemblyQualifiedNames">The assembly-qualified names of the accepted binder types.</param>
        public RequireBinderAttribute(params string[] assemblyQualifiedNames)
        {
            AssemblyQualifiedNames = assemblyQualifiedNames;
        }
    }
}