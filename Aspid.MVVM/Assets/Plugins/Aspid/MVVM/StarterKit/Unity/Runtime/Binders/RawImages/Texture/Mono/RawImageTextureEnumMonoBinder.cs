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
    /// <see cref="EnumMonoBinder{RawImage, Texture, Converter}"/> that sets the <see cref="RawImage.texture"/>
    /// property to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    /// <remarks>
    /// Disables the <see cref="RawImage"/> component when the resolved texture is <see langword="null"/> and
    /// the Disable When Null option is enabled.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture Enum")]
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture", SubPath = "Enum")]
    public class RawImageTextureEnumMonoBinder : EnumMonoBinder<RawImage, Texture, Converter>
    {
        [Tooltip("When true, disables the RawImage component automatically when the bound texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;
        
        /// <inheritdoc/>
        protected sealed override void SetValue(Texture value)
        {
            CachedComponent.texture = value;
            CachedComponent.enabled = !_disabledWhenNull || value;
        }
    }
}