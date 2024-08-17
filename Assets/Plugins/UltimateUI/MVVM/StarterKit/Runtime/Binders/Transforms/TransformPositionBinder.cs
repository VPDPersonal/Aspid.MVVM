using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public class TransformPositionBinder : TransformBinderBase, IVectorBinder
    {
        protected readonly Space Space;
        
        public TransformPositionBinder(Transform transform, Space space = Space.World) : base(transform)
        {
            Space = space;
        }
        
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
        public void SetValue(Vector3 value)
        {
            switch (Space)
            {
                case Space.Self: Transform.localPosition = value; break;
                case Space.World: Transform.position = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}