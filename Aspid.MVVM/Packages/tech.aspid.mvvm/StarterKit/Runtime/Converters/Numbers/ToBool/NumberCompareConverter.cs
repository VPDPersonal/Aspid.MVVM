using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts numeric values to boolean based on comparison operations.
    /// </summary>
    /// <remarks>
    /// The tolerance follows the incoming type: none for <see cref="int"/> and <see cref="long"/>,
    /// relative 1e-6 for <see cref="float"/>, relative 1e-12 for <see cref="double"/>.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Bool",
        Name = "Compare",
        Tooltip = "Converts numeric values to boolean based on comparison operations")]
    public sealed class NumberCompareConverter :
        IConverter<int, bool>,
        IConverter<long, bool>,
        IConverter<float, bool>,
        IConverter<double, bool>
    {
        // A float carries ~7 significant digits and a double ~15; both tolerances stay an order of
        // magnitude clear of the last one so that arithmetic noise does not read as a difference.
        private const double FloatTolerance = 1E-06d;
        private const double DoubleTolerance = 1E-12d;

        [Tooltip("How the bound number is compared with the value below.")]
        [SerializeField] private ComparisonMode _comparison;

        [Tooltip("The number the bound one is compared against.")]
        [SerializeField] private double _value;

        /// <remarks>Default: testing equality with zero.</remarks>
        public NumberCompareConverter() { }

        /// <param name="comparison">How the bound number is compared with <paramref name="value"/>.</param>
        /// <param name="value">The number the bound one is compared against.</param>
        public NumberCompareConverter(ComparisonMode comparison, double value)
        {
            _value = value;
            _comparison = comparison;
        }

        /// <summary>
        /// Compares the bound number with the authored one.
        /// </summary>
        /// <param name="value">The value to compare.</param>
        /// <returns>
        /// The result of the comparison. An undeclared comparison reports an error and returns
        /// <see langword="false"/>.
        /// </returns>
        public bool Convert(float value) =>
            Compare(value, Tolerance(value, relative: FloatTolerance, floor: float.Epsilon * 8d));

        /// <inheritdoc cref="Convert(float)"/>
        public bool Convert(double value) =>
            Compare(value, Tolerance(value, relative: DoubleTolerance, floor: double.Epsilon * 8d));

        /// <inheritdoc cref="Convert(float)"/>
        public bool Convert(int value) => Compare(value);

        /// <inheritdoc cref="Convert(float)"/>
        /// <remarks>
        /// A long beyond 2^53 loses its last digits on the way to <see cref="double"/>, so two
        /// neighboring values that far out compare as one.
        /// </remarks>
        public bool Convert(long value) => Compare(value);

        private bool Compare(double value, double tolerance = 0d)
        {
            var difference = Math.Abs(value - _value);

            return _comparison switch
            {
                ComparisonMode.Equal => difference <= tolerance,
                // Negated rather than ">", so a NaN — which loses every comparison — is not equal.
                ComparisonMode.NotEqual => !(difference <= tolerance),
                ComparisonMode.LessThan => value < _value - tolerance,
                ComparisonMode.GreaterThan => value > _value + tolerance,
                ComparisonMode.LessThanOrEqual => value <= _value + tolerance,
                ComparisonMode.GreaterThanOrEqual => value >= _value - tolerance,
                _ => Undeclared()
            };
        }

        private bool Undeclared()
        {
            this.LogError(
                problem: $"the comparison {_comparison.Describe()} is not a declared {nameof(ComparisonMode)}",
                consequence: "Reporting false.");

            return false;
        }

        // Relative to the larger operand, with a floor for the comparison at zero, where the
        // relative part collapses to nothing.
        private double Tolerance(double value, double relative, double floor) =>
            Math.Max(relative * Math.Max(Math.Abs(value), Math.Abs(_value)), floor);
    }
}
