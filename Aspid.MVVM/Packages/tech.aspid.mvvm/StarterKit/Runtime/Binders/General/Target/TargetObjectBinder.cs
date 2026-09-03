#nullable enable
using System;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget, TProperty}">TargetBinder&lt;TTarget, TObject&gt;</see> that binds a
    /// <see cref="Object">UnityEngine.Object</see> reference, normalizing destroyed references to <see langword="null"/> in both directions.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the bound property.</typeparam>
    /// <typeparam name="TObject">The type of <see cref="Object">UnityEngine.Object</see> the property holds.</typeparam>
    [Serializable]
    public abstract class TargetObjectBinder<TTarget, TObject> : TargetBinder<TTarget, TObject>
        where TObject : Object
    {
        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected TargetObjectBinder() { }

        /// <inheritdoc/>
        protected TargetObjectBinder(
            TTarget target,
            IConverter<TObject?, TObject?>? converter,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected override TObject? GetConvertedValue(TObject? value)
        {
            var converted = base.GetConvertedValue(value);
            return converted ? converted : null;
        }

        /// <inheritdoc/>
        protected override TObject? GetConvertedBackValue(TObject? value)
        {
            var converted = base.GetConvertedBackValue(value);
            return converted ? converted : null;
        }
    }
}
