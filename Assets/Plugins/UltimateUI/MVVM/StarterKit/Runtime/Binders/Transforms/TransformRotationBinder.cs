using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public class TransformRotationBinder : TransformBinderBase, IRotationBinder
    {
        protected readonly Space Space;

        public TransformRotationBinder(Transform transform, Space space = Space.World)
            : base(transform)
        {
            Space = space;
        }

        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
        public void SetValue(Vector3 value) =>
            SetValue(Quaternion.Euler(value));
        
        public void SetValue(Quaternion value)
        {
            switch (Space)
            {
                case Space.Self: Transform.localRotation = value; break;
                case Space.World: Transform.rotation = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }

}