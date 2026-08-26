using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherBinder{T1, T2, T3}">SwitcherBinder&lt;TTarget, Vector3, IConverter&lt;Vector3, Vector3&gt;&gt;</see> that fixes
    /// the value type to <see cref="Vector3"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of target object that exposes the target property.</typeparam>
    public abstract class SwitcherVector3Binder<TTarget> : SwitcherBinder<TTarget, Vector3, Converter>
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