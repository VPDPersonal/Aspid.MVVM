#nullable enable
using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ColorLerpConverter"/> — the clamped default, the shaping curve, and
    /// the unclamped path that skips the curve entirely.
    /// </summary>
    [TestFixture]
    public sealed class ColorLerpConverterTests
    {
        [Test]
        public void ColorLerp_MovesBetweenTheStops()
        {
            var converter = new ColorLerpConverter(Color.red, Color.green);

            Assert.AreEqual(Color.red, converter.Convert(0f));
            Assert.AreEqual(Color.green, converter.Convert(1f));
        }

        [Test]
        public void Convert_MovesBetweenTheTwoColors() =>
            AssertColorsEqual(new Color(0.5f, 0.5f, 0f), new ColorLerpConverter(Color.red, Color.green).Convert(0.5f));

        [Test]
        public void Convert_Clamped_HoldsTheAmountInsideZeroToOne() =>
            AssertColorsEqual(Color.green, new ColorLerpConverter(Color.red, Color.green).Convert(2f));

        // Not through the curve: a curve answers with its end key past its own range, which would
        // clamp the amount right here — so the unclamped path bypasses it entirely.
        [Test]
        public void Convert_Unclamped_CarriesPastTheTwoColorsAndSkipsTheCurve()
        {
            var color = new ColorLerpConverter(Color.red, Color.green, curve: null);
            SetClamp(color, false);

            var result = color.Convert(2f);

            Assert.Greater(result.g, 1f);
        }

        [Test]
        public void Convert_Curve_ShapesTheAmount()
        {
            var converter = new ColorLerpConverter(Color.black, Color.white, AnimationCurve.Constant(0f, 1f, 1f));

            AssertColorsEqual(Color.white, converter.Convert(0f));
        }

        // A curve field never authored deserializes as a curve with no keys rather than as null, and
        // evaluating that returns zero — pinning every value to the first color if not guarded.
        [Test]
        public void Convert_EmptyCurve_IsTreatedAsNoCurve()
        {
            var converter = new ColorLerpConverter(Color.black, Color.white, new AnimationCurve());

            AssertColorsEqual(new Color(0.5f, 0.5f, 0.5f), converter.Convert(0.5f));
        }

        private static void SetClamp(ColorLerpConverter converter, bool clamp)
        {
            var field = typeof(ColorLerpConverter).GetField(
                "_clamp",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            Assert.IsNotNull(field, "ColorLerpConverter has no field _clamp");
            field!.SetValue(converter, clamp);
        }

        private static void AssertColorsEqual(Color expected, Color actual)
        {
            Assert.AreEqual(expected.r, actual.r, 1e-4f);
            Assert.AreEqual(expected.g, actual.g, 1e-4f);
            Assert.AreEqual(expected.b, actual.b, 1e-4f);
        }
    }
}
