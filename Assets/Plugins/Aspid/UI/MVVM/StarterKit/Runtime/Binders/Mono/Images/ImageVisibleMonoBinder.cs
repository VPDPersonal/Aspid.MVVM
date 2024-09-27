using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Visible")]
    public partial class ImageVisibleMonoBinder : ComponentMonoBinder<Image>, IBinder<bool>, IBinder<Sprite>
    {
        [field: Header("Parameters")]
        [field: SerializeField] protected bool IsInvert { get; private set; }
        
        [BinderLog]
        public void SetValue(bool value)
        {
            if (IsInvert) value = !IsInvert;
            CachedComponent.enabled = value;
        }

        public void SetValue(Sprite sprite)
        {
            var value = sprite != null;
            SetValue(value);
        }
    }
}