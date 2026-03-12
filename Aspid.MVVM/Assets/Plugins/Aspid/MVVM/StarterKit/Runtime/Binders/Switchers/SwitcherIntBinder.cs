using System.Runtime.CompilerServices;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherBinder{TTarget, int, IConverter{int, int}}"/> that fixes
    /// the value type to <see cref="int"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of target object that exposes the target property.</typeparam>
    public abstract class SwitcherIntBinder<TTarget> : SwitcherBinder<TTarget, int, Converter>
    {
        /// <inheritdoc/>
        protected SwitcherIntBinder(
            TTarget target, 
            int trueValue, 
            int falseValue,
            IConverter<int, int>? converter, 
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, GetConverter(converter), mode) { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Converter? GetConverter(IConverter<int, int>? converter)
        {
            #if UNITY_2023_1_OR_NEWER
            return converter;
            #else
            return converter?.ToConvertSpecific();
            #endif
        }
    }
}