using System;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a function or a generic converter as one of the named numeric converter
    /// interfaces.
    /// </summary>
    /// <remarks>
    /// <c>ToConvert</c> takes a function, <c>ToConvertSpecific</c> takes a converter that is
    /// already the right shape but not the named type. Both exist because a binder before
    /// Unity 2023.1 declares its field as the named interface rather than the generic one,
    /// so a lambda cannot be assigned to it directly.
    /// </remarks>
    public static class NumberConverterSpecificExtensions
    {
        #region Int Methods
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterInt"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterInt ToConvert(this Func<int, int> converter) =>
            new ConverterInt(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterInt"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterInt ToConvertSpecific(this IConverter<int, int> converter) =>
            new ConverterInt(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterIntToLong"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterIntToLong ToConvert(this Func<int, long> converter) =>
            new ConverterIntToLong(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterIntToLong"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterIntToLong ToConvertSpecific(this IConverter<int, long> converter) =>
            new ConverterIntToLong(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterIntToFloat"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterIntToFloat ToConvert(this Func<int, float> converter) =>
            new ConverterIntToFloat(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterIntToFloat"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterIntToFloat ToConvertSpecific(this IConverter<int, float> converter) =>
            new ConverterIntToFloat(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterIntToDouble"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterIntToDouble ToConvert(this Func<int, double> converter) =>
            new ConverterIntToDouble(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterIntToDouble"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterIntToDouble ToConvertSpecific(this IConverter<int, double> converter) =>
            new ConverterIntToDouble(converter);
        #endregion
        
        #region Long Methods
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterLongToInt"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterLongToInt ToConvert(this Func<long, int> converter) =>
            new ConverterLongToInt(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterLongToInt"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterLongToInt ToConvertSpecific(this IConverter<long, int> converter) =>
            new ConverterLongToInt(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterLong"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterLong ToConvert(this Func<long, long> converter) =>
            new ConverterLong(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterLong"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterLong ToConvertSpecific(this IConverter<long, long> converter) =>
            new ConverterLong(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterLongToFloat"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterLongToFloat ToConvert(this Func<long, float> converter) =>
            new ConverterLongToFloat(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterLongToFloat"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterLongToFloat ToConvertSpecific(this IConverter<long, float> converter) =>
            new ConverterLongToFloat(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterLongToDouble"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterLongToDouble ToConvert(this Func<long, double> converter) =>
            new ConverterLongToDouble(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterLongToDouble"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterLongToDouble ToConvertSpecific(this IConverter<long, double> converter) =>
            new ConverterLongToDouble(converter);
        #endregion
        
        #region Float Methods
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterFloatToInt"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterFloatToInt ToConvert(this Func<float, int> converter) =>
            new ConverterFloatToInt(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterFloatToInt"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterFloatToInt ToConvertSpecific(this IConverter<float, int> converter) =>
            new ConverterFloatToInt(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterFloatToLong"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterFloatToLong ToConvert(this Func<float, long> converter) =>
            new ConverterFloatToLong(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterFloatToLong"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterFloatToLong ToConvertSpecific(this IConverter<float, long> converter) =>
            new ConverterFloatToLong(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterFloat"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterFloat ToConvert(this Func<float, float> converter) =>
            new ConverterFloat(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterFloat"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterFloat ToConvertSpecific(this IConverter<float, float> converter) =>
            new ConverterFloat(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterFloatToDouble"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterFloatToDouble ToConvert(this Func<float, double> converter) =>
            new ConverterFloatToDouble(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterFloatToDouble"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterFloatToDouble ToConvertSpecific(this IConverter<float, double> converter) =>
            new ConverterFloatToDouble(converter);
        #endregion
        
        #region Double Methods
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterDoubleToInt"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterDoubleToInt ToConvert(this Func<double, int> converter) =>
            new ConverterDoubleToInt(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterDoubleToInt"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterDoubleToInt ToConvertSpecific(this IConverter<double, int> converter) =>
            new ConverterDoubleToInt(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterDoubleToLong"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterDoubleToLong ToConvert(this Func<double, long> converter) =>
            new ConverterDoubleToLong(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterDoubleToLong"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterDoubleToLong ToConvertSpecific(this IConverter<double, long> converter) =>
            new ConverterDoubleToLong(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterDoubleToFloat"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterDoubleToFloat ToConvert(this Func<double, float> converter) =>
            new ConverterDoubleToFloat(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterDoubleToFloat"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterDoubleToFloat ToConvertSpecific(this IConverter<double, float> converter) =>
            new ConverterDoubleToFloat(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterDouble"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterDouble ToConvert(this Func<double, double> converter) =>
            new ConverterDouble(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterDouble"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterDouble ToConvertSpecific(this IConverter<double, double> converter) =>
            new ConverterDouble(converter);
        #endregion
        
        #region Int Clasess
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterInt : GenericFuncConverter<int, int>, IConverterInt
        {
            public ConverterInt(IConverter<int, int> converter) 
                : base(converter) { }

            public ConverterInt(Func<int, int> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterIntToLong : GenericFuncConverter<int, long>, IConverterIntToLong
        {
            public ConverterIntToLong(IConverter<int, long> converter) 
                : base(converter) { }

            public ConverterIntToLong(Func<int, long> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterIntToFloat : GenericFuncConverter<int, float>, IConverterIntToFloat
        {
            public ConverterIntToFloat(IConverter<int, float> converter) 
                : base(converter) { }

            public ConverterIntToFloat(Func<int, float> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterIntToDouble : GenericFuncConverter<int, double>, IConverterIntToDouble
        {
            public ConverterIntToDouble(IConverter<int, double> converter) 
                : base(converter) { }

            public ConverterIntToDouble(Func<int, double> converter) 
                : base(converter) { }
        }
        #endregion
        
        #region Long Classes
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterLongToInt : GenericFuncConverter<long, int>, IConverterLongToInt
        {
            public ConverterLongToInt(IConverter<long, int> converter) 
                : base(converter) { }

            public ConverterLongToInt(Func<long, int> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterLong : GenericFuncConverter<long, long>, IConverterLong
        {
            public ConverterLong(IConverter<long, long> converter) 
                : base(converter) { }

            public ConverterLong(Func<long, long> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterLongToFloat : GenericFuncConverter<long, float>, IConverterLongToFloat
        {
            public ConverterLongToFloat(IConverter<long, float> converter) 
                : base(converter) { }

            public ConverterLongToFloat(Func<long, float> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterLongToDouble : GenericFuncConverter<long, double>, IConverterLongToDouble
        {
            public ConverterLongToDouble(IConverter<long, double> converter) 
                : base(converter) { }

            public ConverterLongToDouble(Func<long, double> converter) 
                : base(converter) { }
        }
        #endregion
        
        #region Float Classes
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterFloatToInt : GenericFuncConverter<float, int>, IConverterFloatToInt
        {
            public ConverterFloatToInt(IConverter<float, int> converter) 
                : base(converter) { }

            public ConverterFloatToInt(Func<float, int> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterFloatToLong : GenericFuncConverter<float, long>, IConverterFloatToLong
        {
            public ConverterFloatToLong(IConverter<float, long> converter) 
                : base(converter) { }

            public ConverterFloatToLong(Func<float, long> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterFloat : GenericFuncConverter<float, float>, IConverterFloat
        {
            public ConverterFloat(IConverter<float, float> converter) 
                : base(converter) { }

            public ConverterFloat(Func<float, float> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterFloatToDouble : GenericFuncConverter<float, double>, IConverterFloatToDouble
        {
            public ConverterFloatToDouble(IConverter<float, double> converter) 
                : base(converter) { }

            public ConverterFloatToDouble(Func<float, double> converter) 
                : base(converter) { }
        }
        #endregion
        
        #region Double Classes
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterDoubleToInt : GenericFuncConverter<double, int>, IConverterDoubleToInt
        {
            public ConverterDoubleToInt(IConverter<double, int> converter) 
                : base(converter) { }

            public ConverterDoubleToInt(Func<double, int> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterDoubleToLong : GenericFuncConverter<double, long>, IConverterDoubleToLong
        {
            public ConverterDoubleToLong(IConverter<double, long> converter) 
                : base(converter) { }

            public ConverterDoubleToLong(Func<double, long> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterDoubleToFloat : GenericFuncConverter<double, float>, IConverterDoubleToFloat
        {
            public ConverterDoubleToFloat(IConverter<double, float> converter) 
                : base(converter) { }

            public ConverterDoubleToFloat(Func<double, float> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
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