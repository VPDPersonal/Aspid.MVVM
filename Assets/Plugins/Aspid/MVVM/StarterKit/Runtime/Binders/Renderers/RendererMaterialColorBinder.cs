#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.Mono.Generation;
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
        
        public RendererMaterialColorBinder(Renderer target, BindMode mode) 
            : this(target, "_BaseColor", null, mode) { }
        
        public RendererMaterialColorBinder(Renderer target, Converter converter, BindMode mode = BindMode.OneWay) 
            : this(target, "_BaseColor", converter, mode) { }
        
        public RendererMaterialColorBinder(Renderer target, string colorPropertyName, BindMode mode) 
            : this(target, colorPropertyName, null, mode) { }
        
        public RendererMaterialColorBinder(
            Renderer target,
            string colorPropertyName = "_BaseColor",
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
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