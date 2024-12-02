#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    public sealed class TransformPositionSwitcherBinder : SwitcherBinder<Vector3>
    {
        private readonly Space _space;
        private readonly VectorMode _mode;
        private readonly Transform _transform;
        
        public TransformPositionSwitcherBinder(Transform transform, Vector3 trueValue, Vector3 falseValue,
            Space space = Space.World, VectorMode mode = VectorMode.XYZ) 
            : base(trueValue, falseValue)
        {
            _mode = mode;
            _space = space;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        protected override void SetValue(Vector3 value) =>
            _transform.SetPosition(value, _mode, _space);
    }
}