using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentColorMonoBinder{Renderer}"/> that sets a named color property on all materials of a <see cref="Renderer"/> component.
    /// The color property name defaults to <c>"_BaseColor"</c> and can be configured in the Inspector.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current color value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – MaterialsColor")]
    public class RendererMaterialsColorMonoBinder : ComponentColorMonoBinder<Renderer>
    {
        [Tooltip("The name of the shader color property to set on all materials. Defaults to \"_BaseColor\".")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);

        protected sealed override Color Property
        {
            get => CachedComponent.material.GetColor(ColorPropertyId);
            set
            {
                foreach (var material in CachedComponent.materials)
                    material.SetColor(ColorPropertyId, value);
            }
        }
    }
}