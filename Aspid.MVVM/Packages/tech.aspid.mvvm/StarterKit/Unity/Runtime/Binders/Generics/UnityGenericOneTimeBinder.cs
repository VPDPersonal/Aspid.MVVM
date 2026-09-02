#nullable enable
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="UnityGenericOneWayBinder{T}"/> pre-configured with <see cref="BindMode.OneTime"/>,
    /// applying the bound value exactly once.
    /// </summary>
    /// <typeparam name="T">The type of the value to bind.</typeparam>
    /// <remarks>
    /// Unity-specific variant of <see cref="GenericOneTimeBinder{T}"/> that accepts a <see cref="UnityAction{T}"/>.
    /// </remarks>
    [System.Obsolete("Use the GenericOneTime binder instead: it takes a plain Action, which a UnityAction converts to implicitly. The Unity-flavoured copies exist only for that conversion and will be removed in the next major version.")]
    public class UnityGenericOneTimeBinder<T> : UnityGenericOneWayBinder<T>
    {
        /// <param name="setValue">The <see cref="UnityAction{T}"/> invoked once with the bound value.</param>
        public UnityGenericOneTimeBinder(UnityAction<T?> setValue)
            : base(setValue, BindMode.OneTime) { }
    }

    /// <summary>
    /// <see cref="UnityGenericOneWayBinder{TTarget,T}"/> pre-configured with <see cref="BindMode.OneTime"/>,
    /// applying the bound value exactly once.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is being set.</typeparam>
    /// <typeparam name="T">The type of the value to bind.</typeparam>
    /// <remarks>
    /// Unity-specific variant of <see cref="GenericOneTimeBinder{TTarget,T}"/> that accepts a <see cref="UnityAction{T0,T1}"/>.
    /// </remarks>
    [System.Obsolete("Use the GenericOneTime binder instead: it takes a plain Action, which a UnityAction converts to implicitly. The Unity-flavoured copies exist only for that conversion and will be removed in the next major version.")]
    public class UnityGenericOneTimeBinder<TTarget, T> : UnityGenericOneWayBinder<TTarget, T>
    {
        /// <param name="target">The target object whose property is updated.</param>
        /// <param name="setValue">The <see cref="UnityAction{T0,T1}"/> invoked once with the target and the bound value.</param>
        public UnityGenericOneTimeBinder(TTarget target, UnityAction<TTarget, T?> setValue)
            : base(target, setValue, BindMode.OneTime) { }
    }
}