#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AddressableMonoBinder{TAsset, TComponent}"/> that loads a <see cref="Texture"/> into
    /// <see cref="RawImage.texture"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture Addressable")]
    public sealed class RawImageTextureAddressableMonoBinder : AddressableMonoBinder<Texture, RawImage>
    {
        [Tooltip("Shown while loading or when loading fails.")]
        [SerializeField] private Texture _defaultTexture;

        [Tooltip("Disable the RawImage while the texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;

        /// <inheritdoc/>
        protected override Texture GetDefaultAsset() =>
            _defaultTexture;

        /// <inheritdoc/>
        protected override void SetAsset(Texture texture)
        {
            var component = CachedComponent;
            if (!component) return;

            component.SetTexture(texture, _disabledWhenNull);
        }
    }
}
#endif
