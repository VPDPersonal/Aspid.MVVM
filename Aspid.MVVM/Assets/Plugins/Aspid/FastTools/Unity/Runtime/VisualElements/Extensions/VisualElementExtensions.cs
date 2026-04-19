using System;
using Unity.Properties;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static partial class VisualElementExtensions
    {
        /// <summary>
        /// Sets <see cref="VisualElement.name"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The name of this VisualElement.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The name to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetName<T>(this T element, string value)
            where T : VisualElement
        {
            element.name = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="VisualElement.visible"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Indicates whether or not this element should be rendered.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether the element is visible.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetVisible<T>(this T element, bool value)
            where T : VisualElement
        {
            element.visible = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="VisualElement.tooltip"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Text to display inside an information box after the user hovers the element for a small amount of time. This is only supported in the Editor UI.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The tooltip text to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetTooltip<T>(this T element, string value)
            where T : VisualElement
        {
            element.tooltip = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="VisualElement.userData"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This property can be used to associate application-specific user data with this VisualElement.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The user data to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUserData<T>(this T element, object value)
            where T : VisualElement
        {
            element.userData = value;
            return element;
        }

        /// <summary>
        /// Changes the VisualElement enabled state and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// A disabled visual element does not receive most events.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether the element is enabled.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetEnabledSelf<T>(this T element, bool value)
            where T : VisualElement
        {
            element.SetEnabled(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="VisualElement.dataSource"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Assigns a data source to this VisualElement which overrides any inherited data source. This data source is inherited by all children.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The data source to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetDataSource<T>(this T element, object value)
            where T : VisualElement
        {
            element.dataSource = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="VisualElement.viewDataKey"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Used for view data persistence, such as tree expanded states, scroll position, or zoom level.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The view data key to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetViewDataKey<T>(this T element, string value)
            where T : VisualElement
        {
            element.viewDataKey = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="VisualElement.dataSourceType"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The possible type of data source assignable to this VisualElement.
        /// This information is only used by the UI Builder as a hint to provide some completion to the data source path field when the effective data source cannot be specified at design time.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The data source type to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetDataSourceType<T>(this T element, Type value)
            where T : VisualElement
        {
            element.dataSourceType = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="VisualElement.usageHints"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// A combination of hint values that specify high-level intended usage patterns for the VisualElement.
        /// This property can only be set when the VisualElement is not yet part of a Panel. Once part of a Panel, this property becomes effectively read-only, and attempts to change it will throw an exception.
        /// The specification of proper UsageHints drives the system to make better decisions on how to process or accelerate certain operations based on the anticipated usage pattern.
        /// Note that those hints do not affect behavioral or visual results, but only affect the overall performance of the panel and the elements within.
        /// It's advised to always consider specifying the proper UsageHints, but keep in mind that some UsageHints might be internally ignored under certain conditions (e.g. due to hardware limitations on the target platform).
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The usage hints to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUsageHints<T>(this T element, UsageHints value)
            where T : VisualElement
        {
            element.usageHints = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="VisualElement.pickingMode"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Determines if this element can be the target of pointer events or picked by IPanel.Pick queries.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The picking mode to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetPickingMode<T>(this T element, PickingMode value)
            where T : VisualElement
        {
            element.pickingMode = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="VisualElement.disablePlayModeTint"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Play-mode tint is applied by default unless this is set to true. It's applied hierarchically to this VisualElement and to all its children that exist on an editor panel.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether to disable the play-mode tint.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetDisablePlayModeTint<T>(this T element, bool value)
            where T : VisualElement
        {
            element.disablePlayModeTint = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="VisualElement.dataSourcePath"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Path from the data source to the value.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The data source path to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetDataSourcePath<T>(this T element, PropertyPath value)
            where T : VisualElement
        {
            element.dataSourcePath = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="VisualElement.languageDirection"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Indicates the directionality of the element's text. The value will propagate to the element's children.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The language direction to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLanguageDirection<T>(this T element, LanguageDirection value)
            where T : VisualElement
        {
            element.languageDirection = value;
            return element;
        }
    }
}
