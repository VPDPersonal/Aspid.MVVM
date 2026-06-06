using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public static class BoolConverterSpecificExtensions
    {
        public static IConverterDoubleToBool ToConvert(this Func<double, bool> converter) =>
            new ConverterDoubleToBool(converter);
        
        public static IConverterDoubleToBool ToConvertSpecific(this IConverter<double, bool> converter) =>
            new ConverterDoubleToBool(converter);
        
        public static IConverterFloatToBool ToConvert(this Func<float, bool> converter) =>
            new ConverterFloatToBool(converter);
        
        public static IConverterFloatToBool ToConvertSpecific(this IConverter<float, bool> converter) =>
            new ConverterFloatToBool(converter);
        
        public static IConverterIntToBool ToConvert(this Func<int, bool> converter) =>
            new ConverterIntToBool(converter);
        
        public static IConverterIntToBool ToConvertSpecific(this IConverter<int, bool> converter) =>
            new ConverterIntToBool(converter);
        
        public static IConverterLongToBool ToConvert(this Func<long, bool> converter) =>
            new ConverterLongToBool(converter);
        
        public static IConverterLongToBool ToConvertSpecific(this IConverter<long, bool> converter) =>
            new ConverterLongToBool(converter);
        
        public static IConverterObjectToBool ToConvert(this Func<object?, bool> converter) =>
            new ConverterObjectToBool(converter);
        
        public static IConverterObjectToBool ToConvertSpecific(this IConverter<object?, bool> converter) =>
            new ConverterObjectToBool(converter);
        
        public static IConverterStringToBool ToConvert(this Func<string?, bool> converter) =>
            new ConverterStringToBool(converter);
        
        public static IConverterStringToBool ToConvertSpecific(this IConverter<string?, bool> converter) =>
            new ConverterStringToBool(converter);
        
        private sealed class ConverterDoubleToBool : GenericFuncConverter<double, bool>, IConverterDoubleToBool
        {
            public ConverterDoubleToBool(IConverter<double, bool> converter) 
                : base(converter) { }

            public ConverterDoubleToBool(Func<double, bool> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterFloatToBool : GenericFuncConverter<float, bool>, IConverterFloatToBool
        {
            public ConverterFloatToBool(IConverter<float, bool> converter)
                : base(converter.Convert) { }
            
            public ConverterFloatToBool(Func<float, bool> converter)
                : base(converter) { }
        }
        
        private sealed class ConverterIntToBool : GenericFuncConverter<int, bool>, IConverterIntToBool
        {
            public ConverterIntToBool(IConverter<int, bool> converter)
                : base(converter.Convert) { }
            
            public ConverterIntToBool(Func<int, bool> converter)
                : base(converter) { }
        }
        
        private sealed class ConverterLongToBool : GenericFuncConverter<long, bool>, IConverterLongToBool
        {
            public ConverterLongToBool(IConverter<long, bool> converter)
                : this(converter.Convert) { }
            
            public ConverterLongToBool(Func<long, bool> converter)
                : base(converter) { }
        }
        
        private sealed class ConverterObjectToBool : GenericFuncConverter<object?, bool>, IConverterObjectToBool
        {
            public ConverterObjectToBool(IConverter<object?, bool> converter)
                : base(converter.Convert) { }
            
            public ConverterObjectToBool(Func<object?, bool> converter)
                : base(converter) { }
        }
        
        private sealed class ConverterStringToBool : GenericFuncConverter<string?, bool>, IConverterStringToBool
        {
            public ConverterStringToBool(IConverter<string?, bool> converter) 
                : base(converter.Convert) { }
            
            public ConverterStringToBool(Func<string?, bool> converter)
                : base(converter) { }
        }
    }
}