using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public partial class TransformPositionBinder : TransformBinderBase, IVectorBinder
    {
        [Header("Parameters")]
        [SerializeField] private Space _space;
        
        protected Space Space => _space;
        
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
        [BinderLog]
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