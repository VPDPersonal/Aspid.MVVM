// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherBinderWithConverter{T1, T2}">SwitcherBinderWithConverter&lt;TTarget, string&gt;</see> that fixes
    /// the value type to <see cref="string"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of target object that exposes the target property.</typeparam>
    public abstract class SwitcherStringBinder<TTarget> : SwitcherBinderWithConverter<TTarget, string>
    {
        /// <inheritdoc/>
        protected SwitcherStringBinder(
            TTarget target,
            string trueValue, 
            string falseValue, 
            IConverter<string?, string?>? converter,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode) { }
        
    }
}