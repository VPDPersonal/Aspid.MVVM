using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterColor;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder - MaterialColor Enum")]
    public sealed class RendererMaterialColorEnumMonoBinder : EnumComponentMonoBinder<Renderer, Color>
    {
        [Header("Parameter")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";

        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);

        protected override void SetValue(Color value) 
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.material.SetColor(ColorPropertyId, value);
        }
    }
}