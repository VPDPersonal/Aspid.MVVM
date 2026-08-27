#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherBinderWithConverter{T1, T2}">SwitcherBinderWithConverter&lt;TTarget, Color&gt;</see> that fixes
    /// the value type to <see cref="Color"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of target object that exposes the target property.</typeparam>
    public abstract class SwitcherColorBinder<TTarget> : SwitcherBinderWithConverter<TTarget, Color>
    {
        /// <inheritdoc/>
        protected SwitcherColorBinder(
            TTarget target, 
            Color trueValue, 
            Color falseValue,
            IConverter<Color, Color>? converter, 
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }
    }
}
