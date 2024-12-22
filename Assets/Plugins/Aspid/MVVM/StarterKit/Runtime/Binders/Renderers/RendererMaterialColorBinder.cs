#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class RendererMaterialColorBinder : Binder, IColorBinder
    {
        [Header("Component")]
        [SerializeField] private Renderer _renderer;
        
        [Header("Parameter")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Color, Color>? _converter;

        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);
        
        public RendererMaterialColorBinder(Renderer renderer, Func<Color, Color> converter)
            : this(renderer, converter.ToConvert()) { }
        
        public RendererMaterialColorBinder(Renderer renderer, IConverter<Color, Color> converter) 
            : this(renderer, "_BaseColor", converter) { }
        
        public RendererMaterialColorBinder(Renderer renderer, string colorPropertyName = "_BaseColor") 
            : this(renderer, colorPropertyName, null as IConverter<Color, Color>) { }

        public RendererMaterialColorBinder(
            Renderer renderer,
            string colorPropertyName,
            Func<Color, Color> converter)
            : this(renderer, colorPropertyName, converter.ToConvert()) { }
        
        public RendererMaterialColorBinder(
            Renderer renderer,
            string colorPropertyName = "_BaseColor",
            IConverter<Color, Color>? converter = null)
        {
            _converter = converter;
            _colorPropertyName = colorPropertyName;
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        [BinderLog]
        public void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            _renderer.material.SetColor(ColorPropertyId, value);
        }
    }
}