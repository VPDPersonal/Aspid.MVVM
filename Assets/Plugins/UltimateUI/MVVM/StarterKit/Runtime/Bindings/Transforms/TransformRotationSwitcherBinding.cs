using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Transforms
{
    public partial class TransformRotationSwitcherBinding : TransformBindingBase, ITargetBinding<bool>
    {
        [Header("Parameters")]
        [SerializeField] private Space _space;
        [SerializeField] private Vector3 _trueRotation;
        [SerializeField] private Vector3 _falseRotation;
        
        protected Space Space => _space;

        protected Vector3 TrueRotation => _trueRotation;
        
        protected Vector3 FalseRotation => _falseRotation;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(bool value)
        {
            switch (Space)
            {
                case Space.Self: CachedTransform.localRotation = GetRotation(value); break;
                case Space.World: CachedTransform.rotation = GetRotation(value); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        protected Quaternion GetRotation(bool value) =>
            Quaternion.Euler(value ? TrueRotation : FalseRotation);
    }
}