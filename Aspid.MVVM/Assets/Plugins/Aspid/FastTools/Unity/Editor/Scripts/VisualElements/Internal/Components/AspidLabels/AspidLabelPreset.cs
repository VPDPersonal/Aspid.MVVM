using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Configuration preset for an <see cref="AspidLabel"/>.
    /// Use the fluent builder methods to create a customised preset.
    /// </summary>
    public struct AspidLabelPreset
    {
        /// <summary>
        /// The default preset: light theme, H5 size, bold font, and a default dividing line.
        /// </summary>
        public static AspidLabelPreset Default => new AspidLabelPreset()
            .SetLabelTheme(ThemeStyle.Type.Light)
            .SetLabelSize(AspidLabelSizeStyle.Type.H5)
            .SetLine(AspidDividingLinePreset.Default)
            .SetFontStyle(UnityEngine.FontStyle.Bold);

        /// <summary>
        /// Whether the user can select the label text.
        /// </summary>
        public bool Selectable;

        /// <summary>
        /// The visual theme of the label.
        /// </summary>
        public ThemeStyle.Type Theme;

        /// <summary>
        /// The status color accent of the label.
        /// </summary>
        public StatusStyle.Type Status;

        /// <summary>
        /// The font size style of the label.
        /// </summary>
        public AspidLabelSizeStyle.Type Size;

        /// <summary>
        /// The font style and weight of the label.
        /// </summary>
        public StyleEnum<FontStyle> FontStyle;

        /// <summary>
        /// The configuration preset for the optional dividing line beneath the label.
        /// </summary>
        public AspidDividingLinePreset Line;

        /// <summary>
        /// Sets <see cref="Theme"/> and propagates the value to <see cref="AspidDividingLinePreset.Theme"/>,
        /// then returns the modified preset.
        /// </summary>
        /// <param name="value">The new theme.</param>
        public AspidLabelPreset SetTheme(ThemeStyle.Type value)
        {
            Theme = value;
            Line.SetTheme(value);
            return this;
        }

        /// <summary>
        /// Sets <see cref="AspidDividingLinePreset.Theme"/> only and returns the modified preset.
        /// </summary>
        /// <param name="value">The new line theme.</param>
        public AspidLabelPreset SetLineTheme(ThemeStyle.Type value)
        {
            Line.SetTheme(value);
            return this;
        }

        /// <summary>
        /// Sets <see cref="Theme"/> only and returns the modified preset.
        /// </summary>
        /// <param name="value">The new label theme.</param>
        public AspidLabelPreset SetLabelTheme(ThemeStyle.Type value)
        {
            Theme = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Status"/> and propagates the value to <see cref="AspidDividingLinePreset.Status"/>,
        /// then returns the modified preset.
        /// </summary>
        /// <param name="value">The new status.</param>
        public AspidLabelPreset SetStatus(StatusStyle.Type value)
        {
            Status = value;
            Line.SetStatus(value);
            return this;
        }

        /// <summary>
        /// Sets <see cref="AspidDividingLinePreset.Status"/> only and returns the modified preset.
        /// </summary>
        /// <param name="value">The new line status.</param>
        public AspidLabelPreset SetLineStatus(StatusStyle.Type value)
        {
            Line.SetStatus(value);
            return this;
        }

        /// <summary>
        /// Sets <see cref="Status"/> only and returns the modified preset.
        /// </summary>
        /// <param name="value">The new label status.</param>
        public AspidLabelPreset SetLabelStatus(StatusStyle.Type value)
        {
            Status = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Size"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new label size.</param>
        public AspidLabelPreset SetLabelSize(AspidLabelSizeStyle.Type value)
        {
            Size = value;
            return this;
        }

        /// <summary>
        /// Replaces the entire <see cref="Line"/> preset and returns the modified preset.
        /// </summary>
        /// <param name="value">The new dividing-line preset.</param>
        public AspidLabelPreset SetLine(AspidDividingLinePreset value)
        {
            Line = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="AspidDividingLinePreset.Size"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new line size.</param>
        public AspidLabelPreset SetLineSize(AspidDividingLineSizeStyle.Type value)
        {
            Line.SetSize(value);
            return this;
        }

        /// <summary>
        /// Sets <see cref="AspidDividingLinePreset.Direction"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new line direction.</param>
        public AspidLabelPreset SetLineDirection(AspidDividingLineDirectionStyle.Type value)
        {
            Line.SetDirection(value);
            return this;
        }

        /// <summary>
        /// Sets <see cref="FontStyle"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new font style and weight.</param>
        public AspidLabelPreset SetFontStyle(StyleEnum<FontStyle> value)
        {
            FontStyle = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Selectable"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">Whether the label text should be selectable. Defaults to <see langword="true"/>.</param>
        public AspidLabelPreset SetSelectable(bool value = true)
        {
            Selectable = value;
            return this;
        }
    }
}
