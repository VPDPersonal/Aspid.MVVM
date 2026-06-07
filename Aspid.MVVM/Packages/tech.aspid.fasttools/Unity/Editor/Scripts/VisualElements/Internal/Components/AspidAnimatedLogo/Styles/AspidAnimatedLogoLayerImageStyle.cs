using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the background texture of a single <see cref="AspidAnimatedLogo"/> layer.
    /// The texture can be inherited from one of the <see cref="Layer1StyleProperty"/>,
    /// <see cref="Layer2StyleProperty"/> or <see cref="Layer3StyleProperty"/> USS custom
    /// properties or set explicitly in code; once set explicitly it is no longer overridden
    /// by USS resolution.
    /// </summary>
    /// <remarks>
    /// The USS property is resolved on the parent <see cref="AspidAnimatedLogo"/>
    /// (the event source), but applied as <c>background-image</c> to the layer
    /// (the target), so a single declaration on the logo configures all three layers.
    /// </remarks>
    internal readonly struct AspidAnimatedLogoLayerImageStyle
    {
        /// <summary>
        /// Custom USS property for the first logo layer.
        /// </summary>
        public static readonly CustomStyleProperty<Texture2D> Layer1StyleProperty =
            new("--aspid-fasttools-prop-animated_logo-layer_1");

        /// <summary>
        /// Custom USS property for the second logo layer.
        /// </summary>
        public static readonly CustomStyleProperty<Texture2D> Layer2StyleProperty =
            new("--aspid-fasttools-prop-animated_logo-layer_2");

        /// <summary>
        /// Custom USS property for the third logo layer.
        /// </summary>
        public static readonly CustomStyleProperty<Texture2D> Layer3StyleProperty =
            new("--aspid-fasttools-prop-animated_logo-layer_3");

        private readonly InlineStyle<Texture2D> _value;
        private readonly CustomStyleProperty<Texture2D> _styleProperty;

        /// <summary>
        /// The current layer texture.
        /// </summary>
        public Texture2D Value => _value;

        /// <summary>
        /// Creates a layer-image binding that listens for USS resolution on
        /// <paramref name="eventSource"/> and applies the resolved texture to
        /// <paramref name="target"/>'s <c>background-image</c>.
        /// </summary>
        /// <param name="target">The layer element whose background image is updated.</param>
        /// <param name="eventSource">The element that receives <see cref="CustomStyleResolvedEvent"/> (typically the parent logo).</param>
        /// <param name="styleProperty">The USS custom property for this specific layer.</param>
        /// <param name="value">The initial texture (may be <c>null</c>).</param>
        public AspidAnimatedLogoLayerImageStyle(
            VisualElement target,
            VisualElement eventSource,
            CustomStyleProperty<Texture2D> styleProperty,
            Texture2D value)
        {
            _styleProperty = styleProperty;
            _value = new InlineStyle<Texture2D>(value, (_, newValue) =>
            {
                target.style.backgroundImage = newValue is null
                    ? StyleKeyword.Null
                    : new StyleBackground(newValue);
            });

            eventSource.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        /// <summary>
        /// Explicitly sets the layer texture. Subsequent USS resolutions will not override this value.
        /// </summary>
        public void SetValue(Texture2D value) =>
            _value.SetInlineValue(value);

        /// <summary>
        /// Sets the layer texture only if it has not already been overridden via <see cref="SetValue"/>.
        /// Used when applying USS-resolved values.
        /// </summary>
        public void SetDefaultValue(Texture2D value) =>
            _value.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetValue(_styleProperty, out var value))
                SetDefaultValue(value);
        }
    }
}
