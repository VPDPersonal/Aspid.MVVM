using System;
using UnityEngine;

namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public sealed class TransformRotationSwitcherBinder : SwitcherBinder<Vector3>
    {
        private readonly Space _space;
        private readonly Transform _transform;

        public TransformRotationSwitcherBinder(Transform transform, Vector3 trueValue, Vector3 falseValue, Space space = Space.World) 
            : base(trueValue, falseValue)
        {
            _space = space;
            _transform = transform;
        }

        protected override void SetValue(Vector3 value)
        {
            switch (_space)
            {
                case Space.Self: _transform.localRotation = Quaternion.Euler(value); break;
                case Space.World: _transform.rotation = Quaternion.Euler(value); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}