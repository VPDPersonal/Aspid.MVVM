using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Texture")]
    public partial class RawImageTextureMonoBinder : ComponentMonoBinder<RawImage>, IBinder<Texture2D>, IBinder<Sprite>
    {
        [Header("Parameters")]
        [SerializeField] private bool _disabledWhenNull = true;
        
        [BinderLog]
        public void SetValue(Texture2D value)
        {
            CachedComponent.texture = value;
            if (_disabledWhenNull) CachedComponent.enabled = value != null;
        }

        [BinderLog]
        public void SetValue(Sprite value) =>
            SetValue(value?.texture);
    }
}