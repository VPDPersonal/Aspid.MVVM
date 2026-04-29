using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Configuration preset for an <see cref="AspidGradientButton"/>.
    /// Use the fluent builder methods to create a customized preset.
    /// </summary>
    public struct AspidGradientButtonPreset
    {
        /// <summary>
        /// The default preset: empty label, no click handler, zero colors (resolved from USS).
        /// </summary>
        public static AspidGradientButtonPreset Default => new AspidGradientButtonPreset()
            .SetText(string.Empty);

        /// <summary>
        /// The button label text.
        /// </summary>
        public string Text;

        /// <summary>
        /// The handler invoked when the button is clicked, or <see langword="null"/> if no handler should be wired.
        /// </summary>
        public Action<EventBase> OnClick;

        /// <summary>
        /// Gradient background color. Falls back to USS when default.
        /// </summary>
        public Color Gradient;

        /// <summary>
        /// Accent (hover) color. Falls back to USS when default.
        /// </summary>
        public Color Accent;

        /// <summary>
        /// Sets <see cref="Text"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new label text.</param>
        public AspidGradientButtonPreset SetText(string value)
        {
            Text = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="OnClick"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new click handler.</param>
        public AspidGradientButtonPreset SetOnClick(Action<EventBase> value)
        {
            OnClick = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Gradient"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new gradient background color.</param>
        public AspidGradientButtonPreset SetGradient(Color value)
        {
            Gradient = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Accent"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new accent (hover) color.</param>
        public AspidGradientButtonPreset SetAccent(Color value)
        {
            Accent = value;
            return this;
        }
    }
}
