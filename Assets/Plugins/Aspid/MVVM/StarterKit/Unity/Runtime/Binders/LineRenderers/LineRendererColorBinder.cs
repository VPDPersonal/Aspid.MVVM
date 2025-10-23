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
    public class LineRendererColorBinder : TargetBinder<LineRenderer>, IColorBinder
    {
        [SerializeField] private LineRendererColorMode _colorMode;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public LineRendererColorBinder(
            LineRenderer target,
            BindMode mode)
            : this(target, LineRendererColorMode.StartAndEnd, null, mode) { }
        
        public LineRendererColorBinder(
            LineRenderer target,
            LineRendererColorMode colorMode,
            BindMode mode)
            : this(target, colorMode, null, mode) { }
        
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
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _colorMode = colorMode;
            _converter = converter;
        }

        public void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.SetColor(value, _colorMode);
        }
    }
}