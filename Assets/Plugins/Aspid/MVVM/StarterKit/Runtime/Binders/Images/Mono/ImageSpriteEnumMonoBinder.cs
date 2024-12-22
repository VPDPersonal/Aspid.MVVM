using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Image/Image Binder - Sprite Enum")]
    public sealed class ImageSpriteEnumMonoBinder : EnumComponentMonoBinder<Image, Sprite>
    {
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected override void SetValue(Sprite value) 
        {
            CachedComponent.sprite = value;
            if (_disabledWhenNull) CachedComponent.enabled = value is not null;
        }
    }
}