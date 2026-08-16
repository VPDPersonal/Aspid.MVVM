using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Which bound <see cref="ClampNumberConverter"/> applies.
    /// </summary>
    public enum ClampMode
    {
        /// <summary>Keep the value between both bounds.</summary>
        Both,

        /// <summary>Only raise the value to the minimum.</summary>
        Min,

        /// <summary>Only lower the value to the maximum.</summary>
        Max,
    }

    /// <summary>
    /// Keeps a number inside a range.
    /// </summary>
    /// <remarks>
    /// A View property with a legal range — <c>Image.fillAmount</c>, an alpha, a slider — will take
    /// whatever the ViewModel sends and render it wrong rather than complain. Clamping at the
    /// boundary keeps a bad number from becoming a bad frame.
    /// </remarks>
    [Serializable]
    public sealed class ClampNumberConverter : IConverterFloat
    {
        [Tooltip("The lowest value allowed through.")]
        [SerializeField] private float _min;

        [Tooltip("The highest value allowed through.")]
        [SerializeField] private float _max = 1f;

        [Tooltip("Which bound to apply.")]
        [SerializeField] private ClampMode _mode = ClampMode.Both;

        /// <remarks>Default: clamping to 0..1.</remarks>
        public ClampNumberConverter() { }

        /// <param name="min">The lowest value allowed through.</param>
        /// <param name="max">The highest value allowed through.</param>
        /// <param name="mode">Which bound to apply.</param>
        public ClampNumberConverter(float min, float max, ClampMode mode = ClampMode.Both)
        {
            _min = min;
            _max = max;
            _mode = mode;
        }

        /// <summary>
        /// Clamps the specified value.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <returns>The value, held inside the configured bounds.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        public float Convert(float value) => _mode switch
        {
            ClampMode.Both => Mathf.Clamp(value, _min, _max),
            ClampMode.Min => Mathf.Max(value, _min),
            ClampMode.Max => Mathf.Min(value, _max),
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };
    }

    /// <summary>
    /// How <see cref="RoundNumberConverter"/> drops the fraction.
    /// </summary>
    public enum RoundMode
    {
        /// <summary>To the nearest, halves going to the even neighbour.</summary>
        Round,

        /// <summary>Towards negative infinity.</summary>
        Floor,

        /// <summary>Towards positive infinity.</summary>
        Ceil,

        /// <summary>Towards zero.</summary>
        Truncate,
    }

    /// <summary>
    /// Rounds a number, in a way the caller chooses.
    /// </summary>
    /// <remarks>
    /// Rounding used to be an implicit truncation inside a cast, with no say in the matter. The
    /// direction is rarely arbitrary: a countdown floored shows 0:00 for a whole second before it
    /// fires, and a score truncated loses the point the player just earned.
    /// </remarks>
    [Serializable]
    public sealed class RoundNumberConverter : IConverterFloat, IConverterFloatToInt
    {
        [Tooltip("Which way to drop the fraction.")]
        [SerializeField] private RoundMode _mode;

        [Tooltip("How many decimal places to keep. Ignored when converting to int.")]
        [SerializeField] private int _digits;

        /// <remarks>Default: rounding to the nearest whole number.</remarks>
        public RoundNumberConverter() { }

        /// <param name="mode">Which way to drop the fraction.</param>
        /// <param name="digits">How many decimal places to keep.</param>
        public RoundNumberConverter(RoundMode mode, int digits = 0)
        {
            _mode = mode;
            _digits = digits;
        }

        /// <summary>
        /// Rounds the specified value to the configured number of decimal places.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <returns>The rounded value.</returns>
        public float Convert(float value)
        {
            if (_digits <= 0) return Apply(value);

            var scale = Mathf.Pow(10f, _digits);
            return Apply(value * scale) / scale;
        }

        int IConverter<float, int>.Convert(float value) => (int)Apply(value);

        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        private float Apply(float value) => _mode switch
        {
            RoundMode.Round => Mathf.Round(value),
            RoundMode.Floor => Mathf.Floor(value),
            RoundMode.Ceil => Mathf.Ceil(value),
            RoundMode.Truncate => (float)Math.Truncate(value),
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };
    }

    /// <summary>
    /// Snaps a number to the nearest multiple of a step.
    /// </summary>
    /// <remarks>
    /// A volume slider that moves in fives, a rotation that lands on 45° marks. Doing it in the
    /// ViewModel means the ViewModel knows how the control is drawn.
    /// </remarks>
    [Serializable]
    public sealed class SnapToStepConverter : IConverterFloat
    {
        [Tooltip("The size of one step. A step of zero passes the value through.")]
        [SerializeField] private float _step = 1f;

        [Tooltip("Shifts where the steps fall.")]
        [SerializeField] private float _offset;

        /// <remarks>Default: snapping to whole numbers.</remarks>
        public SnapToStepConverter() { }

        /// <param name="step">The size of one step.</param>
        /// <param name="offset">Shifts where the steps fall.</param>
        public SnapToStepConverter(float step, float offset = 0f)
        {
            _step = step;
            _offset = offset;
        }

        /// <summary>
        /// Snaps the specified value to the nearest step.
        /// </summary>
        /// <param name="value">The value to snap.</param>
        /// <returns>The nearest multiple of the step, or the value unchanged when the step is zero.</returns>
        public float Convert(float value) =>
            _step == 0f ? value : Mathf.Round((value - _offset) / _step) * _step + _offset;
    }

    /// <summary>
    /// The single-argument functions <see cref="UnaryMathConverter"/> can apply.
    /// </summary>
    public enum UnaryMathOperation
    {
        /// <summary>The distance from zero.</summary>
        Abs,

        /// <summary>The value with its sign flipped.</summary>
        Negate,

        /// <summary>-1, 0 or 1.</summary>
        Sign,

        /// <summary>The square root. A negative value yields zero rather than NaN.</summary>
        Sqrt,

        /// <summary>One divided by the value. Zero yields zero rather than infinity.</summary>
        Reciprocal,

        /// <summary>The natural logarithm. A non-positive value yields zero.</summary>
        Log,

        /// <summary>The base-10 logarithm. A non-positive value yields zero.</summary>
        Log10,

        /// <summary>e raised to the value.</summary>
        Exp,

        /// <summary>The sine, in radians.</summary>
        Sin,

        /// <summary>The cosine, in radians.</summary>
        Cos,

        /// <summary>The tangent, in radians.</summary>
        Tan,
    }

    /// <summary>
    /// Applies a single-argument mathematical function.
    /// </summary>
    /// <remarks>
    /// Eleven one-line conversions that would otherwise be eleven converter classes. The functions
    /// with a domain — square root, logarithm, reciprocal — return zero outside it rather than NaN or
    /// infinity, because a NaN reaching a <see cref="Transform"/> corrupts it silently while a zero
    /// is merely wrong.
    /// </remarks>
    [Serializable]
    public sealed class UnaryMathConverter : IConverterFloat
    {
        [Tooltip("The function to apply.")]
        [SerializeField] private UnaryMathOperation _operation;

        /// <remarks>Default: applying <see cref="UnaryMathOperation.Abs"/>.</remarks>
        public UnaryMathConverter() { }

        /// <param name="operation">The function to apply.</param>
        public UnaryMathConverter(UnaryMathOperation operation)
        {
            _operation = operation;
        }

        /// <summary>
        /// Applies the configured function to the specified value.
        /// </summary>
        /// <param name="value">The value to transform.</param>
        /// <returns>The result of the function.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the operation is not a declared value.</exception>
        public float Convert(float value) => _operation switch
        {
            UnaryMathOperation.Abs => Mathf.Abs(value),
            UnaryMathOperation.Negate => -value,
            UnaryMathOperation.Sign => Mathf.Sign(value) * (value == 0f ? 0f : 1f),
            UnaryMathOperation.Sqrt => value <= 0f ? 0f : Mathf.Sqrt(value),
            UnaryMathOperation.Reciprocal => value == 0f ? 0f : 1f / value,
            UnaryMathOperation.Log => value <= 0f ? 0f : Mathf.Log(value),
            UnaryMathOperation.Log10 => value <= 0f ? 0f : Mathf.Log10(value),
            UnaryMathOperation.Exp => Mathf.Exp(value),
            UnaryMathOperation.Sin => Mathf.Sin(value),
            UnaryMathOperation.Cos => Mathf.Cos(value),
            UnaryMathOperation.Tan => Mathf.Tan(value),
            _ => throw new ArgumentOutOfRangeException(nameof(_operation), _operation, null)
        };
    }
}
