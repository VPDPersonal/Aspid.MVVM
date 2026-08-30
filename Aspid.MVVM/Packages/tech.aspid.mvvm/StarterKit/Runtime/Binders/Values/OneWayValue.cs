using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that stores the most recently received
    /// ViewModel value of type <typeparamref name="T"/> and notifies subscribers when it changes.
    /// </summary>
    /// <typeparam name="T">The type of the bindable value.</typeparam>
    [Serializable]
    public class OneWayValue<T> : Binder, IBinder<T>
    {
        [Tooltip("The stored value. Set in the Inspector, it is the value before the first ViewModel push.")]
        [SerializeField] private T? _value;

        [Tooltip("Optional converter applied to each incoming value before it is stored.")]
        [SerializeReference] private IConverter<T?, T?>? _converter;

        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public OneWayValue(BindMode mode = BindMode.OneWay)
            : this(default, mode) { }

        /// <param name="value">The initial value.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public OneWayValue(T? value, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _value = value;
        }

        /// <param name="value">The initial value.</param>
        /// <param name="converter">
        /// An optional converter applied to each incoming value before it is stored in <see cref="Value"/>.
        /// Pass <see langword="null"/> to store values unchanged.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public OneWayValue(T? value, IConverter<T?, T?>? converter, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();

            _value = value;
            _converter = converter;
        }

        /// <summary>
        /// Raised with the new pre-conversion value when <see cref="Value"/> is updated.
        /// </summary>
        public event Action<T?>? Changed;

        /// <summary>
        /// Gets the most recently received (and optionally converted) value.
        /// </summary>
        public T? Value
        {
            get => _value;
            private set => _value = value;
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
        /// Implicitly converts a <see cref="OneWayValue{T}"/> to its current <see cref="Value"/>.
        /// </summary>
        /// <param name="binder">The binder whose value is extracted.</param>
        /// <returns>The current value stored in <paramref name="binder"/>.</returns>
        public static implicit operator T?(OneWayValue<T?> binder) => binder.Value;
    }
}