#nullable enable
using System;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> and <see cref="IReverseBinder{T}"/>
    /// that synchronises values of type <typeparamref name="T"/> in both directions between the ViewModel and the View.
    /// </summary>
    /// <typeparam name="T">The type of the value exchanged between View and ViewModel.</typeparam>
    /// <remarks>
    /// Unity-specific variant of <see cref="GenericTwoWayBinder{T}"/> that accepts <see cref="UnityAction{T}"/>
    /// callbacks instead of plain <see cref="System.Action{T}"/> delegates.
    /// Optionally pushes a value to the ViewModel when the binding is established (<see cref="OnBound"/>)
    /// or just before it is released (<see cref="OnUnbinding"/>), controlled by the
    /// onBoundValueChanged and onUnboundValueChanged factory functions respectively.
    /// </remarks>
    /// <include file="XmlExampleDoc-UnityGenerics-1.1.0.xml" path="doc//member[@name='UnityGenericTwoWayBinder{1}']/*" />
    public class UnityGenericTwoWayBinder<T> : Binder, IBinder<T>, IReverseBinder<T>
    {
        /// <summary>
        /// Raised when the View-side value changes and should be propagated to the ViewModel.
        /// </summary>
        public event Action<T?>? ValueChanged;
        
        private readonly UnityAction<T?> _setValue;
        private readonly Func<T?>? _onBoundValueChanged;
        private readonly Func<T?>? _onUnboundValueChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="UnityGenericTwoWayBinder{T}"/> and immediately
        /// wires up the View-side event via <paramref name="initialize"/>.
        /// </summary>
        /// <param name="initialize">
        /// A <see cref="UnityAction{T}"/> that receives the internal <see cref="OnValueChanged"/> callback and registers it
        /// with the appropriate View event so that View changes are propagated to the ViewModel.
        /// </param>
        /// <param name="setValue">The <see cref="UnityAction{T}"/> invoked when a new value arrives from the ViewModel.</param>
        /// <param name="onBoundValueChanged">
        /// Optional factory invoked when the binding is established; the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="onUnboundValueChanged">
        /// Optional factory invoked just before the binding is released; the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        public UnityGenericTwoWayBinder(
            UnityAction<UnityAction<T>> initialize, 
            UnityAction<T?> setValue,
            Func<T?>? onBoundValueChanged = null,
            Func<T?>? onUnboundValueChanged = null)
            : this(setValue, onBoundValueChanged, onUnboundValueChanged)
        {
            initialize.Invoke(OnValueChanged);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="UnityGenericTwoWayBinder{T}"/> without an event-wire-up action.
        /// </summary>
        /// <param name="setValue">The <see cref="UnityAction{T}"/> invoked when a new value arrives from the ViewModel.</param>
        /// <param name="onBoundValueChanged">
        /// Optional factory invoked when the binding is established; the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="onUnboundValueChanged">
        /// Optional factory invoked just before the binding is released; the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        public UnityGenericTwoWayBinder(
            UnityAction<T?> setValue,
            Func<T?>? onBoundValueChanged = null,
            Func<T?>? onUnboundValueChanged = null)
            : base(BindMode.TwoWay)
        {
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Forwards <paramref name="value"/> from the ViewModel to the View setter.
        /// </summary>
        /// <param name="value">The new value received from the ViewModel.</param>
        public void SetValue(T? value) =>
            _setValue.Invoke(value);

        /// <summary>
        /// Called after binding is established.
        /// Invokes the onBoundValueChanged factory and pushes the returned value to the ViewModel,
        /// if the factory was provided.
        /// </summary>
        protected override void OnBound()
        {
            if (_onBoundValueChanged is not null)
                OnValueChanged(_onBoundValueChanged.Invoke());
        }

        /// <summary>
        /// Called just before the binding is released.
        /// Invokes the onUnboundValueChanged factory and pushes the returned value to the ViewModel,
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
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> and <see cref="IReverseBinder{T}"/>
    /// that synchronises values of type <typeparamref name="T"/> in both directions between the ViewModel
    /// and a specific <typeparamref name="TTarget"/> View object.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target View object that both receives and exposes the value.</typeparam>
    /// <typeparam name="T">The type of the value exchanged between View and ViewModel.</typeparam>
    /// <remarks>
    /// Unity-specific variant of <see cref="GenericTwoWayBinder{TTarget,T}"/> that accepts
    /// <see cref="UnityAction{T0,T1}"/> callbacks instead of plain <see cref="System.Action{T1,T2}"/> delegates.
    /// Holds a reference to a <typeparamref name="TTarget"/> instance and passes it to all factory
    /// functions, avoiding closures over Unity component references.
    /// Otherwise behaves identically to <see cref="UnityGenericTwoWayBinder{T}"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-UnityGenerics-1.1.0.xml" path="doc//member[@name='UnityGenericTwoWayBinder{2}']/*" />
    public class UnityGenericTwoWayBinder<TTarget, T> : Binder, IBinder<T>, IReverseBinder<T>
    {
        /// <summary>
        /// Raised when the View-side value changes and should be propagated to the ViewModel.
        /// </summary>
        public event Action<T?>? ValueChanged;
        
        private readonly TTarget _target;
        private readonly UnityAction<TTarget, T?> _setValue;
        private readonly Func<TTarget, T?>? _onBoundValueChanged;
        private readonly Func<TTarget, T?>? _onUnboundValueChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="UnityGenericTwoWayBinder{TTarget,T}"/> and immediately
        /// wires up the View-side event via <paramref name="initialize"/>.
        /// </summary>
        /// <param name="target">The target View object.</param>
        /// <param name="initialize">
        /// A <see cref="UnityAction{T0,T1}"/> that receives <paramref name="target"/> and the internal <see cref="OnValueChanged"/>
        /// callback, and registers it with the appropriate View event.
        /// </param>
        /// <param name="setValue">
        /// The <see cref="UnityAction{T0,T1}"/> invoked with the target and each new value received from the ViewModel.
        /// </param>
        /// <param name="onBoundValueChanged">
        /// Optional factory invoked with <paramref name="target"/> when the binding is established;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="onUnboundValueChanged">
        /// Optional factory invoked with <paramref name="target"/> just before the binding is released;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> or <paramref name="setValue"/> is <see langword="null"/>.
        /// </exception>
        public UnityGenericTwoWayBinder(
            TTarget target,
            UnityAction<TTarget, UnityAction<T>> initialize, 
            UnityAction<TTarget, T?> setValue,
            Func<TTarget, T?>? onBoundValueChanged = null,
            Func<TTarget, T?>? onUnboundValueChanged = null)
            : base(BindMode.TwoWay)
        {
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            
            initialize.Invoke(target, OnValueChanged);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="UnityGenericTwoWayBinder{TTarget,T}"/> without an
        /// event-wire-up action. At least one of <paramref name="onBoundValueChanged"/> or
        /// <paramref name="onUnboundValueChanged"/> must be non-<see langword="null"/>.
        /// </summary>
        /// <param name="target">The target View object.</param>
        /// <param name="setValue">
        /// The <see cref="UnityAction{T0,T1}"/> invoked with the target and each new value received from the ViewModel.
        /// </param>
        /// <param name="onBoundValueChanged">
        /// Optional factory invoked with <paramref name="target"/> when the binding is established;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="onUnboundValueChanged">
        /// Optional factory invoked with <paramref name="target"/> just before the binding is released;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> or <paramref name="setValue"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when both <paramref name="onBoundValueChanged"/> and <paramref name="onUnboundValueChanged"/>
        /// are <see langword="null"/>.
        /// </exception>
        public UnityGenericTwoWayBinder(
            TTarget target,
            UnityAction<TTarget, T?> setValue,
            Func<TTarget, T?>? onBoundValueChanged = null,
            Func<TTarget, T?>? onUnboundValueChanged = null)
            : base(BindMode.TwoWay)
        {
            if (onBoundValueChanged is null && onUnboundValueChanged is null)
                throw new Exception("OnBoundValueChanged and OnUnboundValueChanged are both null");
            
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Forwards <paramref name="value"/> from the ViewModel to the View setter together with the stored target.
        /// </summary>
        /// <param name="value">The new value received from the ViewModel.</param>
        public void SetValue(T? value) =>
            _setValue.Invoke(_target, value);

        /// <summary>
        /// Called after binding is established.
        /// Invokes the onBoundValueChanged factory with the stored <typeparamref name="TTarget"/>
        /// and pushes the returned value to the ViewModel, if the factory was provided.
        /// </summary>
        protected override void OnBound()
        {
            if (_onBoundValueChanged is not null)
                OnValueChanged(_onBoundValueChanged.Invoke(_target));
        }

        /// <summary>
        /// Called just before the binding is released.
        /// Invokes the onUnboundValueChanged factory with the stored <typeparamref name="TTarget"/>
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