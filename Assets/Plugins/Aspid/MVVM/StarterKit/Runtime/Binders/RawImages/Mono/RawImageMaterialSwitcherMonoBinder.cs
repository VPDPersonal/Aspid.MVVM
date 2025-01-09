using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/UI/Raw Image/RawImage Binder - Material Switcher")]
    public sealed class RawImageMaterialSwitcherMonoBinder : SwitcherMonoBinder<RawImage, Material>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Material, Material> _converter;
#else
        private IConverterMaterial _converter;
#endif
        
        protected override void SetValue(Material value) =>
            CachedComponent.material = _converter?.Convert(value) ?? value;
    }
}