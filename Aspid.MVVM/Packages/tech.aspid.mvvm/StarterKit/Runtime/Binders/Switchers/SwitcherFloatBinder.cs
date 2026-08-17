using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;

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
        /// <inheritdoc/>
        protected SwitcherFloatBinder(
            TTarget target, 
            float trueValue, 
            float falseValue,
            IConverter<float, float>? converter, 
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }


        protected override void SetValue(float value)
        {
            throw new System.NotImplementedException();
        }
    }
}