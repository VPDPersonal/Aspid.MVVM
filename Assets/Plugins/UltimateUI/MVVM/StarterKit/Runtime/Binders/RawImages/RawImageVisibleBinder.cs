using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.RawImages
{
    public class RawImageVisibleBinder : RawImageBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private bool _isInvert;

        protected bool IsInvert => _isInvert;

        public void SetValue(bool value)
        {
            if (IsInvert) value = !IsInvert;
            CachedImage.enabled = value;
        }
    }
}