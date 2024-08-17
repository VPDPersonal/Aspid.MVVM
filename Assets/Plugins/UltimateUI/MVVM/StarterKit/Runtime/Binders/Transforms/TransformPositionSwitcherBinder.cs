using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public class TransformPositionSwitcherBinder : TransformBinderBase, IBinder<bool>
    {
        protected readonly Space Space;
        protected readonly Vector3 TruePosition;
        protected readonly Vector3 FalsePosition;
        
        public TransformPositionSwitcherBinder(
            Transform transform,
            Vector3 truePosition, 
            Vector3 falsePosition, 
            Space space = Space.World) 
            : base(transform)
        {
            Space = space;
            TruePosition = truePosition;
            FalsePosition = falsePosition;
        }
        
        public void SetValue(bool value)
        {
            switch (Space)
            {
                case Space.Self: Transform.localPosition = GetPosition(value); break;
                case Space.World: Transform.position = GetPosition(value); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        protected Vector3 GetPosition(bool value) =>
            value ? TruePosition : FalsePosition;
    }
}