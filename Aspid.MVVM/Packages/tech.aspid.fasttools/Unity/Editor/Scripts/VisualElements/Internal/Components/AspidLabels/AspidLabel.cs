using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// A <see cref="VisualElement"/> label with Aspid theming, status accents, font size control,
    /// and an optional <see cref="AspidDividingLine"/> beneath the text.
    /// Theme and status can be driven by USS custom properties or set explicitly in code.
    /// </summary>
    [UxmlElement(libraryPath = "Aspid/FastTools")]
    internal sealed partial class AspidLabel : VisualElement
    {
        private const string StyleSheetPath = "UI/Components/Aspid-FastTools-AspidLabel";
        
        private readonly Label _label;
        private readonly AspidDividingLine _line;

        private readonly ThemeStyle _theme;
        private readonly StatusStyle _status;
        private readonly AspidLabelSizeStyle _size;
        private readonly AspidLabelFontStyle _fontStyle;

        /// <summary>
        /// Gets or sets the displayed text.
        /// </summary>
        [UxmlAttribute]
        public string Text
        {
            get => _label.text;
            set => _label.text = value;
        }

        /// <summary>
        /// Gets or sets whether the text is selectable by the user.
        /// </summary>
        [UxmlAttribute]
        public bool Selectable
        {
            get => _label.selection.isSelectable;
            set => _label.SetIsSelectable(value);
        }

        /// <summary>
        /// Gets or sets the visual theme of the label.
        /// </summary>
        [UxmlAttribute]
        public ThemeStyle.Type LabelTheme
        {
            get => _theme.Value;
            set => _theme.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the status color accent of the label.
        /// </summary>
        [UxmlAttribute]
        public StatusStyle.Type LabelStatus
        {
            get => _status.Value;
            set => _status.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the font size of the label.
        /// </summary>
        [UxmlAttribute]
        public AspidLabelSizeStyle.Type LabelSize
        {
            get => _size.Value;
            set => _size.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the font style of the label.
        /// </summary>
        [UxmlAttribute]
        public FontStyle LabelFontStyle
        {
            get => _fontStyle.Value.value;
            set => _fontStyle.SetValue(new StyleEnum<FontStyle>(value));
        }

        /// <summary>
        /// Gets or sets the visual theme of the dividing line.
        /// </summary>
        [UxmlAttribute]
        public ThemeStyle.Type LineTheme
        {
            get => _line.Theme;
            set => _line.Theme = value;
        }

        /// <summary>
        /// Gets or sets the status color accent of the dividing line.
        /// </summary>
        [UxmlAttribute]
        public StatusStyle.Type LineStatus
        {
            get => _line.Status;
            set => _line.Status = value;
        }

        /// <summary>
        /// Gets or sets the thickness of the dividing line.
        /// </summary>
        [UxmlAttribute]
        public AspidDividingLineSizeStyle.Type LineSize
        {
            get => _line.Size;
            set => _line.Size = value;
        }

        /// <summary>
        /// Creates an <see cref="AspidLabel"/> with empty text and the default preset.
        /// </summary>
        public AspidLabel()
            : this(string.Empty) { }

        /// <summary>
        /// Creates an <see cref="AspidLabel"/> with empty text and the given preset.
        /// </summary>
        /// <param name="preset">The configuration preset to apply.</param>
        public AspidLabel(AspidLabelPreset preset)
            : this(string.Empty, preset) { }

        /// <summary>
        /// Creates an <see cref="AspidLabel"/> with the given text and the default preset.
        /// </summary>
        /// <param name="text">The initial label text.</param>
        public AspidLabel(string text)
            : this(text, AspidLabelPreset.Default) { }

        /// <summary>
        /// Creates an <see cref="AspidLabel"/> with the given text and preset.
        /// </summary>
        /// <param name="text">The initial label text.</param>
        /// <param name="preset">The configuration preset to apply.</param>
        public AspidLabel(string text, AspidLabelPreset preset)
        {
            _label = new Label(text);
            _line = new AspidDividingLine(preset.Line);
            
            this.AddStyleSheetsFromResource(StyleSheetPath)
                .AddChild(_label)
                .AddChild(_line);

            Selectable = preset.Selectable;
            _theme = new ThemeStyle(this, preset.Theme);
            _status = new StatusStyle(this, preset.Status);
            _size = new AspidLabelSizeStyle(this, preset.Size);
            _fontStyle = new AspidLabelFontStyle(this, preset.FontStyle);
        }
    }
}
