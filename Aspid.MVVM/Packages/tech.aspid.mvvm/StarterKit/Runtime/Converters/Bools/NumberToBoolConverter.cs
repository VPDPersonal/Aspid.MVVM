using Aspid.FastTools.Types;
using System;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts numeric values to boolean based on comparison operations.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Bool", Name = "Number To Bool", Tooltip = "Converts numeric values to boolean based on comparison operations")]
    public class NumberToBoolConverter :
        IConverterFloatToBool,
        IConverterDoubleToBool,
        IConverterIntToBool,
        IConverterLongToBool
    {
        [UnityEngine.Tooltip("How the bound number is compared with the value below.")]
        [UnityEngine.SerializeField]
        private Comparisons _comparison;

        [UnityEngine.Tooltip("The number the bound one is compared against.")]
        [UnityEngine.SerializeField]
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
        /// Performs the configured comparison of the bound value against the authored one.
        /// </summary>
        /// <remarks>
        /// The authored value is held as a <see langword="float"/> and rounded on assignment, so widening
        /// here restores nothing: a large <see langword="int"/> or <see langword="long"/> reaches this
        /// method intact and is measured against a threshold that is not.
        /// <para>
        /// <see cref="Comparisons.Equal"/> and <see cref="Comparisons.NotEqual"/> match within a
        /// magnitude-scaled tolerance while the four ordering comparisons are bare operators, so the two
        /// disagree at the boundary: against two million, a value one above reports as
        /// <see cref="Comparisons.Equal"/> and <see cref="Comparisons.GreaterThan"/> at once.
        /// </para>
        /// </remarks>
        private bool Compare(double value) => _comparison switch
        {
            Comparisons.LessThan => value < _value,
            Comparisons.GreaterThan => value > _value,
            Comparisons.LessThanOrEqual => value <= _value,
            Comparisons.GreaterThanOrEqual => value >= _value,
            Comparisons.Equal => Approximately(_value, value),
            Comparisons.NotEqual => !Approximately(_value, value),
            _ => throw new ArgumentOutOfRangeException(nameof(_comparison), _comparison, null)
        };

        /// <summary>
        /// Checks whether two values are approximately equal, within a tolerance scaled to their magnitude.
        /// </summary>
        /// <remarks>
        /// Both tolerance constants are calibrated for <see langword="float"/> — 1e-6 relative, and
        /// <see cref="float.Epsilon"/> times eight as the floor near zero — while the parameters are
        /// <see langword="double"/>. A <see langword="double"/> or <see langword="long"/> is therefore
        /// judged against a float-grade epsilon and reads as equal long before its own precision runs out.
        /// </remarks>
        private static bool Approximately(double a, double b) =>
            Math.Abs(b - a) < Math.Max(1E-06f * Math.Max(Math.Abs(a), Math.Abs(b)), float.Epsilon * 8f);
    }
}