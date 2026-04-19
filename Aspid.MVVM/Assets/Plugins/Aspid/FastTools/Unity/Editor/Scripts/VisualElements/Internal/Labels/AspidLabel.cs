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
    public sealed class AspidLabel : VisualElement
    {
        private readonly Label _label;
        private readonly AspidDividingLine _line;

        private StyleOverride<ThemeStyle> _theme;
        private StyleOverride<StatusStyle> _status;
        private StyleOverride<AspidLabelSizeStyle> _size;
        private StyleOverride<StyleEnum<FontStyle>> _fontStyle;

        /// <summary>
        /// Gets or sets the displayed text.
        /// </summary>
        public string Text
        {
            get => _label.text;
            set => _label.text = value;
        }

        /// <summary>
        /// Gets or sets whether the text is selectable by the user.
        /// </summary>
        public bool Selectable
        {
            get => _label.selection.isSelectable;
            set => _label.selection.isSelectable = value;
        }

        /// <summary>
        /// Gets or sets the visual theme of the label.
        /// </summary>
        public ThemeStyle LabelTheme
        {
            get => _theme;
            set => _theme.Set(value);
        }

        /// <summary>
        /// Gets or sets the status color accent of the label.
        /// </summary>
        public StatusStyle LabelStatus
        {
            get => _status;
            set => _status.Set(value);
        }

        /// <summary>
        /// Gets or sets the font size of the label.
        /// </summary>
        public AspidLabelSizeStyle LabelSize
        {
            get => _size;
            set => _size.Set(value);
        }

        /// <summary>
        /// Gets or sets the font style and weight of the label.
        /// </summary>
        public StyleEnum<FontStyle> UnityFontStyleAndWeight
        {
            get => _fontStyle;
            set => _fontStyle.Set(value);
        }

        /// <summary>
        /// Gets or sets the visual theme of the dividing line.
        /// </summary>
        public ThemeStyle LineTheme
        {
            get => _line.Theme;
            set => _line.Theme = value;
        }

        /// <summary>
        /// Gets or sets the status color accent of the dividing line.
        /// </summary>
        public StatusStyle LineStatus
        {
            get => _line.Status;
            set => _line.Status = value;
        }

        /// <summary>
        /// Gets or sets the thickness of the dividing line.
        /// </summary>
        public DividingLineSize LineSize
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
        public AspidLabel(LabelPreset preset)
            : this(string.Empty, preset) { }

        /// <summary>
        /// Creates an <see cref="AspidLabel"/> with the given text and the default preset.
        /// </summary>
        /// <param name="text">The initial label text.</param>
        public AspidLabel(string text)
            : this(text, LabelPreset.Default) { }

        /// <summary>
        /// Creates an <see cref="AspidLabel"/> with the given text and preset.
        /// </summary>
        /// <param name="text">The initial label text.</param>
        /// <param name="preset">The configuration preset to apply.</param>
        public AspidLabel(string text, LabelPreset preset)
        {
            _label = new Label(text);
            _line = new AspidDividingLine(preset.Line);
            Selectable = preset.Selectable;

            this.AddChild(_label)
                .AddChild(_line);

            _theme = new StyleOverride<ThemeStyle>(preset.Theme, (oldValue, newValue) =>
            {
                this.RemoveClass(oldValue.ToUss())
                    .AddClass(newValue.ToUss());
            });

            _status = new StyleOverride<StatusStyle>(preset.Status, (oldValue, newValue) =>
            {
                this.RemoveClass(oldValue.ToUss())
                    .AddClass(newValue.ToUss());
            });

            _size = new StyleOverride<AspidLabelSizeStyle>(preset.Size, (_, newValue) =>
            {
                this.SetFontSize((int)newValue);
            });
            
            _fontStyle = new StyleOverride<StyleEnum<FontStyle>>(preset.UnityFontStyleAndWeight, (_, newValue) =>
            {
                style.SetUnityFontStyleAndWeight(newValue);
            });
            
            RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetByEnum<ThemeStyle>(StyleClasses.Theme.Property, out var themeValue))
                _theme.SetDefault(themeValue);

            if (evt.customStyle.TryGetByEnum<StatusStyle>(StyleClasses.Status.Property, out var statusValue))
                _status.SetDefault(statusValue);

            if (evt.customStyle.TryGetByEnum<AspidLabelSizeStyle>(StyleClasses.Label.SizeProperty, out var sizeValue))
                _size.SetDefault(sizeValue);

            if (evt.customStyle.TryGetByEnum<FontStyle>(StyleClasses.Label.FontStyleProperty, out var fontValue))
                _fontStyle.SetDefault(fontValue);
        }
    }
}
