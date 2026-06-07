using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// A <see cref="VisualElement"/> container with an Aspid background and theme support.
    /// The theme can be driven by USS via the <c>--aspid-fasttools-prop-theme</c> custom property
    /// or set explicitly in code.
    /// </summary>
    [UxmlElement(libraryPath = "Aspid/FastTools")]
    internal sealed partial class AspidBox : VisualElement
    {
        private const string StyleSheetPath = "UI/Components/Aspid-FastTools-AspidBox";

        private readonly ThemeStyle _theme;
        private readonly StatusStyle _status;

        /// <summary>
        /// Gets or sets the visual theme of this box.
        /// </summary>
        [UxmlAttribute]
        public ThemeStyle.Type Theme
        {
            get => _theme.Value;
            set => _theme.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the status color accent of this box.
        /// </summary>
        [UxmlAttribute]
        public StatusStyle.Type Status
        {
            get => _status.Value;
            set => _status.SetValue(value);
        }

        /// <summary>
        /// Creates an <see cref="AspidBox"/> using <see cref="AspidBoxPreset.Default"/>.
        /// </summary>
        public AspidBox()
            : this(AspidBoxPreset.Default) { }

        /// <summary>
        /// Creates an <see cref="AspidBox"/> with the given preset.
        /// </summary>
        /// <param name="preset">The configuration preset to apply.</param>
        public AspidBox(AspidBoxPreset preset)
        {
            this.AddStyleSheetsFromResource(StyleSheetPath)
                .AddClass(AspidStyles.BackgroundStyle)
                .AddClass(AspidStyles.BackgroundRoundedState);

            _theme = new ThemeStyle(this, preset.Theme);
            _status = new StatusStyle(this, preset.Status);
        }
    }
}
