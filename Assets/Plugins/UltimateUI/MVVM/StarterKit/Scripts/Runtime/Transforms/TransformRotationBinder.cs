using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Transforms
{
    public class TransformRotationBinder : TransformBinderBase, IBinder<Vector3>, IBinder<Quaternion>
    {
        [Header("Parameters")]
        [SerializeField] private Space _space;
        
        public void SetValue(Vector3 value) =>
            SetValue(Quaternion.Euler(value));
        
        public void SetValue(Quaternion value)
        {
            switch (_space)
            {
                case Space.Self: CachedTransform.localRotation = value; break;
                case Space.World: CachedTransform.rotation = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}