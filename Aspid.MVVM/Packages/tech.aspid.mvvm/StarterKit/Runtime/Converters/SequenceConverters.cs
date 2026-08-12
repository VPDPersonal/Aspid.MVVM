using System;

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
    public class SequenceConverters<T> : IConverter<T, T>
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
#if UNITY_2023_1_OR_NEWER
        [UnityEngine.SerializeReference]
#endif
        private IConverter<T, T>?[] _converters;

        public SequenceConverters()
            : this(Array.Empty<IConverter<T, T>>()) { }

        /// <param name="converters">The converters to apply in sequence. Null entries are skipped.</param>
        public SequenceConverters(params IConverter<T, T>[] converters)
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
    }
}
