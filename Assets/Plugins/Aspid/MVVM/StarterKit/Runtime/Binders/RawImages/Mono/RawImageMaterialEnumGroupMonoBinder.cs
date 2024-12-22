using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Raw Image/RawImage Binder - Material EnumGroup")]
    public sealed class RawImageMaterialEnumGroupMonoBinder : EnumGroupMonoBinder<RawImage>
    {
        [Header("Parameters")]
        [SerializeField] private Material _defaultValue;
        [SerializeField] private Material _selectedValue;
        
        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Material, Material> _defaultValueConverter;
#else
        private IConverterMaterial _defaultValueConverter;
#endif
        
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Material, Material> _selectedValueConverter;
#else
        private IConverterMaterial _selectedValueConverter;
#endif

        protected override void SetDefaultValue(RawImage element) =>
            element.material = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(RawImage element) =>
            element.material = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}