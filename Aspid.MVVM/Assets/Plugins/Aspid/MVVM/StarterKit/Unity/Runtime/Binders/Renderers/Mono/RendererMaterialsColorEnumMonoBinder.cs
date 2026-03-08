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
    /// MonoBehaviour binder that sets the material color property on a <see cref="Renderer"/> component
    /// to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – MaterialsColor Enum")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "Enum")]
    public sealed class RendererMaterialsColorEnumMonoBinder : EnumMonoBinder<Renderer, Color, Converter>
    {
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);

        protected override void SetValue(Color value) 
        {
            foreach (var material in CachedComponent.materials)
                material.SetColor(ColorPropertyId, value);
        }
    }
}