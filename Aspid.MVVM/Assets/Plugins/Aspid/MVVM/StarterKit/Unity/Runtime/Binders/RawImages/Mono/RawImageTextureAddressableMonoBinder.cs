#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="RawImage.texture"/> property on a <see cref="RawImage"/> component
    /// by loading a <see cref="Texture2D"/> asset from the Addressables system when the bound ViewModel value changes.
    /// Optionally disables the <see cref="RawImage"/> when the loaded texture is <see langword="null"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture Addressable")]
    public sealed class RawImageTextureAddressableMonoBinder : AddressableMonoBinder<Texture2D, RawImage>
    {
        [SerializeField] private Texture2D _defaultTexture;
        [SerializeField] private bool _disabledWhenNull = true;

        protected override Texture2D GetDefaultAsset() =>
            _defaultTexture;

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