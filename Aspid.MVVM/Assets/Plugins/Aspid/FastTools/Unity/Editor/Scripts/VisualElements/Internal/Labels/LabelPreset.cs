using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Configuration preset for an <see cref="AspidLabel"/>.
    /// Use the fluent builder methods to create a customised preset.
    /// </summary>
    public struct LabelPreset
    {
        /// <summary>
        /// The default preset: light theme, H2 size, bold font, and a default dividing line.
        /// </summary>
        public static readonly LabelPreset Default = new LabelPreset()
            .SetLabelTheme(ThemeStyle.Light)
            .SetLabelSize(AspidLabelSizeStyle.H2)
            .SetLine(DividingLinePreset.Default)
            .SetUnityFontStyleAndWeight(FontStyle.Bold);

        /// <summary>
        /// Whether the label text can be selected by the user.
        /// </summary>
        public bool Selectable;

        /// <summary>
        /// The visual theme of the label.
        /// </summary>
        public ThemeStyle Theme;

        /// <summary>
        /// The status color accent of the label.
        /// </summary>
        public StatusStyle Status;

        /// <summary>
        /// The font size style of the label.
        /// </summary>
        public AspidLabelSizeStyle Size;

        /// <summary>
        /// The font style and weight of the label.
        /// </summary>
        public StyleEnum<FontStyle> UnityFontStyleAndWeight;

        /// <summary>
        /// The configuration preset for the optional dividing line beneath the label.
        /// </summary>
        public DividingLinePreset Line;

        /// <summary>
        /// Sets <see cref="Theme"/> and <see cref="DividingLinePreset.Theme"/> and returns the modified preset.
        /// </summary>
        public LabelPreset SetTheme(ThemeStyle value)
        {
            Theme = value;
            Line.SetTheme(value);
            return this;
        }

        /// <summary>
        /// Sets <see cref="DividingLinePreset.Theme"/> only and returns the modified preset.
        /// </summary>
        public LabelPreset SetLineTheme(ThemeStyle value)
        {
            Line.SetTheme(value);
            return this;
        }

        /// <summary>
        /// Sets <see cref="Theme"/> only and returns the modified preset.
        /// </summary>
        public LabelPreset SetLabelTheme(ThemeStyle value)
        {
            Theme = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Status"/> and <see cref="DividingLinePreset.Status"/> and returns the modified preset.
        /// </summary>
        public LabelPreset SetStatus(StatusStyle value)
        {
            Status = value;
            Line.SetStatus(value);
            return this;
        }

        /// <summary>
        /// Sets <see cref="DividingLinePreset.Status"/> only and returns the modified preset.
        /// </summary>
        public LabelPreset SetLineStatus(StatusStyle value)
        {
            Line.SetStatus(value);
            return this;
        }

        /// <summary>
        /// Sets <see cref="Status"/> only and returns the modified preset.
        /// </summary>
        public LabelPreset SetLabelStatus(StatusStyle value)
        {
            Status = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Size"/> and returns the modified preset.
        /// </summary>
        public LabelPreset SetLabelSize(AspidLabelSizeStyle value)
        {
            Size = value;
            return this;
        }

        /// <summary>
        /// Replaces the entire <see cref="Line"/> preset and returns the modified preset.
        /// </summary>
        public LabelPreset SetLine(DividingLinePreset value)
        {
            Line = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="DividingLinePreset.Size"/> and returns the modified preset.
        /// </summary>
        public LabelPreset SetLineSize(DividingLineSize value)
        {
            Line.SetSize(value);
            return this;
        }

        /// <summary>
        /// Sets <see cref="DividingLinePreset.Direction"/> and returns the modified preset.
        /// </summary>
        public LabelPreset SetLineDirection(DividingLineDirection value)
        {
            Line.SetDirection(value);
            return this;
        }

        /// <summary>
        /// Sets <see cref="UnityFontStyleAndWeight"/> and returns the modified preset.
        /// </summary>
        public LabelPreset SetUnityFontStyleAndWeight(StyleEnum<FontStyle> value)
        {
            UnityFontStyleAndWeight = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Selectable"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">Whether the label text should be selectable. Defaults to <see langword="true"/>.</param>
        public LabelPreset SetSelectable(bool value = true)
        {
            Selectable = value;
            return this;
        }
    }
}
