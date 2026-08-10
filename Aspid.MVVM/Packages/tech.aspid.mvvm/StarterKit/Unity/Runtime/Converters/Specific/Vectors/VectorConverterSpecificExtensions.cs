#nullable enable
using System;
using Aspid.FastTools.Types;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a function or a generic converter as one of the named vector converter
    /// interfaces.
    /// </summary>
    /// <remarks>
    /// <c>ToConvert</c> takes a function, <c>ToConvertSpecific</c> takes a converter that is
    /// already the right shape but not the named type. Both exist because a binder before
    /// Unity 2023.1 declares its field as the named interface rather than the generic one,
    /// so a lambda cannot be assigned to it directly.
    /// </remarks>
    [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
    public static class VectorConverterSpecificExtensions
    {
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterVector2"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterVector2 ToConvert(this Func<Vector2, Vector2> converter) =>
            new ConverterVector2(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterVector2"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterVector2 ToConvertSpecific(this IConverter<Vector2, Vector2> converter) =>
            new ConverterVector2(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterVector2ToVector3"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterVector2ToVector3 ToConvert(this Func<Vector2, Vector3> converter) =>
            new ConverterVector2ToVector3(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterVector2ToVector3"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterVector2ToVector3 ToConvertSpecific(this IConverter<Vector2, Vector3> converter) =>
            new ConverterVector2ToVector3(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterVector3"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterVector3 ToConvert(this Func<Vector3, Vector3> converter) =>
            new ConverterVector3(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterVector3"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterVector3 ToConvertSpecific(this IConverter<Vector3, Vector3> converter) =>
            new ConverterVector3(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterVector3ToVector2"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterVector3ToVector2 ToConvert(this Func<Vector3, Vector2> converter) =>
            new ConverterVector3ToVector2(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterVector3ToVector2"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        [Obsolete("Only needed to assign a lambda to a field typed as a named converter alias, which Unity before 2023.1 required. The package now requires Unity 6000.0, so assign the converter directly. This will be removed in the next major version.")]
        public static IConverterVector3ToVector2 ToConvertSpecific(this IConverter<Vector3, Vector2> converter) =>
            new ConverterVector3ToVector2(converter);
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterVector2 : GenericFuncConverter<Vector2, Vector2>, IConverterVector2
        {
            public ConverterVector2(IConverter<Vector2, Vector2> converter) 
                : base(converter) { }

            public ConverterVector2(Func<Vector2, Vector2> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterVector2ToVector3 : GenericFuncConverter<Vector2, Vector3>, IConverterVector2ToVector3
        {
            public ConverterVector2ToVector3(IConverter<Vector2, Vector3> converter) 
                : base(converter) { }

            public ConverterVector2ToVector3(Func<Vector2, Vector3> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterVector3 : GenericFuncConverter<Vector3, Vector3>, IConverterVector3
        {
            public ConverterVector3(IConverter<Vector3, Vector3> converter) 
                : base(converter) { }

            public ConverterVector3(Func<Vector3, Vector3> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterVector3ToVector2 : GenericFuncConverter<Vector3, Vector2>, IConverterVector3ToVector2
        {
            public ConverterVector3ToVector2(IConverter<Vector3, Vector2> converter) 
                : base(converter) { }

            public ConverterVector3ToVector2(Func<Vector3, Vector2> converter) 
                : base(converter) { }
        }
    }
}