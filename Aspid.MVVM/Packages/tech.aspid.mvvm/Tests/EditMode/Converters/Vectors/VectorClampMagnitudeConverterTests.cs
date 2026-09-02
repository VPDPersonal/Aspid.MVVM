using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="VectorClampMagnitudeConverter"/> — the ordered bounds, the reversed
    /// pair, and the negative ceiling.
    /// </summary>
    [TestFixture]
    public sealed class VectorClampMagnitudeConverterTests
    {
        // The shared scale, which all three widths route through. Note the argument order is
        // (magnitude, min, max) here while the constructors take (max, min) — the rows name the
        // bounds so the two orders cannot be confused.
        //
        // Ordinary pairs first: too long is pulled back to the ceiling, too short is pushed out to
        // the floor, and anything between them is left alone.
        [TestCase(10f, 0f, 1f, 0.1f)]
        [TestCase(0.5f, 2f, 10f, 4f)]
        [TestCase(5f, 0f, 10f, 1f)]
        // A pair typed the wrong way round is read in the order that holds the vector inside both
        // bounds: 1 and 5 mean 1..5 whichever field they were typed into. Taken raw, the first of
        // these rows would scale a length of 10 down to 1 — under the floor of 5 — so one instance
        // would break both of its own bounds at once.
        [TestCase(10f, 5f, 1f, 0.5f)]
        [TestCase(0.5f, 5f, 1f, 2f)]
        // Scaling by a negative ceiling turns the vector around, which is the one thing a length
        // clamp must never do; zero is the nearest legal length.
        [TestCase(5f, 0f, -2f, 0f)]
        [TestCase(5f, -5f, -1f, 0f)]
        // With one bound negative the survivor becomes the ceiling, and the floor goes with it — so a
        // short vector is left short rather than being stretched to the 3 that was typed as the floor.
        [TestCase(5f, 3f, -2f, 0.6f)]
        [TestCase(1f, 3f, -2f, 1f)]
        // A ceiling of zero is a real instruction and not "unset"; only the floor reads zero that way.
        [TestCase(5f, 0f, 0f, 0f)]
        [TestCase(5f, 10f, 10f, 2f)]
        public void ClampScale_OrdersThePairAndHoldsItAtZero(
            float magnitude,
            float min,
            float max,
            float expected) =>
            Assert.AreEqual(expected, VectorClampMagnitudeConverter.ClampScale(magnitude, min, max), 1e-4f);

        // End to end on a real vector: length 10 with the pair typed backwards is held at 5, not at
        // the 1 that a raw ceiling would give, and the direction is untouched.
        [Test]
        public void VectorClampMagnitude_ReversedBounds_ClampsToTheLargerOfThePair()
        {
            LogAssert.Expect(LogType.Error, _invalidLengthBounds);

            AssertClose(
                new Vector3(3f, 4f, 0f),
                new VectorClampMagnitudeConverter(maxMagnitude: 1f, minMagnitude: 5f).Convert(new Vector3(6f, 8f, 0f)));
        }

        [Test]
        public void VectorClampMagnitude_ReversedBounds_RaisesToTheSmallerOfThePair()
        {
            LogAssert.Expect(LogType.Error, _invalidLengthBounds);

            AssertClose(
                new Vector3(0.6f, 0.8f, 0f),
                new VectorClampMagnitudeConverter(maxMagnitude: 1f, minMagnitude: 5f)
                    .Convert(new Vector3(0.3f, 0.4f, 0f)));
        }

        // Exactly zero, not the (-1.2, -1.6, 0) that scaling by -2/5 would produce: the result must
        // not point the other way.
        [Test]
        public void VectorClampMagnitude_NegativeCeiling_CollapsesToZeroRatherThanReversing()
        {
            LogAssert.Expect(LogType.Error, _invalidLengthBounds);

            Assert.AreEqual(
                Vector3.zero,
                new VectorClampMagnitudeConverter(maxMagnitude: -2f).Convert(new Vector3(3f, 4f, 0f)));
        }

        [Test]
        public void Vector2ClampMagnitude_ReversedBounds_ReadsThePairInOrder()
        {
            // One expectation per push: the report is not muted after the first conversion.
            LogAssert.Expect(LogType.Error, _invalidLengthBounds);
            LogAssert.Expect(LogType.Error, _invalidLengthBounds);

            var converter = AsWidth<Vector2>(
                new VectorClampMagnitudeConverter(maxMagnitude: 1f, minMagnitude: 5f));

            AssertClose(new Vector2(3f, 4f), converter.Convert(new Vector2(6f, 8f)));
            AssertClose(new Vector2(0.6f, 0.8f), converter.Convert(new Vector2(0.3f, 0.4f)));
        }

        [Test]
        public void Vector2ClampMagnitude_NegativeCeiling_CollapsesToZeroRatherThanReversing()
        {
            LogAssert.Expect(LogType.Error, _invalidLengthBounds);

            Assert.AreEqual(
                Vector2.zero,
                AsWidth<Vector2>(new VectorClampMagnitudeConverter(maxMagnitude: -2f)).Convert(new Vector2(3f, 4f)));
        }

        [Test]
        public void Vector2ClampMagnitude_KeepsTheDirectionWhileShorteningTheVector() =>
            AssertClose(
                new Vector2(0.6f, 0.8f),
                AsWidth<Vector2>(new VectorClampMagnitudeConverter(1f)).Convert(new Vector2(3f, 4f)));

        // A floor of zero means "no floor" — the default — so a short vector stays short instead of
        // being stretched to the ceiling.
        [Test]
        public void Vector2ClampMagnitude_ZeroFloor_LeavesAShortVectorAlone() =>
            AssertClose(
                new Vector2(0.3f, 0.4f),
                AsWidth<Vector2>(new VectorClampMagnitudeConverter(10f)).Convert(new Vector2(0.3f, 0.4f)));

        // A zero vector has no direction to stretch along, so the floor cannot be applied to it: the
        // converter hands it back rather than inventing an axis to grow on.
        [Test]
        public void Vector2ClampMagnitude_ZeroVector_StaysZeroEvenWithAFloor() =>
            Assert.AreEqual(
                Vector2.zero,
                AsWidth<Vector2>(new VectorClampMagnitudeConverter(10f, 2f)).Convert(Vector2.zero));

        // The four-component width has no Vector4.ClampMagnitude behind it, so the same scale is
        // applied by hand: a vector of length 2 held at 1 keeps its direction with every component
        // halved, W included.
        [Test]
        public void Vector4ClampMagnitude_ShortensEveryComponentIncludingW() =>
            AssertClose(
                new Vector4(0.5f, 0.5f, 0.5f, 0.5f),
                AsWidth<Vector4>(new VectorClampMagnitudeConverter(1f)).Convert(Vector4.one));

        // Both clamp families report a half-authored pair on every push, so the fixture has to say so
        // for each conversion it makes with one — LogAssert fails the test on any error it did not ask
        // for, and on any expectation nothing produced.
        private static readonly Regex _invalidLengthBounds =
            new("are not two ordered non-negative lengths");

        private static void AssertClose(Vector3 expected, Vector3 actual, float delta = 1e-4f)
        {
            Assert.AreEqual(expected.x, actual.x, delta, $"x of {actual}, expected {expected}");
            Assert.AreEqual(expected.y, actual.y, delta, $"y of {actual}, expected {expected}");
            Assert.AreEqual(expected.z, actual.z, delta, $"z of {actual}, expected {expected}");
        }

        private static void AssertClose(Vector2 expected, Vector2 actual, float delta = 1e-4f)
        {
            Assert.AreEqual(expected.x, actual.x, delta, $"x of {actual}, expected {expected}");
            Assert.AreEqual(expected.y, actual.y, delta, $"y of {actual}, expected {expected}");
        }

        private static void AssertClose(Vector4 expected, Vector4 actual, float delta = 1e-4f)
        {
            Assert.AreEqual(expected.x, actual.x, delta, $"x of {actual}, expected {expected}");
            Assert.AreEqual(expected.y, actual.y, delta, $"y of {actual}, expected {expected}");
            Assert.AreEqual(expected.z, actual.z, delta, $"z of {actual}, expected {expected}");
            Assert.AreEqual(expected.w, actual.w, delta, $"w of {actual}, expected {expected}");
        }

        private static IConverter<T, T> AsWidth<T>(IConverter<T, T> converter) => converter;
    }
}
