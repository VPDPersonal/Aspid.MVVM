using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.CanvasGroups
{
    public partial class CanvasGroupAlphaSwitcherBinder : CanvasGroupBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] [Range(0, 1)] private float _trueAlpha;
        [SerializeField] [Range(0, 1)] private float _falseAlpha;

        protected float TrueAlpha => _trueAlpha;
        
        protected float FalseAlpha => _falseAlpha;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(bool value) =>
            CachedCanvasGroup.alpha = GetAlpha(value);
        
        protected float GetAlpha(bool value) =>
            value ? TrueAlpha : FalseAlpha;
    }
}