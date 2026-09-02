using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> and <see cref="IReverseBinder{T}"/> that synchronises
    /// a value in both directions between the ViewModel and the View.
    /// </summary>
    /// <typeparam name="T">The type of the value exchanged between View and ViewModel.</typeparam>
    public class GenericTwoWayBinder<T> : Binder, IBinder<T>, IReverseBinder<T>
    {
        private readonly Action<T?> _setValue;
        private readonly Func<T?>? _getValueOnBound;
        private readonly Func<T?>? _getValueOnUnbinding;

        /// <param name="subscribe">Receives the callback that raises <see cref="ValueChanged"/>; subscribe it to the View event.</param>
        /// <param name="setValue">The action invoked with each value received from the ViewModel.</param>
        /// <param name="getValueOnBound">Optional factory whose result is pushed to the ViewModel on binding.</param>
        /// <param name="getValueOnUnbinding">Optional factory whose result is pushed to the ViewModel just before unbinding.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="subscribe"/> or <paramref name="setValue"/> is <see langword="null"/>.
        /// </exception>
        public GenericTwoWayBinder(
            Action<Action<T>> subscribe,
            Action<T?> setValue,
            Func<T?>? getValueOnBound = null,
            Func<T?>? getValueOnUnbinding = null)
            : this(setValue, getValueOnBound, getValueOnUnbinding)
        {
            if (subscribe is null) throw new ArgumentNullException(nameof(subscribe));
            subscribe.Invoke(OnValueChanged);
        }

        /// <param name="setValue">The action invoked with each value received from the ViewModel.</param>
        /// <param name="getValueOnBound">Optional factory whose result is pushed to the ViewModel on binding.</param>
        /// <param name="getValueOnUnbinding">Optional factory whose result is pushed to the ViewModel just before unbinding.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        public GenericTwoWayBinder(
            Action<T?> setValue,
            Func<T?>? getValueOnBound = null,
            Func<T?>? getValueOnUnbinding = null)
            : base(BindMode.TwoWay)
        {
            _getValueOnBound = getValueOnBound;
            _getValueOnUnbinding = getValueOnUnbinding;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <inheritdoc/>
        public event Action<T?>? ValueChanged;

        /// <summary>
        /// Forwards <paramref name="value"/> to the setter action.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(T? value) =>
            _setValue.Invoke(value);

        /// <summary>
        /// Pushes the <c>getValueOnBound</c> result to the ViewModel, when that factory was provided.
        /// </summary>
        protected override void OnBound()
        {
            if (_getValueOnBound is not null)
                OnValueChanged(_getValueOnBound.Invoke());
        }

        /// <summary>
        /// Pushes the <c>getValueOnUnbinding</c> result to the ViewModel, when that factory was provided.
        /// </summary>
        protected override void OnUnbinding()
        {
            if (_getValueOnUnbinding is not null)
                OnValueChanged(_getValueOnUnbinding.Invoke());
        }

        private void OnValueChanged(T? value) =>
            ValueChanged?.Invoke(value);
    }

    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> and <see cref="IReverseBinder{T}"/> that synchronises
    /// a value in both directions between the ViewModel and the View, passing the stored <typeparamref name="TTarget"/> to every callback.
    /// </summary>
    /// <typeparam name="TTarget">The type of the View object that receives and exposes the value.</typeparam>
    /// <typeparam name="T">The type of the value exchanged between View and ViewModel.</typeparam>
    public class GenericTwoWayBinder<TTarget, T> : Binder, IBinder<T>, IReverseBinder<T>
    {
        private readonly TTarget _target;
        private readonly Action<TTarget, T?> _setValue;
        private readonly Func<TTarget, T?>? _getValueOnBound;
        private readonly Func<TTarget, T?>? _getValueOnUnbinding;

        /// <param name="target">The View object passed to every callback.</param>
        /// <param name="subscribe">Receives <paramref name="target"/> and the callback that raises <see cref="ValueChanged"/>; subscribe it to the View event.</param>
        /// <param name="setValue">The action invoked with the target and each value received from the ViewModel.</param>
        /// <param name="getValueOnBound">Optional factory whose result is pushed to the ViewModel on binding.</param>
        /// <param name="getValueOnUnbinding">Optional factory whose result is pushed to the ViewModel just before unbinding.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/>, <paramref name="subscribe"/> or <paramref name="setValue"/> is <see langword="null"/>.
        /// </exception>
        public GenericTwoWayBinder(
            TTarget target,
            Action<TTarget, Action<T>> subscribe,
            Action<TTarget, T?> setValue,
            Func<TTarget, T?>? getValueOnBound = null,
            Func<TTarget, T?>? getValueOnUnbinding = null)
            : base(BindMode.TwoWay)
        {
            if (subscribe is null) throw new ArgumentNullException(nameof(subscribe));

            _getValueOnBound = getValueOnBound;
            _getValueOnUnbinding = getValueOnUnbinding;

            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));

            subscribe.Invoke(target, OnValueChanged);
        }

        /// <param name="target">The View object passed to every callback.</param>
        /// <param name="setValue">The action invoked with the target and each value received from the ViewModel.</param>
        /// <param name="getValueOnBound">Optional factory whose result is pushed to the ViewModel on binding.</param>
        /// <param name="getValueOnUnbinding">Optional factory whose result is pushed to the ViewModel just before unbinding.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> or <paramref name="setValue"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when both <paramref name="getValueOnBound"/> and <paramref name="getValueOnUnbinding"/> are <see langword="null"/>.
        /// </exception>
        public GenericTwoWayBinder(
            TTarget target,
            Action<TTarget, T?> setValue,
            Func<TTarget, T?>? getValueOnBound = null,
            Func<TTarget, T?>? getValueOnUnbinding = null)
            : base(BindMode.TwoWay)
        {
            if (getValueOnBound is null && getValueOnUnbinding is null)
                throw new ArgumentException($"{nameof(getValueOnBound)} and {nameof(getValueOnUnbinding)} are both null.");

            _getValueOnBound = getValueOnBound;
            _getValueOnUnbinding = getValueOnUnbinding;

            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <inheritdoc/>
        public event Action<T?>? ValueChanged;

        /// <summary>
        /// Forwards <paramref name="value"/>, with the stored target, to the setter action.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(T? value) =>
            _setValue.Invoke(_target, value);

        /// <summary>
        /// Pushes the <c>getValueOnBound</c> result to the ViewModel, when that factory was provided.
        /// </summary>
        protected override void OnBound()
        {
            if (_getValueOnBound is not null)
                OnValueChanged(_getValueOnBound.Invoke(_target));
        }

        /// <summary>
        /// Pushes the <c>getValueOnUnbinding</c> result to the ViewModel, when that factory was provided.
        /// </summary>
        protected override void OnUnbinding()
        {
            if (_getValueOnUnbinding is not null)
                OnValueChanged(_getValueOnUnbinding.Invoke(_target));
        }

        private void OnValueChanged(T? value) =>
            ValueChanged?.Invoke(value);
    }
}
