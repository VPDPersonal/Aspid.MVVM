#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> and <see cref="IReverseBinder{T}"/> that stores a value
    /// and synchronizes it in both directions. Supports every binding mode; in <see cref="BindMode.OneWayToSource"/>,
    /// the current value is pushed to the ViewModel on binding.
    /// </summary>
    /// <typeparam name="T">The type of the stored value.</typeparam>
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public class ValueTwoWayBinder<T> : Binder, IBinder<T>, IReverseBinder<T>
    {
        [Tooltip("Initial value until the ViewModel pushes one.")]
        [SerializeField] private T? _value;

        [Tooltip("Optional converter applied to the value; empty leaves it as-is. Reverses only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<T?, T?>? _converter;

        private Action<T?>? _valueChanged;

        /// <param name="value">The initial value.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
        public ValueTwoWayBinder(
            T? value = default,
            BindMode mode = BindMode.TwoWay)
            : base(mode)
        {
            mode.ThrowExceptionIfNone();
            _value = value;
        }

        /// <param name="value">The initial value.</param>
        /// <param name="converter">
        /// The converter applied to each ViewModel value, or <see langword="null"/> to store it unchanged.
        /// Runs in reverse only if it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
        public ValueTwoWayBinder(
            T? value, 
            IConverter<T?, T?>? converter, 
            BindMode mode = BindMode.TwoWay)
            : base(mode)
        {
            mode.ThrowExceptionIfNone();

            _value = value;
            _converter = converter;
        }

        /// <summary>
        /// Raised with the unconverted ViewModel value when it updates <see cref="Value"/>.
        /// </summary>
        public event Action<T?>? Changed;

        /// <inheritdoc/>
        event Action<T?>? IReverseBinder<T>.ValueChanged
        {
            add => _valueChanged += value;
            remove => _valueChanged -= value;
        }

        /// <summary>
        /// Gets or sets the current value. Setting it notifies the ViewModel through <see cref="IReverseBinder{T}.ValueChanged"/>.
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

        /// <summary>
        /// Stores the converted <paramref name="value"/> and raises <see cref="Changed"/> with the original one.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        /// <remarks>
        /// Writes the field directly: the <see cref="Value"/> setter would echo the update back to the ViewModel.
        /// </remarks>
        void IBinder<T>.SetValue(T? value)
        {
            _value = _converter is not null ? _converter.Convert(value) : value;
            Changed?.Invoke(value);
        }

        /// <summary>
        /// Pushes the current <see cref="Value"/> to the ViewModel in <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;
            _valueChanged?.Invoke(GetConvertedBackValue(Value));
        }

        private T? GetConvertedBackValue(T? value) => _converter is ITwoWayConverter<T?, T?> twoWay 
            ? twoWay.ConvertBack(value)
            : value;

        /// <summary>
        /// Returns <see cref="Value"/>.
        /// </summary>
        /// <param name="binder">The binder to read.</param>
        /// <returns>The current <see cref="Value"/>.</returns>
        public static implicit operator T?(ValueTwoWayBinder<T> binder) => binder.Value;
    }
}
