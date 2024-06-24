using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Transforms
{
    public class TransformPositionBinder : TransformBinderBase, IBinder<Vector3>
    {
        [Header("Parameters")]
        [SerializeField] private Space _space;
        
        protected Space Space => _space;
        
        public void SetValue(Vector3 value)
        {
            switch (Space)
            {
                case Space.Self: CachedTransform.localPosition = value; break;
                case Space.World: CachedTransform.position = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}