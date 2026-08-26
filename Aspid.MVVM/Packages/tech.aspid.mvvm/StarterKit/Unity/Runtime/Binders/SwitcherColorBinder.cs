#nullable enable
using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherBinder{T1, T2, T3}">SwitcherBinder&lt;TTarget, Color, IConverter&lt;Color, Color&gt;&gt;</see> that fixes
    /// the value type to <see cref="Color"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of target object that exposes the target property.</typeparam>
    public abstract class SwitcherColorBinder<TTarget> : SwitcherBinder<TTarget, Color, Converter>
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
