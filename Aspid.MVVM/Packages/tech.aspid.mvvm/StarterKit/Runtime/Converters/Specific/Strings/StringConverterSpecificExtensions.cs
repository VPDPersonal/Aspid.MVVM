using System;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a function or a generic converter as one of the named string converter
    /// interfaces.
    /// </summary>
    /// <remarks>
    /// <c>ToConvert</c> takes a function, <c>ToConvertSpecific</c> takes a converter that is
    /// already the right shape but not the named type. Both exist because a binder before
    /// Unity 2023.1 declares its field as the named interface rather than the generic one,
    /// so a lambda cannot be assigned to it directly.
    /// </remarks>
    public static class StringConverterSpecificExtensions
    {
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterString"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterString ToConvert(this Func<string?, string?> converter) =>
            new ConverterString(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterString"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterString ToConvertSpecific(this IConverter<string?, string?> converter) =>
            new ConverterString(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterObjectToString"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterObjectToString ToConvert(this Func<object?, string?> converter) =>
            new ConverterObjectToString(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterObjectToString"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterObjectToString ToConvertSpecific(this IConverter<object?, string?> converter) =>
            new ConverterObjectToString(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterTimeSpanToString"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterTimeSpanToString ToConvert(this Func<TimeSpan, string?> converter) =>
            new ConverterTimeSpanToString(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterTimeSpanToString"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterTimeSpanToString ToConvertSpecific(this IConverter<TimeSpan, string?> converter) =>
            new ConverterTimeSpanToString(converter);
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterString : GenericFuncConverter<string?, string?>, IConverterString
        {
            public ConverterString(IConverter<string?, string?> converter) 
                : base(converter) { }

            public ConverterString(Func<string?, string?> converter)
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterObjectToString : GenericFuncConverter<object?, string?>, IConverterObjectToString
        {
            public ConverterObjectToString(IConverter<object?, string?> converter)
                : base(converter) { }

            public ConverterObjectToString(Func<object?, string?> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterTimeSpanToString : GenericFuncConverter<TimeSpan, string?>, IConverterTimeSpanToString
        {
            public ConverterTimeSpanToString(IConverter<TimeSpan, string?> converter) 
                : base(converter) { }

            public ConverterTimeSpanToString(Func<TimeSpan, string?> converter) 
                : base(converter) { }
        }
    }
}