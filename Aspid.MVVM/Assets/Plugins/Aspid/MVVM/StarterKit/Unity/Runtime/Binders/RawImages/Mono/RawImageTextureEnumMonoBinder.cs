using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Texture, UnityEngine.Texture>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterTexture;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the texture of the host <see cref="RawImage"/> component
    /// by mapping an enum-indexed source value to a <see cref="Texture"/>.
    /// Optionally disables the <see cref="RawImage"/> when the bound texture is <see langword="null"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture Enum")]
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture", SubPath = "Enum")]
    public class RawImageTextureEnumMonoBinder : EnumMonoBinder<RawImage, Texture, Converter>
    {
        [SerializeField] private bool _disabledWhenNull = true;
        
        protected sealed override void SetValue(Texture value)
        {
            CachedComponent.texture = value;
            CachedComponent.enabled = !_disabledWhenNull || value;
        }
    }
}