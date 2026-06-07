// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Configuration preset for an <see cref="AspidBox"/>.
    /// Use the fluent builder methods to create a customized preset.
    /// </summary>
    internal struct AspidBoxPreset
    {
        /// <summary>
        /// The default preset: light theme, no status.
        /// </summary>
        public static AspidBoxPreset Default => new AspidBoxPreset()
            .SetTheme(ThemeStyle.Type.Light)
            .SetStatus(StatusStyle.Type.None);

        /// <summary>
        /// The visual theme of the box.
        /// </summary>
        public ThemeStyle.Type Theme;

        /// <summary>
        /// The status color accent of the box.
        /// </summary>
        public StatusStyle.Type Status;

        /// <summary>
        /// Sets <see cref="Theme"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new theme.</param>
        public AspidBoxPreset SetTheme(ThemeStyle.Type value)
        {
            Theme = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Status"/> and returns the modified preset.
        /// </summary>
        /// <param name="value">The new status.</param>
        public AspidBoxPreset SetStatus(StatusStyle.Type value)
        {
            Status = value;
            return this;
        }
    }
}
