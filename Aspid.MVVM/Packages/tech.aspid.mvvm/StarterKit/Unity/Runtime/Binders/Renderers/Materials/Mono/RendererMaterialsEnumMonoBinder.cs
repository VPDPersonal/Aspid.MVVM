using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{T1, T2}">EnumMonoBinder&lt;Renderer, Material[]&gt;</see> that sets the <see cref="Renderer.materials"/> array
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials Enum")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "Enum")]
    public sealed class RendererMaterialsEnumMonoBinder : EnumMonoBinder<Renderer, Material[]>
    {
        [Tooltip("The optional converter applied to each material before assignment.")]
        [SerializeReference] private Converter _converter;

        /// <summary>
        /// Sets the <see cref="Renderer.materials"/> array, applying the optional converter to each material.
        /// </summary>
        /// <param name="values">The materials received from the ViewModel.</param>
        protected override void SetValue(Material[] values) =>
            CachedComponent.SetMaterials(_converter, values);
    }
}