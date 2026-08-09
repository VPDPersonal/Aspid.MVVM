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
    /// One or more base types can be supplied; the picker shows only types
    /// assignable to <b>all</b> of them.
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
    /// Constrain to the intersection of several base types:
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
        /// Which special type categories (abstract classes, interfaces) the picker should
        /// include in addition to plain concrete classes. Defaults to <see cref="TypeAllow.All"/>,
        /// so a type-name field (a <c>string</c> or a <see cref="SerializableType"/>) offers abstract
        /// classes and interfaces too — set <see cref="TypeAllow.None"/> to restrict it to concrete types.
        /// Ignored on a <c>[SerializeReference]</c> managed reference: that path always lists only
        /// concrete, instantiable types regardless of this value.
        /// </summary>
        public TypeAllow Allow { get; set; } = TypeAllow.All;

        /// <summary>
        /// Requires the field to hold a value. When <see langword="true"/>, an unset field shows an inline "required"
        /// warning in the inspector and counts as a violation for the build/CI gate. Defaults to <see langword="false"/>
        /// (the field may be left empty).
        /// </summary>
        /// <remarks>
        /// What counts as "unset" depends on the field shape this attribute drives:
        /// <list type="bullet">
        ///   <item><description>a <c>[SerializeReference]</c> managed reference — unset when it is <see langword="null"/>;</description></item>
        ///   <item><description>a <c>string</c> field (assembly-qualified name) — unset when it is empty;</description></item>
        ///   <item><description>a <see cref="SerializableType"/> / <see cref="SerializableType{T}"/> field — unset when its stored type name is empty.</description></item>
        /// </list>
        /// A managed reference that <i>is</i> set but whose type can no longer be resolved (renamed or deleted class) is
        /// <b>not</b> a <c>Required</c> violation: that broken-data case is handled by the separate missing-type
        /// notice/gate, which fires regardless of this flag.
        /// </remarks>
        /// <example>
        /// <code>
        /// [TypeSelector(typeof(IWeapon), Required = true)]
        /// [SerializeReference] private IWeapon _weapon;
        ///
        /// [TypeSelector(typeof(MonoBehaviour), Required = true)]
        /// [SerializeField] private string _behaviourType;
        /// </code>
        /// </example>
        public bool Required { get; set; }

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
            AssemblyQualifiedNames = types?
                .Where(type => type is not null)
                .Select(type => type.AssemblyQualifiedName)
                .ToArray() ?? Array.Empty<string>();
        }

        /// <summary>
        /// Creates an attribute constrained to a single base type named by a string.
        /// </summary>
        /// <param name="assemblyQualifiedName">
        /// Either an <b>assembly-qualified type name</b> (e.g. <c>"MyGame.IWeapon, MyGame"</c>) resolved with
        /// <see cref="System.Type.GetType(string)"/>, or the <b>name of a member</b> on the same object that supplies
        /// the constraint at inspector time — see the constructor remarks.
        /// </param>
        /// <remarks>
        /// Resolved <b>member-first</b>: if the string is a C# identifier matching an instance field or property on the
        /// target object, that member's current value supplies the base type(s) — so the constraint can be driven live by
        /// another field. Otherwise, it is treated as an assembly-qualified type name.
        /// <para>
        /// A member may be a <see cref="System.Type"/>, <c>string</c> (assembly-qualified name),
        /// <see cref="SerializableType"/> / <see cref="SerializableType{T}"/>, or an array of these.
        /// Prefer <c>nameof(...)</c> so a rename keeps the reference intact; use the
        /// <see cref="TypeSelectorAttribute(System.Type)"/> overload when <c>typeof(...)</c> is possible.
        /// </para>
        /// A name that resolves to nothing is surfaced as an inline inspector notice.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Constrain the picker to the value of another field, resolved live:
        /// [SerializeField] private SerializableType _category;
        ///
        /// [TypeSelector(nameof(_category))]
        /// [SerializeField] private string _subType;
        /// </code>
        /// </example>
        public TypeSelectorAttribute(string assemblyQualifiedName)
            : this(assemblyQualifiedNames: assemblyQualifiedName) { }

        /// <summary>
        /// Creates an attribute constrained to one or more base types, each named by a string.
        /// </summary>
        /// <param name="assemblyQualifiedNames">
        /// Each entry is resolved independently, member-first: an identifier matching an instance field/property on the
        /// target object supplies its value as a constraint, otherwise the entry is an assembly-qualified type name. See
        /// <see cref="TypeSelectorAttribute(string)"/> for the full contract and the supported member types.
        /// </param>
        public TypeSelectorAttribute(params string[] assemblyQualifiedNames)
        {
            AssemblyQualifiedNames = assemblyQualifiedNames ?? Array.Empty<string>();
        }
    }
}
