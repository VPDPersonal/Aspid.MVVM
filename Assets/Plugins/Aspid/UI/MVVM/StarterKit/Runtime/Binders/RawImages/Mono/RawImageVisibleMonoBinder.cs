using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Visible")]
    public partial class RawImageVisibleMonoBinder : ComponentMonoBinder<RawImage>, IBinder<bool>
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