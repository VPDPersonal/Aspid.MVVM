using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(RawImage), "m_Material")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Raw Image/RawImage Binder - Material EnumGroup")]
    [AddComponentContextMenu(typeof(LineRenderer),"Add RawImage Binder/RawImage Binder - Material EnumGroup")]
    public sealed class RawImageMaterialEnumGroupMonoBinder : EnumGroupMonoBinder<RawImage>
    {
        [Header("Parameters")]
        [SerializeField] private Material _defaultValue;
        [SerializeField] private Material _selectedValue;
        
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;

        protected override void SetDefaultValue(RawImage element) =>
            element.material = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(RawImage element) =>
            element.material = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}