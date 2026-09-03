#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reshapes a 0..1 value along an easing curve.
    /// </summary>
    /// <remarks>Clamps what goes in, never what comes out: Back and Elastic overshoot 0..1 on purpose. Evaluated in <see langword="float"/>.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Easing",
        Tooltip = "Reshapes a 0..1 value along an easing curve")]
    public sealed class EasingConverter : IConverter<float, float>, IConverter<double, double>
    {
        [Tooltip("The curve applied to the value.")]
        [SerializeField] private EaseType _ease = EaseType.QuadOut;

        [Tooltip("Hold the incoming value inside 0..1. Back and Elastic still overshoot on the way out.")]
        [SerializeField] private bool _clamp = true;

        // Penner constants, named as in the reference implementation.
        private const float C1 = 1.70158f;
        private const float C2 = C1 * 1.525f;
        private const float C3 = C1 + 1f;
        private const float C4 = 2f * Mathf.PI / 3f;
        private const float C5 = 2f * Mathf.PI / 4.5f;
        private const float BounceFactor = 7.5625f;
        private const float BounceStep = 2.75f;

        /// <remarks>Default: easing out quadratically.</remarks>
        public EasingConverter() { }

        /// <param name="ease">The curve applied to the value.</param>
        /// <param name="clamp">If <see langword="true"/>, holds the incoming value inside 0..1.</param>
        public EasingConverter(
            EaseType ease,
            bool clamp = true)
        {
            _ease = ease;
            _clamp = clamp;
        }

        /// <summary>
        /// Eases the specified value.
        /// </summary>
        /// <param name="value">The 0..1 position along the curve.</param>
        /// <returns>The eased value. An undeclared curve reports an error and returns the value unchanged.</returns>
        public float Convert(float value) =>
            Evaluate(_ease, _clamp ? Mathf.Clamp01(value) : value);

        double IConverter<double, double>.Convert(double value) =>
            Convert(NumericSaturation.ToFloat(value));

        /// <summary>
        /// Evaluates an easing curve at a position.
        /// </summary>
        /// <param name="ease">The curve to evaluate.</param>
        /// <param name="t">The position along it, normally 0..1.</param>
        /// <returns>The eased position. An undeclared curve reports an error and returns <paramref name="t"/> unchanged.</returns>
        internal static float Evaluate(EaseType ease, float t) => ease switch
        {
            EaseType.Linear => t,

            EaseType.SineIn => 1f - Mathf.Cos(t * Mathf.PI / 2f),
            EaseType.SineOut => Mathf.Sin(t * Mathf.PI / 2f),
            EaseType.SineInOut => -(Mathf.Cos(Mathf.PI * t) - 1f) / 2f,

            EaseType.QuadIn => t * t,
            EaseType.QuadOut => 1f - (1f - t) * (1f - t),
            EaseType.QuadInOut => t < 0.5f
                ? 2f * t * t
                : 1f - Square(-2f * t + 2f) / 2f,

            EaseType.CubicIn => t * t * t,
            EaseType.CubicOut => 1f - Cube(1f - t),
            EaseType.CubicInOut => t < 0.5f
                ? 4f * t * t * t
                : 1f - Cube(-2f * t + 2f) / 2f,

            EaseType.QuartIn => Square(t * t),
            EaseType.QuartOut => 1f - Square(Square(1f - t)),
            EaseType.QuartInOut => t < 0.5f
                ? 8f * Square(t * t)
                : 1f - Square(Square(-2f * t + 2f)) / 2f,

            EaseType.QuintIn => Square(t * t) * t,
            EaseType.QuintOut => 1f - Square(Square(1f - t)) * (1f - t),
            EaseType.QuintInOut => t < 0.5f
                ? 16f * Square(t * t) * t
                : 1f - Square(Square(-2f * t + 2f)) * (-2f * t + 2f) / 2f,

            EaseType.ExpoIn => t <= 0f ? 0f : Mathf.Pow(2f, 10f * t - 10f),
            EaseType.ExpoOut => t >= 1f ? 1f : 1f - Mathf.Pow(2f, -10f * t),
            EaseType.ExpoInOut => ExpoInOut(t),

            EaseType.CircIn => 1f - SafeSqrt(1f - t * t),
            EaseType.CircOut => SafeSqrt(1f - Square(t - 1f)),
            EaseType.CircInOut => CircInOut(t),

            EaseType.BackIn => C3 * t * t * t - C1 * t * t,
            EaseType.BackOut => 1f + C3 * Cube(t - 1f) + C1 * Square(t - 1f),
            EaseType.BackInOut => BackInOut(t),

            EaseType.ElasticIn => ElasticIn(t),
            EaseType.ElasticOut => ElasticOut(t),
            EaseType.ElasticInOut => ElasticInOut(t),

            EaseType.BounceIn => 1f - BounceOut(1f - t),
            EaseType.BounceOut => BounceOut(t),
            EaseType.BounceInOut => t < 0.5f
                ? (1f - BounceOut(1f - 2f * t)) / 2f
                : (1f + BounceOut(2f * t - 1f)) / 2f,

            _ => Undeclared(ease, t)
        };

        private static float Undeclared(EaseType ease, float t)
        {
            ConverterLogger.LogError(
                converterType: typeof(EasingConverter),
                problem: $"the ease {ease.Describe()} is not a declared {nameof(EaseType)}",
                consequence: "Returning the value unchanged.");

            return t;
        }

        private static float Square(float value) =>
            value * value;

        private static float Cube(float value) =>
            value * value * value;

        private static float SafeSqrt(float value) => value <= 0f
            ? 0f
            : Mathf.Sqrt(value);

        private static float ExpoInOut(float t)
        {
            if (t <= 0f) return 0f;
            if (t >= 1f) return 1f;

            return t < 0.5f
                ? Mathf.Pow(2f, 20f * t - 10f) / 2f
                : (2f - Mathf.Pow(2f, -20f * t + 10f)) / 2f;
        }

        private static float CircInOut(float t) => t < 0.5f
            ? (1f - SafeSqrt(1f - Square(2f * t))) / 2f
            : (SafeSqrt(1f - Square(-2f * t + 2f)) + 1f) / 2f;

        private static float BackInOut(float t) => t < 0.5f
            ? Square(2f * t) * ((C2 + 1f) * 2f * t - C2) / 2f
            : (Square(2f * t - 2f) * ((C2 + 1f) * (2f * t - 2f) + C2) + 2f) / 2f;

        private static float ElasticIn(float t)
        {
            if (t <= 0f) return 0f;
            if (t >= 1f) return 1f;

            return -Mathf.Pow(2f, 10f * t - 10f) * Mathf.Sin((10f * t - 10.75f) * C4);
        }

        private static float ElasticOut(float t)
        {
            if (t <= 0f) return 0f;
            if (t >= 1f) return 1f;

            return Mathf.Pow(2f, -10f * t) * Mathf.Sin((10f * t - 0.75f) * C4) + 1f;
        }

        private static float ElasticInOut(float t)
        {
            if (t <= 0f) return 0f;
            if (t >= 1f) return 1f;

            var oscillation = Mathf.Sin((20f * t - 11.125f) * C5);

            return t < 0.5f
                ? -(Mathf.Pow(2f, 20f * t - 10f) * oscillation) / 2f
                : Mathf.Pow(2f, -20f * t + 10f) * oscillation / 2f + 1f;
        }

        private static float BounceOut(float t)
        {
            if (t < 1f / BounceStep) return BounceFactor * t * t;

            if (t < 2f / BounceStep)
            {
                t -= 1.5f / BounceStep;
                return BounceFactor * t * t + 0.75f;
            }

            if (t < 2.5f / BounceStep)
            {
                t -= 2.25f / BounceStep;
                return BounceFactor * t * t + 0.9375f;
            }

            t -= 2.625f / BounceStep;
            return BounceFactor * t * t + 0.984375f;
        }
    }
}
