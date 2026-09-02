using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="VectorNormalizeConverter"/> — the unit-length reduction and Unity's
    /// length-epsilon floor.
    /// </summary>
    /// <remarks>
    /// Unity's <c>normalized</c> has a length floor of 1e-5 (<c>kEpsilon</c>), below which it answers
    /// zero instead of a direction — undocumented, and inherited rather than written here.
    /// </remarks>
    [TestFixture]
    public sealed class VectorNormalizeConverterTests
    {
        [Test]
        public void Vector2Normalize_ReducesToUnitLength() =>
            AssertClose(
                new Vector2(0.6f, 0.8f),
                AsWidth<Vector2>(new VectorNormalizeConverter()).Convert(new Vector2(3f, 4f)));

        [Test]
        public void Vector2Normalize_NegativeDirection_KeepsItsSign() =>
            AssertClose(
                new Vector2(0f, -1f),
                AsWidth<Vector2>(new VectorNormalizeConverter()).Convert(new Vector2(0f, -4f)));

        // An already-unit vector must come back untouched rather than a hair off.
        [Test]
        public void Vector2Normalize_UnitInput_IsUnchanged() =>
            AssertClose(Vector2.up, AsWidth<Vector2>(new VectorNormalizeConverter()).Convert(Vector2.up));

        [Test]
        public void Vector2Normalize_ZeroStaysZeroRatherThanNaN() =>
            Assert.AreEqual(Vector2.zero, AsWidth<Vector2>(new VectorNormalizeConverter()).Convert(Vector2.zero));

        // Every component takes part in the length, so a unit four-vector is a half in each: a width
        // that normalized only the first three would answer (0.577, 0.577, 0.577, 1).
        [Test]
        public void Vector4Normalize_ReducesToUnitLengthOverFourComponents() =>
            AssertClose(
                new Vector4(0.5f, 0.5f, 0.5f, 0.5f),
                AsWidth<Vector4>(new VectorNormalizeConverter()).Convert(Vector4.one));

        [Test]
        public void Vector2Normalize_BelowTheLengthEpsilon_IsZeroNotAUnitVector() =>
            Assert.AreEqual(
                Vector2.zero,
                AsWidth<Vector2>(new VectorNormalizeConverter()).Convert(new Vector2(1e-6f, 0f)));

        [Test]
        public void VectorNormalize_BelowTheLengthEpsilon_IsZeroNotAUnitVector() =>
            Assert.AreEqual(Vector3.zero, new VectorNormalizeConverter().Convert(new Vector3(1e-6f, 0f, 0f)));

        // The other side of the same threshold, which is what makes the two tests above a floor
        // rather than a blanket "short vectors are dropped": ten times the floor and the direction
        // survives.
        [Test]
        public void Vector2Normalize_JustAboveTheLengthEpsilon_KeepsTheDirection() =>
            AssertClose(
                Vector2.right,
                AsWidth<Vector2>(new VectorNormalizeConverter()).Convert(new Vector2(1e-4f, 0f)));

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
