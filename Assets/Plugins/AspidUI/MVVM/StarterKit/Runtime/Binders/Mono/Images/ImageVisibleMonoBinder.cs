using UnityEngine;
using UnityEngine.UI;
using AspidUI.MVVM.Unity.Generation;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Visible")]
    public partial class ImageVisibleMonoBinder : ComponentMonoBinder<Image>, IBinder<bool>
    {
        [field: Header("Parameters")]
        [field: SerializeField] protected bool IsInvert { get; private set; }
        
        [BinderLog]
        public void SetValue(bool value)
        {
            if (IsInvert) value = !IsInvert;
            CachedComponent.enabled = value;
        }
    }
}