using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Image/Image Binder - Sprite Enum Group")]
    public sealed class ImageSpriteEnumGroupMonoBinder : EnumGroupMonoBinder<Image, Sprite>
    {
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected override void SetValue(Image image, Sprite value) 
        {
            image.sprite = value;
            if (_disabledWhenNull) image.enabled = value is not null;
        }
    }
}