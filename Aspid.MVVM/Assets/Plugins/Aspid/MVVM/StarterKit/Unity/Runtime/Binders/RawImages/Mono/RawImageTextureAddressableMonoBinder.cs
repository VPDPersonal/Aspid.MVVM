#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Raw Image/RawImage Binder – Texture Addressable")]
    public sealed class RawImageTextureAddressableMonoBinder : AddressableMonoBinder<Texture2D, RawImage>
    {
        [SerializeField] private Texture2D _defaultTexture;
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected override Texture2D GetDefaultAsset() => 
            _defaultTexture;
        
        protected override void SetAsset(Texture2D texture)
        {
            CachedComponent.texture = texture;
            CachedComponent.enabled = !_disabledWhenNull || texture;
        }
    }
}
#endif