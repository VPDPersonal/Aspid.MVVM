#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinderWithConverter{T1, T2}">TargetBinderWithConverter&lt;TTarget, bool&gt;</see> that binds a <see langword="bool"/> property.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see langword="bool"/> property.</typeparam>
    [Serializable]
    public abstract class TargetBoolBinder<TTarget> : TargetBinderWithConverter<TTarget, bool>
    {
        /// <inheritdoc/>
        protected TargetBoolBinder(TTarget target, IConverter<bool, bool>? converter, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
