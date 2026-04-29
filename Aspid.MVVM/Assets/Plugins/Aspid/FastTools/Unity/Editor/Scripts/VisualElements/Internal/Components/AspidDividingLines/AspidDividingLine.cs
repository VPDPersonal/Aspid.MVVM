using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// A <see cref="VisualElement"/> that renders a styled dividing line.
    /// Supports theme, status, and size customization, all of which can be driven by USS
    /// custom properties or set explicitly in code.
    /// </summary>
    [UxmlElement(libraryPath = "Aspid/FastTools")]
    public sealed partial class AspidDividingLine : VisualElement
    {
        private const string StyleSheetPath = "UI/Components/Aspid-FastTools-AspidDividingLine";

        private readonly ThemeStyle _theme;
        private readonly StatusStyle _status;
        private readonly AspidDividingLineSizeStyle _size;
        private readonly AspidDividingLineDirectionStyle _direction;

        /// <summary>
        /// Gets or sets the visual theme of the line.
        /// </summary>
        [UxmlAttribute]
        public ThemeStyle.Type Theme
        {
            get => _theme.Value;
            set => _theme.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the status color accent of the line.
        /// </summary>
        [UxmlAttribute]
        public StatusStyle.Type Status
        {
            get => _status.Value;
            set => _status.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the thickness of the line.
        /// </summary>
        [UxmlAttribute]
        public AspidDividingLineSizeStyle.Type Size
        {
            get => _size.Value;
            set => _size.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the orientation of the line.
        /// </summary>
        [UxmlAttribute]
        public AspidDividingLineDirectionStyle.Type Direction
        {
            get => _direction.Value;
            set => _direction.SetValue(value);
        }

        /// <summary>
        /// Creates an <see cref="AspidDividingLine"/> using <see cref="AspidDividingLinePreset.Default"/>.
        /// </summary>
        public AspidDividingLine()
            : this(AspidDividingLinePreset.Default) { }

        /// <summary>
        /// Creates an <see cref="AspidDividingLine"/> with the given preset.
        /// </summary>
        /// <param name="preset">The configuration preset to apply.</param>
        public AspidDividingLine(AspidDividingLinePreset preset)
        {
            this.AddStyleSheetsFromResource(StyleSheetPath);

            _theme = new ThemeStyle(this, preset.Theme);
            _status = new StatusStyle(this, preset.Status);
            _size = new AspidDividingLineSizeStyle(this, preset.Size);
            _direction = new AspidDividingLineDirectionStyle(this, preset.Direction);
        }
    }
}
