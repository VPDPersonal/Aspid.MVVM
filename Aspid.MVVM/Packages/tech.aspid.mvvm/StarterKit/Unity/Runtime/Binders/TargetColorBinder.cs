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
    /// Abstract base <see cref="TargetBinder{T1, T2, T3}">TargetBinder&lt;TTarget, Color, IConverter&lt;Color, Color&gt;&gt;</see> that binds a <see cref="Color"/> property,
    /// implementing <see cref="IColorBinder"/> to accept both <see cref="Color"/> values and HTML color strings.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see cref="Color"/> property.</typeparam>
    public abstract class TargetColorBinder<TTarget> : TargetBinder<TTarget, Color, Converter>, IColorBinder
    {
        /// <inheritdoc/>
        protected TargetColorBinder(TTarget target, IConverter<Color, Color>? converter, BindMode mode = BindMode.OneWay)
            : base(target, ConverterBridgeUnity.Color(converter), mode) { }
        
    }
}