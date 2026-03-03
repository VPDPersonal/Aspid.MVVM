#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite Addressable")]
    public sealed class ImageSpriteAddressableMonoBinder : AddressableMonoBinder<Sprite, Image>
    {
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private bool _disabledWhenNull = true;

        protected override Sprite GetDefaultAsset() => 
            _defaultSprite;

        protected override void SetAsset(Sprite sprite)
        {
            var component = CachedComponent;
            if (component is null) return;
            
            component.sprite = sprite;
            component.enabled = !_disabledWhenNull || sprite;
        }
    }
}
#endif