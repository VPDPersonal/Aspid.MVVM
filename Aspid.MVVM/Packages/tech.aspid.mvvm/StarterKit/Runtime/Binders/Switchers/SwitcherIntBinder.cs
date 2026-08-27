// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherBinderWithConverter{T1, T2}">SwitcherBinderWithConverter&lt;TTarget, int&gt;</see> that fixes
    /// the value type to <see cref="int"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of target object that exposes the target property.</typeparam>
    public abstract class SwitcherIntBinder<TTarget> : SwitcherBinderWithConverter<TTarget, int>
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