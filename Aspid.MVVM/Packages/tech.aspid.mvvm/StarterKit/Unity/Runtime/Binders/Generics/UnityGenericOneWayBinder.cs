#nullable enable
using System;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that forwards values of type <typeparamref name="T"/>
    /// from the ViewModel to a <see cref="UnityAction{T}"/> setter.
    /// </summary>
    /// <typeparam name="T">The type of the value to bind.</typeparam>
    /// <remarks>
    /// Unity-specific variant of <see cref="GenericOneWayBinder{T}"/> that accepts a <see cref="UnityAction{T}"/>
    /// instead of a plain <see cref="System.Action{T}"/>.
    /// Only non-two-way bind modes are supported; <see cref="BindMode.TwoWay"/> and <see cref="BindMode.OneWayToSource"/> throw.
    /// </remarks>
    /// <include file="XmlExampleDoc-UnityGenerics-1.1.0.xml" path="doc//member[@name='UnityGenericOneWayBinder{1}']/*" />
    [System.Obsolete("Use the GenericOneWay binder instead: it takes a plain Action, which a UnityAction converts to implicitly. The Unity-flavoured copies exist only for that conversion and will be removed in the next major version.")]
    public class UnityGenericOneWayBinder<T> : Binder, IBinder<T>
    {
        private readonly UnityAction<T?> _setValue;

        /// <summary>
        /// Initializes a new instance of <see cref="UnityGenericOneWayBinder{T}"/>.
        /// </summary>
        /// <param name="setValue">The <see cref="UnityAction{T}"/> invoked with each new value received from the ViewModel.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        public UnityGenericOneWayBinder(UnityAction<T?> setValue, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Forwards <paramref name="value"/> to the setter action.
        /// </summary>
        /// <param name="value">The new value received from the ViewModel.</param>
        public void SetValue(T? value) =>
            _setValue.Invoke(value);
    }
    
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that forwards values of type <typeparamref name="T"/>
    /// from the ViewModel to a <see cref="UnityAction{T0,T1}"/> setter together with a stored <typeparamref name="TTarget"/> instance.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is being set.</typeparam>
    /// <typeparam name="T">The type of the value to bind.</typeparam>
    /// <remarks>
    /// Unity-specific variant of <see cref="GenericOneWayBinder{TTarget,T}"/> that accepts a <see cref="UnityAction{T0,T1}"/>
    /// instead of a plain <see cref="System.Action{T1,T2}"/>.
    /// Holding a <typeparamref name="TTarget"/> instance avoids capturing it in a closure when using
    /// method-group-style setters on Unity components.
    /// Only non-two-way bind modes are supported; <see cref="BindMode.TwoWay"/> and <see cref="BindMode.OneWayToSource"/> throw.
    /// </remarks>
    /// <include file="XmlExampleDoc-UnityGenerics-1.1.0.xml" path="doc//member[@name='UnityGenericOneWayBinder{2}']/*" />
    [System.Obsolete("Use the GenericOneWay binder instead: it takes a plain Action, which a UnityAction converts to implicitly. The Unity-flavoured copies exist only for that conversion and will be removed in the next major version.")]
    public class UnityGenericOneWayBinder<TTarget, T> : Binder, IBinder<T>
    {
        private readonly TTarget _target;
        private readonly UnityAction<TTarget, T?> _setValue;

        /// <summary>
        /// Initializes a new instance of <see cref="UnityGenericOneWayBinder{TTarget,T}"/>.
        /// </summary>
        /// <param name="target">The target object passed as the first argument to <paramref name="setValue"/>.</param>
        /// <param name="setValue">
        /// The <see cref="UnityAction{T0,T1}"/> invoked with the target and each new value received from the ViewModel.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> or <paramref name="setValue"/> is <see langword="null"/>.
        /// </exception>
        public UnityGenericOneWayBinder(
            TTarget target,
            UnityAction<TTarget, T?> setValue, 
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Forwards <paramref name="value"/> together with the stored target to the setter action.
        /// </summary>
        /// <param name="value">The new value received from the ViewModel.</param>
        public void SetValue(T? value) =>
            _setValue.Invoke(_target, value);
    }
}