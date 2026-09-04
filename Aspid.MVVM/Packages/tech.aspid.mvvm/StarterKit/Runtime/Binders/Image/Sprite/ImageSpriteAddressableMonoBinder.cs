#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AddressableMonoBinder{TAsset, TComponent}"/> that loads a <see cref="Sprite"/> into
    /// <see cref="Image.sprite"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite Addressable")]
    public sealed class ImageSpriteAddressableMonoBinder : AddressableMonoBinder<Sprite, Image>
    {
        [Tooltip("Shown while loading or when loading fails.")]
        [SerializeField] private Sprite _defaultSprite;

        [Tooltip("Disable the Image while the sprite is null.")]
        [SerializeField] private bool _disabledWhenNull = true;

        /// <inheritdoc/>
        protected override Sprite GetDefaultAsset() =>
            _defaultSprite;

        /// <inheritdoc/>
        protected override void SetAsset(Sprite sprite)
        {
            var component = CachedComponent;
            if (!component) return;

            component.SetSprite(sprite, _disabledWhenNull);
        }
    }
}
#endif
