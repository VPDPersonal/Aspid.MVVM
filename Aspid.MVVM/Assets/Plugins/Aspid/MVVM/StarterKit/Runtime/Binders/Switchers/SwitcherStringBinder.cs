using System.Runtime.CompilerServices;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherBinder{TTarget, string, IConverter{string, string}}"/> that fixes
    /// the value type to <see cref="string"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of target object that exposes the target property.</typeparam>
    public abstract class SwitcherStringBinder<TTarget> : SwitcherBinder<TTarget, string, Converter>
    {
        /// <inheritdoc />
        protected SwitcherStringBinder(
            TTarget target,
            string trueValue, 
            string falseValue, 
            IConverter<string?, string?>? converter,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, GetConverter(converter), mode) { }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Converter? GetConverter(IConverter<string?, string?>? converter)
        {
            #if UNITY_2023_1_OR_NEWER
            return converter;
            #else
            return converter?.ToConvertSpecific();
            #endif
        }
    }
}