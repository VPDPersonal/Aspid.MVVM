#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class RendererMaterialColorSwitcherBinder : SwitcherBinder<Color>
    {
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        [Header("Component")]
        [SerializeField] private Renderer _renderer;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Color, Color>? _converter;

        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);
        
        public RendererMaterialColorSwitcherBinder(
            Color trueValue,
            Color falseValue,
            Renderer renderer, 
            Func<Color, Color> converter)
            : this(trueValue, falseValue, renderer, converter.ToConvert()) { }
        
        public RendererMaterialColorSwitcherBinder(
            Color trueValue,
            Color falseValue,
            Renderer renderer, 
            IConverter<Color, Color> converter) 
            : this(trueValue, falseValue, renderer, "_BaseColor", converter) { }
        
        public RendererMaterialColorSwitcherBinder(
            Color trueValue,
            Color falseValue,
            Renderer renderer, 
            string colorPropertyName = "_BaseColor") 
            : this(trueValue, falseValue, renderer, colorPropertyName, null as IConverter<Color, Color>) { }

        public RendererMaterialColorSwitcherBinder(
            Color trueValue,
            Color falseValue,
            Renderer renderer,
            string colorPropertyName,
            Func<Color, Color> converter)
            : this(trueValue, falseValue, renderer, colorPropertyName, converter.ToConvert()) { }
        
        public RendererMaterialColorSwitcherBinder(
            Color trueValue,
            Color falseValue,
            Renderer renderer,
            string colorPropertyName = "_BaseColor",
            IConverter<Color, Color>? converter = null)
            : base(trueValue, falseValue)
        {
            _converter = converter;
            _colorPropertyName = colorPropertyName;
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }
        
        protected override void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            _renderer.material.SetColor(ColorPropertyId, value);
        }
    }
}