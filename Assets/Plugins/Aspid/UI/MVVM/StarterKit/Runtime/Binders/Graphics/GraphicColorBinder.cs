#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Graphics
{
    public class GraphicColorBinder : Binder, IColorBinder
    {
        private readonly Graphic _graphic;
        private readonly IConverter<Color, Color>? _converter;

        public GraphicColorBinder(Graphic graphic, Func<Color, Color> converter)
            : this(graphic, new GenericFuncConverter<Color, Color>(converter)) { }
        
        public GraphicColorBinder(Graphic graphic, IConverter<Color, Color>? converter = null)
        {
            _converter = converter;
            _graphic = graphic ?? throw new ArgumentNullException(nameof(graphic));
        }

        public void SetValue(Color value) => 
            _graphic.color = _converter?.Convert(value) ?? value;
    }
}