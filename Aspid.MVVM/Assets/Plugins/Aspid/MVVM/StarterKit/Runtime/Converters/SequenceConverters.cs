using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Chains multiple converters together, applying them sequentially to a value.
    /// </summary>
    /// <typeparam name="T">The type of the value being converted.</typeparam>
    [Serializable]
    public class SequenceConverters<T> : IConverter<T, T>
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
#if UNITY_2023_1_OR_NEWER
        [SerializeReferenceDropdown]
        [UnityEngine.SerializeReference]
#endif
        private IConverter<T, T>[] _converters;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceConverters{T}"/> class.
        /// </summary>
        /// <param name="converters">The converters to apply in sequence.</param>
        public SequenceConverters(params IConverter<T, T>[] converters)
        {
            _converters = converters;
        }

        /// <summary>
        /// Converts the specified value by applying each converter in sequence.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result after all converters have been applied.</returns>
        public T Convert(T value)
        {
            foreach (var converter in _converters)
                value = converter.Convert(value);

            return value;
        }
    }
}