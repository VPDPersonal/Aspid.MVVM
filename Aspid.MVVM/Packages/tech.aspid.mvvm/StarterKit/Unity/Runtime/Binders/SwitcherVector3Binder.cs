using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherBinderWithConverter{T1, T2}">SwitcherBinderWithConverter&lt;TTarget, Vector3&gt;</see> that fixes
    /// the value type to <see cref="Vector3"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of target object that exposes the target property.</typeparam>
    public abstract class SwitcherVector3Binder<TTarget> : SwitcherBinderWithConverter<TTarget, Vector3>
    {
        /// <inheritdoc/>
        protected SwitcherVector3Binder(
            TTarget target, 
            Vector3 trueValue, 
            Vector3 falseValue,
            IConverter<Vector3, Vector3>? converter, 
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }
    }
}