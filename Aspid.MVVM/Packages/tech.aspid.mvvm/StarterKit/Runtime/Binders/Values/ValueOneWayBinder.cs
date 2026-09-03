#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that stores the latest ViewModel value and raises <see cref="Changed"/>.
    /// </summary>
    /// <typeparam name="T">The type of the stored value.</typeparam>
    [Serializable]
    public class ValueOneWayBinder<T> : Binder, IBinder<T>
    {
        [Tooltip("Initial value until the ViewModel pushes one.")]
        [SerializeField] private T? _value;

        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<T?, T?>? _converter;

        /// <param name="value">The initial value.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public ValueOneWayBinder(
            T? value = default,
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _value = value;
        }

        /// <param name="value">The initial value.</param>
        /// <param name="converter">The converter applied to each incoming value, or <see langword="null"/> to store it unchanged.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public ValueOneWayBinder(
            T? value,
            IConverter<T?, T?>? converter, 
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();

            _value = value;
            _converter = converter;
        }

        /// <summary>
        /// Raised with the unconverted ViewModel value when <see cref="Value"/> is updated.
        /// </summary>
        public event Action<T?>? Changed;

        /// <summary>
        /// Gets the latest, converted value.
        /// </summary>
        public T? Value
        {
            get => _value;
            private set => _value = value;
        }

        /// <summary>
        /// Stores the converted <paramref name="value"/> and raises <see cref="Changed"/> with the original one.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        void IBinder<T>.SetValue(T? value)
        {
            Value = _converter is not null ? _converter.Convert(value) : value;
            Changed?.Invoke(value);
        }

        /// <summary>
        /// Returns <see cref="Value"/>.
        /// </summary>
        /// <param name="binder">The binder to read.</param>
        /// <returns>The current <see cref="Value"/>.</returns>
        public static implicit operator T?(ValueOneWayBinder<T> binder) => binder.Value;
    }
}
