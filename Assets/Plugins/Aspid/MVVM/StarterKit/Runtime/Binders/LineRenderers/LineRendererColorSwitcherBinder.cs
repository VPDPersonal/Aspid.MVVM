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
    public sealed class LineRendererColorSwitcherBinder : SwitcherBinder<LineRenderer, Color>
    {
        [SerializeField] private LineRendererColorMode _mode;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public LineRendererColorSwitcherBinder(
            LineRenderer target,
            Color trueValue,
            Color falseValue,
            LineRendererColorMode mode,
            Func<Color, Color> converter)
            : this(target, trueValue, falseValue, mode, converter.ToConvert()) { }
        
        public LineRendererColorSwitcherBinder(
            LineRenderer target,
            Color trueValue,
            Color falseValue,
            Func<Color, Color> converter)
            : this(target, trueValue, falseValue, LineRendererColorMode.StartAndEnd, converter.ToConvert()) { }
        
        public LineRendererColorSwitcherBinder(
            LineRenderer target,
            Color trueValue,
            Color falseValue,
            Converter? converter)
            : this(target, trueValue, falseValue, LineRendererColorMode.StartAndEnd, converter) { }
        
        public LineRendererColorSwitcherBinder(
            LineRenderer target,
            Color trueValue,
            Color falseValue,
            LineRendererColorMode mode = LineRendererColorMode.StartAndEnd,
            Converter? converter = null)
            : base(target, trueValue, falseValue)
        {
            _mode = mode;
            _converter = converter;
        }

        protected override void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.SetColor(value, _mode);
        }
    }
}