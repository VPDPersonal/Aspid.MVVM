#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class RendererMaterialColorBinder : TargetColorBinder<Renderer>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);
        
        protected sealed override Color Property
        {
            get => Target.material.GetColor(ColorPropertyId);
            set
            {
                foreach (var material in Target.materials)
                    material.SetColor(ColorPropertyId, value);
            }
        }
        
        public RendererMaterialColorBinder(Renderer target, BindMode mode) 
            : this(target, colorPropertyName: "_BaseColor", converter: null, mode) { }
        
        public RendererMaterialColorBinder(Renderer target, Converter converter, BindMode mode = BindMode.OneWay) 
            : this(target, colorPropertyName: "_BaseColor", converter, mode) { }
        
        public RendererMaterialColorBinder(Renderer target, string colorPropertyName, BindMode mode) 
            : this(target, colorPropertyName, converter: null, mode) { }
        
        public RendererMaterialColorBinder(
            Renderer target,
            string colorPropertyName = "_BaseColor",
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _colorPropertyName = colorPropertyName;
        }
    }
}