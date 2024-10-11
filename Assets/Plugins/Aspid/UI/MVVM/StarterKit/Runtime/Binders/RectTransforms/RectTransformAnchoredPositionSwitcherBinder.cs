#nullable enable
using System;
using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.RectTransforms
{
    public sealed class RectTransformAnchoredPositionSwitcherBinder : SwitcherBinder<Vector3>
    {
        private readonly RectTransform _transform;

        public RectTransformAnchoredPositionSwitcherBinder(RectTransform transform, Vector3 trueValue, Vector3 falseValue) 
            : base(trueValue, falseValue)
        {
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        protected override void SetValue(Vector3 value) =>
            _transform.anchoredPosition3D = value;
    }
}