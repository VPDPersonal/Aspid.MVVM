#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides non-generic access to a property stored by a <see cref="DynamicViewModel"/>.
    /// </summary>
    public interface IDynamicProperty
    {
        /// <summary>
        /// Gets the identifier used by binders to resolve the property.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the property's value type.
        /// </summary>
        public Type ValueType { get; }

        /// <summary>
        /// Gets the binding capability exposed by the property.
        /// </summary>
        public BindMode Mode { get; }

        /// <summary>
        /// Gets or sets the current value without compile-time type information.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when the assigned value is incompatible with <see cref="ValueType"/>.
        /// </exception>
        public object? UntypedValue { get; set; }

        /// <summary>
        /// Gets the binding endpoint used to connect a binder to the property.
        /// </summary>
        public IBinderAdder GetAdder();
    }

    /// <summary>
    /// Provides typed access to a property stored by a <see cref="DynamicViewModel"/>.
    /// </summary>
    /// <typeparam name="T">The property's value type.</typeparam>
    public interface IDynamicProperty<T> : IDynamicProperty
    {
        /// <summary>
        /// Raised after the property's value changes.
        /// </summary>
        public event Action<T?>? ValueChanged;

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        public T? Value { get; set; }
    }
}
