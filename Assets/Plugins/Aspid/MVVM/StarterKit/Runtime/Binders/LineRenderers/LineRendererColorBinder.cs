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
    public class LineRendererColorBinder : TargetBinder<LineRenderer>, IColorBinder
    {
        [Header("Parameter")]
        [SerializeField] private LineRendererColorMode _mode;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public LineRendererColorBinder(
            LineRenderer target,
            LineRendererColorMode mode,
            Func<Color, Color> converter)
            : this(target, mode, converter.ToConvert()) { }
        
        public LineRendererColorBinder(
            LineRenderer target,
            LineRendererColorMode mode = LineRendererColorMode.StartAndEnd,
            Converter? converter = null)
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