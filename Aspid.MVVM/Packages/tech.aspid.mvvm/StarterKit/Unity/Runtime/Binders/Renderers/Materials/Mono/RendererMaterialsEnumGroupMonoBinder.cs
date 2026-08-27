using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;

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

        [Tooltip("The optional converter applied to each material in the default value.")]
        [SerializeReference] private Converter _defaultValueConverter;

        [Tooltip("The optional converter applied to each material in the selected value.")]
        [SerializeReference] private Converter _selectedValueConverter;

        /// <summary>
        /// Sets <see cref="Renderer.materials"/> on <paramref name="element"/> from the default value array, applying the default converter.
        /// </summary>
        /// <param name="element">The component this entry of the group writes to.</param>
        protected override void SetDefaultValue(Renderer element) =>
            element.SetMaterials(_defaultValueConverter, _defaultValue);

        /// <summary>
        /// Sets <see cref="Renderer.materials"/> on <paramref name="element"/> from the selected value array, applying the selected converter.
        /// </summary>
        /// <param name="element">The component this entry of the group writes to.</param>
        protected override void SetSelectedValue(Renderer element) =>
            element.SetMaterials(_selectedValueConverter, _selectedValue);
    }
}