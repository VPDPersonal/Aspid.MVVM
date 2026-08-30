using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IReverseBinder{T}"/> that propagates values of type <typeparamref name="T"/>
    /// from the View back to the ViewModel.
    /// </summary>
    /// <typeparam name="T">The type of the value reported to the ViewModel.</typeparam>
    public class GenericOneWayToSourceBinder<T> : Binder, IReverseBinder<T>
    {
        private readonly Func<T?>? _onBoundValueChanged;
        private readonly Func<T?>? _onUnboundValueChanged;

        /// <param name="initialize">
        /// An action that receives the internal <c>OnValueChanged</c> callback and registers it with the View event.
        /// </param>
        /// <param name="onBoundValueChanged">
        /// Optional factory invoked when the binding is established; the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="onUnboundValueChanged">
        /// Optional factory invoked just before the binding is released; the returned value is pushed to the ViewModel.
        /// </param>
        public GenericOneWayToSourceBinder(
            Action<Action<T>> initialize,
            Func<T?>? onBoundValueChanged = null,
            Func<T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            initialize.Invoke(OnValueChanged);

            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
        }

        /// <param name="onBoundValueChanged">
        /// Optional factory invoked when the binding is established; the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="onUnboundValueChanged">
        /// Optional factory invoked just before the binding is released; the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="Exception">
        /// Thrown when both <paramref name="onBoundValueChanged"/> and <paramref name="onUnboundValueChanged"/>
        /// are <see langword="null"/>.
        /// </exception>
        public GenericOneWayToSourceBinder(
            Func<T?>? onBoundValueChanged = null,
            Func<T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            if (onBoundValueChanged is null && onUnboundValueChanged is null)
                throw new Exception("OnBoundValueChanged and OnUnboundValueChanged are both null");

            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
        }

        /// <summary>
        /// Raised when the View-side value changes and should be propagated to the ViewModel.
        /// </summary>
        public event Action<T?>? ValueChanged;

        /// <summary>
        /// Called after binding is established.
        /// Invokes the <c>onBoundValueChanged</c> factory and pushes the returned value to the ViewModel,
        /// if the factory was provided.
        /// </summary>
        protected override void OnBound()
        {
            if (_onBoundValueChanged is not null)
                OnValueChanged(_onBoundValueChanged.Invoke());
        }

        /// <summary>
        /// Called just before the binding is released.
        /// Invokes the <c>onUnboundValueChanged</c> factory and pushes the returned value to the ViewModel,
        /// if the factory was provided.
        /// </summary>
        protected override void OnUnbinding()
        {
            if (_onUnboundValueChanged is not null)
                OnValueChanged(_onUnboundValueChanged.Invoke());
        }

        private void OnValueChanged(T? value) =>
            ValueChanged?.Invoke(value);
    }

    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IReverseBinder{T}"/> that propagates values of type <typeparamref name="T"/>
    /// from the View back to the ViewModel, holding a <typeparamref name="TTarget"/> reference to avoid closures.
    /// </summary>
    /// <typeparam name="TTarget">The type of the View-side target object that exposes the value.</typeparam>
    /// <typeparam name="T">The type of the value reported to the ViewModel.</typeparam>
    /// <remarks>
    /// Holds a reference to a <typeparamref name="TTarget"/> instance and passes it to all factory
    /// functions, avoiding closures. Otherwise behaves identically to <see cref="GenericOneWayToSourceBinder{T}"/>.
    /// </remarks>
    public class GenericOneWayToSourceBinder<TTarget, T> : Binder, IReverseBinder<T>
    {
        private readonly TTarget _target;
        private readonly Func<TTarget, T?>? _onBoundValueChanged;
        private readonly Func<TTarget, T?>? _onUnboundValueChanged;

        /// <param name="target">The target object whose event or value is monitored.</param>
        /// <param name="initialize">
        /// An action that receives <paramref name="target"/> and the internal <c>OnValueChanged</c>
        /// callback, and registers it with the appropriate View event.
        /// </param>
        /// <param name="onBoundValueChanged">
        /// Optional factory invoked with <paramref name="target"/> when the binding is established;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="onUnboundValueChanged">
        /// Optional factory invoked with <paramref name="target"/> just before the binding is released;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="target"/> is <see langword="null"/>.</exception>
        public GenericOneWayToSourceBinder(
            TTarget target,
            Action<TTarget, Action<T>> initialize,
            Func<TTarget, T?>? onBoundValueChanged = null,
            Func<TTarget, T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            _target = target ?? throw new ArgumentNullException(nameof(target));

            initialize.Invoke(target, OnValueChanged);
        }

        /// <param name="target">The target object whose value is read when bound or unbound.</param>
        /// <param name="onBoundValueChanged">
        /// Optional factory invoked with <paramref name="target"/> when the binding is established;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="onUnboundValueChanged">
        /// Optional factory invoked with <paramref name="target"/> just before the binding is released;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="target"/> is <see langword="null"/>.</exception>
        /// <exception cref="Exception">
        /// Thrown when both <paramref name="onBoundValueChanged"/> and <paramref name="onUnboundValueChanged"/>
        /// are <see langword="null"/>.
        /// </exception>
        public GenericOneWayToSourceBinder(
            TTarget target,
            Func<TTarget, T?>? onBoundValueChanged = null,
            Func<TTarget, T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            if (onBoundValueChanged is null && onUnboundValueChanged is null)
                throw new Exception("OnBoundValueChanged and OnUnboundValueChanged are both null");

            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            _target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Raised when the View-side value changes and should be propagated to the ViewModel.
        /// </summary>
        public event Action<T?>? ValueChanged;

        /// <summary>
        /// Called after binding is established.
        /// Invokes the <c>onBoundValueChanged</c> factory with the stored <typeparamref name="TTarget"/>
        /// and pushes the returned value to the ViewModel, if the factory was provided.
        /// </summary>
        protected override void OnBound()
        {
            if (_onBoundValueChanged is not null)
                OnValueChanged(_onBoundValueChanged.Invoke(_target));
        }

        /// <summary>
        /// Called just before the binding is released.
        /// Invokes the <c>onUnboundValueChanged</c> factory with the stored <typeparamref name="TTarget"/>
        /// and pushes the returned value to the ViewModel, if the factory was provided.
        /// </summary>
        protected override void OnUnbinding()
        {
            if (_onUnboundValueChanged is not null)
                OnValueChanged(_onUnboundValueChanged.Invoke(_target));
        }

        private void OnValueChanged(T? value) =>
            ValueChanged?.Invoke(value);
    }
}