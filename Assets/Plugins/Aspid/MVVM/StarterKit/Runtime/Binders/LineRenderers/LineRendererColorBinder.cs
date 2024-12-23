#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class LineRendererColorBinder : TargetBinder<LineRenderer>, IColorBinder
    {
        [Header("Parameter")]
        [SerializeField] private LineRendererColorMode _mode;
    
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Color, Color>? _converter;

        public LineRendererColorBinder(
            LineRenderer target,
            LineRendererColorMode mode,
            Func<Color, Color> converter)
            : this(target, mode, converter.ToConvert()) { }
        
        public LineRendererColorBinder(
            LineRenderer target,
            LineRendererColorMode mode = LineRendererColorMode.StartAndEnd,
            IConverter<Color, Color>? converter = null)
            : base(target)
        {
            _mode = mode;
            _converter = converter;
        }

        public void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.SetColor(value, _mode);
        }
    }
}