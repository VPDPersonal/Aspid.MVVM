using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public partial class TransformRotationBinder : TransformBinderBase, IVectorBinder, IBinder<Quaternion>
    {
        [Header("Parameters")]
        [SerializeField] private Space _space;

        protected Space Space => _space;
        
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
        public void SetValue(Vector3 value) =>
            SetValue(Quaternion.Euler(value));
        
        [BinderLog]
        public void SetValue(Quaternion value)
        {
            switch (Space)
            {
                case Space.Self: CachedTransform.localRotation = value; break;
                case Space.World: CachedTransform.rotation = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }

}