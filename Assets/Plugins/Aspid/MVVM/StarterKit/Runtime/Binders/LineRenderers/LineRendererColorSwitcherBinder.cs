#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class LineRendererColorSwitcherBinder : SwitcherBinder<LineRenderer, Color>
    {
        [SerializeField] private LineRendererColorMode _mode;
    
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Color, Color>? _converter;

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
            IConverter<Color, Color>? converter)
            : this(target, trueValue, falseValue, LineRendererColorMode.StartAndEnd, converter) { }
        
        public LineRendererColorSwitcherBinder(
            LineRenderer target,
            Color trueValue,
            Color falseValue,
            LineRendererColorMode mode = LineRendererColorMode.StartAndEnd,
            IConverter<Color, Color>? converter = null)
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