#nullable enable
using System;

namespace Aspid.MVVM.StarterKit.Unity
{
    public static class NumberConverter2022Extensions
    {
        #region Int Methods
        public static IConverterInt ToConvert(this Func<int, int> converter) =>
            new ConverterInt(converter);
        
        public static IConverterInt ToConvert2022(this IConverter<int, int> converter) =>
            new ConverterInt(converter);
        
        public static IConverterIntToLong ToConvert(this Func<int, long> converter) =>
            new ConverterIntToLong(converter);
        
        public static IConverterIntToLong ToConvert2022(this IConverter<int, long> converter) =>
            new ConverterIntToLong(converter);
        
        public static IConverterIntToFloat ToConvert(this Func<int, float> converter) =>
            new ConverterIntToFloat(converter);
        
        public static IConverterIntToFloat ToConvert2022(this IConverter<int, float> converter) =>
            new ConverterIntToFloat(converter);
        
        public static IConverterIntToDouble ToConvert(this Func<int, double> converter) =>
            new ConverterIntToDouble(converter);
        
        public static IConverterIntToDouble ToConvert2022(this IConverter<int, double> converter) =>
            new ConverterIntToDouble(converter);
        #endregion
        
        #region Long Methods
        public static IConverterLongToInt ToConvert(this Func<long, int> converter) =>
            new ConverterLongToInt(converter);
        
        public static IConverterLongToInt ToConvert2022(this IConverter<long, int> converter) =>
            new ConverterLongToInt(converter);
        
        public static IConverterLong ToConvert(this Func<long, long> converter) =>
            new ConverterLong(converter);
        
        public static IConverterLong ToConvert2022(this IConverter<long, long> converter) =>
            new ConverterLong(converter);
        
        public static IConverterLongToFloat ToConvert(this Func<long, float> converter) =>
            new ConverterLongToFloat(converter);
        
        public static IConverterLongToFloat ToConvert2022(this IConverter<long, float> converter) =>
            new ConverterLongToFloat(converter);
        
        public static IConverterLongToDouble ToConvert(this Func<long, double> converter) =>
            new ConverterLongToDouble(converter);
        
        public static IConverterLongToDouble ToConvert2022(this IConverter<long, double> converter) =>
            new ConverterLongToDouble(converter);
        #endregion
        
        #region Float Methods
        public static IConverterFloatToInt ToConvert(this Func<float, int> converter) =>
            new ConverterFloatToInt(converter);
        
        public static IConverterFloatToInt ToConvert2022(this IConverter<float, int> converter) =>
            new ConverterFloatToInt(converter);
        
        public static IConverterFloatToLong ToConvert(this Func<float, long> converter) =>
            new ConverterFloatToLong(converter);
        
        public static IConverterFloatToLong ToConvert2022(this IConverter<float, long> converter) =>
            new ConverterFloatToLong(converter);
        
        public static IConverterFloat ToConvert(this Func<float, float> converter) =>
            new ConverterFloat(converter);
        
        public static IConverterFloat ToConvert2022(this IConverter<float, float> converter) =>
            new ConverterFloat(converter);
        
        public static IConverterFloatToDouble ToConvert(this Func<float, double> converter) =>
            new ConverterFloatToDouble(converter);
        
        public static IConverterFloatToDouble ToConvert2022(this IConverter<float, double> converter) =>
            new ConverterFloatToDouble(converter);
        #endregion
        
        #region Double Methods
        public static IConverterDoubleToInt ToConvert(this Func<double, int> converter) =>
            new ConverterDoubleToInt(converter);
        
        public static IConverterDoubleToInt ToConvert2022(this IConverter<double, int> converter) =>
            new ConverterDoubleToInt(converter);
        
        public static IConverterDoubleToLong ToConvert(this Func<double, long> converter) =>
            new ConverterDoubleToLong(converter);
        
        public static IConverterDoubleToLong ToConvert2022(this IConverter<double, long> converter) =>
            new ConverterDoubleToLong(converter);
        
        public static IConverterDoubleToFloat ToConvert(this Func<double, float> converter) =>
            new ConverterDoubleToFloat(converter);
        
        public static IConverterDoubleToFloat ToConvert2022(this IConverter<double, float> converter) =>
            new ConverterDoubleToFloat(converter);
        
        public static IConverterDouble ToConvert(this Func<double, double> converter) =>
            new ConverterDouble(converter);
        
