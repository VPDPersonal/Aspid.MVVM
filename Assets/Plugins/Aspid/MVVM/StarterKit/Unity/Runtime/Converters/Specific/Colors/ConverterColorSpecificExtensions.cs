#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public static class ConverterColorSpecificExtensions
    {
        public static IConverterColor ToConvert(this Func<Color, Color> converter) =>
            new ConverterColor(converter);
        
        public static IConverterColor ToConvertSpecific(this IConverter<Color, Color> converter) =>
            new ConverterColor(converter);
        
        public static IConverterStringToColor ToConvert(this Func<string?, Color> converter) =>
            new ConverterStringToColor(converter);
        
        public static IConverterStringToColor ToConvertSpecific(this IConverter<string?, Color> converter) =>
            new ConverterStringToColor(converter);
        
        private sealed class ConverterColor : GenericFuncConverter<Color, Color>, IConverterColor
        {
            public ConverterColor(IConverter<Color, Color> converter) 
                : base(converter) { }

            public ConverterColor(Func<Color, Color> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterStringToColor : GenericFuncConverter<string?, Color>, IConverterStringToColor
        {
            public ConverterStringToColor(IConverter<string?, Color> converter)
                : base(converter) { }

            public ConverterStringToColor(Func<string?, Color> converter)
                : base(converter) { }
        }
    }
}