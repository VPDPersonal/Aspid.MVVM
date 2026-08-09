using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Maps a number from one range onto another.
    /// </summary>
    /// <remarks>
    /// The most common transformation in game UI: health onto a bar, temperature onto a gauge,
    /// distance onto an arrow. Without it the coefficient has to be worked out by hand and hidden in
    /// a two-link chain, where nobody can see what range it came from.
    /// </remarks>
    [Serializable]
    public sealed class RemapNumberConverter : ITwoWayConverter<float, float>
    {
        [Tooltip("The low end of the incoming range.")]
        [SerializeField] private float _fromMin;

        [Tooltip("The high end of the incoming range.")]
        [SerializeField] private float _fromMax = 1f;

        [Tooltip("The low end of the outgoing range.")]
        [SerializeField] private float _toMin;

        [Tooltip("The high end of the outgoing range.")]
        [SerializeField] private float _toMax = 1f;

        [Tooltip("Hold the result inside the outgoing range.")]
        [SerializeField] private bool _clamp = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemapNumberConverter"/> class mapping 0..1 onto 0..1.
        /// </summary>
        public RemapNumberConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemapNumberConverter"/> class.
        /// </summary>
        /// <param name="fromMin">The low end of the incoming range.</param>
        /// <param name="fromMax">The high end of the incoming range.</param>
        /// <param name="toMin">The low end of the outgoing range.</param>
        /// <param name="toMax">The high end of the outgoing range.</param>
        /// <param name="clamp">If <see langword="true"/>, holds the result inside the outgoing range.</param>
        public RemapNumberConverter(float fromMin, float fromMax, float toMin, float toMax, bool clamp = true)
        {
            _fromMin = fromMin;
            _fromMax = fromMax;
            _toMin = toMin;
            _toMax = toMax;
            _clamp = clamp;
        }

        /// <summary>
        /// Maps the specified value from the incoming range onto the outgoing one.
        /// </summary>
        /// <param name="value">The value to map.</param>
        /// <returns>The mapped value. A degenerate incoming range yields the outgoing low end.</returns>
        public float Convert(float value) => Map(value, _fromMin, _fromMax, _toMin, _toMax, _clamp);

        /// <summary>
        /// Maps the specified value back from the outgoing range onto the incoming one.
        /// </summary>
        /// <param name="value">The value to map back.</param>
        /// <returns>The value in the incoming range.</returns>
        public float ConvertBack(float value) => Map(value, _toMin, _toMax, _fromMin, _fromMax, _clamp);

        internal static float Map(float value, float fromMin, float fromMax, float toMin, float toMax, bool clamp)
        {
            var span = fromMax - fromMin;
            if (span == 0f) return toMin;

            var t = (value - fromMin) / span;
            if (clamp) t = Mathf.Clamp01(t);

            return toMin + (toMax - toMin) * t;
        }
    }

    /// <summary>
    /// Converts a value in a range to its 0..1 position within it.
    /// </summary>
    /// <remarks>
    /// The health-bar converter: current health in, fill amount out. The ViewModel keeps the number
    /// it actually has instead of a fraction computed for one particular bar.
    /// </remarks>
    [Serializable]
    public sealed class InverseLerpConverter : ITwoWayConverter<float, float>
    {
        [Tooltip("The value that maps to 0.")]
        [SerializeField] private float _min;

        [Tooltip("The value that maps to 1.")]
        [SerializeField] private float _max = 1f;

        [Tooltip("Hold the result inside 0..1.")]
        [SerializeField] private bool _clamp = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="InverseLerpConverter"/> class over 0..1.
        /// </summary>
        public InverseLerpConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InverseLerpConverter"/> class.
        /// </summary>
        /// <param name="min">The value that maps to 0.</param>
        /// <param name="max">The value that maps to 1.</param>
        /// <param name="clamp">If <see langword="true"/>, holds the result inside 0..1.</param>
        public InverseLerpConverter(float min, float max, bool clamp = true)
        {
            _min = min;
            _max = max;
            _clamp = clamp;
        }

        /// <summary>
        /// Converts the specified value to its position in the range.
        /// </summary>
        /// <param name="value">The value to locate.</param>
        /// <returns>Its 0..1 position.</returns>
        public float Convert(float value) => RemapNumberConverter.Map(value, _min, _max, 0f, 1f, _clamp);

        /// <summary>
        /// Converts a 0..1 position back to a value in the range.
        /// </summary>
        /// <param name="value">The position to convert.</param>
        /// <returns>The value at that position.</returns>
        public float ConvertBack(float value) => RemapNumberConverter.Map(value, 0f, 1f, _min, _max, _clamp);
    }

    /// <summary>
    /// Converts a 0..1 position to a value in a range.
    /// </summary>
    /// <remarks>The other direction of <see cref="InverseLerpConverter"/>.</remarks>
    [Serializable]
    public sealed class LerpNumberConverter : ITwoWayConverter<float, float>
    {
        [Tooltip("The value 0 maps to.")]
        [SerializeField] private float _from;

        [Tooltip("The value 1 maps to.")]
        [SerializeField] private float _to = 1f;

        [Tooltip("Hold the incoming position inside 0..1.")]
        [SerializeField] private bool _clamp = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="LerpNumberConverter"/> class over 0..1.
        /// </summary>
        public LerpNumberConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LerpNumberConverter"/> class.
        /// </summary>
        /// <param name="from">The value 0 maps to.</param>
        /// <param name="to">The value 1 maps to.</param>
        /// <param name="clamp">If <see langword="true"/>, holds the incoming position inside 0..1.</param>
        public LerpNumberConverter(float from, float to, bool clamp = true)
        {
            _from = from;
            _to = to;
            _clamp = clamp;
        }

        /// <summary>
        /// Converts the specified position to a value in the range.
        /// </summary>
        /// <param name="value">The 0..1 position.</param>
        /// <returns>The value at that position.</returns>
        public float Convert(float value) => RemapNumberConverter.Map(value, 0f, 1f, _from, _to, _clamp);

        /// <summary>
        /// Converts a value in the range back to its position.
        /// </summary>
        /// <param name="value">The value to locate.</param>
        /// <returns>Its 0..1 position.</returns>
        public float ConvertBack(float value) => RemapNumberConverter.Map(value, _from, _to, 0f, 1f, _clamp);
    }

    /// <summary>
    /// Converts a 0..1 fraction to a percentage.
    /// </summary>
    [Serializable]
    public sealed class NormalizedToPercentConverter : ITwoWayConverter<float, float>
    {
        [Tooltip("Round the percentage to a whole number.")]
        [SerializeField] private bool _round;

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalizedToPercentConverter"/> class.
        /// </summary>
        public NormalizedToPercentConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalizedToPercentConverter"/> class.
        /// </summary>
        /// <param name="round">If <see langword="true"/>, rounds the percentage to a whole number.</param>
        public NormalizedToPercentConverter(bool round)
        {
            _round = round;
        }

        /// <summary>
        /// Converts the specified fraction to a percentage.
        /// </summary>
        /// <param name="value">The 0..1 fraction.</param>
        /// <returns>The percentage.</returns>
        public float Convert(float value)
        {
            var percent = value * 100f;
            return _round ? Mathf.Round(percent) : percent;
        }

        /// <summary>
        /// Converts a percentage back to a fraction.
        /// </summary>
        /// <param name="value">The percentage.</param>
        /// <returns>The 0..1 fraction.</returns>
        public float ConvertBack(float value) => value / 100f;
    }

    /// <summary>
    /// How <see cref="WrapNumberConverter"/> folds a value back into its range.
    /// </summary>
    public enum WrapMode
    {
        /// <summary>Past the end, start again from the beginning.</summary>
        Repeat,

        /// <summary>Past the end, travel back towards the beginning.</summary>
        PingPong,
    }

    /// <summary>
    /// Folds a number back into a range instead of clamping it.
    /// </summary>
    /// <remarks>
    /// For values that cycle rather than stop: a rotation past 360°, a carousel index past the last
    /// page, a progress bar that fills repeatedly.
    /// </remarks>
    [Serializable]
    public sealed class WrapNumberConverter : IConverterFloat
    {
        [Tooltip("How to fold a value that leaves the range.")]
        [SerializeField] private WrapMode _mode;

        [Tooltip("The low end of the range.")]
        [SerializeField] private float _min;

        [Tooltip("The high end of the range.")]
        [SerializeField] private float _max = 1f;

        /// <summary>
        /// Initializes a new instance of the <see cref="WrapNumberConverter"/> class over 0..1.
        /// </summary>
        public WrapNumberConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrapNumberConverter"/> class.
        /// </summary>
        /// <param name="mode">How to fold a value that leaves the range.</param>
        /// <param name="min">The low end of the range.</param>
        /// <param name="max">The high end of the range.</param>
        public WrapNumberConverter(WrapMode mode, float min, float max)
        {
            _mode = mode;
            _min = min;
            _max = max;
        }

        /// <summary>
        /// Folds the specified value into the range.
        /// </summary>
        /// <param name="value">The value to fold.</param>
        /// <returns>The folded value. A degenerate range yields its low end.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        public float Convert(float value)
        {
            var span = _max - _min;
            if (span <= 0f) return _min;

            return _mode switch
            {
                WrapMode.Repeat => _min + Mathf.Repeat(value - _min, span),
                WrapMode.PingPong => _min + Mathf.PingPong(value - _min, span),
                _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
            };
        }
    }

    /// <summary>
    /// Converts seconds remaining to a 0..1 progress value.
    /// </summary>
    /// <remarks>
    /// A timer ring driven by the same number the label shows, rather than a second property that
    /// has to be kept in step with it.
    /// </remarks>
    [Serializable]
    public sealed class CountdownProgressConverter : IConverterFloat
    {
        [Tooltip("The full duration, in seconds.")]
        [SerializeField] private float _totalSeconds = 1f;

        [Tooltip("Return the elapsed fraction instead of the remaining one.")]
        [SerializeField] private bool _elapsed;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountdownProgressConverter"/> class over one second.
        /// </summary>
        public CountdownProgressConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountdownProgressConverter"/> class.
        /// </summary>
        /// <param name="totalSeconds">The full duration, in seconds.</param>
        /// <param name="elapsed">If <see langword="true"/>, returns the elapsed fraction.</param>
        public CountdownProgressConverter(float totalSeconds, bool elapsed = false)
        {
            _totalSeconds = totalSeconds;
            _elapsed = elapsed;
        }

        /// <summary>
        /// Converts the specified seconds remaining to a progress value.
        /// </summary>
        /// <param name="value">The seconds remaining.</param>
        /// <returns>The 0..1 progress. A duration of zero yields a finished timer.</returns>
        public float Convert(float value)
        {
            if (_totalSeconds <= 0f) return _elapsed ? 1f : 0f;

            var remaining = Mathf.Clamp01(value / _totalSeconds);
            return _elapsed ? 1f - remaining : remaining;
        }
    }

    /// <summary>
    /// Eases a value between two bounds with <see cref="Mathf.SmoothStep"/>.
    /// </summary>
    [Serializable]
    public sealed class SmoothStepConverter : IConverterFloat
    {
        [Tooltip("The value that maps to 0.")]
        [SerializeField] private float _from;

        [Tooltip("The value that maps to 1.")]
        [SerializeField] private float _to = 1f;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmoothStepConverter"/> class over 0..1.
        /// </summary>
        public SmoothStepConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmoothStepConverter"/> class.
        /// </summary>
        /// <param name="from">The value that maps to 0.</param>
        /// <param name="to">The value that maps to 1.</param>
        public SmoothStepConverter(float from, float to)
        {
            _from = from;
            _to = to;
        }

        /// <summary>
        /// Eases the specified value.
        /// </summary>
        /// <param name="value">The value to ease.</param>
        /// <returns>The eased value.</returns>
        public float Convert(float value) => Mathf.SmoothStep(_from, _to, value);
    }
}
