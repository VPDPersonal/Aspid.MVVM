using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets a color property on all materials of a <see cref="Renderer"/> component
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – MaterialsColor")]
    public class RendererMaterialsColorMonoBinder : ComponentColorMonoBinder<Renderer>, IColorBinder
    {
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