using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Visible")]
    public partial class ImageVisibleMonoBinder : ComponentMonoBinder<Image>, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value)
        {
            if (_isInvert) value = !_isInvert;
            CachedComponent.enabled = value;
        }
    }
}