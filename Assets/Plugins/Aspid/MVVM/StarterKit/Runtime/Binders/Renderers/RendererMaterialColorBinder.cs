#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterColor;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class RendererMaterialColorBinder : TargetBinder<Renderer>, IColorBinder
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameter")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);
        
        public RendererMaterialColorBinder(Renderer target, Func<Color, Color> converter)
            : this(target, converter.ToConvert()) { }
        
        public RendererMaterialColorBinder(Renderer target, Converter converter) 
            : this(target, "_BaseColor", converter) { }
        
        public RendererMaterialColorBinder(Renderer target, string colorPropertyName = "_BaseColor") 
            : this(target, colorPropertyName, null as Converter) { }

        public RendererMaterialColorBinder(
            Renderer target,
            string colorPropertyName,
            Func<Color, Color> converter)
            : this(target, colorPropertyName, converter.ToConvert()) { }
        
        public RendererMaterialColorBinder(
            Renderer target,
            string colorPropertyName = "_BaseColor",
            Converter? converter = null)
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