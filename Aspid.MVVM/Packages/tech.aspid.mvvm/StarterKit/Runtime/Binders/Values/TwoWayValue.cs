using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> and <see cref="IReverseBinder{T}"/>
    /// that stores a value of type <typeparamref name="T"/> and synchronises it in both directions between the ViewModel and the View.
    /// Supports all binding modes; in <see cref="BindMode.OneWayToSource"/>, the current value is pushed
    /// to the ViewModel when binding is established.
    /// </summary>
    /// <typeparam name="T">The type of the bindable value.</typeparam>
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

        [Tooltip("The stored value. Set in the Inspector, it is the value before the first ViewModel push.")]
        [SerializeField] private T? _value;

        [Tooltip("Optional converter applied to each incoming value before it is stored. " +
            "Reverses only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<T?, T?>? _converter;

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
                _valueChanged?.Invoke(GetConvertedBackValue(value));
            }
        }

        /// <param name="mode">The binding mode to use.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
        public TwoWayValue(BindMode mode = BindMode.TwoWay)
            : this(default, mode) { }

        /// <param name="value">The initial value.</param>
        /// <param name="mode">The binding mode to use.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
        public TwoWayValue(T? value, BindMode mode = BindMode.TwoWay)
            : base(mode)
        {
            mode.ThrowExceptionIfNone();
            _value = value;
        }

        /// <param name="value">The initial value.</param>
        /// <param name="converter">
        /// An optional converter applied to each value received from the ViewModel before it is stored.
        /// Pass <see langword="null"/> to store values unchanged.
        /// </param>
        /// <param name="mode">The binding mode to use.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
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
        /// <remarks>
        /// The backing field is written directly rather than through <see cref="Value"/>: that property's setter
        /// is the View-side entry point and raises <see cref="IReverseBinder{T}.ValueChanged"/>, which would send
        /// every ViewModel update straight back to the ViewModel.
        /// </remarks>
        /// <param name="value">The new value received from the ViewModel.</param>
        void IBinder<T>.SetValue(T? value)
        {
            _value = _converter is not null ? _converter.Convert(value) : value;
            Changed?.Invoke(value);
        }

        /// <summary>
        /// Called after binding is established.
        /// In <see cref="BindMode.OneWayToSource"/> mode, immediately pushes the current <see cref="Value"/>
        /// to the ViewModel to synchronise the initial state.
        /// </summary>
        /// <remarks>
        /// The push goes through <see cref="GetConvertedBackValue"/>, so the initial value reaches the
        /// ViewModel in the same space as every later one.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;
            _valueChanged?.Invoke(GetConvertedBackValue(Value));
        }

        /// <summary>
        /// Implicitly converts a <see cref="TwoWayValue{T}"/> to its current <see cref="Value"/>.
        /// </summary>
        /// <param name="binder">The binder whose value is extracted.</param>
        /// <returns>The current value stored in <paramref name="binder"/>.</returns>
        public static implicit operator T?(TwoWayValue<T?> binder) => binder.Value;

        /// <summary>
        /// Converts a value on its way back to the ViewModel.
        /// </summary>
        /// <param name="value">The stored value, which the converter has already shaped.</param>
        /// <returns>
        /// The value as the ViewModel expects it: undone by the converter when it offers
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/>, and unchanged when it does not.
        /// </returns>
        /// <remarks>
        /// <see cref="Value"/> holds what the converter produced, so raising it unchanged handed the
        /// ViewModel the View's own presentation — a ViewModel that set X immediately received
        /// <c>Convert(X)</c> back. Undoing the conversion makes the round trip an identity again for
        /// a two-way converter, and leaves it as it was for a one-way one.
        /// </remarks>
        private T? GetConvertedBackValue(T? value) =>
            _converter is ITwoWayConverter<T?, T?> twoWay ? twoWay.ConvertBack(value) : value;

    }
}