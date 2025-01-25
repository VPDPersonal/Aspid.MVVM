#nullable enable
using System;

namespace Aspid.MVVM.StarterKit.Converters
{
    public static class StringConverter2022Extensions
    {
        public static IConverterString ToConvert(this Func<string?, string?> converter) =>
            new ConverterString(converter);
        
        public static IConverterString ToConvert2022(this IConverter<string?, string?> converter) =>
            new ConverterString(converter);
        
        public static IConverterObjectToString ToConvert(this Func<object?, string?> converter) =>
            new ConverterObjectToString(converter);
        
        public static IConverterObjectToString ToConvert2022(this IConverter<object?, string?> converter) =>
            new ConverterObjectToString(converter);
        
        public static IConverterTimeSpanToString ToConvert(this Func<TimeSpan, string?> converter) =>
            new ConverterTimeSpanToString(converter);
        
        public static IConverterTimeSpanToString ToConvert2022(this IConverter<TimeSpan, string?> converter) =>
            new ConverterTimeSpanToString(converter);
        
        private sealed class ConverterString : GenericFuncConverter<string?, string?>, IConverterString
        {
            public ConverterString(IConverter<string?, string?> converter) 
                : base(converter) { }

            public ConverterString(Func<string?, string?> converter)
                : base(converter) { }
        }
        
        private sealed class ConverterObjectToString : GenericFuncConverter<object?, string?>, IConverterObjectToString
        {
            public ConverterObjectToString(IConverter<object?, string?> converter)
                : base(converter) { }

            public ConverterObjectToString(Func<object?, string?> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterTimeSpanToString : GenericFuncConverter<TimeSpan, string?>, IConverterTimeSpanToString
        {
            public ConverterTimeSpanToString(IConverter<TimeSpan, string?> converter) 
                : base(converter) { }

            public ConverterTimeSpanToString(Func<TimeSpan, string?> converter) 
                : base(converter) { }
        }
    }
}