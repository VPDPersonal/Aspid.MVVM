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
    public sealed class LineRendererColorSwitcherBinder : SwitcherBinder<LineRenderer, Color>
    {
        [SerializeField] private LineRendererColorMode _colorMode;
        
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public LineRendererColorSwitcherBinder(
            LineRenderer target,
            Color trueValue,
            Color falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, LineRendererColorMode.StartAndEnd, null, mode) { }
        
        public LineRendererColorSwitcherBinder(
            LineRenderer target,
            Color trueValue,
            Color falseValue,
            LineRendererColorMode colorMode,
            BindMode mode)
            : this(target, trueValue, falseValue, colorMode, null, mode) { }
        
        public LineRendererColorSwitcherBinder(
            LineRenderer target,
            Color trueValue,
            Color falseValue,
            Converter? converter,
            BindMode mode = BindMode.OneWay)
            : this(target, trueValue, falseValue, LineRendererColorMode.StartAndEnd, converter, mode) { }
        
        public LineRendererColorSwitcherBinder(
            LineRenderer target,
            Color trueValue,
            Color falseValue,
            LineRendererColorMode colorMode = LineRendererColorMode.StartAndEnd,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            _colorMode = colorMode;
            _converter = converter;
        }

        protected override void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.SetColor(value, _colorMode);
        }
    }
}