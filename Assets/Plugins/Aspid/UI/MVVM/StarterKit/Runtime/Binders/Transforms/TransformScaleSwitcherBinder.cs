#nullable enable
using System;
using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public sealed class TransformScaleSwitcherBinder : SwitcherBinder<Vector3>
    {
        private readonly VectorMode _mode;
        private readonly Transform _transform;

        public TransformScaleSwitcherBinder(Transform transform, Vector3 trueValue, Vector3 falseValue, VectorMode mode = VectorMode.XYZ) 
            : base(trueValue, falseValue)
        {
            _mode = mode;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        protected override void SetValue(Vector3 value) =>
            _transform.SetScale(value, _mode);
    }
}