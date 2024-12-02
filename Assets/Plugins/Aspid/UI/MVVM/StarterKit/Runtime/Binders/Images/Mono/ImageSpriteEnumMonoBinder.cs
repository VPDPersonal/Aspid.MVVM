using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Sprite Enum")]
    public sealed class ImageSpriteEnumMonoBinder : EnumMonoBinder<Image, Sprite>
    {
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected override void SetValue(Sprite value) 
        {
            CachedComponent.sprite = value;
            if (_disabledWhenNull) CachedComponent.enabled = value != null;
        }
    }
}