using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// A non-interactive overlay <see cref="VisualElement"/> that paints a horizontal accent
    /// gradient (a row of vertical strips with a quadratic alpha falloff) and smoothly
    /// fades it in or out toward a target progress between 0 and 1.
    /// </summary>
    [UxmlElement(libraryPath = "Aspid/FastTools")]
    internal sealed partial class AspidHoverGradientOverlay : VisualElement
    {
        private const long TickMs = 16;
        private const float DrawThreshold = 0.01f;
        private const float ProgressEpsilon = 0.001f;

        private const int DefaultSteps = 75;
        private const float DefaultLerpRate = 0.12f;
        private const float DefaultAlphaScale = 0.35f;

        private const string StyleSheetPath = "UI/Components/Aspid-FastTools-AspidHoverGradientOverlay";

        private readonly AspidHoverGradientOverlayColorStyle _color;
        private readonly AspidHoverGradientOverlayMetricsStyle _metrics;

        private float _progress;
        private float _targetProgress;
        private IVisualElementScheduledItem _animation;

        /// <summary>
        /// Gets or sets the base color of the overlay. The painted alpha is multiplied by the
        /// current progress and the per-strip falloff.
        /// </summary>
        [UxmlAttribute]
        public Color Color
        {
            get => _color.Value;
            set => _color.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the number of vertical strips painted across the overlay width.
        /// </summary>
        [UxmlAttribute]
        public int Steps
        {
            get => _metrics.Steps;
            set => _metrics.SetSteps(value);
        }

        /// <summary>
        /// Gets or sets the per-tick lerp rate driving the fade-in/fade-out animation.
        /// </summary>
        [UxmlAttribute]
        public float LerpRate
        {
            get => _metrics.LerpRate;
            set => _metrics.SetLerpRate(value);
        }

        /// <summary>
        /// Gets or sets the peak alpha scale at progress = 1 (multiplied by the per-strip falloff).
        /// </summary>
        [UxmlAttribute]
        public float AlphaScale
        {
            get => _metrics.AlphaScale;
            set => _metrics.SetAlphaScale(value);
        }

        /// <summary>
        /// Creates an <see cref="AspidHoverGradientOverlay"/> in the hidden state and starts the
        /// fade animation loop.
        /// </summary>
        public AspidHoverGradientOverlay()
        {
            this.AddStyleSheetsFromResource(StyleSheetPath);
            pickingMode = PickingMode.Ignore;

            _color = new AspidHoverGradientOverlayColorStyle(this, default, MarkDirtyRepaint);
            _metrics = new AspidHoverGradientOverlayMetricsStyle(this, DefaultSteps, DefaultLerpRate, DefaultAlphaScale, MarkDirtyRepaint);

            generateVisualContent += DrawOverlay;
            _animation = schedule.Execute(Tick).Every(TickMs);

            RegisterCallback<AttachToPanelEvent>(_ => _animation.Resume());
            RegisterCallback<DetachFromPanelEvent>(_ => _animation.Pause());
        }

        /// <summary>
        /// Sets the target progress that the overlay smoothly lerps toward. Values are clamped to <c>[0, 1]</c>.
        /// </summary>
        /// <param name="target">The target progress, where 0 is fully hidden and 1 is fully visible.</param>
        public void SetTarget(float target) => _targetProgress = Mathf.Clamp01(target);

        private void Tick()
        {
            var previous = _progress;
            _progress = Mathf.Lerp(_progress, _targetProgress, _metrics.LerpRate);

            if (Mathf.Abs(_progress - previous) > ProgressEpsilon)
                MarkDirtyRepaint();
        }

        private void DrawOverlay(MeshGenerationContext ctx)
        {
            if (_progress <= DrawThreshold) return;

            var painter = ctx.painter2D;
            var rect = contentRect;
            var steps = Mathf.Max(1, _metrics.Steps);
            var stripWidth = rect.width / steps;
            var alphaScale = _metrics.AlphaScale;
            var baseColor = _color.Value;

            for (var i = 0; i < steps; i++)
            {
                var t = (float)i / Mathf.Max(1, steps - 1);
                var alpha = (1f - t) * (1f - t) * _progress * alphaScale;

                painter.fillColor = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);

                painter.BeginPath();
                painter.MoveTo(new Vector2(i * stripWidth, 0f));
                painter.LineTo(new Vector2((i + 1) * stripWidth, 0f));
                painter.LineTo(new Vector2((i + 1) * stripWidth, rect.height));
                painter.LineTo(new Vector2(i * stripWidth, rect.height));
                painter.ClosePath();
                painter.Fill();
            }
        }
    }
}
