using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// A <see cref="VisualElement"/> that displays a styled help message with an optional title,
    /// a message-type icon, and status-driven color accents.
    /// The icon and accents are USS-driven via the <c>aspid-fasttools-help-box--{info|warning|error}</c>
    /// and <c>aspid-fasttools-status--*</c> classes; <see cref="HelpBoxMessageType.None"/> hides the icon entirely.
    /// </summary>
    [UxmlElement(libraryPath = "Aspid/FastTools")]
    internal sealed partial class AspidHelpBox : VisualElement
    {
        private const string StyleSheetPath = "UI/Components/Aspid-FastTools-AspidHelpBox";

        private const string IconClass = "aspid-fasttools-help-box__icon";
        private const string IconHiddenClass = "aspid-fasttools-help-box__icon--hidden";
        private const string TextContainerClass = "aspid-fasttools-help-box__text-container";

        private const string MessageTypeInfoClass = "aspid-fasttools-help-box--info";
        private const string MessageTypeWarningClass = "aspid-fasttools-help-box--warning";
        private const string MessageTypeErrorClass = "aspid-fasttools-help-box--error";

        private readonly VisualElement _imageElement;
        private readonly AspidLabel _messageElement;
        private readonly VisualElement _textContainer;
        private readonly AspidLabelPreset _titlePreset;

        private readonly StatusStyle _status;
        private AspidLabel _titleElement;
        private HelpBoxMessageType _messageType;

        /// <summary>
        /// Gets or sets the optional title text. Setting an empty or whitespace value removes the title element.
        /// The title element is lazily initialized on the first non-empty assignment and re-attached if it was
        /// previously detached by clearing the title.
        /// </summary>
        [UxmlAttribute]
        public string Title
        {
            get => _titleElement?.Text ?? string.Empty;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _titleElement?.RemoveFromHierarchy();
                    return;
                }

                if (_titleElement is not null && _titleElement.Text == value && _titleElement.parent is not null) return;

                if (_titleElement is null) _titleElement = new AspidLabel(value, _titlePreset);
                else _titleElement.Text = value;

                if (_titleElement.parent is null) _textContainer.InsertChild(index: 0, _titleElement);
            }
        }

        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        [UxmlAttribute]
        public string Message
        {
            get => _messageElement.Text;
            set => _messageElement.Text = value;
        }

        /// <summary>
        /// Gets or sets the status color accent of the help box.
        /// </summary>
        [UxmlAttribute]
        public StatusStyle.Type Status
        {
            get => _status.Value;
            set => _status.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the message type, which controls the icon displayed alongside the message.
        /// Setting <see cref="HelpBoxMessageType.None"/> hides the icon.
        /// </summary>
        [UxmlAttribute]
        public HelpBoxMessageType MessageType
        {
            get => _messageType;
            set
            {
                if (_messageType == value) return;

                this.RemoveClass(GetMessageTypeClass(_messageType));
                _messageType = value;
                this.AddClass(GetMessageTypeClass(value));

                if (value == HelpBoxMessageType.None) _imageElement.AddClass(IconHiddenClass);
                else _imageElement.RemoveClass(IconHiddenClass);
            }
        }

        /// <summary>
        /// Creates an <see cref="AspidHelpBox"/> using <see cref="AspidHelpBoxPreset.Default"/>.
        /// </summary>
        public AspidHelpBox()
            : this(AspidHelpBoxPreset.Default) { }

        /// <summary>
        /// Creates an <see cref="AspidHelpBox"/> with the given preset and empty title/message.
        /// </summary>
        /// <param name="preset">The configuration preset.</param>
        public AspidHelpBox(AspidHelpBoxPreset preset)
            : this(string.Empty, string.Empty, preset) { }

        /// <summary>
        /// Creates an <see cref="AspidHelpBox"/> with a message and the given preset.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="preset">The configuration preset.</param>
        public AspidHelpBox(string message, AspidHelpBoxPreset preset)
            : this(string.Empty, message, preset) { }

        /// <summary>
        /// Creates an <see cref="AspidHelpBox"/> with a title, message, and the given preset.
        /// </summary>
        /// <param name="title">The optional title text.</param>
        /// <param name="message">The message text.</param>
        /// <param name="preset">The configuration preset.</param>
        public AspidHelpBox(string title, string message, AspidHelpBoxPreset preset)
        {
            this.AddStyleSheetsFromResource(StyleSheetPath);

            _titlePreset = preset.TitlePreset;
            _textContainer = new VisualElement().AddClass(TextContainerClass);
            _messageElement = new AspidLabel(message, preset.MessagePreset);
            _textContainer.AddChild(_messageElement);

            _imageElement = new VisualElement()
                .AddClass(IconClass)
                .AddClass(IconHiddenClass);

            this.AddChild(_imageElement)
                .AddChild(_textContainer);

            _status = new StatusStyle(this, preset.Status);
            MessageType = preset.MessageType;
            Title = title;
        }

        private static string GetMessageTypeClass(HelpBoxMessageType type) => type switch
        {
            HelpBoxMessageType.Info => MessageTypeInfoClass,
            HelpBoxMessageType.Warning => MessageTypeWarningClass,
            HelpBoxMessageType.Error => MessageTypeErrorClass,
            _ => string.Empty,
        };
    }
}
