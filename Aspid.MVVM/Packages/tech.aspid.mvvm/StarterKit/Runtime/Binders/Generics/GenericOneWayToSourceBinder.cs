using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IReverseBinder{T}"/> that propagates View values back to the ViewModel.
    /// </summary>
    /// <typeparam name="T">The type of the value reported to the ViewModel.</typeparam>
    public class GenericOneWayToSourceBinder<T> : Binder, IReverseBinder<T>
    {
        private readonly Func<T?>? _getValueOnBound;
        private readonly Func<T?>? _getValueOnUnbinding;

        /// <param name="subscribe">Receives the callback that raises <see cref="ValueChanged"/>; subscribe it to the View event.</param>
        /// <param name="getValueOnBound">Optional factory whose result is pushed to the ViewModel on binding.</param>
        /// <param name="getValueOnUnbinding">Optional factory whose result is pushed to the ViewModel just before unbinding.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="subscribe"/> is <see langword="null"/>.</exception>
        public GenericOneWayToSourceBinder(
            Action<Action<T>> subscribe,
            Func<T?>? getValueOnBound = null,
            Func<T?>? getValueOnUnbinding = null)
            : base(BindMode.OneWayToSource)
        {
            if (subscribe is null) throw new ArgumentNullException(nameof(subscribe));

            _getValueOnBound = getValueOnBound;
            _getValueOnUnbinding = getValueOnUnbinding;

            subscribe.Invoke(OnValueChanged);
        }

        /// <param name="getValueOnBound">Optional factory whose result is pushed to the ViewModel on binding.</param>
        /// <param name="getValueOnUnbinding">Optional factory whose result is pushed to the ViewModel just before unbinding.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when both <paramref name="getValueOnBound"/> and <paramref name="getValueOnUnbinding"/> are <see langword="null"/>.
        /// </exception>
        public GenericOneWayToSourceBinder(
            Func<T?>? getValueOnBound = null,
            Func<T?>? getValueOnUnbinding = null)
            : base(BindMode.OneWayToSource)
        {
            if (getValueOnBound is null && getValueOnUnbinding is null)
                throw new ArgumentException($"{nameof(getValueOnBound)} and {nameof(getValueOnUnbinding)} are both null.");

            _getValueOnBound = getValueOnBound;
            _getValueOnUnbinding = getValueOnUnbinding;
        }

        /// <inheritdoc/>
        public event Action<T?>? ValueChanged;

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
    /// <see cref="Binder"/> implementing <see cref="IReverseBinder{T}"/> that propagates View values back to the ViewModel,
    /// passing the stored <typeparamref name="TTarget"/> to every callback.
    /// </summary>
    /// <typeparam name="TTarget">The type of the View object that exposes the value.</typeparam>
    /// <typeparam name="T">The type of the value reported to the ViewModel.</typeparam>
    public class GenericOneWayToSourceBinder<TTarget, T> : Binder, IReverseBinder<T>
    {
        private readonly TTarget _target;
        private readonly Func<TTarget, T?>? _getValueOnBound;
        private readonly Func<TTarget, T?>? _getValueOnUnbinding;

        /// <param name="target">The View object passed to every callback.</param>
        /// <param name="subscribe">Receives <paramref name="target"/> and the callback that raises <see cref="ValueChanged"/>; subscribe it to the View event.</param>
        /// <param name="getValueOnBound">Optional factory whose result is pushed to the ViewModel on binding.</param>
        /// <param name="getValueOnUnbinding">Optional factory whose result is pushed to the ViewModel just before unbinding.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> or <paramref name="subscribe"/> is <see langword="null"/>.
        /// </exception>
        public GenericOneWayToSourceBinder(
            TTarget target,
            Action<TTarget, Action<T>> subscribe,
            Func<TTarget, T?>? getValueOnBound = null,
            Func<TTarget, T?>? getValueOnUnbinding = null)
            : base(BindMode.OneWayToSource)
        {
            if (subscribe is null) throw new ArgumentNullException(nameof(subscribe));

            _getValueOnBound = getValueOnBound;
            _getValueOnUnbinding = getValueOnUnbinding;
            _target = target ?? throw new ArgumentNullException(nameof(target));

            subscribe.Invoke(target, OnValueChanged);
        }

        /// <param name="target">The View object passed to every callback.</param>
        /// <param name="getValueOnBound">Optional factory whose result is pushed to the ViewModel on binding.</param>
        /// <param name="getValueOnUnbinding">Optional factory whose result is pushed to the ViewModel just before unbinding.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="target"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown when both <paramref name="getValueOnBound"/> and <paramref name="getValueOnUnbinding"/> are <see langword="null"/>.
        /// </exception>
        public GenericOneWayToSourceBinder(
            TTarget target,
            Func<TTarget, T?>? getValueOnBound = null,
            Func<TTarget, T?>? getValueOnUnbinding = null)
            : base(BindMode.OneWayToSource)
        {
            if (getValueOnBound is null && getValueOnUnbinding is null)
                throw new ArgumentException($"{nameof(getValueOnBound)} and {nameof(getValueOnUnbinding)} are both null.");

            _getValueOnBound = getValueOnBound;
            _getValueOnUnbinding = getValueOnUnbinding;
            _target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <inheritdoc/>
        public event Action<T?>? ValueChanged;

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
