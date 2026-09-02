using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Renderer, Color}"/> that sets a named color property on all materials of a <see cref="Renderer"/> component.
    /// </summary>
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – MaterialsColor")]
    public class RendererMaterialsColorMonoBinder : ComponentMonoBinder<Renderer, Color>, IColorBinder
    {
        [Tooltip("The name of the shader color property to set on all materials.")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        private ShaderPropertyId _colorPropertyId;
        private Material[] _materials;
        
        private int ColorPropertyId => _colorPropertyId.Resolve(_colorPropertyName);

        protected sealed override Color Property
        {
            get => CachedComponent.sharedMaterial.GetColor(ColorPropertyId);
            set
            {
                _materials ??= CachedComponent.materials;

                foreach (var material in _materials)
                    material.SetColor(ColorPropertyId, value);
            }
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