using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Configuration preset for an <see cref="AspidHelpBox"/>.
    /// Use <see cref="GetDefault(HelpBoxMessageType)"/> to obtain a standard preset,
    /// then customise it with the fluent builder methods.
    /// </summary>
    public struct AspidHelpBoxPreset
    {
        /// <summary>
        /// The status color accent applied to the help box and its labels.
        /// </summary>
        public StatusStyle Status;

        /// <summary>
        /// The <see cref="LabelPreset"/> used for the optional title label.
        /// </summary>
        public LabelPreset TitlePreset;

        /// <summary>
        /// The <see cref="LabelPreset"/> used for the message label.
        /// </summary>
        public LabelPreset MessagePreset;

        /// <summary>
        /// The message type that determines the icon displayed in the help box.
        /// </summary>
        public HelpBoxMessageType MessageType;

        /// <summary>
        /// Returns a default preset for the given message type.
        /// The <see cref="Status"/> is inferred automatically from the message type.
        /// </summary>
        /// <param name="messageType">The help box message type.</param>
        public static AspidHelpBoxPreset GetDefault(HelpBoxMessageType messageType = HelpBoxMessageType.None)
        {
            return GetDefault(messageType, GetDefaultStatus());

            StatusStyle GetDefaultStatus() => messageType switch
            {
                HelpBoxMessageType.Info => StatusStyle.Info,
                HelpBoxMessageType.Warning => StatusStyle.Warning,
                HelpBoxMessageType.Error => StatusStyle.Error,
                _ => StatusStyle.None,
            };
        }

        /// <summary>
        /// Returns a default preset for the given message type and explicit status.
        /// </summary>
        /// <param name="messageType">The help box message type.</param>
        /// <param name="status">The status color accent to apply.</param>
        public static AspidHelpBoxPreset GetDefault(HelpBoxMessageType messageType, StatusStyle status)
        {
            var preset = new AspidHelpBoxPreset();

            return preset
                .SetStatus(status)
                .SetMessageType(messageType)
                .SetTitle(preset.TitlePreset
                    .SetSelectable()
                    .SetStatus(status)
                    .SetLineTheme(ThemeStyle.Dark)
                    .SetLabelTheme(ThemeStyle.Lightness)
                    .SetLineSize(DividingLineSize.Thin)
                    .SetLabelSize(AspidLabelSizeStyle.H2)
                    .SetUnityFontStyleAndWeight(FontStyle.Bold)
                )
                .SetMessage(preset.MessagePreset
                    .SetSelectable()
                    .SetStatus(status)
                    .SetLabelTheme(ThemeStyle.Light)
                    .SetLineSize(DividingLineSize.None)
                    .SetLabelSize(AspidLabelSizeStyle.H4)
                );
        }

        /// <summary>
        /// Sets <see cref="TitlePreset"/> and returns the modified preset.
        /// </summary>
        public AspidHelpBoxPreset SetTitle(LabelPreset value)
        {
            TitlePreset = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="MessagePreset"/> and returns the modified preset.
        /// </summary>
        public AspidHelpBoxPreset SetMessage(LabelPreset value)
        {
            MessagePreset = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Status"/> on this preset, <see cref="TitlePreset"/>, and <see cref="MessagePreset"/>,
        /// then returns the modified preset.
        /// </summary>
        public AspidHelpBoxPreset SetStatus(StatusStyle status)
        {
            Status = status;
            TitlePreset.SetStatus(status);
            MessagePreset.SetStatus(status);

            return this;
        }

        /// <summary>
        /// Sets <see cref="MessageType"/> and returns the modified preset.
        /// </summary>
        public AspidHelpBoxPreset SetMessageType(HelpBoxMessageType type)
        {
            MessageType = type;
            return this;
        }
    }
}
