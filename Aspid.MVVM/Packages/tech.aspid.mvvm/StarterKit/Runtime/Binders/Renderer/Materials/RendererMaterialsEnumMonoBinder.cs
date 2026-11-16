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

        private readonly MaterialBindingScope _materialScope = new();

        /// <inheritdoc/>
        protected override void OnDestroy()
        {
            try
            {
                base.OnDestroy();
            }
            finally
            {
                _materialScope.Dispose();
            }
        }

        /// <inheritdoc/>
        protected override void SetValue(Material[] value) =>
            _materialScope.Set(CachedComponent, _converter, value);

        /// <inheritdoc/>
        protected override void OnDetach()
        {
            _materialScope.Dispose();
            base.OnDetach();
        }
    }
}
