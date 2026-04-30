#nullable enable
using UnityEngine;
using System.Runtime.CompilerServices;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget, Color, IConverter{Color, Color}}"/> that binds a <see cref="Color"/> property,
    /// implementing <see cref="IColorBinder"/> to accept both <see cref="Color"/> values and HTML color strings.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see cref="Color"/> property.</typeparam>
    public abstract class TargetColorBinder<TTarget> : TargetBinder<TTarget, Color, Converter>, IColorBinder
    {
        /// <inheritdoc/>
        protected TargetColorBinder(TTarget target, IConverter<Color, Color>? converter, BindMode mode = BindMode.OneWay)
            : base(target, GetConverter(converter), mode) { }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Converter? GetConverter(IConverter<Color, Color>? converter)
        {
            #if UNITY_2023_1_OR_NEWER
            return converter;
            #else
            return converter?.ToConvertSpecific();
            #endif
        }
    }
}