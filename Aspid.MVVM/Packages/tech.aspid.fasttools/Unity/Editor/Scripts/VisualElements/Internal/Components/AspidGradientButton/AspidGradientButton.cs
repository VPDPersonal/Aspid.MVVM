using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// A button-like <see cref="VisualElement"/> with a horizontal gradient background and an
    /// animated accent overlay that fades in on hover. The gradient and accent colors are sourced
    /// from USS custom properties or set via UXML attributes / fluent extensions.
    /// </summary>
    [UxmlElement(libraryPath = "Aspid/FastTools")]
    internal sealed partial class AspidGradientButton : VisualElement
    {
        private const string StyleSheetPath = "UI/Components/Aspid-FastTools-AspidGradientButton";

        private const string BlockClass = "aspid-fasttools-gradient-button";
        private const string LabelClass = "aspid-fasttools-gradient-button__label";
        private const string TrailingLabelClass = "aspid-fasttools-gradient-button__trailing-label";

        private readonly Label _label;
        private readonly Label _trailingLabel;
        private readonly AspidHoverGradientOverlay _overlay;
        private readonly AspidGradientButtonColorsStyle _colors;

        private Texture2D _gradientTexture;

        /// <summary>
        /// Gets or sets the button label text.
        /// </summary>
        [UxmlAttribute]
        public string Text
        {
            get => _label.text;
            set => _label.text = value;
        }

        /// <summary>
        /// Gets or sets the trailing label text shown at the right edge. Empty hides the label.
        /// </summary>
        [UxmlAttribute]
        public string TrailingText
        {
            get => _trailingLabel.text;
            set
            {
                _trailingLabel.text = value ?? string.Empty;
                _trailingLabel.style.display = string.IsNullOrEmpty(value)
                    ? DisplayStyle.None
                    : DisplayStyle.Flex;
            }
        }

        /// <summary>
        /// Gets or sets the gradient background color.
        /// </summary>
        [UxmlAttribute]
        public Color Gradient
        {
            get => _colors.Gradient;
            set => _colors.SetGradient(value);
        }

        /// <summary>
        /// Gets or sets the accent (hover) color.
        /// </summary>
        [UxmlAttribute]
        public Color Accent
        {
            get => _colors.Accent;
            set => _colors.SetAccent(value);
        }

        /// <summary>
        /// Creates an <see cref="AspidGradientButton"/> using <see cref="AspidGradientButtonPreset.Default"/>.
        /// </summary>
        public AspidGradientButton()
            : this(AspidGradientButtonPreset.Default) { }

        /// <summary>
        /// Creates an <see cref="AspidGradientButton"/> with the given label and click handler.
        /// </summary>
        /// <param name="text">The button label.</param>
        /// <param name="onClick">Optional handler invoked when the button is clicked.</param>
        public AspidGradientButton(string text, Action<EventBase> onClick = null)
            : this(AspidGradientButtonPreset.Default.SetText(text).SetOnClick(onClick)) { }

        /// <summary>
        /// Creates an <see cref="AspidGradientButton"/> with the given label, trailing text and click handler.
        /// </summary>
        /// <param name="text">The button label.</param>
        /// <param name="trailingText">The trailing label text shown at the right edge. Pass <see langword="null"/> or empty to hide it.</param>
        /// <param name="onClick">Optional handler invoked when the button is clicked.</param>
        public AspidGradientButton(string text, string trailingText, Action<EventBase> onClick = null)
            : this(AspidGradientButtonPreset.Default
                .SetText(text)
                .SetTrailingText(trailingText)
                .SetOnClick(onClick)) { }

        /// <summary>
        /// Creates an <see cref="AspidGradientButton"/> with the given preset.
        /// </summary>
        /// <param name="preset">The configuration preset to apply.</param>
        public AspidGradientButton(AspidGradientButtonPreset preset)
        {
            this.AddClass(BlockClass)
                .AddStyleSheetsFromResource(StyleSheetPath);
            focusable = true;

            // Drawn on top of the static gradient pill but BEFORE the label, so the text
            // always reads cleanly above the hover effect.
            _overlay = new AspidHoverGradientOverlay();
            Add(_overlay);

            _label = new Label(preset.Text)
                .SetFlexGrow(1f)
                .SetPickingMode(PickingMode.Ignore);
            _label.AddClass(LabelClass);
            Add(_label);

            _trailingLabel = new Label(preset.TrailingText ?? string.Empty)
                .SetPickingMode(PickingMode.Ignore);
            _trailingLabel.AddClass(LabelClass);
            _trailingLabel.AddClass(TrailingLabelClass);
            if (string.IsNullOrEmpty(preset.TrailingText))
                _trailingLabel.style.display = DisplayStyle.None;
            Add(_trailingLabel);

            if (preset.OnClick != null)
                this.AddManipulator(new Clickable(preset.OnClick));

            _colors = new AspidGradientButtonColorsStyle(
                this,
                preset.Gradient,
                preset.Accent,
                RebuildGradient,
                accent => _overlay.Color = accent);

            RegisterCallback<MouseEnterEvent>(OnMouseEnter);
            RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
        }

        private void OnMouseEnter(MouseEnterEvent _)
        {
            _overlay.SetTarget(1f);
            _label.style.color = _colors.Accent;
            _trailingLabel.style.color = _colors.Accent;
        }

        private void OnMouseLeave(MouseLeaveEvent _)
        {
            _overlay.SetTarget(0f);
            _label.style.color = StyleKeyword.Null;
            _trailingLabel.style.color = StyleKeyword.Null;
        }

        private void OnAttachToPanel(AttachToPanelEvent _)
        {
            if (_gradientTexture == null)
                RebuildGradient(_colors.Gradient);
        }

        private void OnDetachFromPanel(DetachFromPanelEvent _) => DisposeTexture();

        private void RebuildGradient(Color color)
        {
            DisposeTexture();
            _gradientTexture = CreateHorizontalFadeTexture(color);
            style.backgroundImage = new StyleBackground(_gradientTexture);
        }

        private void DisposeTexture()
        {
            if (_gradientTexture == null) return;

            UnityEngine.Object.DestroyImmediate(_gradientTexture);
            _gradientTexture = null;
        }

        private static Texture2D CreateHorizontalFadeTexture(Color baseColor)
        {
            const int width = 256;
            var texture = new Texture2D(width, 1, TextureFormat.RGBA32, mipChain: false)
            {
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Bilinear,
                hideFlags = HideFlags.HideAndDontSave,
            };

            var pixels = new Color[width];
            for (var i = 0; i < width; i++)
            {
                var t = (float)i / (width - 1);
                pixels[i] = new Color(baseColor.r, baseColor.g, baseColor.b, baseColor.a * (1f - t));
            }

            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }
    }
}
