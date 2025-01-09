using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Renderer/Renderer Binder - MaterialColor Switcher")]
    public sealed class RendererMaterialColorSwitcherMonoBinder : SwitcherMonoBinder<Renderer, Color>
    {
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Color, Color> _converter;
#else
        private IConverterColor _converter;
#endif

        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);

        protected override void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.material.SetColor(ColorPropertyId, value);
        }
    }
}