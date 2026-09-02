using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="GenericOneWayBinder{T}"/> fixed to <see cref="BindMode.OneTime"/>: the setter runs once, for the first value.
    /// </summary>
    /// <typeparam name="T">The type of the bound value.</typeparam>
    public class GenericOneTimeBinder<T> : GenericOneWayBinder<T>
    {
        /// <param name="setValue">The action invoked once with the bound value.</param>
        public GenericOneTimeBinder(Action<T?> setValue)
            : base(setValue, BindMode.OneTime) { }
    }

    /// <summary>
    /// <see cref="GenericOneWayBinder{TTarget,T}"/> fixed to <see cref="BindMode.OneTime"/>: the setter runs once, for the first value.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is set.</typeparam>
    /// <typeparam name="T">The type of the bound value.</typeparam>
    public class GenericOneTimeBinder<TTarget, T> : GenericOneWayBinder<TTarget, T>
    {
        /// <param name="target">The target object passed as the first argument to <paramref name="setValue"/>.</param>
        /// <param name="setValue">The action invoked once with the target and the bound value.</param>
        public GenericOneTimeBinder(TTarget target, Action<TTarget, T?> setValue)
            : base(target, setValue, BindMode.OneTime) { }
    }
}
