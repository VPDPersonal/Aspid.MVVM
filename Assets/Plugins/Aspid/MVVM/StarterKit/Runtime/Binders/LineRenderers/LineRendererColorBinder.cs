#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class LineRendererColorBinder : Binder, IColorBinder
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

        public LineRendererColorBinder(
            LineRenderer lineRenderer,
            LineRendererColorMode mode,
            Func<Color, Color> converter)
            : this(lineRenderer, mode, converter.ToConvert()) { }
        
        public LineRendererColorBinder(
            LineRenderer lineRenderer,
            LineRendererColorMode mode = LineRendererColorMode.StartAndEnd,
            IConverter<Color, Color>? converter = null)
        {
            _lineRenderer = lineRenderer;
            _mode = mode;
            _converter = converter;
        }

        public void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            _lineRenderer.SetColor(value, _mode);
        }
    }
}