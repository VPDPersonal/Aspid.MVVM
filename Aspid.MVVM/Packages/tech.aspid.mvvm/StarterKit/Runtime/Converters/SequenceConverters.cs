using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Chains multiple converters together, applying them sequentially to a value.
    /// </summary>
    /// <typeparam name="T">The type of the value being converted.</typeparam>
    /// <remarks>
    /// A chain authored in the Inspector is routinely incomplete — the type picker's
    /// <c>&lt;None&gt;</c> entry is a valid selection and serializes as a null element — so gaps are
    /// skipped rather than treated as an error.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid", Name = "Sequence", Tooltip = "Chains multiple converters together, applying them sequentially to a value")]
    public class SequenceConverters<T> : ITwoWayConverter<T, T>
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        [Tooltip("The converters applied in order. Empty slots are skipped.")]
        [SerializeReference] private IConverter<T, T>?[] _converters;

        public SequenceConverters()
            : this(Array.Empty<IConverter<T, T>>()) { }


        /// <param name="converters">The converters to apply in sequence. Null entries are skipped.</param>
        public SequenceConverters(params IConverter<T, T>[]? converters)
        {
            _converters = converters ?? Array.Empty<IConverter<T, T>>();
        }

        /// <summary>
        /// Converts the specified value by applying each converter in sequence.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result after all converters have been applied.</returns>
        public T Convert(T value)
        {
            if (_converters is null) return value;

            foreach (var converter in _converters)
            {
                if (converter is not null)
                    value = converter.Convert(value);
            }

            return value;
        }

        /// <summary>
        /// Converts the specified value by undoing each converter in reverse order.
        /// </summary>
        /// <param name="value">The value to convert back.</param>
        /// <returns>
        /// The result after every link has been undone, or the value unchanged if any link converts
        /// one way only — undoing part of a chain would leave the value in neither space.
        /// </returns>
        public T ConvertBack(T value)
        {
            if (_converters is null) return value;

            for (var i = _converters.Length - 1; i >= 0; i--)
            {
                if (_converters[i] is null) continue;
                if (_converters[i] is not ITwoWayConverter<T, T>) return value;
            }

            for (var i = _converters.Length - 1; i >= 0; i--)
                if (_converters[i] is ITwoWayConverter<T, T> twoWay)
                    value = twoWay.ConvertBack(value);

            return value;
        }
    }
}