        public static IConverterDouble ToConvert2022(this IConverter<double, double> converter) =>
            new ConverterDouble(converter);
        #endregion
        
        #region Int Clasess
        private sealed class ConverterInt : GenericFuncConverter<int, int>, IConverterInt
        {
            public ConverterInt(IConverter<int, int> converter) 
                : base(converter) { }

            public ConverterInt(Func<int, int> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterIntToLong : GenericFuncConverter<int, long>, IConverterIntToLong
        {
            public ConverterIntToLong(IConverter<int, long> converter) 
                : base(converter) { }

            public ConverterIntToLong(Func<int, long> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterIntToFloat : GenericFuncConverter<int, float>, IConverterIntToFloat
        {
            public ConverterIntToFloat(IConverter<int, float> converter) 
                : base(converter) { }

            public ConverterIntToFloat(Func<int, float> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterIntToDouble : GenericFuncConverter<int, double>, IConverterIntToDouble
        {
            public ConverterIntToDouble(IConverter<int, double> converter) 
                : base(converter) { }

            public ConverterIntToDouble(Func<int, double> converter) 
                : base(converter) { }
        }
        #endregion
        
        #region Long Classes
        private sealed class ConverterLongToInt : GenericFuncConverter<long, int>, IConverterLongToInt
        {
            public ConverterLongToInt(IConverter<long, int> converter) 
                : base(converter) { }

            public ConverterLongToInt(Func<long, int> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterLong : GenericFuncConverter<long, long>, IConverterLong
        {
            public ConverterLong(IConverter<long, long> converter) 
                : base(converter) { }

            public ConverterLong(Func<long, long> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterLongToFloat : GenericFuncConverter<long, float>, IConverterLongToFloat
        {
            public ConverterLongToFloat(IConverter<long, float> converter) 
                : base(converter) { }

            public ConverterLongToFloat(Func<long, float> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterLongToDouble : GenericFuncConverter<long, double>, IConverterLongToDouble
        {
            public ConverterLongToDouble(IConverter<long, double> converter) 
                : base(converter) { }

            public ConverterLongToDouble(Func<long, double> converter) 
                : base(converter) { }
        }
        #endregion
        
        #region Float Classes
        private sealed class ConverterFloatToInt : GenericFuncConverter<float, int>, IConverterFloatToInt
        {
            public ConverterFloatToInt(IConverter<float, int> converter) 
                : base(converter) { }

            public ConverterFloatToInt(Func<float, int> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterFloatToLong : GenericFuncConverter<float, long>, IConverterFloatToLong
        {
            public ConverterFloatToLong(IConverter<float, long> converter) 
                : base(converter) { }

            public ConverterFloatToLong(Func<float, long> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterFloat : GenericFuncConverter<float, float>, IConverterFloat
        {
            public ConverterFloat(IConverter<float, float> converter) 
                : base(converter) { }

            public ConverterFloat(Func<float, float> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterFloatToDouble : GenericFuncConverter<float, double>, IConverterFloatToDouble
        {
            public ConverterFloatToDouble(IConverter<float, double> converter) 
                : base(converter) { }

            public ConverterFloatToDouble(Func<float, double> converter) 
                : base(converter) { }
        }
        #endregion
        
        #region Double Classes
        private sealed class ConverterDoubleToInt : GenericFuncConverter<double, int>, IConverterDoubleToInt
        {
            public ConverterDoubleToInt(IConverter<double, int> converter) 
                : base(converter) { }

            public ConverterDoubleToInt(Func<double, int> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterDoubleToLong : GenericFuncConverter<double, long>, IConverterDoubleToLong
        {
            public ConverterDoubleToLong(IConverter<double, long> converter) 
                : base(converter) { }

            public ConverterDoubleToLong(Func<double, long> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterDoubleToFloat : GenericFuncConverter<double, float>, IConverterDoubleToFloat
        {
            public ConverterDoubleToFloat(IConverter<double, float> converter) 
                : base(converter) { }

            public ConverterDoubleToFloat(Func<double, float> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterDouble : GenericFuncConverter<double, double>, IConverterDouble
        {
            public ConverterDouble(IConverter<double, double> converter) 
                : base(converter) { }

            public ConverterDouble(Func<double, double> converter) 
                : base(converter) { }
        }
        #endregion
    }
}