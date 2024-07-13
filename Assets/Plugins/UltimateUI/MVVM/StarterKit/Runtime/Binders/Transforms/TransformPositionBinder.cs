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
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
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