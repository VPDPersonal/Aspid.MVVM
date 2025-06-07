using UnityEngine;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Renderer), "m_Materials")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder - Materials EnumGroup")]
    [AddComponentContextMenu(typeof(Renderer),"Add Renderer Binder/Renderer Binder - Materials EnumGroup")]
    public sealed class RendererMaterialsEnumGroupMonoBinder : EnumGroupMonoBinder<Renderer>
    {
        [Header("Parameters")]
        [SerializeField] private Material[] _defaultValue;
        [SerializeField] private Material[] _selectedValue;
        
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;

        protected override void SetDefaultValue(Renderer element) =>
            element.SetMaterials(_defaultValueConverter, _defaultValue);

        protected override void SetSelectedValue(Renderer element) =>
            element.SetMaterials(_selectedValueConverter, _selectedValue);
    }
}