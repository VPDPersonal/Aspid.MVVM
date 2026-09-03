using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="Renderer.materials"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials Switcher")]
    public sealed class RendererMaterialsSwitcherMonoBinder : SwitcherMonoBinder<Renderer, Material[]>
    {
        [Tooltip("Optional converter applied to each material; empty leaves it as-is.")]
        [SerializeReference] private IConverter<Material, Material> _converter;

        /// <inheritdoc/>
        protected override void SetValue(Material[] value) =>
            CachedComponent.SetMaterials(_converter, value);
    }
}
