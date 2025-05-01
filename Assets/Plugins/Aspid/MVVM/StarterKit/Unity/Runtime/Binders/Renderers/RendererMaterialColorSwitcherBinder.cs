#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterColor;
#endif

namespace Aspid.MVVM.StarterKit.Unity
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
            BindMode mode) 
            : this(target, trueValue, falseValue, "_BaseColor", null, mode) { }
        
        public RendererMaterialColorSwitcherBinder(
            Renderer target, 
            Color trueValue,
            Color falseValue,
            Converter converter,
            BindMode mode = BindMode.OneWay) 
            : this(target, trueValue, falseValue, "_BaseColor", converter, mode) { }
        
        public RendererMaterialColorSwitcherBinder(
            Renderer target, 
            Color trueValue,
            Color falseValue,
            string colorPropertyName,
            BindMode mode) 
            : this(target, trueValue, falseValue, colorPropertyName, null, mode) { }
        
        public RendererMaterialColorSwitcherBinder(
            Renderer target,
            Color trueValue,
            Color falseValue,
            string colorPropertyName = "_BaseColor",
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
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