using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Transforms
{
    public partial class TransformPositionSwitcherBinding : TransformBindingBase, ITargetBinding<bool>
    {
        [Header("Parameters")]
        [SerializeField] private Space _space;
        [SerializeField] private Vector3 _truePosition;
        [SerializeField] private Vector3 _falsePosition;
        
        protected Space Space => _space;

        protected Vector3 TruePosition => _truePosition;
        
        protected Vector3 FalsePosition => _falsePosition;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(bool value)
        {
            switch (Space)
            {
                case Space.Self: CachedTransform.localPosition = GetPosition(value); break;
                case Space.World: CachedTransform.position = GetPosition(value); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        protected Vector3 GetPosition(bool value) =>
            value ? TruePosition : FalsePosition;
    }
}