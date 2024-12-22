using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Mono.Generation;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Raw Image/RawImage Binder - Texture")]
    public partial class RawImageTextureMonoBinder : ComponentMonoBinder<RawImage>, IBinder<Texture2D>, IBinder<Sprite>
    {
        [Header("Parameter")]
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