using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.RawImages
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Visible")]
    public partial class RawImageVisibleMonoBinder : ComponentMonoBinder<RawImage>, IBinder<bool>, IBinder<Texture2D>
    {
        [field: Header("Parameters")]
        [field: SerializeField] 
        protected bool IsInvert { get; private set; }
        
        [BinderLog]
        public void SetValue(bool value)
        {
            if (IsInvert) value = !IsInvert;
            CachedComponent.enabled = value;
        }

        public void SetValue(Texture2D texture)
        {
            var value = texture != null;
            SetValue(value);
        }
    }
}