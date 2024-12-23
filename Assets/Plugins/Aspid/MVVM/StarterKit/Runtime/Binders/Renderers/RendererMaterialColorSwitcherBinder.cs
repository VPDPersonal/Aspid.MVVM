#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class RendererMaterialColorSwitcherBinder : SwitcherBinder<Renderer, Color>
    {
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Color, Color>? _converter;

        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);
        
        public RendererMaterialColorSwitcherBinder(
            Renderer target, 
            Color trueValue,
            Color falseValue,
            Func<Color, Color> converter)
            : this(target, trueValue, falseValue, converter.ToConvert()) { }
        
        public RendererMaterialColorSwitcherBinder(
            Renderer target, 
            Color trueValue,
            Color falseValue,
            IConverter<Color, Color> converter) 
            : this(target, trueValue, falseValue, "_BaseColor", converter) { }
        
        public RendererMaterialColorSwitcherBinder(
            Renderer target, 
            Color trueValue,
            Color falseValue,
            string colorPropertyName = "_BaseColor") 
            : this(target, trueValue, falseValue, colorPropertyName, null as IConverter<Color, Color>) { }

        public RendererMaterialColorSwitcherBinder(
            Renderer target,
            Color trueValue,
            Color falseValue,
            string colorPropertyName,
            Func<Color, Color> converter)
            : this(target, trueValue, falseValue, colorPropertyName, converter.ToConvert()) { }
        
        public RendererMaterialColorSwitcherBinder(
            Renderer target,
            Color trueValue,
            Color falseValue,
            string colorPropertyName = "_BaseColor",
            IConverter<Color, Color>? converter = null)
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
            _colorPropertyName = colorPropertyName; 
        }
        
        protected override void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.material.SetColor(ColorPropertyId, value);
        }
    }
}