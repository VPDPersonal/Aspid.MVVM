using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Mono.Generation;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Image/Image Binder - Sprite")]
    public partial class ImageSpriteMonoBinder : ComponentMonoBinder<Image>, IBinder<Sprite>
    {
        [Header("Parameters")]
        [SerializeField] private bool _disabledWhenNull = true;

        [BinderLog]
        public void SetValue(Sprite value)
        {
            CachedComponent.sprite = value;
            if (_disabledWhenNull) CachedComponent.enabled = value is not null;
        }
    }
}