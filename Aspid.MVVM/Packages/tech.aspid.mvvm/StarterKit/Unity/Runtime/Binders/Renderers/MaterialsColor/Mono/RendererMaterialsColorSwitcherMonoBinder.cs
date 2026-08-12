using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherColorMonoBinder{Renderer}"/> that switches a named color property on all materials of a <see cref="Renderer"/> component
    /// between two <see cref="Color"/> values based on the bound boolean ViewModel value.
    /// The color property name defaults to <c>"_BaseColor"</c> and can be configured in the Inspector.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – MaterialsColor Switcher")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "Switcher")]
    public sealed class RendererMaterialsColorSwitcherMonoBinder : SwitcherColorMonoBinder<Renderer>
    {
        [Tooltip("The name of the shader color property to set on all materials. Defaults to \"_BaseColor\".")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        private int? _colorPropertyId;
        private Material[] _materials;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);

        /// <summary>
        /// Called when applying the selected value to the material color property.
        /// Sets the named color property on all materials of the <see cref="Renderer"/>.
        /// </summary>
        protected override void SetValue(Color value)
        {
            _materials ??= CachedComponent.materials;

            foreach (var material in _materials)
                material.SetColor(ColorPropertyId, value);
        }

        /// <summary>
        /// Called after unbinding. Clears the cached materials array so it is re-fetched on the next bind.
        /// </summary>
        protected override void OnUnbound()
        {
            _materials = null;
            base.OnUnbound();
        }

    }
}