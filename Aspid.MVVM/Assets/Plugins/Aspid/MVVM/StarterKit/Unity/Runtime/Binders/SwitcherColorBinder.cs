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
    /// Abstract base <see cref="SwitcherBinder{TTarget, Color, IConverter{Color, Color}}"/> that fixes
    /// the value type to <see cref="Color"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of target object that exposes the target property.</typeparam>
    public abstract class SwitcherColorBinder<TTarget> : SwitcherBinder<TTarget, Color, Converter>
    {
        /// <inheritdoc />
        protected SwitcherColorBinder(
            TTarget target, 
            Color trueValue, 
            Color falseValue,
            IConverter<Color, Color>? converter, 
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, GetConverter(converter), mode) { }

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
