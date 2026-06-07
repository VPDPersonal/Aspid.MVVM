using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Configuration preset for an <see cref="AspidHelpBox"/>.
    /// Use the fluent builder methods to create a customized preset.
    /// </summary>
    internal struct AspidHelpBoxPreset
    {
        /// <summary>
        /// The default preset: no status, no message type, with title and message label presets
        /// pre-configured for help-box typography.
        /// </summary>
        public static AspidHelpBoxPreset Default => new AspidHelpBoxPreset()
            .SetTitle(new AspidLabelPreset()
                .SetSelectable()
                .SetLineTheme(ThemeStyle.Type.Light)
                .SetLabelTheme(ThemeStyle.Type.Lightness)
                .SetLineSize(AspidDividingLineSizeStyle.Type.Thin)
                .SetLabelSize(AspidLabelSizeStyle.Type.H5)
                .SetFontStyle(FontStyle.Bold))
            .SetMessage(new AspidLabelPreset()
                .SetSelectable()
                .SetLabelTheme(ThemeStyle.Type.Light)
                .SetLineSize(AspidDividingLineSizeStyle.Type.None)
                .SetLabelSize(AspidLabelSizeStyle.Type.H7));

        /// <summary>
        /// The status color accent applied to the help box and its labels.
        /// </summary>
        public StatusStyle.Type Status;

        /// <summary>
        /// The <see cref="AspidLabelPreset"/> used for the optional title label.
        /// </summary>
        public AspidLabelPreset TitlePreset;

        /// <summary>
        /// The <see cref="AspidLabelPreset"/> used for the message label.
        /// </summary>
        public AspidLabelPreset MessagePreset;

        /// <summary>
        /// The message type that determines the icon displayed in the help box.
        /// </summary>
        public HelpBoxMessageType MessageType;

        /// <summary>
        /// Replaces the entire <see cref="TitlePreset"/> (the label preset used for the title,
        /// not the title text itself) and returns the modified preset.
        /// </summary>
        /// <param name="value">The new title label preset.</param>
        /// <returns>The modified preset, for chaining.</returns>
        public AspidHelpBoxPreset SetTitle(AspidLabelPreset value)
        {
            TitlePreset = value;
            return this;
        }

        /// <summary>
        /// Replaces the entire <see cref="MessagePreset"/> (the label preset used for the message,
        /// not the message text itself) and returns the modified preset.
        /// </summary>
        /// <param name="value">The new message label preset.</param>
        /// <returns>The modified preset, for chaining.</returns>
        public AspidHelpBoxPreset SetMessage(AspidLabelPreset value)
        {
            MessagePreset = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Status"/> on this preset, <see cref="TitlePreset"/>, and <see cref="MessagePreset"/>,
        /// then returns the modified preset.
        /// </summary>
        /// <param name="value">The status accent to apply to the help box and its labels.</param>
        /// <returns>The modified preset, for chaining.</returns>
        public AspidHelpBoxPreset SetStatus(StatusStyle.Type value)
        {
            Status = value;
            TitlePreset = TitlePreset.SetStatus(value);
            MessagePreset = MessagePreset.SetStatus(value);
            return this;
        }

        /// <summary>
        /// Sets <see cref="MessageType"/> and returns the modified preset.
        /// If <see cref="Status"/> has not been set explicitly, it is automatically derived
        /// from the message type (Info/Warning/Error → matching status accent).
        /// </summary>
        /// <param name="value">The message type that determines the help-box icon.</param>
        /// <returns>The modified preset, for chaining.</returns>
        public AspidHelpBoxPreset SetMessageType(HelpBoxMessageType value)
        {
            MessageType = value;
            if (Status == StatusStyle.Type.None) SetStatus(MapToStatus(value));
            return this;
        }

        private static StatusStyle.Type MapToStatus(HelpBoxMessageType type) => type switch
        {
            HelpBoxMessageType.Info => StatusStyle.Type.Info,
            HelpBoxMessageType.Warning => StatusStyle.Type.Warning,
            HelpBoxMessageType.Error => StatusStyle.Type.Error,
            _ => StatusStyle.Type.None,
        };
    }
}
