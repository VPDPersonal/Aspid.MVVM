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
        private bool _highlighted;
        private bool _hovered;

        /// <summary>
        /// Shows the hover visual (accent overlay + accent-tinted labels) programmatically, for host-driven states
        /// like a keyboard focus ring.
        /// </summary>
        /// <remarks>Mouse hover and this flag compose: the visual stays on while either is active.</remarks>
        internal bool Highlighted
        {
            get => _highlighted;
            set
            {
                _highlighted = value;
                ApplyHoverVisual(_highlighted || _hovered);
            }
        }

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

        /// <summary>
        /// Inserts <paramref name="content"/> ahead of the text label in the button's child order, so it leads the
        /// label — to its left in the default row layout, or above it when the button is switched to a column
        /// flex-direction. Lets the button carry a richer body (e.g. a header line) alongside its
        /// <see cref="Text"/> action label.
        /// </summary>
        /// <typeparam name="T">The content element type.</typeparam>
        /// <param name="content">The element to insert ahead of the label.</param>
        /// <returns>The same <paramref name="content"/>, for fluent chaining.</returns>
        public T AddLeadingContent<T>(T content) where T : VisualElement
        {
            Insert(IndexOf(_label), content);
            return content;
        }

        /// <summary>
        /// Inserts <paramref name="content"/> just after the text label in the button's child order, so it trails the
        /// label — to its right in the default row layout, or below it when the button is switched to a column
        /// flex-direction. The mirror of <see cref="AddLeadingContent{T}"/>.
        /// </summary>
        /// <typeparam name="T">The content element type.</typeparam>
        /// <param name="content">The element to insert after the label.</param>
        /// <returns>The same <paramref name="content"/>, for fluent chaining.</returns>
        public T AddTrailingContent<T>(T content) where T : VisualElement
        {
            Insert(IndexOf(_label) + 1, content);
            return content;
        }

        /// <summary>
        /// Hands the row's free space to a flex-grow <see cref="AddLeadingContent"/> element instead of the text label:
        /// the <see cref="Text"/> label stops growing and shrinks to its own text, so the leading content fills the row
        /// and the label merely pins after it. Without this the label claims the free space and the leading content
        /// sits at its natural width. Affects only this instance.
        /// </summary>
        public void FillWithLeadingContent() => _label.style.flexGrow = 0f;

        /// <summary>
        /// Hands the row's free space to a flex-grow <see cref="AddTrailingContent"/> element instead of the text label:
        /// the <see cref="Text"/> label stops growing and shrinks to its own text, so the trailing content fills the row
        /// and the label merely pins before it. The mirror of <see cref="FillWithLeadingContent"/>.
        /// </summary>
        public void FillWithTrailingContent() => _label.style.flexGrow = 0f;

        private void OnMouseEnter(MouseEnterEvent _)
        {
            _hovered = true;
            ApplyHoverVisual(true);
        }

        private void OnMouseLeave(MouseLeaveEvent _)
        {
            _hovered = false;
            ApplyHoverVisual(_highlighted);
        }

        private void ApplyHoverVisual(bool on)
        {
            _overlay.SetTarget(on ? 1f : 0f);
            var labelColor = on ? new StyleColor(_colors.Accent) : new StyleColor(StyleKeyword.Null);
            _label.style.color = labelColor;
            _trailingLabel.style.color = labelColor;
        }

        private void OnAttachToPanel(AttachToPanelEvent _)
        {
            if (_gradientTexture == null)
                RebuildGradient(_colors.Gradient);
        }

        private void OnDetachFromPanel(DetachFromPanelEvent _) => DisposeTexture();

        private void RebuildGradient(Color color)
        {
            // Textures live only while the element is attached: OnAttachToPanel builds the first
            // one and OnDetachFromPanel disposes it, so a detached button never holds a texture.
            if (panel == null) return;

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
