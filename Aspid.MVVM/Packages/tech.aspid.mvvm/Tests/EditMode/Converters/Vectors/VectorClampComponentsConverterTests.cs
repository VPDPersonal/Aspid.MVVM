using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="VectorClampComponentsConverter"/> — the per-axis box, the reversed
    /// pairs, and axes a narrower width does not read.
    /// </summary>
    [TestFixture]
    public sealed class VectorClampComponentsConverterTests
    {
        // The shared per-axis clamp. The reversed rows are the point: Mathf.Clamp taken raw with the
        // bounds the wrong way round answers -1 for the first and 1 for the second — both ends
        // inverted, which reads as "the binding stopped working" rather than as a typo.
        [TestCase(5f, -1f, 1f, 1f)]
        [TestCase(-5f, -1f, 1f, -1f)]
        [TestCase(0.5f, -1f, 1f, 0.5f)]
        [TestCase(5f, 1f, -1f, 1f)]
        [TestCase(-5f, 1f, -1f, -1f)]
        [TestCase(0f, 1f, -1f, 0f)]
        [TestCase(3f, 2f, 2f, 2f)]
        public void ClampComponent_OrdersThePair(float value, float min, float max, float expected) =>
            Assert.AreEqual(expected, VectorClampComponentsConverter.ClampComponent(value, min, max), 1e-6f);

        [Test]
        public void VectorClampComponents_DefaultConstructed_HoldsEveryAxisWithinOne() =>
            AssertClose(
                new Vector3(1f, -1f, 0.5f),
                new VectorClampComponentsConverter().Convert(new Vector3(5f, -5f, 0.5f)));

        // Each axis carries its own pair, so one axis typed backwards must not disturb the others:
        // x is an ordinary 0..10, y is reversed, z is left at the default box.
        [Test]
        public void VectorClampComponents_OneAxisReversed_LeavesTheOtherAxesAlone()
        {
            LogAssert.Expect(LogType.Error, _invertedAxisBounds);

            AssertClose(
                new Vector3(0f, 5f, 0.5f),
                new VectorClampComponentsConverter(new Vector4(0f, 5f, -1f, 0f), new Vector4(10f, -5f, 1f, 0f))
                    .Convert(new Vector3(-3f, 100f, 0.5f)));
        }

        [Test]
        public void VectorClampComponents_ReversedBox_ClampsTheSameWayAsTheOrderedOne()
        {
            LogAssert.Expect(LogType.Error, _invertedAxisBounds);

            var value = new Vector3(5f, -5f, 0f);
            var lower = new Vector4(-1f, -1f, -1f, 0f);
            var upper = new Vector4(1f, 1f, 1f, 0f);

            var ordered = new VectorClampComponentsConverter(lower, upper).Convert(value);
            var reversed = new VectorClampComponentsConverter(upper, lower).Convert(value);

            AssertClose(ordered, reversed);

            // Pinning the value as well as the agreement, so the pair cannot both be wrong together.
            AssertClose(new Vector3(1f, -1f, 0f), reversed);
        }

        [Test]
        public void Vector2ClampComponents_ReversedBox_ReadsThePairInOrder()
        {
            LogAssert.Expect(LogType.Error, _invertedAxisBounds);

            AssertClose(
                new Vector2(1f, -1f),
                AsWidth<Vector2>(new VectorClampComponentsConverter(
                        new Vector4(1f, 1f, 0f, 0f),
                        new Vector4(-1f, -1f, 0f, 0f)))
                    .Convert(new Vector2(5f, -5f)));
        }

        [Test]
        public void Vector2ClampComponents_DefaultConstructed_HoldsBothAxesWithinOne() =>
            AssertClose(
                new Vector2(1f, -0.25f),
                AsWidth<Vector2>(new VectorClampComponentsConverter()).Convert(new Vector2(5f, -0.25f)));

        // The box is held four-wide while the binding decides how much of it is read, so the pair on
        // an axis a 2D binding never sees must not be reported against it — otherwise every Vector2
        // field whose Z and W bounds were left alone would log on every push. Only x and y are
        // ordered here; Z and W are reversed and have to stay silent.
        [Test]
        public void Vector2ClampComponents_ReversedBoundsOnAnAxisItDoesNotRead_ReportsNothing()
        {
            AssertClose(
                new Vector2(1f, -1f),
                AsWidth<Vector2>(new VectorClampComponentsConverter(
                        new Vector4(-1f, -1f, 5f, 5f),
                        new Vector4(1f, 1f, -5f, -5f)))
                    .Convert(new Vector2(5f, -5f)));

            LogAssert.NoUnexpectedReceived();
        }

        // The four-component width is the only one that reads the W pair, and the pair here is 2..3
        // rather than the ±1 of the other axes — so a W left out of the clamp would answer 0.
        [Test]
        public void Vector4ClampComponents_ReadsTheFourthPairToo() =>
            AssertClose(
                new Vector4(1f, 0f, 0.5f, 2f),
                AsWidth<Vector4>(new VectorClampComponentsConverter(
                        new Vector4(0f, 0f, -1f, 2f),
                        new Vector4(1f, 1f, 1f, 3f)))
                    .Convert(new Vector4(5f, -5f, 0.5f, 0f)));

        private static readonly Regex _invertedAxisBounds =
            new("on at least one axis");

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
