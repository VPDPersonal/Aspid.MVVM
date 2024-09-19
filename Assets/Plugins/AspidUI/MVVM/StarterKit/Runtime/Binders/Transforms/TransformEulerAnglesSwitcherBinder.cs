using System;
using UnityEngine;

namespace AspidUI.MVVM.StarterKit.Binders.Transforms
{
    public sealed class TransformEulerAnglesSwitcherBinder : SwitcherBinder<Vector3>
    {
        private readonly Space _space;
        private readonly Transform _transform;
        
        public TransformEulerAnglesSwitcherBinder(Transform transform, Vector3 trueValue, Vector3 falseValue, Space space = Space.World)
            : base(trueValue, falseValue)
        {
            _space = space;
            _transform = transform;
        }
        
        protected override void SetValue(Vector3 value)
        {
            switch (_space)
            {
                case Space.Self: _transform.localEulerAngles = value; break;
                case Space.World: _transform.eulerAngles = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}