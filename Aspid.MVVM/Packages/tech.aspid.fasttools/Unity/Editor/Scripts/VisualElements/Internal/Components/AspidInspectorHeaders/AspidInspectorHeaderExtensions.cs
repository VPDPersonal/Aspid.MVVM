using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Fluent extension methods for <see cref="AspidInspectorHeader"/>.
    /// </summary>
    internal static class AspidInspectorHeaderExtensions
    {
        /// <summary>
        /// Sets <see cref="AspidInspectorHeader.Text"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new primary text.</param>
        public static AspidInspectorHeader SetText(this AspidInspectorHeader element, string value)
        {
            element.Text = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidInspectorHeader.Obj"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new Unity Object whose script is opened on icon double-click,
        /// or <see langword="null"/> to disable the command.</param>
        public static AspidInspectorHeader SetObj(this AspidInspectorHeader element, Object value)
        {
            element.Obj = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidInspectorHeader.Subtext"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new subtext.</param>
        public static AspidInspectorHeader SetSubtext(this AspidInspectorHeader element, string value)
        {
            element.Subtext = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidInspectorHeader.Status"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new status.</param>
        public static AspidInspectorHeader SetStatus(this AspidInspectorHeader element, StatusStyle.Type value)
        {
            element.Status = value;
            return element;
        }
    }
}
