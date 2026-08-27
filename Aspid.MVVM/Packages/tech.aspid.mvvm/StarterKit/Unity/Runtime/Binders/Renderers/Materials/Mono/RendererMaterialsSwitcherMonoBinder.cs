using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{T1, T2}">SwitcherMonoBinder&lt;Renderer, Material[]&gt;</see> that switches the <see cref="Renderer.materials"/> array
    /// between two predefined <see cref="Material"/> arrays based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials Switcher")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "Switcher")]
    public sealed class RendererMaterialsSwitcherMonoBinder : SwitcherMonoBinder<Renderer, Material[]>
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