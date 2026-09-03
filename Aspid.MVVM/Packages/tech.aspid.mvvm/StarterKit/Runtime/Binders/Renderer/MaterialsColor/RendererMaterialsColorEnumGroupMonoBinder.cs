using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets a color property on all materials of each element.
    /// </summary>
    /// <remarks>
    /// Writes to <see cref="Renderer.materials"/>, so the materials are instanced for this renderer.
    /// </remarks>
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – MaterialsColor EnumGroup")]
    public sealed class RendererMaterialsColorEnumGroupMonoBinder : EnumGroupMonoBinder<Renderer, Color>
    {
        [Tooltip("Shader color property set on every material.")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";

        private ShaderPropertyId _colorPropertyId;

        private int ColorPropertyId => _colorPropertyId.Resolve(_colorPropertyName);

        /// <inheritdoc/>
        protected override void SetValue(Renderer element, Color value)
        {
            foreach (var material in element.materials)
                material.SetColor(ColorPropertyId, value);
        }
    }
}
