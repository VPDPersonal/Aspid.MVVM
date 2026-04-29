using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the gradient and accent colors of an <see cref="AspidGradientButton"/>. Each color can
    /// be inherited from its USS custom property or set explicitly in code; once set explicitly it is
    /// no longer overridden by USS resolution.
    /// </summary>
    public readonly struct AspidGradientButtonColorsStyle
    {
        /// <summary>
        /// Custom USS property for overriding the gradient background color via USS.
        /// </summary>
        public static readonly CustomStyleProperty<Color> GradientProperty = new("--aspid-fasttools-colors-gradient-button-bg");

        /// <summary>
        /// Custom USS property for overriding the accent (hover) color via USS.
        /// </summary>
        public static readonly CustomStyleProperty<Color> AccentProperty = new("--aspid-fasttools-colors-gradient-button-accent");

        private readonly InlineStyle<Color> _gradient;
        private readonly InlineStyle<Color> _accent;

        /// <summary>
        /// The current gradient background color.
        /// </summary>
        public Color Gradient => _gradient.Value;

        /// <summary>
        /// The current accent (hover) color.
        /// </summary>
        public Color Accent => _accent.Value;

        /// <summary>
        /// Creates a colors binding for <paramref name="element"/> with initial gradient and accent colors.
        /// Registers a <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven values are
        /// applied as defaults until <see cref="SetGradient"/> or <see cref="SetAccent"/> is called.
        /// </summary>
        /// <param name="element">The element whose USS custom properties drive the colors.</param>
        /// <param name="gradient">The initial gradient background color.</param>
        /// <param name="accent">The initial accent color.</param>
        /// <param name="onGradientChanged">Optional callback invoked with the new color whenever the gradient color changes.</param>
        /// <param name="onAccentChanged">Optional callback invoked with the new color whenever the accent color changes.</param>
        public AspidGradientButtonColorsStyle(
            VisualElement element,
            Color gradient,
            Color accent,
            Action<Color> onGradientChanged = null,
            Action<Color> onAccentChanged = null)
        {
            _gradient = new InlineStyle<Color>(gradient, (_, value) => onGradientChanged?.Invoke(value));
            _accent = new InlineStyle<Color>(accent, (_, value) => onAccentChanged?.Invoke(value));

            element.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        /// <summary>
        /// Explicitly sets the gradient background color. Subsequent USS resolutions will not override it.
        /// </summary>
        public void SetGradient(Color value) =>
            _gradient.SetInlineValue(value);

        /// <summary>
        /// Explicitly sets the accent (hover) color. Subsequent USS resolutions will not override it.
        /// </summary>
        public void SetAccent(Color value) =>
            _accent.SetInlineValue(value);

        /// <summary>
        /// Sets the gradient color only if it has not already been overridden via <see cref="SetGradient"/>.
        /// </summary>
        public void SetDefaultGradient(Color value) =>
            _gradient.SetDefaultValue(value);

        /// <summary>
        /// Sets the accent color only if it has not already been overridden via <see cref="SetAccent"/>.
        /// </summary>
        public void SetDefaultAccent(Color value) =>
            _accent.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetValue(GradientProperty, out var gradient))
                SetDefaultGradient(gradient);

            if (evt.customStyle.TryGetValue(AccentProperty, out var accent))
                SetDefaultAccent(accent);
        }
    }
}
