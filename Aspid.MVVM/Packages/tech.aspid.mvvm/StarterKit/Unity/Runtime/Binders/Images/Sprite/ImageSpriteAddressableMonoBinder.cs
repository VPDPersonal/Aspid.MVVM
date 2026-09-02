#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AddressableMonoBinder{Sprite, Image}"/> that sets the <see cref="Image.sprite"/> property
    /// by loading a <see cref="Sprite"/> asset from the Addressables system based on the bound ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite Addressable")]
    public sealed class ImageSpriteAddressableMonoBinder : AddressableMonoBinder<Sprite, Image>
    {
        [Tooltip("Shown while the Addressable sprite loads or if loading fails.")]
        [SerializeField] private Sprite _defaultSprite;
        
        [Tooltip("When enabled, disables the Image component when the loaded sprite is null.")]
        [SerializeField] private bool _disabledWhenNull = true;

        protected override Sprite GetDefaultAsset() => 
            _defaultSprite;

        protected override void SetAsset(Sprite sprite)
        {
            var component = CachedComponent;
            if (!component) return;
            
            component.sprite = sprite;
            if (_disabledWhenNull) component.enabled = sprite;
        }
    }
}
#endif