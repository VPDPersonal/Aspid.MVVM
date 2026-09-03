using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets a color property on all materials of
    /// a <see cref="Renderer"/>.
    /// </summary>
    /// <remarks>
    /// Writes to <see cref="Renderer.materials"/>, so the materials are instanced for this renderer.
    /// </remarks>
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – MaterialsColor Enum")]
    public sealed class RendererMaterialsColorEnumMonoBinder : EnumMonoBinder<Renderer, Color>
    {
        [Tooltip("Shader color property set on every material.")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";

        private ShaderPropertyId _colorPropertyId;
        private Material[] _materials;

        private int ColorPropertyId => _colorPropertyId.Resolve(_colorPropertyName);

        /// <inheritdoc/>
        protected override void SetValue(Color value)
        {
            _materials ??= CachedComponent.materials;

            foreach (var material in _materials)
                material.SetColor(ColorPropertyId, value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            _materials = null;
            base.OnUnbound();
        }
    }
}
