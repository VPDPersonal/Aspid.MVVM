using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Declares a leaf binder family to be generated for one property of one component.
    /// </summary>
    /// <remarks>
    /// Applied to the assembly once per family. Emits <c>{Prefix}Binder</c> over the matching <c>Target*Binder</c>
    /// base and <c>{Prefix}MonoBinder</c> over the matching <c>Component*MonoBinder</c> base; the value type is read
    /// from the property itself.
    /// <para/>
    /// Generation is skipped for a family whose classes already exist by name, so a hand-written override stays untouched.
    /// </remarks>
    /// <example>
    /// <code>
    /// [assembly: GenerateBinders(typeof(UnityEngine.UI.LayoutElement), "preferredWidth",
    ///     Prefix = "LayoutElementPreferredWidth",
    ///     Menu = "Aspid/MVVM/Binders/UI/LayoutElement/LayoutElement Binder – Preferred Width",
    ///     SerializedName = "m_PreferredWidth")]
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class GenerateBindersAttribute : Attribute
    {
        /// <summary>
        /// Gets the component type whose property is bound.
        /// </summary>
        public Type Component { get; }

        /// <summary>
        /// Gets the name of the property, exactly as the component declares it.
        /// </summary>
        public string Property { get; }

        /// <summary>
        /// Gets or sets the name the generated classes are built from. Defaults to the component name followed by the
        /// capitalised property name.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the Add Component menu path of the generated MonoBehaviour binder.
        /// </summary>
        /// <remarks>
        /// Left empty, the binder is generated without a menu entry rather than a guessed one.
        /// </remarks>
        public string Menu { get; set; }

        /// <summary>
        /// Gets or sets the serialized property name the Inspector's context menu offers the binder under.
        /// </summary>
        /// <remarks>
        /// This is the leaf name Unity iterates — <c>m_PreferredWidth</c>, not <c>m_LayoutElement.m_PreferredWidth</c>.
        /// Left empty, the binder is offered for any property of the component.
        /// </remarks>
        public string SerializedName { get; set; }

        /// <param name="component">The component type whose property is bound.</param>
        /// <param name="property">The name of the property, exactly as the component declares it.</param>
        public GenerateBindersAttribute(Type component, string property)
        {
            Component = component;
            Property = property;
        }
    }
}
