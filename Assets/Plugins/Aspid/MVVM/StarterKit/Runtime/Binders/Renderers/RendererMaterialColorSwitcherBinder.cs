#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterColor;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class RendererMaterialColorSwitcherBinder : SwitcherBinder<Renderer, Color>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

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
            Converter converter) 
            : this(target, trueValue, falseValue, "_BaseColor", converter) { }
        
        public RendererMaterialColorSwitcherBinder(
            Renderer target, 
            Color trueValue,
            Color falseValue,
            string colorPropertyName = "_BaseColor") 
            : this(target, trueValue, falseValue, colorPropertyName, null as Converter) { }

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
            Converter? converter = null)
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