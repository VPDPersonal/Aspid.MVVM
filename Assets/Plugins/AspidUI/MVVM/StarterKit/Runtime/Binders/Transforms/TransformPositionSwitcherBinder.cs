using System;
using UnityEngine;

namespace AspidUI.MVVM.StarterKit.Binders.Transforms
{
    public sealed class TransformPositionSwitcherBinder : SwitcherBinder<Vector3>
    {
        private readonly Space _space;
        private readonly Transform _transform;
        
        public TransformPositionSwitcherBinder(Transform transform, Vector3 trueValue, Vector3 falseValue, Space space = Space.World) 
            : base(trueValue, falseValue)
        {
            _space = space;
            _transform = transform;
        }
        
        protected override void SetValue(Vector3 value)
        {
            switch (_space)
            {
                case Space.Self: _transform.localPosition = value; break;
                case Space.World: _transform.position = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}