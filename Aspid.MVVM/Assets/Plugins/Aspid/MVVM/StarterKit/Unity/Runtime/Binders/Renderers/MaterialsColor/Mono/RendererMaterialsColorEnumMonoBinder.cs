using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumColorMonoBinder{Renderer}"/> that sets a named color property on all materials of a <see cref="Renderer"/> component
    /// based on the bound enum ViewModel value.
    /// The color property name defaults to <c>"_BaseColor"</c> and can be configured in the Inspector.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – MaterialsColor Enum")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "Enum")]
    public sealed class RendererMaterialsColorEnumMonoBinder : EnumColorMonoBinder<Renderer>
    {
        [Tooltip("The name of the shader color property to set on all materials. Defaults to \"_BaseColor\".")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);

        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets the named color property on all materials of the <see cref="Renderer"/>.
        /// </summary>
        protected override void SetValue(Color value) 
        {
            foreach (var material in CachedComponent.materials)
                material.SetColor(ColorPropertyId, value);
        }
    }
}