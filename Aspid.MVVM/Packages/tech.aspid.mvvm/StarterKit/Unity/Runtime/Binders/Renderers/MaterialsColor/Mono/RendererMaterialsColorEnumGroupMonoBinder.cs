using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupColorMonoBinder{Renderer}"/> that sets a named color property on all materials of each <see cref="Renderer"/> component
    /// in the group based on the bound enum ViewModel value.
    /// The color property name defaults to <c>"_BaseColor"</c> and can be configured in the Inspector.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – MaterialsColor EnumGroup")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "EnumGroup")]
    public sealed class RendererMaterialsColorEnumGroupMonoBinder : EnumGroupColorMonoBinder<Renderer>
    {
        [Tooltip("The name of the shader color property to set on all materials. Defaults to \"_BaseColor\".")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        private ShaderPropertyId _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId.Resolve(_colorPropertyName);
        
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets the named color property on all materials of the element.
        /// </summary>
        /// <param name="element">The component this entry of the group writes to.</param>
        /// <param name="value">The value the bound enum resolved to for this element.</param>
        protected override void SetValue(Renderer element, Color value)
        {
            foreach (var material in element.materials)
                material.SetColor(ColorPropertyId, value);
        }
    }
}