#nullable enable
using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public sealed class RectTransformSizeDeltaSwitcherBinder : SwitcherBinder<Vector2>
    {
        private readonly SizeDeltaMode _mode;
        private readonly RectTransform _transform;

        public RectTransformSizeDeltaSwitcherBinder(RectTransform transform, Vector2 trueValue, Vector2 falseValue, 
            SizeDeltaMode mode = SizeDeltaMode.SizeDelta)
            : base(trueValue, falseValue)
        {
            _mode = mode;
            _transform = transform;
        }

        protected override void SetValue(Vector2 value) =>
            _transform.SetSizeDelta(value, _mode);
    }
}