using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// A non-interactive overlay <see cref="VisualElement"/> that paints a smooth horizontal accent
    /// gradient with a quadratic alpha falloff and smoothly fades it in or out toward a target
    /// progress between 0 and 1.
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

        // Conservative cap on the step count: each step emits 6 indices, so 65535 / 6 = 10922 steps
        // keeps even the total index count within ushort range. The hard limits — the largest emitted
        // vertex index, (ushort)(i * 2 + 3) = 2 * steps + 1, and the 65535-vertex allocation ceiling —
        // are only reached above ~32k steps, well past this cap.
        private const int MaxSteps = ushort.MaxValue / 6;

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
        /// Gets or sets the number of gradient segments painted across the overlay width.
        /// More segments approximate the quadratic alpha falloff more finely.
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

            var rect = contentRect;
            if (rect.width <= 0f || rect.height <= 0f) return;

            var steps = Mathf.Clamp(_metrics.Steps, 1, MaxSteps);
            var alphaScale = _metrics.AlphaScale;
            var baseColor = _color.Value;

            // One vertical column of vertices per gradient stop (top + bottom). Adjacent quads
            // reuse their shared boundary column, so the mesh carries no internal anti-aliased
            // edges — only its outer silhouette is smoothed. That removes the dark seams that
            // appeared when each strip was filled separately and faded its own edges to transparent.
            var columns = steps + 1;
            var mesh = ctx.Allocate(columns * 2, steps * 6);

            for (var j = 0; j < columns; j++)
            {
                var t = (float)j / steps;
                var alpha = (1f - t) * (1f - t) * _progress * alphaScale;
                var color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
                var x = t * rect.width;

                mesh.SetNextVertex(new Vertex { position = new Vector3(x, 0f, Vertex.nearZ), tint = color });
                mesh.SetNextVertex(new Vertex { position = new Vector3(x, rect.height, Vertex.nearZ), tint = color });
            }

            for (var i = 0; i < steps; i++)
            {
                var topLeft = (ushort)(i * 2);
                var bottomLeft = (ushort)(i * 2 + 1);
                var topRight = (ushort)(i * 2 + 2);
                var bottomRight = (ushort)(i * 2 + 3);

                mesh.SetNextIndex(bottomLeft);
                mesh.SetNextIndex(topLeft);
                mesh.SetNextIndex(topRight);

                mesh.SetNextIndex(topRight);
                mesh.SetNextIndex(bottomRight);
                mesh.SetNextIndex(bottomLeft);
            }
        }
    }
}
