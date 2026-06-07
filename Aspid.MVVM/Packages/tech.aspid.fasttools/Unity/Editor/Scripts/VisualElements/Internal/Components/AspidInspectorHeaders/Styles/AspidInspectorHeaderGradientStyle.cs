using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Bridges the <c>--aspid-fasttools-colors-gradient</c> USS custom property declared on an
    /// <see cref="AspidInspectorHeader"/> to the <see cref="AspidHoverGradientOverlay.Color"/> of its overlay.
    /// </summary>
    internal readonly struct AspidInspectorHeaderGradientStyle
    {
        /// <summary>
        /// Custom USS property that drives the overlay gradient color.
        /// </summary>
        public static readonly CustomStyleProperty<Color> StyleProperty = new("--aspid-fasttools-colors-gradient");

        private readonly AspidHoverGradientOverlay _overlay;

        /// <summary>
        /// Creates the binding by registering a <see cref="CustomStyleResolvedEvent"/> handler on
        /// <paramref name="element"/> that pushes the resolved color into <paramref name="overlay"/>.
        /// </summary>
        /// <param name="element">The element whose USS custom property drives the color.</param>
        /// <param name="overlay">The overlay receiving the color.</param>
        public AspidInspectorHeaderGradientStyle(AspidInspectorHeader element, AspidHoverGradientOverlay overlay)
        {
            _overlay = overlay;
            element.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetValue(StyleProperty, out var color))
                _overlay.Color = color;
        }
    }
}
