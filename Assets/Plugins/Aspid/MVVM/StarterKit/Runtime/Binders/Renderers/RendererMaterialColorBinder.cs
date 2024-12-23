#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class RendererMaterialColorBinder : TargetBinder<Renderer>, IColorBinder
    {
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
        
        public RendererMaterialColorBinder(Renderer target, Func<Color, Color> converter)
            : this(target, converter.ToConvert()) { }
        
        public RendererMaterialColorBinder(Renderer target, IConverter<Color, Color> converter) 
            : this(target, "_BaseColor", converter) { }
        
        public RendererMaterialColorBinder(Renderer target, string colorPropertyName = "_BaseColor") 
            : this(target, colorPropertyName, null as IConverter<Color, Color>) { }

        public RendererMaterialColorBinder(
            Renderer target,
            string colorPropertyName,
            Func<Color, Color> converter)
            : this(target, colorPropertyName, converter.ToConvert()) { }
        
        public RendererMaterialColorBinder(
            Renderer target,
            string colorPropertyName = "_BaseColor",
            IConverter<Color, Color>? converter = null)
            : base(target)
        {
            _converter = converter;
            _colorPropertyName = colorPropertyName;
        }

        [BinderLog]
        public void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.material.SetColor(ColorPropertyId, value);
        }
    }
}