using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.CanvasGroups
{
    public partial class CanvasGroupAlphaBinder : CanvasGroupBinderBase, IBinder<bool>, IBinder<float>
    {
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(bool value) =>
            CachedCanvasGroup.alpha = value ? 1 : 0;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(float value) =>
            CachedCanvasGroup.alpha = Mathf.Clamp(value, 0, 1);
    }
}