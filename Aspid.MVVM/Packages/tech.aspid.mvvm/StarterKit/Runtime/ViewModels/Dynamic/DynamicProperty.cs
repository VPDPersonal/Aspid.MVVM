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
    public class DynamicProperty<T> : IDynamicProperty<T>
    {
        private T? _value;
        private readonly IBinderAdder _bindableMember;
        private readonly Action<T?> _publishValue;

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
            : this(id, value, mode, CreateMember) { }

        private protected DynamicProperty(
            string id,
            T? value,
            BindMode mode,
            Func<BindMode, T?, Action<T?>, (IBinderAdder Member, Action<T?> Publish)> createMember)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("A dynamic property ID cannot be null, empty, or whitespace.", nameof(id));

            if (mode is BindMode.None)
                throw new ArgumentException("BindMode.None cannot expose a dynamic property.", nameof(mode));

            Id = id;
            Mode = mode;
            _value = value;
            (_bindableMember, _publishValue) = createMember(mode, value, SetValue);
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
        public IBinderAdder GetAdder() => _bindableMember;

        private void SetValue(T? value)
        {
            if (EqualityComparer<T?>.Default.Equals(_value, value)) return;
            _value = value;

            _publishValue(value);

            ValueChanged?.Invoke(value);
        }

        private static (IBinderAdder Member, Action<T?> Publish) CreateMember(
            BindMode mode,
            T? value,
            Action<T?> setter)
        {
            switch (mode)
            {
                case BindMode.OneTime:
                    var oneTime = new OneTimeBindableMember<T>(value);
                    return (oneTime, next => oneTime.Value = next);

                case BindMode.OneWay:
                    var oneWay = new OneWayBindableMember<T>(value);
                    return (oneWay, next => oneWay.Value = next);

                case BindMode.TwoWay:
                case BindMode.OneWayToSource:
                    var twoWay = new TwoWayBindableMember<T>(value, setter);
                    return (twoWay, next => twoWay.Value = next);

                default: throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unsupported binding mode.");
            }
        }
    }
}
