#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class GraphicColorBinder : TargetBinder<Graphic>, IColorBinder
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Color, Color>? _converter;

        public GraphicColorBinder(Graphic target, Func<Color, Color> converter)
            : this(target, converter.ToConvert()) { }
        
        public GraphicColorBinder(Graphic target, IConverter<Color, Color>? converter = null)
            : base(target)
        {
            _converter = converter;
        }

        public void SetValue(Color value) => 
            Target.color = _converter?.Convert(value) ?? value;
    }
}