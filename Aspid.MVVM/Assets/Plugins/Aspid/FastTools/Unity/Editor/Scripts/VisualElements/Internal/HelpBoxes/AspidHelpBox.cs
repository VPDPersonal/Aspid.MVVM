using UnityEditor;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// A <see cref="VisualElement"/> that displays a styled help message with an optional title,
    /// a message-type icon, and status-driven color accents.
    /// The title element is lazily created only when a non-empty title is assigned.
    /// </summary>
    public class AspidHelpBox : VisualElement
    {
        private AspidLabel _titleElement;
        private VisualElement _imageElement;
        private readonly AspidLabel _messageElement;
        private readonly VisualElement _textContainer;

        private HelpBoxMessageType _messageType;
        private StyleOverride<StatusStyle> _status;
        private readonly LabelPreset _titlePreset;

        /// <summary>
        /// Gets or sets the optional title text. Setting an empty or whitespace value removes the title element.
        /// The title element is lazily initialised on the first non-empty assignment.
        /// </summary>
        public string Title
        {
            get => _titleElement?.Text ?? string.Empty;
            set
            {
                if (_titleElement?.Text == value) return;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (_titleElement is null)
                    {
                        _titleElement = new AspidLabel(value, _titlePreset);
                        _textContainer.InsertChild(index: 0, _titleElement);
                    }

                    _titleElement.Text = value;
                }
                else
                {
                    _titleElement?.RemoveFromHierarchy();
                }
            }
        }

        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        public string Message
        {
            get => _messageElement.Text;
            set => _messageElement.Text = value;
        }

        /// <summary>
        /// Gets or sets the status color accent of the help box.
        /// </summary>
        public StatusStyle Status
        {
            get => _status;
            set => _status.Set(value);
        }

        /// <summary>
        /// Gets or sets the message type, which controls the icon displayed alongside the message.
        /// Setting <see cref="HelpBoxMessageType.None"/> hides the icon.
        /// </summary>
        public HelpBoxMessageType MessageType
        {
            get => _messageType;
            set
            {
                if (_messageType == value) return;

                _messageType = value;
                UpdateIcon(_messageType);
            }
        }

        /// <summary>
        /// Creates an <see cref="AspidHelpBox"/> with a message and a default preset derived from the message type.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="messageType">The message type used to select the icon and derive the default status.</param>
        public AspidHelpBox(string message, HelpBoxMessageType messageType)
            : this(null, message, AspidHelpBoxPreset.GetDefault(messageType)) { }

        /// <summary>
        /// Creates an <see cref="AspidHelpBox"/> with a message and an explicit preset.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="preset">The configuration preset.</param>
        public AspidHelpBox(string message, AspidHelpBoxPreset preset)
            : this(null, message, preset) { }

        /// <summary>
        /// Creates an <see cref="AspidHelpBox"/> with a title, message, and a default preset derived from the message type.
        /// </summary>
        /// <param name="title">The optional title text.</param>
        /// <param name="message">The message text.</param>
        /// <param name="messageType">The message type used to select the icon and derive the default status.</param>
        public AspidHelpBox(string title, string message, HelpBoxMessageType messageType)
            : this(title, message, AspidHelpBoxPreset.GetDefault(messageType)) { }

        /// <summary>
        /// Creates an <see cref="AspidHelpBox"/> with a title, message, and an explicit preset.
        /// </summary>
        /// <param name="title">The optional title text.</param>
        /// <param name="message">The message text.</param>
        /// <param name="preset">The configuration preset.</param>
        public AspidHelpBox(string title, string message, AspidHelpBoxPreset preset)
        {
            _titlePreset = preset.TitlePreset;
            _textContainer = new VisualElement().SetName("text-container");

            Title = title;
            _messageElement = new AspidLabel(message, preset.MessagePreset);
            _textContainer.AddChild(_messageElement);
            
            _messageType = preset.MessageType;
            UpdateIcon(_messageType);
            
            _status = new StyleOverride<StatusStyle>(preset.Status, (oldValue, newValue) =>
            {
                this.RemoveClass(oldValue.ToUss())
                    .AddClass(newValue.ToUss());
            });
            
            this.AddChild(_textContainer);
        }

        private void UpdateIcon(HelpBoxMessageType messageType)
        {
            var icon = messageType switch
            {
                HelpBoxMessageType.Warning => "d_console.warnicon@2x",
                HelpBoxMessageType.Error => "d_console.erroricon@2x",
                HelpBoxMessageType.Info => "d_console.infoicon@2x",
                _ => null,
            };
            
            if (icon is null)
            {
                _imageElement?.RemoveFromHierarchy();
            }
            else
            {
                if (_imageElement is null)
                {
                    _imageElement = new VisualElement().SetName("icon");
                    this.InsertChild(index: 0, _imageElement);
                }

                _imageElement.SetBackgroundImage(EditorGUIUtility.FindTexture(icon));
            }
        }
    }
}
