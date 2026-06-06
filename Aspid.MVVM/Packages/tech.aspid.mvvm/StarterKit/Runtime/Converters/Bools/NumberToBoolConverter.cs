using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts numeric values to boolean based on comparison operations.
    /// </summary>
    [Serializable]
    public class NumberToBoolConverter :
        IConverterFloatToBool,
        IConverterDoubleToBool,
        IConverterIntToBool,
        IConverterLongToBool
    {
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        private Comparisons _comparison;

#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        private float _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberToBoolConverter"/> class with default settings.
        /// </summary>
        public NumberToBoolConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberToBoolConverter"/> class.
        /// </summary>
        /// <param name="comparison">The comparison operation to perform.</param>
        /// <param name="value">The value to compare against.</param>
        public NumberToBoolConverter(Comparisons comparison, float value)
        {
            _value = value;
            _comparison = comparison;
        }

        /// <summary>
        /// Converts a float value to boolean using the configured comparison.
        /// </summary>
        /// <param name="value">The value to compare.</param>
        /// <returns>The result of the comparison operation.</returns>
        public bool Convert(float value) =>
            Compare(value);

        /// <summary>
        /// Converts a double value to boolean using the configured comparison.
        /// </summary>
        /// <param name="value">The value to compare.</param>
        /// <returns>The result of the comparison operation.</returns>
        public bool Convert(double value) =>
            Compare(value);

        /// <summary>
        /// Converts an int value to boolean using the configured comparison.
        /// </summary>
        /// <param name="value">The value to compare.</param>
        /// <returns>The result of the comparison operation.</returns>
        public bool Convert(int value) =>
            Compare(value);

        /// <summary>
        /// Converts a long value to boolean using the configured comparison.
        /// </summary>
        /// <param name="value">The value to compare.</param>
        /// <returns>The result of the comparison operation.</returns>
        public bool Convert(long value) =>
            Compare(value);

        /// <summary>
        /// Performs the configured comparison in double precision so that large
        /// integer and double magnitudes compare exactly without float rounding.
        /// </summary>
        private bool Compare(double value) => _comparison switch
        {
            Comparisons.LessThan => value < _value,
            Comparisons.GreaterThan => value > _value,
            Comparisons.LessThanOrEqual => value <= _value,
            Comparisons.GreaterThanOrEqual => value >= _value,
            Comparisons.Equal => Approximately(_value, value),
            Comparisons.Inequality => !Approximately(_value, value),
            _ => throw new ArgumentOutOfRangeException()
        };

        /// <summary>
        /// Checks if two float values are approximately equal with tolerance for floating-point precision.
        /// </summary>
        private static bool Approximately(double a, double b) =>
            Math.Abs(b - a) < Math.Max(1E-06f * Math.Max(Math.Abs(a), Math.Abs(b)), float.Epsilon * 8f);
    }
}