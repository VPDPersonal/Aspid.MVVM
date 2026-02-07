using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – MaterialsColor Switcher")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "Switcher")]
    public sealed class RendererMaterialsColorSwitcherMonoBinder : SwitcherMonoBinder<Renderer, Color>
    {
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);

        protected override void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;

            foreach (var material in CachedComponent.materials)
                material.SetColor(ColorPropertyId, value);
        }
    }
}