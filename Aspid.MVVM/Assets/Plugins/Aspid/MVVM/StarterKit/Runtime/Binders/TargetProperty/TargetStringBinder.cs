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
    /// Abstract base <see cref="TargetBinder{TTarget, string, IConverter{string, string}}"/> that binds an <see langword="string"/> property.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see langword="string"/> property.</typeparam>
    public abstract class TargetStringBinder<TTarget> : TargetBinder<TTarget, string, Converter>
    {
        /// <inheritdoc/>
        protected TargetStringBinder(TTarget target, IConverter<string?, string?>? converter, BindMode mode) 
            : base(target, GetConverter(converter), mode) { }
        
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