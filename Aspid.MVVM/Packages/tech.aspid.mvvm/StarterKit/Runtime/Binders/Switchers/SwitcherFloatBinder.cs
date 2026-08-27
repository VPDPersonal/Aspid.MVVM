// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherBinderWithConverter{T1, T2}">SwitcherBinderWithConverter&lt;TTarget, float&gt;</see> that fixes
    /// the value type to <see cref="float"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of target object that exposes the target property.</typeparam>
    public abstract class SwitcherFloatBinder<TTarget> : SwitcherBinderWithConverter<TTarget, float>
    {
        /// <inheritdoc/>
        protected SwitcherFloatBinder(
            TTarget target, 
            float trueValue, 
            float falseValue,
            IConverter<float, float>? converter, 
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }


    }
}