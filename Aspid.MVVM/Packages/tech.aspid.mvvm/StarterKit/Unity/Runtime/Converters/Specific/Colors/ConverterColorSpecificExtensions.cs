#nullable enable
using System;
using Aspid.FastTools.Types;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a function or a generic converter as one of the named colour converter
    /// interfaces.
    /// </summary>
    /// <remarks>
    /// <c>ToConvert</c> takes a function, <c>ToConvertSpecific</c> takes a converter that is
    /// already the right shape but not the named type. Both exist because a binder before
    /// Unity 2023.1 declares its field as the named interface rather than the generic one,
    /// so a lambda cannot be assigned to it directly.
    /// </remarks>
    public static class ConverterColorSpecificExtensions
    {
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterColor"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterColor ToConvert(this Func<Color, Color> converter) =>
            new ConverterColor(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterColor"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterColor ToConvertSpecific(this IConverter<Color, Color> converter) =>
            new ConverterColor(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterStringToColor"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterStringToColor ToConvert(this Func<string?, Color> converter) =>
            new ConverterStringToColor(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterStringToColor"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterStringToColor ToConvertSpecific(this IConverter<string?, Color> converter) =>
            new ConverterStringToColor(converter);
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterColor : GenericFuncConverter<Color, Color>, IConverterColor
        {
            public ConverterColor(IConverter<Color, Color> converter) 
                : base(converter) { }

            public ConverterColor(Func<Color, Color> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterStringToColor : GenericFuncConverter<string?, Color>, IConverterStringToColor
        {
            public ConverterStringToColor(IConverter<string?, Color> converter)
                : base(converter) { }

            public ConverterStringToColor(Func<string?, Color> converter)
                : base(converter) { }
        }
    }
}