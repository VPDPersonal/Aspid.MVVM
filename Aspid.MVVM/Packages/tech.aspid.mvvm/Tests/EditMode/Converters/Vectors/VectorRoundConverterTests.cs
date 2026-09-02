using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="VectorRoundConverter"/> — the rounding directions, the grid step, and
    /// the undeclared-mode guard.
    /// </summary>
    /// <remarks>
    /// <c>Mathf.Round</c> is banker's rounding: an exact half goes to the even neighbour, not away
    /// from zero.
    /// </remarks>
    [TestFixture]
    public sealed class VectorRoundConverterTests
    {
        // Mathf.Round is Math.Round, which is banker's rounding: an exact half goes to the EVEN
        // neighbour, not away from zero. 1.5 and 2.5 therefore both answer 2 — the row an
        // implementation that "rounds up on a half" fails, and the reason the scalar
        // RoundNumberConverter has a midpoint field that this converter does not.
        [Test]
        public void VectorRound_ExactHalf_GoesToTheEvenNeighbour() =>
            AssertClose(
                new Vector3(0f, 2f, 2f),
                new VectorRoundConverter(RoundMode.Round).Convert(new Vector3(0.5f, 1.5f, 2.5f)));

        // A step of zero is not "multiply the vector away"; it reads as "no grid" and rounds to whole
        // numbers, which is the only thing an unset step can sensibly mean.
        [Test]
        public void VectorRound_DefaultConstructed_RoundsToWholeNumbers() =>
            AssertClose(
                new Vector3(1f, -2f, 2f),
                new VectorRoundConverter().Convert(new Vector3(1.4f, -1.6f, 2.5f)));

        // Floor and Truncate agree above zero and part company below it, which is where a converter
        // written as a cast goes wrong. -1.5 also separates Round from both.
        [TestCase(RoundMode.Round, 1f, -1f, -2f)]
        [TestCase(RoundMode.Floor, 1f, -2f, -2f)]
        [TestCase(RoundMode.Ceil, 2f, -1f, -1f)]
        [TestCase(RoundMode.Truncate, 1f, -1f, -1f)]
        public void VectorRound_Direction_DecidesWhereTheFractionGoes(RoundMode mode, float x, float y, float z) =>
            AssertClose(
                new Vector3(x, y, z),
                new VectorRoundConverter(mode).Convert(new Vector3(1.4f, -1.4f, -1.5f)));

        // The rounding happens on the value divided by the step, so the midpoint rule lands on the
        // grid cell rather than the units place: 0.25 and 1.25 both sit exactly between two 0.5 cells
        // and go to the even one, while 0.75 goes up.
        [Test]
        public void VectorRound_Step_AppliesTheMidpointRuleAtTheGridCell() =>
            AssertClose(
                new Vector3(0f, 1f, 1f),
                new VectorRoundConverter(RoundMode.Round, 0.5f).Convert(new Vector3(0.25f, 0.75f, 1.25f)));

        [TestCase(RoundMode.Floor, 0.5f)]
        [TestCase(RoundMode.Ceil, 1f)]
        public void VectorRound_PositiveStep_SnapsToTheGrid(RoundMode mode, float expected) =>
            AssertClose(Vector3.one * expected, new VectorRoundConverter(mode, 0.5f).Convert(Vector3.one * 0.6f));

        // A negative step is a misconfiguration, not a direction. Taken raw it divides the value by a
        // negative number and mirrors the rounding, so Floor would walk the value UP and Ceil down —
        // the opposite of the mode that was picked. The converter reports it and snaps to a grid of
        // the same size instead, so these rows land exactly where the positive step above lands.
        // One expectation per row also pins that the report is per push, not per axis.
        [TestCase(RoundMode.Floor, 0.5f)]
        [TestCase(RoundMode.Ceil, 1f)]
        public void VectorRound_NegativeStep_ReportsItAndKeepsTheChosenDirection(RoundMode mode, float expected)
        {
            LogAssert.Expect(LogType.Error, new Regex(@"grid step -0\.5 is negative"));

            AssertClose(Vector3.one * expected, new VectorRoundConverter(mode, -0.5f).Convert(Vector3.one * 0.6f));
        }

        [Test]
        public void Vector2Round_NegativeStep_ReportsItAndKeepsTheChosenDirection()
        {
            LogAssert.Expect(LogType.Error, new Regex(@"grid step -0\.25 is negative"));

            AssertClose(
                new Vector2(0.5f, -0.25f),
                AsWidth<Vector2>(new VectorRoundConverter(RoundMode.Ceil, -0.25f)).Convert(new Vector2(0.3f, -0.3f)));
        }

        [Test]
        public void Vector2Round_SnapsBothAxesToTheGrid() =>
            AssertClose(
                new Vector2(0.5f, -0.25f),
                AsWidth<Vector2>(new VectorRoundConverter(RoundMode.Ceil, 0.25f)).Convert(new Vector2(0.3f, -0.3f)));

        [Test]
        public void Vector2Round_DefaultConstructed_RoundsToWholeNumbers() =>
            AssertClose(
                new Vector2(1f, -2f),
                AsWidth<Vector2>(new VectorRoundConverter()).Convert(new Vector2(1.4f, -1.6f)));

        // Floor over four components, W included: a width that stopped at Z would leave -0.5 in it.
        [Test]
        public void Vector4Round_DropsTheFractionOnEveryComponent() =>
            AssertClose(
                new Vector4(1f, -2f, 2f, -1f),
                AsWidth<Vector4>(new VectorRoundConverter(RoundMode.Floor))
                    .Convert(new Vector4(1.7f, -1.2f, 2.9f, -0.5f)));

        // The setting is a serialized field rather than an argument, so an undeclared value — corrupted
        // YAML or a stray cast — is reported on every push and the fraction is kept, rather than
        // throwing the binding down. The inputs carry fractions so a silent rounding would fail here.
        [Test]
        public void VectorRound_UndeclaredMode_ReportsItAndKeepsTheFraction()
        {
            LogAssert.Expect(LogType.Error, new Regex("VectorRoundConverter.*not a declared RoundMode"));

            AssertClose(
                new Vector3(1.4f, -1.6f, 0.5f),
                new VectorRoundConverter((RoundMode)99).Convert(new Vector3(1.4f, -1.6f, 0.5f)));
        }

        [Test]
        public void Vector2Round_UndeclaredMode_ReportsItAndKeepsTheFraction()
        {
            LogAssert.Expect(LogType.Error, new Regex("VectorRoundConverter.*not a declared RoundMode"));

            AssertClose(
                new Vector2(1.4f, -1.6f),
                AsWidth<Vector2>(new VectorRoundConverter((RoundMode)99)).Convert(new Vector2(1.4f, -1.6f)));
        }

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
