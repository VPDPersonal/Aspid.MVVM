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
    public class LineRendererColorBinder : TargetColorBinder<LineRenderer>
    {
        [SerializeField] private LineRendererColorMode _colorMode;
        
        protected sealed override Color Property
        {
            get =>  Target.GetColor(_colorMode);
            set =>  Target.SetColor(value, _colorMode);
        }
        
        public LineRendererColorBinder(
            LineRenderer target,
            BindMode mode)
            : this(target, LineRendererColorMode.StartAndEnd, converter: null, mode) { }
        
        public LineRendererColorBinder(
            LineRenderer target,
            LineRendererColorMode colorMode,
            BindMode mode)
            : this(target, colorMode, converter: null, mode) { }
        
        public LineRendererColorBinder(
            LineRenderer target,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : this(target, LineRendererColorMode.StartAndEnd, converter, mode) { }
        
        public LineRendererColorBinder(
            LineRenderer target,
            LineRendererColorMode colorMode = LineRendererColorMode.StartAndEnd,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _colorMode = colorMode;
        }
    }
}