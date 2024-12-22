#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class GraphicColorBinder : Binder, IColorBinder
    {
        [Header("Component")]
        [SerializeField] private Graphic _graphic;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Color, Color>? _converter;

        public GraphicColorBinder(Graphic graphic, Func<Color, Color> converter)
            : this(graphic, converter.ToConvert()) { }
        
        public GraphicColorBinder(Graphic graphic, IConverter<Color, Color>? converter = null)
        {
            _converter = converter;
            _graphic = graphic ?? throw new ArgumentNullException(nameof(graphic));
        }

        public void SetValue(Color value) => 
            _graphic.color = _converter?.Convert(value) ?? value;
    }
}