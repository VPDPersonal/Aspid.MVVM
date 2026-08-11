#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherBinder{T1, T2, T3}">SwitcherBinder&lt;TTarget, string, IConverter&lt;string, string&gt;&gt;</see> that fixes
    /// the value type to <see cref="string"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of target object that exposes the target property.</typeparam>
    public abstract class SwitcherStringBinder<TTarget> : SwitcherBinder<TTarget, string, Converter>
    {
        /// <inheritdoc/>
        protected SwitcherStringBinder(
            TTarget target,
            string trueValue, 
            string falseValue, 
            IConverter<string?, string?>? converter,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, ConverterBridge.String(converter), mode) { }
        
    }
}