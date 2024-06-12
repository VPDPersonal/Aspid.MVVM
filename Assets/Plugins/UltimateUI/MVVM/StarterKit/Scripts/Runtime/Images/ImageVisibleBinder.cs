using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Images
{
    public class ImageVisibleBinder : ImageBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private bool _isInvert;
        
        public void SetValue(bool value)
        {
            if (_isInvert) value = !_isInvert;
            CachedImage.enabled = value;
        }
    }
    
}