using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public class TransformRotationSwitcherBinder : TransformBinderBase, IBinder<bool>
    {
        protected readonly Space Space;
        protected readonly Vector3 TrueRotation;
        protected readonly Vector3 FalseRotation;

        public TransformRotationSwitcherBinder(
            Transform transform,
            Vector3 trueRotation, 
            Vector3 falseRotation,
            Space space = Space.World)
            : base(transform)
        {
            Space = space;
            TrueRotation = trueRotation;
            FalseRotation = falseRotation;
        }
        
        public void SetValue(bool value)
        {
            switch (Space)
            {
                case Space.Self: Transform.localRotation = GetRotation(value); break;
                case Space.World: Transform.rotation = GetRotation(value); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        protected Quaternion GetRotation(bool value) =>
            Quaternion.Euler(value ? TrueRotation : FalseRotation);
    }
}