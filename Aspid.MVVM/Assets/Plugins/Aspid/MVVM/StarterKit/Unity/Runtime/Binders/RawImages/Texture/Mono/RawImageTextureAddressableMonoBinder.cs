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
    /// <remarks>
    /// Disables the <see cref="RawImage"/> component when the loaded texture is <see langword="null"/> and
    /// the Disable When Null option is enabled.
    /// </remarks>
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture Addressable")]
    public sealed class RawImageTextureAddressableMonoBinder : AddressableMonoBinder<Texture2D, RawImage>
    {
        [Tooltip("The texture to display while the Addressable asset is loading or when no address is set.")]
        [SerializeField] private Texture2D _defaultTexture;
        [Tooltip("When true, disables the RawImage component automatically when the loaded texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;

        /// <inheritdoc/>
        protected override Texture2D GetDefaultAsset() =>
            _defaultTexture;

        /// <inheritdoc/>
        protected override void SetAsset(Texture2D texture)
        {
            var component = CachedComponent;
            if (component is null) return;

            component.texture = texture;
            component.enabled = !_disabledWhenNull || texture;
        }
    }
}
#endif