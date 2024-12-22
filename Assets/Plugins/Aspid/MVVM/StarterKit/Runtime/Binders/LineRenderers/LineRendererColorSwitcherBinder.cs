#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class LineRendererColorSwitcherBinder : SwitcherBinder<Color>
    {
        [SerializeField] private LineRenderer _lineRenderer;
        
        [Header("Parameter")]
        [SerializeField] private LineRendererColorMode _mode;
    
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Color, Color>? _converter;

        public LineRendererColorSwitcherBinder(
            Color trueValue,
            Color falseValue,
            LineRenderer lineRenderer,
            LineRendererColorMode mode,
            Func<Color, Color> converter)
            : this(trueValue, falseValue, lineRenderer, mode, converter.ToConvert()) { }
        
        public LineRendererColorSwitcherBinder(
            Color trueValue,
            Color falseValue,
            LineRenderer lineRenderer,
            LineRendererColorMode mode = LineRendererColorMode.StartAndEnd,
            IConverter<Color, Color>? converter = null)
            : base(trueValue, falseValue)
        {
            _lineRenderer = lineRenderer;
            _mode = mode;
            _converter = converter;
        }

        protected override void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            _lineRenderer.SetColor(value, _mode);
        }
    }
}