using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Material, UnityEngine.Material>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Raw Image/RawImage Binder - Material Enum")]
    public sealed class RawImageMaterialEnumMonoBinder : EnumComponentMonoBinder<RawImage, Material>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(Material value) =>
            CachedComponent.material = _converter?.Convert(value) ?? value;
    }
}