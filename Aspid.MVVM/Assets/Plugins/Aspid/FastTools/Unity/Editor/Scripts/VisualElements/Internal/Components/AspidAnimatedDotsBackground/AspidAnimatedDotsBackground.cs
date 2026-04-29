using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// A <see cref="VisualElement"/> that paints an animated dotted background composed of three
    /// drifting metaball blobs whose colors blend through the dot grid. Colors and dot metrics can
    /// be overridden via USS custom properties or via UXML attributes / fluent extensions.
    /// </summary>
    [UxmlElement(libraryPath = "Aspid/FastTools")]
    public sealed partial class AspidAnimatedDotsBackground : VisualElement
    {
        private const int BlobCount = 3;
        private const string StyleSheetPath = "UI/Components/Aspid-FastTools-AspidAnimatedDotsBackground";

        private readonly Vector2[] _blobRadii = new Vector2[BlobCount];
        private readonly Vector2[] _blobCenters = new Vector2[BlobCount];

        private readonly AspidAnimatedDotsBackgroundColorsStyle _colors;
        private readonly AspidAnimatedDotsBackgroundSizeStyle _size;

        /// <summary>
        /// Gets or sets the color of the first blob.
        /// </summary>
        [UxmlAttribute]
        public Color Color1
        {
            get => _colors.Color1;
            set => _colors.SetColor1(value);
        }

        /// <summary>
        /// Gets or sets the color of the second blob.
        /// </summary>
        [UxmlAttribute]
        public Color Color2
        {
            get => _colors.Color2;
            set => _colors.SetColor2(value);
        }

        /// <summary>
        /// Gets or sets the color of the third blob.
        /// </summary>
        [UxmlAttribute]
        public Color Color3
        {
            get => _colors.Color3;
            set => _colors.SetColor3(value);
        }

        /// <summary>
        /// Gets or sets the base dot radius before window-size scaling.
        /// </summary>
        [UxmlAttribute]
        public float DotRadius
        {
            get => _size.DotRadius;
            set => _size.SetDotRadius(value);
        }

        /// <summary>
        /// Gets or sets the base dot spacing before window-size scaling.
        /// </summary>
        [UxmlAttribute]
        public float DotSpacing
        {
            get => _size.DotSpacing;
            set => _size.SetDotSpacing(value);
        }

        /// <summary>
        /// Gets or sets the reference window size used by the size-scaling curve.
        /// Smaller values amplify how aggressively dots grow with the window.
        /// </summary>
        [UxmlAttribute]
        public float ScaleReferenceSize
        {
            get => _size.ScaleReference;
            set => _size.SetScaleReference(value);
        }

        /// <summary>
        /// Creates an <see cref="AspidAnimatedDotsBackground"/> using <see cref="AspidAnimatedDotsBackgroundPreset.Default"/>.
        /// </summary>
        public AspidAnimatedDotsBackground()
            : this(AspidAnimatedDotsBackgroundPreset.Default) { }

        /// <summary>
        /// Creates an <see cref="AspidAnimatedDotsBackground"/> with the given preset and starts the animation loop.
        /// </summary>
        /// <param name="preset">The configuration preset to apply.</param>
        public AspidAnimatedDotsBackground(AspidAnimatedDotsBackgroundPreset preset)
        {
            this.AddStyleSheetsFromResource(StyleSheetPath);
            generateVisualContent += OnGenerateVisualContent;

            _colors = new AspidAnimatedDotsBackgroundColorsStyle(
                this, preset.Color1, preset.Color2, preset.Color3, MarkDirtyRepaint);

            _size = new AspidAnimatedDotsBackgroundSizeStyle(
                this, preset.DotRadius, preset.DotSpacing, preset.ScaleReferenceSize, MarkDirtyRepaint);

            schedule.Execute(MarkDirtyRepaint).Every(33);
        }

        private void OnGenerateVisualContent(MeshGenerationContext context)
        {
            var rect = contentRect;
            if (rect.width <= 0f || rect.height <= 0f) return;

            var painter = context.painter2D;
            var time = (float)EditorApplication.timeSinceStartup;

            // Scale dot size and spacing with the window — calibrated against the reference size where
            // the base values feel right. Sqrt curve keeps growth gentle: doubling the window only
            // enlarges dots by ~1.4×, not 2×.
            var scale = Mathf.Sqrt(Mathf.Min(rect.width, rect.height) / _size.ScaleReference);
            var spacing = _size.DotSpacing * scale;
            var radius = _size.DotRadius * scale;

            // Blob centers and radii depend only on (i, size, time) — pre-compute once per
            // frame so the inner per-pixel loop doesn't redo BlobCount switch lookups per dot.
            for (var i = 0; i < BlobCount; i++)
            {
                _blobCenters[i] = GetBlobCenter(i, rect.size, time);
                _blobRadii[i] = GetBlobRadius(i, rect.size);
            }

            for (var y = spacing * 0.5f; y < rect.height; y += spacing)
            {
                for (var x = spacing * 0.5f; x < rect.width; x += spacing)
                {
                    var (strength, blendedHighlight) = SampleBlobField(new Vector2(x, y), time);
                    strength = Mathf.SmoothStep(0f, 1f, strength);

                    var color = blendedHighlight;
                    color.a = blendedHighlight.a * strength;

                    painter.fillColor = color;
                    DrawDot(painter, new Vector2(x, y), radius);
                }
            }
        }

        private (float strength, Color blendedHighlight) SampleBlobField(Vector2 point, float time)
        {
            var maxField = 0f;
            var totalContribution = 0f;
            var blendedHighlight = new Color(0f, 0f, 0f, 0f);

            for (var i = 0; i < BlobCount; i++)
            {
                var center = _blobCenters[i];
                var radius = _blobRadii[i];
                var distortedPoint = ApplyDistortion(point, i, time);

                var dx = (distortedPoint.x - center.x) / radius.x;
                var dy = (distortedPoint.y - center.y) / radius.y;

                var distance = dx * dx + dy * dy;
                var contribution = Mathf.Exp(-distance * 1.2f);

                if (contribution > maxField)
                    maxField = contribution;

                totalContribution += contribution;
                blendedHighlight += _colors[i] * contribution;
            }

            if (totalContribution > 0f) blendedHighlight /= totalContribution;
            else blendedHighlight = _colors[0];

            var strength = Mathf.InverseLerp(0.05f, 0.92f, maxField);
            return (strength, blendedHighlight);
        }

        private static Vector2 GetBlobCenter(int index, Vector2 size, float time) => index switch
        {
            0 => new Vector2(
                size.x * (0.24f + Mathf.Sin(time * 0.38f) * 0.11f),
                size.y * (0.30f + Mathf.Cos(time * 0.44f) * 0.09f)),

            1 => new Vector2(
                size.x * (0.63f + Mathf.Sin(time * 0.32f + 1.4f) * 0.14f),
                size.y * (0.42f + Mathf.Cos(time * 0.35f + 0.6f) * 0.11f)),

            _ => new Vector2(
                size.x * (0.46f + Mathf.Sin(time * 0.27f + 2.3f) * 0.16f),
                size.y * (0.73f + Mathf.Cos(time * 0.32f + 2.8f) * 0.1f)),
        };

        private static Vector2 GetBlobRadius(int index, Vector2 size) => index switch
        {
            0 => new Vector2(size.x * 0.22f, size.y * 0.16f),
            1 => new Vector2(size.x * 0.26f, size.y * 0.20f),
            _ => new Vector2(size.x * 0.21f, size.y * 0.15f),
        };

        private static Vector2 ApplyDistortion(Vector2 point, int index, float time)
        {
            var offsetX = Mathf.Sin(point.y * 0.026f + time * 1.18f + index * 1.7f) * 14f
                + Mathf.Cos(point.x * 0.018f - time * 0.82f + index) * 7f;

            var offsetY = Mathf.Cos(point.x * 0.022f - time * 0.96f + index * 0.9f) * 15f
                + Mathf.Sin(point.y * 0.019f + time * 0.67f + index * 1.3f) * 6f;

            return new Vector2(point.x + offsetX, point.y + offsetY);
        }

        private static void DrawDot(Painter2D painter, Vector2 center, float radius)
        {
            const int segments = 10;

            painter.BeginPath();
            {
                for (var i = 0; i < segments; i++)
                {
                    var angle = Mathf.PI * 2f * i / segments;
                    var point = new Vector2(
                        center.x + Mathf.Cos(angle) * radius,
                        center.y + Mathf.Sin(angle) * radius);

                    if (i is 0) painter.MoveTo(point);
                    else painter.LineTo(point);
                }
            }
            painter.ClosePath();

            painter.Fill();
        }
    }
}
