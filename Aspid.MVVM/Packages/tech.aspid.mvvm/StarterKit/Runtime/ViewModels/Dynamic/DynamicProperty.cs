#nullable enable
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A typed, observable property that can be added to a <see cref="DynamicViewModel"/>.
    /// </summary>
    /// <typeparam name="T">The property's value type.</typeparam>
    public sealed class DynamicProperty<T> : IDynamicProperty<T>
    {
        private T? _value;
        private readonly IBinderAdder? _bindableMember;

        /// <inheritdoc/>
        public string Id { get; }

        /// <inheritdoc/>
        public BindMode Mode { get; }

        /// <param name="id">The identifier used by binders to resolve the property.</param>
        /// <param name="value">The initial value.</param>
        /// <param name="mode">The binding capability exposed by the property.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="id"/> is empty or <paramref name="mode"/> is
        /// <see cref="BindMode.None"/>.
        /// </exception>
        /// <remarks>
        /// <see cref="BindMode.OneWayToSource"/> shares the two-way member: the property still pushes its value to the View.
        /// </remarks>
        public DynamicProperty(
            string id,
            T? value = default,
            BindMode mode = BindMode.OneWay)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("A dynamic property ID cannot be null, empty, or whitespace.", nameof(id));

            if (mode is BindMode.None)
                throw new ArgumentException("BindMode.None cannot expose a dynamic property.", nameof(mode));

            Id = id;
            Mode = mode;
            _value = value;
            _bindableMember = mode switch
            {
                BindMode.OneTime => null,
                BindMode.OneWay => new OneWayBindableMember<T>(value),
                BindMode.TwoWay or BindMode.OneWayToSource => new TwoWayBindableMember<T>(value, SetValue),
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unsupported binding mode.")
            };
        }

        /// <inheritdoc/>
        public event Action<T?>? ValueChanged;

        /// <inheritdoc/>
        public Type ValueType => typeof(T);

        /// <inheritdoc/>
        public T? Value
        {
            get => _value;
            set => SetValue(value);
        }

        /// <inheritdoc/>
        public object? UntypedValue
        {
            get => Value;
            set
            {
                if (value is null)
                {
                    Value = default;
                    return;
                }

                if (value is T typedValue)
                {
                    Value = typedValue;
                    return;
                }

                throw new ArgumentException(
                    $"Dynamic property '{Id}' accepts values of type '{ValueType.FullName}', not '{value.GetType().FullName}'.",
                    nameof(value));
            }
        }

        /// <inheritdoc/>
        public IBinderAdder GetAdder() => Mode is BindMode.OneTime
            ? OneTimeBindableMember<T?>.Get(_value)
            : _bindableMember!;

        private void SetValue(T? value)
        {
            if (EqualityComparer<T?>.Default.Equals(_value, value)) return;
            _value = value;

            switch (_bindableMember)
            {
                case OneWayBindableMember<T> oneWay:
                    oneWay.Value = value;
                    break;

                case TwoWayBindableMember<T> twoWay:
                    twoWay.Value = value;
                    break;
            }

            ValueChanged?.Invoke(value);
        }
    }
}
