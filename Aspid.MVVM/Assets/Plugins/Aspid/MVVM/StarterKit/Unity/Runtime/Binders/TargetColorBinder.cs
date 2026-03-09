#nullable enable
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget,TProperty,TConverter}"/> that binds a <see cref="Color"/> property,
    /// implementing <see cref="IColorBinder"/> to accept both <see cref="Color"/> values and HTML color strings.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see cref="Color"/> property.</typeparam>
    public abstract class TargetColorBinder<TTarget> : TargetBinder<TTarget, Color, Converter>, IColorBinder
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TargetColorBinder{TTarget}"/>.
        /// </summary>
        /// <param name="target">The target object whose Color property is managed by this binder.</param>
        /// <param name="converter">An optional Color-to-Color converter applied before the value is stored.</param>
        /// <param name="mode">The binding mode to use.</param>
        protected TargetColorBinder(TTarget target, Converter? converter, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}