using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Image), "m_Sprite")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder - Sprite Enum")]
    [AddComponentContextMenu(typeof(Image),"Add Image Binder/Image Binder - Sprite Enum")]
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