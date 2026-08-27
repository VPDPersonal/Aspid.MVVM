#nullable enable
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{T1, T2}">TargetBinder&lt;TTarget, TObject&gt;</see> that binds a
    /// property holding a reference to a <see cref="Object">UnityEngine.Object</see>, normalizing destroyed
    /// references to <see langword="null"/> in both binding directions.
    /// </summary>
    /// <remarks>
    /// A destroyed Unity object is not a <see langword="null"/> reference: the managed wrapper survives and compares
    /// equal to <see langword="null"/> only through <see cref="Object"/>'s own operators. Without this layer a
    /// ViewModel could hand over a destroyed asset — which the property would accept and the Inspector would show as
    /// <c>Missing</c> — or receive one back in <see cref="BindMode.OneWayToSource"/> and store it as a live value.
    /// </remarks>
    /// <typeparam name="TTarget">The type of the target object that exposes the bound property.</typeparam>
    /// <typeparam name="TObject">The type of <see cref="Object">UnityEngine.Object</see> the property holds.</typeparam>
    public abstract class TargetObjectBinder<TTarget, TObject> : TargetBinder<TTarget, TObject>
        where TObject : Object
    {
        /// <inheritdoc/>
        protected TargetObjectBinder(TTarget target, BindMode mode)
            : base(target, mode) { }

        /// <inheritdoc/>
        /// <remarks>
        /// Returns <see langword="null"/> when <paramref name="value"/> refers to a destroyed object.
        /// </remarks>
        protected override TObject? GetConvertedValue(TObject? value) =>
            // Cast to Object is required: `value != null` would be a reference comparison and miss a destroyed object.
            (Object?)value ? value : null;
    }
}
