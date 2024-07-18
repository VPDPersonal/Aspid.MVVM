using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Transforms
{
    public partial class TransformScaleSwitcherBinding : TransformBindingBase, ITargetBinding<bool>
    {
        [Header("Parameters")]
        [SerializeField] private Vector3 _trueScale;
        [SerializeField] private Vector3 _falseScale;

        protected Vector3 TrueScale => _trueScale;

        protected Vector3 FalseScale => _falseScale;

#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(bool value) =>
            CachedTransform.localScale = GetLocalScale(value);

        protected Vector3 GetLocalScale(bool value) =>
            value ? TrueScale : FalseScale;
    }
}