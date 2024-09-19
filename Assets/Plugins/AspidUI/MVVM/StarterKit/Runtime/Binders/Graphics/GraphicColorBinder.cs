using System;
using UnityEngine;
using UnityEngine.UI;
using AspidUI.MVVM.StarterKit.Converters;

namespace AspidUI.MVVM.StarterKit.Binders.Graphics
{
    public class GraphicColorBinder : Binder, IColorBinder
    {
        protected readonly Graphic Graphic;
        protected readonly IConverter<Color, Color> Converter;

        public GraphicColorBinder(Graphic graphic, Func<Color, Color> converter)
            : this(graphic, new GenericFuncConverter<Color, Color>(converter)) { }
        
        public GraphicColorBinder(Graphic graphic, IConverter<Color, Color> converter = null)
        {
            Graphic = graphic;
            Converter = converter;
        }

        public void SetValue(Color value) =>
            Graphic.color = Converter?.Convert(value) ?? value;
    }
}