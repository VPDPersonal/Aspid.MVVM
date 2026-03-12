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
    /// <see cref="EnumMonoBinder{Renderer, Material[]}"/> that sets the <see cref="Renderer.materials"/> array
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials Enum")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "Enum")]
    public sealed class RendererMaterialsEnumMonoBinder : EnumMonoBinder<Renderer, Material[]>
    {
        [Tooltip("The optional converter applied to each material before it is assigned to the Renderer.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets the <see cref="Renderer.materials"/> array, applying the optional converter to each material.
        /// </summary>
        protected override void SetValue(Material[] values) =>
            CachedComponent.SetMaterials(_converter, values);
    }
}