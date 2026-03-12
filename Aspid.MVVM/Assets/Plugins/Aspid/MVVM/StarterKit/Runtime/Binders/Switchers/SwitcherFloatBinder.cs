using System.Runtime.CompilerServices;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherBinder{TTarget, float, IConverter{float, float}}"/> that fixes
    /// the value type to <see cref="float"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of target object that exposes the target property.</typeparam>
    public abstract class SwitcherFloatBinder<TTarget> : SwitcherBinder<TTarget, float, Converter>
    {
        /// <inheritdoc />
        protected SwitcherFloatBinder(
            TTarget target, 
            float trueValue, 
            float falseValue,
            IConverter<float, float>? converter, 
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, GetConverter(converter), mode) { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Converter? GetConverter(IConverter<float, float>? converter)
        {
            #if UNITY_2023_1_OR_NEWER
            return converter;
            #else
            return converter?.ToConvertSpecific();
            #endif
        }

        protected override void SetValue(float value)
        {
            throw new System.NotImplementedException();
        }
    }
}