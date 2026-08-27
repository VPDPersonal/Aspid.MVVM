#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinderWithConverter{T1, T2}">TargetBinderWithConverter&lt;TTarget, Color&gt;</see> that binds a <see cref="Color"/> property,
    /// implementing <see cref="IColorBinder"/> to accept both <see cref="Color"/> values and HTML color strings.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see cref="Color"/> property.</typeparam>
    public abstract class TargetColorBinder<TTarget> : TargetBinderWithConverter<TTarget, Color>, IColorBinder
    {
        /// <inheritdoc/>
        protected TargetColorBinder(TTarget target, IConverter<Color, Color>? converter, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
        
    }
}