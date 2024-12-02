#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    public sealed class TransformRotationSwitcherBinder : SwitcherBinder<Vector3>
    {
        private readonly Space _space;
        private readonly Transform _transform;

        public TransformRotationSwitcherBinder(Transform transform, Vector3 trueValue, Vector3 falseValue, Space space = Space.World) 
            : base(trueValue, falseValue)
        {
            _space = space;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        protected override void SetValue(Vector3 value) =>
            _transform.SetRotation(Quaternion.Euler(value), _space);
    }
}