// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Configuration preset for an <see cref="AspidDividingLine"/>.
    /// Use the fluent builder methods to create a customized preset.
    /// </summary>
    public struct AspidDividingLinePreset
    {
        /// <summary>
        /// The default preset: light theme, no status, medium size, horizontal orientation.
        /// </summary>
        public static AspidDividingLinePreset Default => new AspidDividingLinePreset()
            .SetTheme(ThemeStyle.Type.Light)
            .SetStatus(StatusStyle.Type.None)
            .SetSize(AspidDividingLineSizeStyle.Type.Medium)
            .SetDirection(AspidDividingLineDirectionStyle.Type.Horizontal);

        /// <summary>
        /// The visual theme of the line.
        /// </summary>
        public ThemeStyle.Type Theme;

        /// <summary>
        /// The status color accent of the line.
        /// </summary>
        public StatusStyle.Type Status;

        /// <summary>
        /// The thickness of the line.
        /// </summary>
        public AspidDividingLineSizeStyle.Type Size;

        /// <summary>
        /// The orientation of the line.
        /// </summary>
        public AspidDividingLineDirectionStyle.Type Direction;

        /// <summary>
        /// Sets <see cref="Theme"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new theme.</param>
        public AspidDividingLinePreset SetTheme(ThemeStyle.Type value)
        {
            Theme = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Status"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new status.</param>
        public AspidDividingLinePreset SetStatus(StatusStyle.Type value)
        {
            Status = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Size"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new size.</param>
        public AspidDividingLinePreset SetSize(AspidDividingLineSizeStyle.Type value)
        {
            Size = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Direction"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new direction.</param>
        public AspidDividingLinePreset SetDirection(AspidDividingLineDirectionStyle.Type value)
        {
            Direction = value;
            return this;
        }
    }
}
