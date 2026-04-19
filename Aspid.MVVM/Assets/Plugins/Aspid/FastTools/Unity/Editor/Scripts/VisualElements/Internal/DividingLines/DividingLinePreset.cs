// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Configuration preset for an <see cref="AspidDividingLine"/>.
    /// Use the fluent builder methods to create a customised preset.
    /// </summary>
    public struct DividingLinePreset
    {
        /// <summary>
        /// The default preset: light theme, no status, medium size, horizontal orientation.
        /// </summary>
        public static readonly DividingLinePreset Default = new DividingLinePreset()
            .SetTheme(ThemeStyle.Light)
            .SetStatus(StatusStyle.None)
            .SetSize(DividingLineSize.Medium)
            .SetDirection(DividingLineDirection.Horizontal);

        /// <summary>
        /// The visual theme of the line.
        /// </summary>
        public ThemeStyle Theme;

        /// <summary>
        /// The status color accent of the line.
        /// </summary>
        public StatusStyle Status;

        /// <summary>
        /// The thickness of the line.
        /// </summary>
        public DividingLineSize Size;

        /// <summary>
        /// The orientation of the line.
        /// </summary>
        public DividingLineDirection Direction;

        /// <summary>
        /// Sets <see cref="Theme"/> and returns the modified preset.
        /// </summary>
        public DividingLinePreset SetTheme(ThemeStyle value)
        {
            Theme = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Status"/> and returns the modified preset.
        /// </summary>
        public DividingLinePreset SetStatus(StatusStyle value)
        {
            Status = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Size"/> and returns the modified preset.
        /// </summary>
        public DividingLinePreset SetSize(DividingLineSize value)
        {
            Size = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Direction"/> and returns the modified preset.
        /// </summary>
        public DividingLinePreset SetDirection(DividingLineDirection value)
        {
            Direction = value;
            return this;
        }
    }
}
