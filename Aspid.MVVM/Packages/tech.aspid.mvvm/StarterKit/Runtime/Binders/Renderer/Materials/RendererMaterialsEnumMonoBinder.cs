using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Renderer.materials"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials Enum")]
    public sealed class RendererMaterialsEnumMonoBinder : EnumMonoBinder<Renderer, Material[]>
    {
        [Tooltip("Optional converter applied to each material; empty leaves it as-is.")]
        [SerializeReference] private IConverter<Material, Material> _converter;

        /// <inheritdoc/>
        protected override void SetValue(Material[] value) =>
            CachedComponent.SetMaterials(_converter, value);
    }
}
