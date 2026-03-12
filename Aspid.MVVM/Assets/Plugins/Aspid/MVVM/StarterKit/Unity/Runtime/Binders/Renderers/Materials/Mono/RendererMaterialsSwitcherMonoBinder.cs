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
    /// <see cref="SwitcherMonoBinder{Renderer, Material[]}"/> that switches the <see cref="Renderer.materials"/> array
    /// between two predefined <see cref="Material"/> arrays based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials Switcher")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "Switcher")]
    public sealed class RendererMaterialsSwitcherMonoBinder : SwitcherMonoBinder<Renderer, Material[]>
    {
        [Tooltip("The optional converter applied to each material before it is assigned to the Renderer.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        /// <summary>
        /// Called when applying the selected value to the <see cref="Renderer.materials"/> array.
        /// Applies the optional converter to each material before assignment.
        /// </summary>
        protected override void SetValue(Material[] values) =>
            CachedComponent.SetMaterials(_converter, values);
    }
}