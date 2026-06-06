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
    /// <see cref="EnumGroupMonoBinder{Renderer}"/> that sets the <see cref="Renderer.materials"/> array
    /// on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials EnumGroup")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "EnumGroup")]
    public sealed class RendererMaterialsEnumGroupMonoBinder : EnumGroupMonoBinder<Renderer>
    {
        [Tooltip("The materials array applied to each element when it is not the selected enum value.")]
        [SerializeField] private Material[] _defaultValue;
        [Tooltip("The materials array applied to the element matching the selected enum value.")]
        [SerializeField] private Material[] _selectedValue;

        [SerializeReferenceDropdown]
        [Tooltip("The optional converter applied to each material in the default value before assignment.")]
        [SerializeReference] private Converter _defaultValueConverter;

        [SerializeReferenceDropdown]
        [Tooltip("The optional converter applied to each material in the selected value before assignment.")]
        [SerializeReference] private Converter _selectedValueConverter;

        /// <summary>
        /// Called when applying the default materials to the specified element.
        /// Sets <see cref="Renderer.materials"/> using the default value array, applying the default converter.
        /// </summary>
        protected override void SetDefaultValue(Renderer element) =>
            element.SetMaterials(_defaultValueConverter, _defaultValue);

        /// <summary>
        /// Called when applying the selected materials to the specified element.
        /// Sets <see cref="Renderer.materials"/> using the selected value array, applying the selected converter.
        /// </summary>
        protected override void SetSelectedValue(Renderer element) =>
            element.SetMaterials(_selectedValueConverter, _selectedValue);
    }
}