#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinderWithConverter{T1, T2}">TargetBinderWithConverter&lt;TTarget, TObject&gt;</see> that binds a
    /// property holding a reference to a <see cref="Object">UnityEngine.Object</see>, normalizing destroyed
    /// references to <see langword="null"/> after the configured converter has run.
    /// </summary>
    /// <remarks>
    /// The converter runs first, so a converter that resolves one asset into another is still checked: whatever it
    /// returns is what reaches the property. See <see cref="TargetObjectBinder{TTarget, TObject}"/> for why the check
    /// is needed at all.
    /// </remarks>
    /// <typeparam name="TTarget">The type of the target object that exposes the bound property.</typeparam>
    /// <typeparam name="TObject">The type of <see cref="Object">UnityEngine.Object</see> the property holds.</typeparam>
    public abstract class TargetObjectBinderWithConverter<TTarget, TObject> : TargetBinderWithConverter<TTarget, TObject>
        where TObject : Object
    {
        /// <inheritdoc/>
        protected TargetObjectBinderWithConverter(TTarget target, IConverter<TObject?, TObject?>? converter, BindMode mode)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        /// <remarks>
        /// Returns <see langword="null"/> when the converted value refers to a destroyed object.
        /// </remarks>
        protected override TObject? GetConvertedValue(TObject? value)
        {
            var converted = base.GetConvertedValue(value);
            return (Object?)converted ? converted : null;
        }
    }
}
