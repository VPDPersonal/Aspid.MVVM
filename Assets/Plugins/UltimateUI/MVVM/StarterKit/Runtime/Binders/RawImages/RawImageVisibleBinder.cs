using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.RawImages
{
    public partial class RawImageVisibleBinder : RawImageBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private bool _isInvert;

        protected bool IsInvert => _isInvert;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(bool value)
        {
            if (IsInvert) value = !IsInvert;
            CachedImage.enabled = value;
        }
    }
}