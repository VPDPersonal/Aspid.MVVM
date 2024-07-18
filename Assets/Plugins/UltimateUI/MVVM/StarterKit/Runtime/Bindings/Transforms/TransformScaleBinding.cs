using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Transforms
{
    public partial class TransformScaleBinding : TransformBindingBase, IVectorTargetBinding
    {
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(Vector3 value) =>
            CachedTransform.localScale = value;
    }
}