using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the materials on a group of <see cref="UnityEngine.Renderer"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials EnumGroup")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "EnumGroup")]
    public sealed class RendererMaterialsEnumGroupMonoBinder : EnumGroupMonoBinder<Renderer>
    {
        [SerializeField] private Material[] _defaultValue;
        [SerializeField] private Material[] _selectedValue;
        
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