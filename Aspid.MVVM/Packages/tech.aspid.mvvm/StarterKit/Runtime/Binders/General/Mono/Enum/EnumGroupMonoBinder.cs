using System;
using UnityEngine;
using Aspid.FastTools.Enums;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that maps a bound <see cref="Enum"/> to a group of elements: the matching entry
    /// receives <see cref="SetSelectedValue"/>, every other entry receives <see cref="SetDefaultValue"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group.</typeparam>
    public abstract partial class EnumGroupMonoBinder<TElement> : MonoBinder, IBinder<Enum>
    {
        [Tooltip("Group element for each enum member.")]
        [SerializeField] private EnumValues<TElement> _enumValues;

        /// <summary>
        /// Applies the selected state to the entry matching <paramref name="value"/> and the default state to the rest.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        /// <remarks>
        /// An entry without an element is logged and skipped; the rest of the group is still updated.
        /// </remarks>
        [BinderLog]
        public void SetValue(Enum value)
        {
            foreach (var entry in _enumValues)
            {
                if (IsEmpty(entry.Value))
                {
                    this.LogError(
                        problem: $"the {entry.Key.Describe()} entry of the enum table has no {typeof(TElement).Name} assigned",
                        consequence: "The entry is skipped; the rest of the group is still updated.");

                    continue;
                }

                if (_enumValues.Equals(value, entry.Key)) SetSelectedValue(entry.Value);
                else SetDefaultValue(entry.Value);
            }
        }

        // Unity's bool conversion: a destroyed element is not null to C#.
        private static bool IsEmpty(TElement element) => element is UnityEngine.Object unityObject
            ? !unityObject
            : element is null;

        /// <summary>
        /// Applies the default state to <paramref name="element"/>.
        /// </summary>
        /// <param name="element">A non-matching group element.</param>
        protected abstract void SetDefaultValue(TElement element);

        /// <summary>
        /// Applies the selected state to <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The group element matching the bound value.</param>
        protected abstract void SetSelectedValue(TElement element);
    }

    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinder{TElement}"/> whose selected and default states are two preset
    /// <typeparamref name="TValue"/>s, each passed through an optional converter before <see cref="SetValue(TElement, TValue)"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group.</typeparam>
    /// <typeparam name="TValue">The type of value applied to each element.</typeparam>
    public abstract class EnumGroupMonoBinder<TElement, TValue> : EnumGroupMonoBinder<TElement>
    {
        [Tooltip("Value applied to non-matching elements.")]
        [SerializeField] private TValue _defaultValue;

        [Tooltip("Value applied to the matching element.")]
        [SerializeField] private TValue _selectedValue;

        [Tooltip("Optional converter applied to the default value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<TValue, TValue> _defaultConverter;

        [Tooltip("Optional converter applied to the selected value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<TValue, TValue> _selectedConverter;

        /// <inheritdoc/>
        protected sealed override void SetDefaultValue(TElement element) =>
            SetValue(element, _defaultConverter is null ? _defaultValue : _defaultConverter.Convert(_defaultValue));

        /// <inheritdoc/>
        protected sealed override void SetSelectedValue(TElement element) =>
            SetValue(element, _selectedConverter is null ? _selectedValue : _selectedConverter.Convert(_selectedValue));

        /// <summary>
        /// Applies <paramref name="value"/> to <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The target element.</param>
        /// <param name="value">The value to apply.</param>
        protected abstract void SetValue(TElement element, TValue value);
    }
}
