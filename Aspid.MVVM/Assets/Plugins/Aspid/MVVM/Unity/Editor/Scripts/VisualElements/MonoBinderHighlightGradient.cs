#nullable enable
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class MonoBinderHighlightGradient : VisualElement
    {
        private Color _color;
        private float _highlightProgress;
        private IVisualElementScheduledItem? _highlightAnimation;

        public MonoBinderHighlightGradient()
        {
            style.position = Position.Absolute;
            style.left = 0;
            style.top = 0;
            style.right = 0;
            style.bottom = 0;
            pickingMode = PickingMode.Ignore;
            generateVisualContent += DrawHighlightGradient;
        }

        public void AnimateHighlight(Color color)
        {
            const int totalSteps = 50;

            _color = color;
            _highlightAnimation?.Pause();
            _highlightAnimation = null;

            _highlightProgress = 1f;
            MarkDirtyRepaint();

            var step = 0;
            IVisualElementScheduledItem? scheduledItem = null;
            scheduledItem = schedule.Execute(() =>
            {
                step++;
                if (step >= totalSteps)
                {
                    _highlightProgress = 0f;
                    MarkDirtyRepaint();
                    scheduledItem?.Pause();
                    return;
                }

                _highlightProgress = 1f - (float)step / totalSteps;
                MarkDirtyRepaint();
            }).Every(16);

            _highlightAnimation = scheduledItem;
        }

        private void DrawHighlightGradient(MeshGenerationContext ctx)
        {
            if (_highlightProgress <= 0.01f) return;

            const int strips = 75;
            var rect = contentRect;
            var painter = ctx.painter2D;
            var stripWidth = rect.width / strips;

            for (var i = 0; i < strips; i++)
            {
                var t = (float)i / (strips - 1);
                var alpha = (1f - t) * (1f - t) * _highlightProgress * 0.35f;
                if (alpha < 0.005f) continue;

                var color = _color;
                color.a = alpha;

                painter.fillColor = color;
                painter.BeginPath();
                {
                    painter.MoveTo(new Vector2(i * stripWidth, 0));
                    painter.LineTo(new Vector2((i + 1) * stripWidth, 0));
                    painter.LineTo(new Vector2((i + 1) * stripWidth, rect.height));
                    painter.LineTo(new Vector2(i * stripWidth, rect.height));
                }
                painter.ClosePath();
                painter.Fill();
            }
        }
    }
}
