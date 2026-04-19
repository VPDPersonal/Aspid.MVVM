#nullable enable
using System;
using UnityEngine;
using System.Linq;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types
{
    /// <summary>
    /// Instructs the Unity Editor to use the type-selector window.
    /// </summary>
    /// <remarks>
    /// Only compiled in editor assemblies (<c>UNITY_EDITOR</c>).
    /// One or more base types can be supplied; the picker will show only their subtypes.
    /// </remarks>
    /// <example>
    /// Constrain to a single base type:
    /// <code>
    /// [TypeSelector(typeof(MonoBehaviour))]
    /// [SerializeField] private string _behaviourType;
    /// </code>
    ///
    /// Accept any type (unconstrained):
    /// <code>
    /// [TypeSelector]
    /// [SerializeField] private string _anyType;
    /// </code>
    ///
    /// Allow multiple independent base types:
    /// <code>
    /// [TypeSelector(typeof(IDisposable), typeof(ScriptableObject))]
    /// [SerializeField] private string _type;
    /// </code>
    /// </example>
    [Conditional(conditionString: "UNITY_EDITOR")]
    public sealed class TypeSelectorAttribute : PropertyAttribute
    {
        /// <summary>
        /// The assembly-qualified names of the base types that constrain the selection.
        /// </summary>
        public readonly string[] AssemblyQualifiedNames;
        
        /// <summary>
        /// Whether abstract types are included in the picker. Defaults to <c>false</c>.
        /// </summary>
        public bool AllowAbstractTypes { get; set; } = false;

        /// <summary>
        /// Whether interface types are included in the picker. Defaults to <c>false</c>.
        /// </summary>
        public bool AllowInterfaces { get; set; } = false;

        /// <summary>
        /// Creates an unconstrained attribute (base type is <see cref="object"/>).
        ///</summary>
        public TypeSelectorAttribute()
            : this(typeof(object)) { }

        /// <summary>
        /// Creates an attribute constrained to a single base type.
        /// </summary>
        /// <param name="type">The base constraint type.</param>
        public TypeSelectorAttribute(Type type)
            : this(types: type) { }

        /// <summary>
        /// Creates an attribute constrained to one or more base types.
        /// </summary>
        /// <param name="types">The base constraint types.</param>
        public TypeSelectorAttribute(params Type[] types)
        {
            AssemblyQualifiedNames = types
                .Select(type => type.AssemblyQualifiedName)
                .ToArray();
        }

        /// <summary>
        /// Creates an attribute constrained to a single base type specified by its assembly-qualified name.
        /// </summary>
        /// <param name="assemblyQualifiedName">The assembly-qualified name of the base constraint type.</param>
        public TypeSelectorAttribute(string assemblyQualifiedName)
            : this(assemblyQualifiedNames: assemblyQualifiedName) { }

        /// <summary>
        /// Creates an attribute constrained to one or more base types specified by their assembly-qualified names.
        /// </summary>
        /// <param name="assemblyQualifiedNames">The assembly-qualified names of the base constraint types.</param>
        public TypeSelectorAttribute(params string[] assemblyQualifiedNames)
        {
            AssemblyQualifiedNames = assemblyQualifiedNames;
        }
    }
}
