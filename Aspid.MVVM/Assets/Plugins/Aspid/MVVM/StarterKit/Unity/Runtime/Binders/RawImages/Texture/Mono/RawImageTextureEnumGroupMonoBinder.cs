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
    /// <see cref="EnumGroupMonoBinder{RawImage, Texture, Converter}"/> that sets the <see cref="RawImage.texture"/> property
    /// on a group of <see cref="RawImage"/> components, applying the configured selected or default value to each entry
    /// based on the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// Disables each <see cref="RawImage"/> component when its resolved texture is <see langword="null"/> and
    /// the Disable When Null option is enabled.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture EnumGroup")]
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture", SubPath = "EnumGroup")]
    public sealed class RawImageTextureEnumGroupMonoBinder : EnumGroupMonoBinder<RawImage, Texture, Converter>
    {
        [Tooltip("When true, disables each RawImage component automatically when its resolved texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;
     
        /// <inheritdoc/>
        protected override void SetValue(RawImage element, Texture value) 
        {
            element.texture = value;
            element.enabled = !_disabledWhenNull || value;
        }
    }
}