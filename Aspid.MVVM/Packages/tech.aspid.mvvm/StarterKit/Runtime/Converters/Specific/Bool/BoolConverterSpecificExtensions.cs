using System;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a function or a generic converter as one of the named boolean converter
    /// interfaces.
    /// </summary>
    /// <remarks>
    /// <c>ToConvert</c> takes a function, <c>ToConvertSpecific</c> takes a converter that is
    /// already the right shape but not the named type. Both exist because a binder before
    /// Unity 2023.1 declares its field as the named interface rather than the generic one,
    /// so a lambda cannot be assigned to it directly.
    /// </remarks>
    [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
    public static class BoolConverterSpecificExtensions
    {
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterDoubleToBool"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterDoubleToBool ToConvert(this Func<double, bool> converter) =>
            new ConverterDoubleToBool(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterDoubleToBool"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterDoubleToBool ToConvertSpecific(this IConverter<double, bool> converter) =>
            new ConverterDoubleToBool(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterFloatToBool"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterFloatToBool ToConvert(this Func<float, bool> converter) =>
            new ConverterFloatToBool(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterFloatToBool"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterFloatToBool ToConvertSpecific(this IConverter<float, bool> converter) =>
            new ConverterFloatToBool(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterIntToBool"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterIntToBool ToConvert(this Func<int, bool> converter) =>
            new ConverterIntToBool(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterIntToBool"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterIntToBool ToConvertSpecific(this IConverter<int, bool> converter) =>
            new ConverterIntToBool(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterLongToBool"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterLongToBool ToConvert(this Func<long, bool> converter) =>
            new ConverterLongToBool(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterLongToBool"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterLongToBool ToConvertSpecific(this IConverter<long, bool> converter) =>
            new ConverterLongToBool(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterObjectToBool"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterObjectToBool ToConvert(this Func<object?, bool> converter) =>
            new ConverterObjectToBool(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterObjectToBool"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterObjectToBool ToConvertSpecific(this IConverter<object?, bool> converter) =>
            new ConverterObjectToBool(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterStringToBool"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterStringToBool ToConvert(this Func<string?, bool> converter) =>
            new ConverterStringToBool(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterStringToBool"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterStringToBool ToConvertSpecific(this IConverter<string?, bool> converter) =>
            new ConverterStringToBool(converter);
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterDoubleToBool : GenericFuncConverter<double, bool>, IConverterDoubleToBool
        {
            public ConverterDoubleToBool(IConverter<double, bool> converter) 
                : base(converter) { }

            public ConverterDoubleToBool(Func<double, bool> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterFloatToBool : GenericFuncConverter<float, bool>, IConverterFloatToBool
        {
            public ConverterFloatToBool(IConverter<float, bool> converter)
                : base(converter.Convert) { }
            
            public ConverterFloatToBool(Func<float, bool> converter)
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterIntToBool : GenericFuncConverter<int, bool>, IConverterIntToBool
        {
            public ConverterIntToBool(IConverter<int, bool> converter)
                : base(converter.Convert) { }
            
            public ConverterIntToBool(Func<int, bool> converter)
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterLongToBool : GenericFuncConverter<long, bool>, IConverterLongToBool
        {
            public ConverterLongToBool(IConverter<long, bool> converter)
                : this(converter.Convert) { }
            
            public ConverterLongToBool(Func<long, bool> converter)
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterObjectToBool : GenericFuncConverter<object?, bool>, IConverterObjectToBool
        {
            public ConverterObjectToBool(IConverter<object?, bool> converter)
                : base(converter.Convert) { }
            
            public ConverterObjectToBool(Func<object?, bool> converter)
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterStringToBool : GenericFuncConverter<string?, bool>, IConverterStringToBool
        {
            public ConverterStringToBool(IConverter<string?, bool> converter) 
                : base(converter.Convert) { }
            
            public ConverterStringToBool(Func<string?, bool> converter)
                : base(converter) { }
        }
    }
}