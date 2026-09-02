#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AddressableMonoBinder{Texture2D, RawImage}"/> that sets the <see cref="RawImage.texture"/> property
    /// by loading a <see cref="Texture2D"/> asset from the Addressables system when the bound ViewModel value changes.
    /// </summary>
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture Addressable")]
    public sealed class RawImageTextureAddressableMonoBinder : AddressableMonoBinder<Texture2D, RawImage>
    {
        [Tooltip("Shown while the asset is loading, or when no address is set.")]
        [SerializeField] private Texture2D _defaultTexture;

        [Tooltip("Disables the RawImage component when the loaded texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;

        /// <inheritdoc/>
        protected override Texture2D GetDefaultAsset() =>
            _defaultTexture;

        /// <inheritdoc/>
        protected override void SetAsset(Texture2D texture)
        {
            var component = CachedComponent;
            if (!component) return;

            component.texture = texture;
            if (_disabledWhenNull) component.enabled = texture;
        }
    }
}
#endif