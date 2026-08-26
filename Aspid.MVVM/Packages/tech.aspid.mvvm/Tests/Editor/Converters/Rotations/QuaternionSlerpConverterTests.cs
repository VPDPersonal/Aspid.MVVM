using UnityEngine;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="QuaternionSlerpConverter"/> — the clamped amount, the shaping curve,
    /// and the short-arc slerp.
    /// </summary>
    /// <remarks>
    /// Contradicts what "a gauge that sweeps between two stops" leads an author to expect: slerp
    /// always takes the short arc, even when the two ends say otherwise.
    /// <para>
    /// <c>_clamp</c> has no constructor parameter and no setter, so that branch is reachable only
    /// through the Inspector and is not covered here.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class QuaternionSlerpConverterTests
    {
        [TestCase(0f, 0f)]
        [TestCase(0.5f, 45f)]
        [TestCase(1f, 90f)]
        public void Convert_ReadsTheRotationAtTheAmount(float amount, float expectedZ) =>
            AssertSameRotation(Quaternion.Euler(0f, 0f, expectedZ), Slerp().Convert(amount), $"an amount of {amount}");

        // The clamp is on and cannot be turned off from code. An unclamped slerp would answer 180°
        // for an amount of 2 and -90° for -1, so these two rows are what tells the branches apart.
        [TestCase(2f, 90f)]
        [TestCase(-1f, 0f)]
        public void Convert_HoldsTheAmountInsideZeroToOne(float amount, float expectedZ) =>
            AssertSameRotation(Quaternion.Euler(0f, 0f, expectedZ), Slerp().Convert(amount), $"an amount of {amount}");

        // The curve is consulted before the turn, so a constant curve pins the rotation wherever it
        // says regardless of the incoming amount — and a constant beyond the range is still clamped.
        [TestCase(1f, 0f, 90f)]
        [TestCase(0f, 1f, 0f)]
        [TestCase(0.5f, 0f, 45f)]
        [TestCase(2f, 0f, 90f)]
        [TestCase(-1f, 1f, 0f)]
        public void Convert_Curve_ShapesTheAmountBeforeTheTurn(float constant, float amount, float expectedZ) =>
            AssertSameRotation(
                Quaternion.Euler(0f, 0f, expectedZ),
                Slerp(AnimationCurve.Constant(0f, 1f, constant)).Convert(amount),
                $"a curve constant at {constant} evaluated at {amount}");

        [Test]
        public void Convert_LinearCurve_LeavesTheAmountAlone() =>
            AssertSameRotation(
                Quaternion.Euler(0f, 0f, 45f),
                Slerp(AnimationCurve.Linear(0f, 0f, 1f, 1f)).Convert(0.5f),
                "a linear curve at the halfway amount");

        // An unassigned curve deserializes as an empty one rather than as null, and Evaluate on an
        // empty curve returns zero — which would pin the rotation at the starting end for every
        // amount. Both spellings of "no curve" have to reach the same place, so an amount of 1 has
        // to land on the far end and not on the near one.
        [Test]
        public void Convert_EmptyCurve_IsTreatedAsNoCurve() =>
            AssertSameRotation(Quaternion.Euler(0f, 0f, 90f), Slerp(new AnimationCurve()).Convert(1f), "an empty curve");

        [Test]
        public void Convert_NullCurve_IsTreatedAsNoCurve() =>
            AssertSameRotation(Quaternion.Euler(0f, 0f, 90f), Slerp().Convert(1f), "a null curve");

        // Contradicts what "a gauge that sweeps between two stops" leads an author to expect: slerp
        // takes the short arc, so a dial authored 0° → 350° travels ten degrees backwards and its
        // midpoint is 355°, not the 175° that sweeping the long way round would give.
        [Test]
        public void Convert_TakesTheShortArcEvenWhenTheEndsSayOtherwise()
        {
            var converter = new QuaternionSlerpConverter(Vector3.zero, new Vector3(0f, 0f, 350f));

            AssertSameRotation(Quaternion.Euler(0f, 0f, 355f), converter.Convert(0.5f), "the midpoint of 0°→350°");
        }

        [TestCase(0f)]
        [TestCase(0.5f)]
        [TestCase(1f)]
        public void Convert_DefaultConstructed_TurnsNowhere(float amount) =>
            AssertSameRotation(
                Quaternion.identity,
                new QuaternionSlerpConverter().Convert(amount),
                $"an amount of {amount}");

        // Slerps from the identity to a 90° turn about Z, which is the pair every amount in this
        // fixture is read against.
        private static QuaternionSlerpConverter Slerp(AnimationCurve curve = null) =>
            new(Vector3.zero, new Vector3(0f, 0f, 90f), curve);

        // Quaternion.Equals is component-wise and exact, and a quaternion and its negation name the
        // same rotation, so anything computed is compared by the turn between the two instead.
        private static void AssertSameRotation(Quaternion expected, Quaternion actual, string what) =>
            Assert.AreEqual(
                0f,
                Quaternion.Angle(expected, actual),
                1e-2f,
                $"{what}: expected {expected.eulerAngles}, got {actual.eulerAngles}.");
    }
}
