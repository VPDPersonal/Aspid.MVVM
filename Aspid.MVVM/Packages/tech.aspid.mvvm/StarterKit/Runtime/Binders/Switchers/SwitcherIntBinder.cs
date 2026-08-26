using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherBinder{T1, T2, T3}">SwitcherBinder&lt;TTarget, int, IConverter&lt;int, int&gt;&gt;</see> that fixes
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
            : base(target, trueValue, falseValue, converter, mode) { }

    }
}