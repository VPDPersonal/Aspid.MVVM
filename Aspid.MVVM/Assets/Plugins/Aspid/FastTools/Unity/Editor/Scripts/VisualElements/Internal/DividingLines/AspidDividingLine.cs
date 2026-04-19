using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// A <see cref="VisualElement"/> that renders a styled dividing line.
    /// Supports theme, status, and size customisation, all of which can be driven by USS
    /// custom properties or set explicitly in code.
    /// </summary>
    public sealed class AspidDividingLine : VisualElement
    {
        private StyleOverride<ThemeStyle> _theme;
        private StyleOverride<StatusStyle> _status;
        private StyleOverride<DividingLineSize> _size;

        /// <summary>
        /// Gets or sets the visual theme of the line.
        /// </summary>
        public ThemeStyle Theme
        {
            get => _theme;
            set => _theme.Set(value);
        }

        /// <summary>
        /// Gets or sets the status color accent of the line.
        /// </summary>
        public StatusStyle Status
        {
            get => _status;
            set => _status.Set(value);
        }

        /// <summary>
        /// Gets or sets the thickness of the line.
        /// </summary>
        public DividingLineSize Size
        {
            get => _size;
            set => _size.Set(value);
        }

        /// <summary>
        /// Creates an <see cref="AspidDividingLine"/> using <see cref="DividingLinePreset.Default"/>.
        /// </summary>
        public AspidDividingLine()
            : this(DividingLinePreset.Default) { }

        /// <summary>
        /// Creates an <see cref="AspidDividingLine"/> with the given preset.
        /// </summary>
        /// <param name="preset">The configuration preset to apply.</param>
        public AspidDividingLine(DividingLinePreset preset)
        {
            this.AddClass(preset.Direction.ToUss());

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

            _size = new StyleOverride<DividingLineSize>(preset.Size, (oldValue, newValue) =>
            {
                this.RemoveClass(oldValue.ToUss())
                    .AddClass(newValue.ToUss());
            });
            
            RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetByEnum<ThemeStyle>(StyleClasses.Theme.Property, out var themeValue))
                _theme.SetDefault(themeValue);

            if (evt.customStyle.TryGetByEnum<StatusStyle>(StyleClasses.Status.Property, out var statusValue))
                _status.SetDefault(statusValue);

            if (evt.customStyle.TryGetByEnum<DividingLineSize>(StyleClasses.DividingLine.SizeProperty, out var sizeValue))
                _size.SetDefault(sizeValue);
        }
    }
} 
