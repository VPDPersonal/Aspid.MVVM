using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="AngleToQuaternionConverter"/> — the axis-normalizing and clockwise
    /// behavior of <see cref="RotationAxis.Custom"/> in both directions, and the zero-axis and
    /// undeclared-axis fallbacks.
    /// </summary>
    /// <remarks>
    /// Expectations were worked through by hand, and one contradicts the summary: the Custom branch's
    /// <c>ConvertBack</c> does not project onto the custom axis, and a clockwise round trip through the
    /// Z axis comes back off by a whole turn.
    /// </remarks>
    [TestFixture]
    public sealed class AngleToQuaternionConverterTests
    {
        #region Convert — RotationAxis.Custom

        // Quaternion.AngleAxis normalizes the axis itself, so an axis nobody bothered to normalize
        // must produce the same rotation as the unit one — not a longer turn and not a scaled
        // quaternion. The 0.001 row is the same claim at the other end of the scale.
        [TestCase(0f, 1f, 0f)]
        [TestCase(0f, 5f, 0f)]
        [TestCase(0f, 0.001f, 0f)]
        public void Convert_Custom_NormalizesTheAuthoredAxis(float x, float y, float z) =>
            AssertSameRotation(
                Quaternion.Euler(0f, 90f, 0f),
                new AngleToQuaternionConverter(new Vector3(x, y, z)).Convert(90f),
                $"90° about ({x}, {y}, {z})");

        // An axis cleared in the Inspector has no direction to turn around. Left silent the binding
        // would simply never move, so it is reported on every push and the documented fallback is
        // the identity — which comes back literal, so an exact comparison is the right one here.
        [Test]
        public void Convert_Custom_ZeroAxis_ReportsItAndTurnsNowhere()
        {
            LogAssert.Expect(LogType.Error, new Regex("custom axis is zero"));

            Assert.AreEqual(Quaternion.identity, new AngleToQuaternionConverter(Vector3.zero).Convert(90f));
        }

        // The offset is added after the clockwise flip, so an input of 0 lands on the offset itself
        // and the flip never touches it. An implementation that negated the sum would answer -30°.
        [Test]
        public void Convert_Custom_Clockwise_LeavesTheOffsetUnflipped() =>
            AssertSameRotation(
                Quaternion.Euler(0f, 30f, 0f),
                new AngleToQuaternionConverter(Vector3.up, offset: 30f, clockwise: true).Convert(0f),
                "an input of zero with a 30° offset");

        [Test]
        public void Convert_Custom_Clockwise_TurnsTheOtherWay() =>
            AssertSameRotation(
                Quaternion.Euler(0f, -90f, 0f),
                new AngleToQuaternionConverter(Vector3.up, clockwise: true).Convert(90f),
                "90° clockwise about up");

        #endregion

        #region ConvertBack — RotationAxis.Custom

        // ToAngleAxis always reports a positive turn and flips the axis when that is what it takes,
        // so the same rotation is describable two ways and the converter has to pick the one the
        // author asked for. Negative angles are the rows that fail if the dot test is dropped.
        [TestCase(0f)]
        [TestCase(45f)]
        [TestCase(90f)]
        [TestCase(-45f)]
        [TestCase(-90f)]
        [TestCase(179f)]
        public void ConvertBack_Custom_RoundTrips(float angle)
        {
            var converter = new AngleToQuaternionConverter(new Vector3(1f, 1f, 0f));

            Assert.AreEqual(angle, converter.ConvertBack(converter.Convert(angle)), 1e-2f);
        }

        [TestCase(45f)]
        [TestCase(-90f)]
        public void ConvertBack_Custom_RoundTripsThroughOffsetAndClockwise(float angle)
        {
            var converter = new AngleToQuaternionConverter(Vector3.up, offset: 30f, clockwise: true);

            Assert.AreEqual(angle, converter.ConvertBack(converter.Convert(angle)), 1e-2f);
        }

        // A rotation built about the opposite axis is the case the dot test exists for: ToAngleAxis
        // hands back +90° about down, and only the sign flip turns that into the -90° the author of
        // an up axis meant.
        [Test]
        public void ConvertBack_Custom_RotationAboutTheOppositeAxis_ReadsBackNegative() =>
            Assert.AreEqual(
                -90f,
                new AngleToQuaternionConverter(Vector3.up).ConvertBack(Quaternion.AngleAxis(90f, Vector3.down)),
                1e-2f);

        // Contradicts the summary "reads the angle back off a rotation" for a chosen axis: nothing
        // is projected onto the axis. A rotation entirely about Z, which carries no turn about the
        // authored up axis at all, reads as its full 90° because the dot is zero and zero is not
        // negative. The right answer for an axis reading would be 0.
        [Test]
        public void ConvertBack_Custom_PerpendicularRotation_ReportsTheWholeTurnNotZero() =>
            Assert.AreEqual(
                90f,
                new AngleToQuaternionConverter(Vector3.up).ConvertBack(Quaternion.Euler(0f, 0f, 90f)),
                1e-2f);

        // The Custom branch is signed where the Euler branches are not: it reads ToAngleAxis and
        // keeps the sign, while X/Y/Z read eulerAngles, which Unity reports in 0..360. The same
        // rotation therefore reads -30 through a custom Z axis and 330 through RotationAxis.Z — the
        // two answers differ by a whole turn, and a ViewModel bound through the wrong one of them
        // sees a value it never wrote.
        [Test]
        public void ConvertBack_Custom_IsSignedWhereTheEulerAxesAreNot()
        {
            var rotation = Quaternion.Euler(0f, 0f, -30f);

            Assert.AreEqual(-30f, new AngleToQuaternionConverter(Vector3.forward).ConvertBack(rotation), 1e-2f);
            Assert.AreEqual(330f, new AngleToQuaternionConverter(RotationAxis.Z).ConvertBack(rotation), 1e-2f);
        }

        // A TwoWay binding whose axis was never filled in reads no angle at all: the rotation is
        // thrown away and what reaches the ViewModel is the offset with the clockwise flip applied
        // to it. That constant would look like a real reading, so it is reported on every push.
        [TestCase(false, -30f)]
        [TestCase(true, 30f)]
        public void ConvertBack_Custom_ZeroAxis_ReportsItAndAnswersTheOffsetAlone(bool clockwise, float expected)
        {
            LogAssert.Expect(LogType.Error, new Regex("custom axis is zero"));

            Assert.AreEqual(
                expected,
                new AngleToQuaternionConverter(Vector3.zero, offset: 30f, clockwise: clockwise)
                    .ConvertBack(Quaternion.Euler(0f, 90f, 0f)),
                1e-4f);
        }

        #endregion

        #region Undeclared axis

        // The axis is a serialized setting rather than an argument, so an undeclared one — corrupted
        // YAML or a stray cast — is reported on every push and answered through Z, the axis a new
        // converter starts on, rather than throwing the binding down.
        [Test]
        public void Convert_UndeclaredAxis_ReportsItAndTurnsAroundZ()
        {
            LogAssert.Expect(LogType.Error, new Regex("AngleToQuaternionConverter.*not a declared RotationAxis"));

            AssertSameRotation(
                Quaternion.Euler(0f, 0f, 45f),
                new AngleToQuaternionConverter((RotationAxis)99).Convert(45f),
                "an undeclared axis");
        }

        [Test]
        public void ConvertBack_UndeclaredAxis_ReadsTheAngleOffZ()
        {
            LogAssert.Expect(LogType.Error, new Regex("AngleToQuaternionConverter.*not a declared RotationAxis"));

            Assert.AreEqual(
                45f,
                new AngleToQuaternionConverter((RotationAxis)99).ConvertBack(Quaternion.Euler(0f, 0f, 45f)),
                1e-2f);
        }

        #endregion

        [Test]
        public void AngleToQuaternion_Double_TurnsTheSameWay() =>
            Assert.AreEqual(
                new AngleToQuaternionConverter().Convert(90f).eulerAngles,
                ((IConverter<double, Quaternion>)new AngleToQuaternionConverter()).Convert(90d).eulerAngles);

        [Test]
        public void AngleToQuaternion_Double_ConvertBack_ReadsTheAngle() =>
            Assert.AreEqual(
                (double)new AngleToQuaternionConverter().ConvertBack(Quaternion.Euler(0f, 90f, 0f)),
                ((ITwoWayConverter<double, Quaternion>)new AngleToQuaternionConverter())
                    .ConvertBack(Quaternion.Euler(0f, 90f, 0f)),
                1e-3d);

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
