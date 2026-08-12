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
        [UnityEngine.Tooltip("Converters applied to the value one after another, in the order listed here.")]
        [UnityEngine.SerializeReference]
#endif
        private IConverter<T, T>[] _converters;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceConverters{T}"/> class with an empty sequence,
        /// leaving the value untouched until converters are added.
        /// </summary>
        /// <remarks>
        /// Required by the type picker, which builds the chosen type through its parameterless constructor: without
        /// one it hands back an instance whose array is <see langword="null"/>, and the first converted value throws.
        /// </remarks>
        public SequenceConverters()
        {
            _converters = Array.Empty<IConverter<T, T>>();
        }

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