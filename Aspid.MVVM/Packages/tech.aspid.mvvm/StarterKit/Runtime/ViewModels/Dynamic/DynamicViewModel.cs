using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// An <see cref="IViewModel"/> whose typed properties are composed at runtime.
    /// </summary>
    public sealed class DynamicViewModel : IViewModel, IEnumerable<IDynamicProperty>
    {
        private readonly bool _throwOnMissingMember;
        private readonly Dictionary<string, IDynamicProperty> _properties;

        /// <summary>
        /// Initializes an empty runtime-composed ViewModel.
        /// </summary>
        /// <param name="throwOnMissingMember">
        /// Whether binder resolution should throw when a requested identifier is absent.
        /// </param>
        /// <param name="idComparer">The comparer used for property identifiers.</param>
        public DynamicViewModel(
            bool throwOnMissingMember = false,
            IEqualityComparer<string>? idComparer = null)
        {
            _throwOnMissingMember = throwOnMissingMember;
            _properties = new Dictionary<string, IDynamicProperty>(idComparer ?? StringComparer.Ordinal);
        }
        
        /// <summary>
        /// Gets the number of properties in the ViewModel.
        /// </summary>
        public int Count => _properties.Count;

        /// <summary>
        /// Gets all properties in the ViewModel.
        /// </summary>
        public IEnumerable<IDynamicProperty> Properties => _properties.Values;

        /// <summary>
        /// Gets a property by its identifier.
        /// </summary>
        /// <param name="id">The property identifier.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="id"/> is empty.</exception>
        /// <exception cref="KeyNotFoundException">Thrown when no property has the specified identifier.</exception>
        public IDynamicProperty this[string id] => _properties[ValidateId(id)];

        /// <summary>
        /// Adds a typed property.
        /// </summary>
        /// <typeparam name="T">The property's value type.</typeparam>
        /// <param name="id">The identifier used by binders.</param>
        /// <param name="value">The initial value.</param>
        /// <param name="mode">The binding capability exposed by the property.</param>
        /// <returns>The property handle used to read, update, or observe the value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="id"/> is empty, already exists, or <paramref name="mode"/> is
        /// <see cref="BindMode.None"/>.
        /// </exception>
        public IDynamicProperty<T> Add<T>(
            string id,
            T? value = default,
            BindMode mode = BindMode.OneWay)
        {
            var property = new DynamicProperty<T>(id, value, mode);
            Add(property);
            return property;
        }

        /// <summary>
        /// Adds a preconstructed or custom dynamic property.
        /// </summary>
        /// <param name="property">The property to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="property"/> is null.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the property identifier is empty or already exists.
        /// </exception>
        public void Add(IDynamicProperty property)
        {
            if (property is null)
                throw new ArgumentNullException(nameof(property));

            var id = ValidateId(property.Id);

            if (!_properties.TryAdd(id, property))
                throw new ArgumentException($"A dynamic property with the ID '{id}' already exists.", nameof(property));
        }

        /// <summary>
        /// Determines whether the ViewModel contains a property with the specified identifier.
        /// </summary>
        /// <param name="id">The property identifier.</param>
        /// <returns><see langword="true"/> when the property exists; otherwise, <see langword="false"/>.</returns>
        public bool Contains(string id) =>
            _properties.ContainsKey(ValidateId(id));

        /// <summary>
        /// Gets a typed property by its identifier.
        /// </summary>
        /// <typeparam name="T">The expected property value type.</typeparam>
        /// <param name="id">The property identifier.</param>
        /// <returns>The typed property.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the property does not exist.</exception>
        /// <exception cref="ArgumentException">Thrown when the property's value type is not <typeparamref name="T"/>.</exception>
        public IDynamicProperty<T> Get<T>(string id)
        {
            var property = this[id];

            if (property is IDynamicProperty<T> typedProperty)
                return typedProperty;

            throw CreateTypeMismatchException<T>(id, property.ValueType);
        }

        /// <summary>
        /// Attempts to get a typed property by its identifier.
        /// </summary>
        /// <typeparam name="T">The expected property value type.</typeparam>
        /// <param name="id">The property identifier.</param>
        /// <param name="property">The matching property, when found with the expected type.</param>
        /// <returns>
        /// <see langword="true"/> when a property with the specified identifier and type exists;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryGet<T>(string id, out IDynamicProperty<T>? property)
        {
            if (_properties.TryGetValue(ValidateId(id), out var candidate) &&
                candidate is IDynamicProperty<T> typedProperty)
            {
                property = typedProperty;
                return true;
            }

            property = null;
            return false;
        }

        /// <summary>
        /// Returns an enumerator over the properties in this ViewModel.
        /// </summary>
        public IEnumerator<IDynamicProperty> GetEnumerator() =>
            _properties.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        /// <inheritdoc/>
        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters)
        {
#if !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (this.Marker())
#endif
            {
                if (_properties.TryGetValue(parameters.Id, out var property))
                    return new FindBindableMemberResult(property.GetAdder());

                return _throwOnMissingMember 
                    ? throw new KeyNotFoundException($"Dynamic property with the ID '{parameters.Id}' was not found.") 
                    : default;
            }
        }

        private static string ValidateId(string id) => string.IsNullOrWhiteSpace(id) 
            ? throw new ArgumentException("A dynamic property ID cannot be null, empty, or whitespace.", nameof(id)) 
            : id;

        private static ArgumentException CreateTypeMismatchException<T>(string id, Type actualType) => new(
            $"Dynamic property '{id}' contains values of type '{actualType.FullName}', not '{typeof(T).FullName}'.", nameof(id));
    }
}
