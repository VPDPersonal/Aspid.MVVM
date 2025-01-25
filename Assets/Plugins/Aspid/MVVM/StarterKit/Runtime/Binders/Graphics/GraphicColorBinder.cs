#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterColor;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class GraphicColorBinder : TargetBinder<Graphic>, IColorBinder
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public GraphicColorBinder(Graphic target, Func<Color, Color> converter)
            : this(target, converter.ToConvert()) { }
        
        public GraphicColorBinder(Graphic target, Converter? converter = null)
            : base(target)
        {
            _converter = converter;
        }

        public void SetValue(Color value) => 
            Target.color = _converter?.Convert(value) ?? value;
    }
}