using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Renderer/Renderer Binder - Materials EnumGroup")]
    public sealed class RendererMaterialsEnumGroupMonoBinder : EnumGroupMonoBinder<Renderer>
    {
        [Header("Parameters")]
        [SerializeField] private Material[] _defaultValue;
        [SerializeField] private Material[] _selectedValue;
        
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

        protected override void SetDefaultValue(Renderer element) =>
            element.SetMaterials(_defaultValueConverter, _defaultValue);

        protected override void SetSelectedValue(Renderer element) =>
            element.SetMaterials(_selectedValueConverter, _selectedValue);
    }
}