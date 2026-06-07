using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> and <see cref="IReverseBinder{T}"/>
    /// that stores a value of type <typeparamref name="T"/> and synchronises it in both directions between the ViewModel and the View.
    /// </summary>
    /// <typeparam name="T">The type of the bindable value.</typeparam>
    /// <remarks>
    /// <para>
    /// Supports all binding modes (<see cref="BindModeOverrideAttribute"/> with <c>IsAll = true</c>).
    /// </para>
    /// <para>
    /// When a new value arrives from the ViewModel via <see cref="IBinder{T}.SetValue"/>, it is
    /// optionally passed through an <see cref="IConverter{TFrom,TTo}"/> before being stored, and the
    /// <see cref="Changed"/> event is raised with the original unconverted value.
    /// </para>
    /// <para>
    /// When <see cref="Value"/> is set from the View side, <see cref="IReverseBinder{T}.ValueChanged"/>
    /// is raised so the ViewModel receives the update.
    /// </para>
    /// <para>
    /// In <see cref="BindMode.OneWayToSource"/> mode, <see cref="OnBound"/> pushes the current
    /// <see cref="Value"/> to the ViewModel immediately on binding so that the initial state is synchronised.
    /// </para>
    /// <para>
    /// An implicit conversion operator allows instances to be used directly where a
    /// <typeparamref name="T"/> value is expected.
    /// </para>
    /// </remarks>
    /// <include file="XmlExampleDoc-Values-1.1.0.xml" path="doc//member[@name='TwoWayValue{1}']/*" />
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public class TwoWayValue<T> : Binder, IBinder<T>, IReverseBinder<T>
    {
        /// <summary>
        /// Raised with the new pre-conversion value when the ViewModel updates <see cref="Value"/> via <see cref="IBinder{T}.SetValue"/>.
        /// </summary>
        public event Action<T?>? Changed;

        /// <inheritdoc/>
        event Action<T?>? IReverseBinder<T>.ValueChanged
        {
            add => _valueChanged += value;
            remove => _valueChanged -= value;
        }

#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        private T? _value;

#if UNITY_2023_1_OR_NEWER
        [UnityEngine.SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<T?, T?>? _converter;

        private Action<T?>? _valueChanged;

        /// <summary>
        /// Gets or sets the current value.
        /// Setting this property raises <see cref="IReverseBinder{T}.ValueChanged"/> so the ViewModel
        /// is notified.
        /// </summary>
        public T? Value
        {
            get => _value;
            set
            {
                _value = value;
                _valueChanged?.Invoke(value);
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TwoWayValue{T}"/> with the default value.
        /// </summary>
        /// <param name="mode">The binding mode to use.</param>
        public TwoWayValue(BindMode mode = BindMode.TwoWay)
            : this(default, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="TwoWayValue{T}"/> with a pre-set initial value.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="mode">The binding mode to use.</param>
        public TwoWayValue(T? value, BindMode mode = BindMode.TwoWay)
            : base(mode)
        {
            mode.ThrowExceptionIfNone();
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TwoWayValue{T}"/> with a pre-set initial value and a converter.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="converter">
        /// An optional converter applied to each value received from the ViewModel before it is stored.
        /// Pass <see langword="null"/> to store values unchanged.
        /// </param>
        /// <param name="mode">The binding mode to use.</param>
        public TwoWayValue(T? value, IConverter<T?, T?>? converter, BindMode mode = BindMode.TwoWay)
            : base(mode)
        {
            mode.ThrowExceptionIfNone();

            _value = value;
            _converter = converter;
        }

        /// <summary>
        /// Stores the incoming ViewModel value (passing it through the converter if one is set)
        /// and raises <see cref="Changed"/> with the original unconverted value.
        /// </summary>
        /// <param name="value">The new value received from the ViewModel.</param>
        void IBinder<T>.SetValue(T? value)
        {
            Value = _converter is not null ? _converter.Convert(value) : value;
            Changed?.Invoke(value);
        }

        /// <summary>
        /// Called after binding is established.
        /// In <see cref="BindMode.OneWayToSource"/> mode, immediately pushes the current <see cref="Value"/>
        /// to the ViewModel to synchronise the initial state.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;
            _valueChanged?.Invoke(Value);
        }

        /// <summary>
        /// Implicitly converts a <see cref="TwoWayValue{T}"/> to its current <see cref="Value"/>.
        /// </summary>
        /// <param name="binder">The binder whose value is extracted.</param>
        /// <returns>The current value stored in <paramref name="binder"/>.</returns>
        public static implicit operator T?(TwoWayValue<T?> binder) => binder.Value;
    }
}