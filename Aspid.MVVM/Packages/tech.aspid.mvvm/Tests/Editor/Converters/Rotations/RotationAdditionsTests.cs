using System;
using UnityEngine;
using NUnit.Framework;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the members added to <c>RotationConverters.cs</c> in the latest catalogue wave —
    /// <see cref="RotationAxis.Custom"/> on <see cref="AngleToQuaternionConverter"/> in both
    /// directions, <see cref="QuaternionToAngleConverter"/>, <see cref="AngleDifferenceConverter"/>,
    /// <see cref="RadiansToDegreesConverter"/>, <see cref="QuaternionSlerpConverter"/> and the
    /// <see cref="QuaternionToVector4Converter"/>/<see cref="Vector4ToQuaternionConverter"/> pair.
    /// </summary>
    /// <remarks>
    /// The mistakes guarded against here are the ones a rotation converter makes silently: an angle
    /// that reads 358° when it moved two degrees the other way, a mirror-image converter that was
    /// copy-pasted the wrong way round and multiplies by 0.0175 where it should multiply by 57.3, a
    /// quaternion rebuilt from four numbers that were never unit length and so scales and shears
    /// whatever it lands on, and a custom axis whose sign is taken from the wrong end.
    /// <para>
    /// The expectations were taken by working the arithmetic through, not from the XML docs, and two
    /// of them contradict what those docs promise. <c>CustomAngle</c> — shared verbatim by
    /// <see cref="AngleToQuaternionConverter"/> and <see cref="QuaternionToAngleConverter"/> — does
    /// not project onto the custom axis at all: it returns the whole turn <c>ToAngleAxis</c> reports
    /// and only borrows a sign from the dot, so a rotation about a perpendicular axis reads as its
    /// full angle rather than as zero. And <see cref="QuaternionSlerpConverter"/> always takes the
    /// short arc, so a gauge authored to sweep from 0° to 350° runs ten degrees backwards instead of
    /// most of the way round. Each such test pins the behaviour and says so where it stands, so a
    /// later fix has to change the test deliberately.
    /// </para>
    /// <para>
    /// One outright defect is pinned rather than fixed, because fixing it belongs to the source and
    /// not to a test: <see cref="AngleToQuaternionConverter"/> negates for <c>clockwise</c> after
    /// reading <c>eulerAngles</c>, which Unity reports in 0..360, so a clockwise round trip of 30°
    /// comes back as -330 — the right rotation named by a number a whole turn away from the one that
    /// went in. The Custom branch escapes it because it reads a signed angle to begin with.
    /// </para>
    /// <para>
    /// Two serialized fields have no constructor parameter and no setter —
    /// <c>QuaternionSlerpConverter._clamp</c> and <c>QuaternionToAngleConverter._customAxis</c> — so
    /// the unclamped slerp branch and a custom axis other than up are only reachable through the
    /// Inspector and are not covered here.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class RotationAdditionsTests
    {
        #region AngleToQuaternionConverter — RotationAxis.Custom, forward

        // Quaternion.AngleAxis normalises the axis itself, so an axis nobody bothered to normalise
        // must produce the same rotation as the unit one — not a longer turn and not a scaled
        // quaternion. The 0.001 row is the same claim at the other end of the scale.
        [TestCase(0f, 1f, 0f)]
        [TestCase(0f, 5f, 0f)]
        [TestCase(0f, 0.001f, 0f)]
        public void AngleToQuaternion_Custom_NormalisesTheAuthoredAxis(float x, float y, float z) =>
            AssertSameRotation(
                Quaternion.Euler(0f, 90f, 0f),
                new AngleToQuaternionConverter(new Vector3(x, y, z)).Convert(90f),
                $"90° about ({x}, {y}, {z})");

        // An axis nobody filled in is the ordinary way to reach this branch, and the identity comes
        // back as the literal Quaternion.identity, so an exact comparison is the right one here.
        [Test]
        public void AngleToQuaternion_Custom_ZeroAxis_IsTheIdentity() =>
            Assert.AreEqual(Quaternion.identity, new AngleToQuaternionConverter(Vector3.zero).Convert(90f));

        // The offset is added after the clockwise flip, so an input of 0 lands on the offset itself
        // and the flip never touches it. An implementation that negated the sum would answer -30°.
        [Test]
        public void AngleToQuaternion_Custom_Clockwise_LeavesTheOffsetUnflipped() =>
            AssertSameRotation(
                Quaternion.Euler(0f, 30f, 0f),
                new AngleToQuaternionConverter(Vector3.up, offset: 30f, clockwise: true).Convert(0f),
                "an input of zero with a 30° offset");

        [Test]
        public void AngleToQuaternion_Custom_Clockwise_TurnsTheOtherWay() =>
            AssertSameRotation(
                Quaternion.Euler(0f, -90f, 0f),
                new AngleToQuaternionConverter(Vector3.up, clockwise: true).Convert(90f),
                "90° clockwise about up");

        #endregion

        #region AngleToQuaternionConverter — RotationAxis.Custom, back

        // ToAngleAxis always reports a positive turn and flips the axis when that is what it takes,
        // so the same rotation is describable two ways and the converter has to pick the one the
        // author asked for. Negative angles are the rows that fail if the dot test is dropped.
        [TestCase(0f)]
        [TestCase(45f)]
        [TestCase(90f)]
        [TestCase(-45f)]
        [TestCase(-90f)]
        [TestCase(179f)]
        public void AngleToQuaternion_Custom_RoundTrips(float angle)
        {
            var converter = new AngleToQuaternionConverter(new Vector3(1f, 1f, 0f));

            Assert.AreEqual(angle, converter.ConvertBack(converter.Convert(angle)), 1e-2f);
        }

        [TestCase(45f)]
        [TestCase(-90f)]
        public void AngleToQuaternion_Custom_RoundTripsThroughOffsetAndClockwise(float angle)
        {
            var converter = new AngleToQuaternionConverter(Vector3.up, offset: 30f, clockwise: true);

            Assert.AreEqual(angle, converter.ConvertBack(converter.Convert(angle)), 1e-2f);
        }

        // A rotation built about the opposite axis is the case the dot test exists for: ToAngleAxis
        // hands back +90° about down, and only the sign flip turns that into the -90° the author of
        // an up axis meant.
        [Test]
        public void AngleToQuaternion_Custom_RotationAboutTheOppositeAxis_ReadsBackNegative() =>
            Assert.AreEqual(
                -90f,
                new AngleToQuaternionConverter(Vector3.up).ConvertBack(Quaternion.AngleAxis(90f, Vector3.down)),
                1e-2f);

        // Contradicts the summary "reads the angle back off a rotation" for a chosen axis: nothing
        // is projected onto the axis. A rotation entirely about Z, which carries no turn about the
        // authored up axis at all, reads as its full 90° because the dot is zero and zero is not
        // negative. The right answer for an axis reading would be 0.
        [Test]
        public void AngleToQuaternion_Custom_PerpendicularRotation_ReportsTheWholeTurnNotZero() =>
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
        public void AngleToQuaternion_Custom_ConvertBackIsSignedWhereTheEulerAxesAreNot()
        {
            var rotation = Quaternion.Euler(0f, 0f, -30f);

            Assert.AreEqual(-30f, new AngleToQuaternionConverter(Vector3.forward).ConvertBack(rotation), 1e-2f);
            Assert.AreEqual(330f, new AngleToQuaternionConverter(RotationAxis.Z).ConvertBack(rotation), 1e-2f);
        }

        // A TwoWay binding whose axis was never filled in does not report "no angle": Convert throws
        // the value away and ConvertBack answers the offset with the clockwise flip applied to it,
        // so the ViewModel is written a constant that looks like a real reading.
        [TestCase(false, -30f)]
        [TestCase(true, 30f)]
        public void AngleToQuaternion_Custom_ZeroAxis_ConvertBackReportsTheOffsetAlone(bool clockwise, float expected) =>
            Assert.AreEqual(
                expected,
                new AngleToQuaternionConverter(Vector3.zero, offset: 30f, clockwise: clockwise)
                    .ConvertBack(Quaternion.Euler(0f, 90f, 0f)),
                1e-4f);

        [Test]
        public void AngleToQuaternion_UndeclaredAxis_Throws() =>
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new AngleToQuaternionConverter((RotationAxis)99).Convert(45f));

        #endregion

        #region QuaternionToAngleConverter

        // The Euler branches read one component of eulerAngles, which Unity reports in 0..360, and
        // the converter folds that into ±180. The 350° rows are the ones that matter: a needle a
        // degree below zero has to read -10, not 350. The last row is the contrast with the custom
        // branch below — a turn about a different axis reads as zero here.
        [TestCase(RotationAxis.X, 30f, 0f, 0f, 30f)]
        [TestCase(RotationAxis.Y, 0f, 30f, 0f, 30f)]
        [TestCase(RotationAxis.Z, 0f, 0f, 30f, 30f)]
        [TestCase(RotationAxis.X, 350f, 0f, 0f, -10f)]
        [TestCase(RotationAxis.Z, 0f, 0f, 350f, -10f)]
        [TestCase(RotationAxis.X, 0f, 0f, 90f, 0f)]
        public void QuaternionToAngle_ReadsTheChosenAxisAsSigned180(
            RotationAxis axis,
            float x,
            float y,
            float z,
            float expected) =>
            Assert.AreEqual(expected, new QuaternionToAngleConverter(axis).Convert(Quaternion.Euler(x, y, z)), 1e-2f);

        [TestCase(0f, 0f, 350f, 350f)]
        [TestCase(0f, 0f, -10f, 350f)]
        [TestCase(0f, 0f, 10f, 10f)]
        public void QuaternionToAngle_Unsigned_ReportsZeroToThreeSixty(float x, float y, float z, float expected) =>
            Assert.AreEqual(
                expected,
                new QuaternionToAngleConverter(RotationAxis.Z, signed: false).Convert(Quaternion.Euler(x, y, z)),
                1e-2f);

        // No constructor reaches _customAxis, so Custom reads around the serialized default of up.
        // The negative row is where this converter differs from AngleToQuaternionConverter's
        // ConvertBack: the raw -90 from ToAngleAxis is folded through Mathf.Repeat into 270 first,
        // and only the signed flag brings it back to -90.
        [TestCase(90f, true, 90f)]
        [TestCase(-90f, true, -90f)]
        [TestCase(90f, false, 90f)]
        [TestCase(-90f, false, 270f)]
        public void QuaternionToAngle_Custom_ReadsAroundTheDefaultUpAxis(float angle, bool signed, float expected) =>
            Assert.AreEqual(
                expected,
                new QuaternionToAngleConverter(RotationAxis.Custom, signed)
                    .Convert(Quaternion.AngleAxis(angle, Vector3.up)),
                1e-2f);

        // The same divergence from the docs as on AngleToQuaternionConverter, and worth pinning on
        // both because the two copies of CustomAngle can drift apart: a rotation about Z carries no
        // turn about up, and the converter reports its full 90° anyway.
        [Test]
        public void QuaternionToAngle_Custom_PerpendicularRotation_ReportsTheWholeTurnNotZero() =>
            Assert.AreEqual(
                90f,
                new QuaternionToAngleConverter(RotationAxis.Custom).Convert(Quaternion.Euler(0f, 0f, 90f)),
                1e-2f);

        // The class remarks promise this: it is not AngleToQuaternionConverter's ConvertBack under
        // another name. It carries no offset to undo, so the two disagree by exactly the offset —
        // the trap for anyone who puts this converter on the OneWay leg of a pair.
        [Test]
        public void QuaternionToAngle_DoesNotUndoAnAngleToQuaternionOffset()
        {
            var source = new AngleToQuaternionConverter(RotationAxis.Z, offset: 30f);
            var rotation = source.Convert(0f);

            Assert.AreEqual(0f, source.ConvertBack(rotation), 1e-2f);
            Assert.AreEqual(30f, new QuaternionToAngleConverter().Convert(rotation), 1e-2f);
        }

        // The other flag, and the more alarming half. AngleToQuaternionConverter reads the Euler
        // axes off eulerAngles, which Unity reports in 0..360, so a clockwise 30° comes back as
        // -(330) = -330 rather than the 30 that went in: the round trip is off by a whole turn, and
        // only for clockwise, because the negation is applied after the 0..360 reading rather than
        // before. This converter folds to ±180 first and answers the -30 the rotation actually is.
        [Test]
        public void QuaternionToAngle_ReadsClockwiseWhereAngleToQuaternionOvershootsByAFullTurn()
        {
            var source = new AngleToQuaternionConverter(RotationAxis.Z, clockwise: true);
            var rotation = source.Convert(30f);

            Assert.AreEqual(-330f, source.ConvertBack(rotation), 1e-2f);
            Assert.AreEqual(-30f, new QuaternionToAngleConverter().Convert(rotation), 1e-2f);
        }

        [Test]
        public void QuaternionToAngle_UndeclaredAxis_Throws() =>
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new QuaternionToAngleConverter((RotationAxis)99).Convert(Quaternion.identity));

        #endregion

        #region AngleDifferenceConverter

        // The whole reason the converter is not a subtraction. Rows 2 and 3 straddle zero, where the
        // plain difference reads ±340 for what is twenty degrees the other way.
        [TestCase(0f, 10f, 10f)]
        [TestCase(350f, 10f, 20f)]
        [TestCase(10f, 350f, -20f)]
        [TestCase(-170f, 170f, -20f)]
        [TestCase(170f, -170f, 20f)]
        // A full turn is no difference at all, however many times it was taken.
        [TestCase(0f, 360f, 0f)]
        [TestCase(0f, 720f, 0f)]
        [TestCase(45f, -315f, 0f)]
        public void AngleDifference_Signed_TakesTheShortWayRound(float reference, float value, float expected) =>
            Assert.AreEqual(expected, new AngleDifferenceConverter(reference).Convert(value), 1e-3f);

        // The wrap across 180, where the sign changes hands. Exactly half a turn is reported as
        // +180 and not -180 — Mathf.DeltaAngle's fold is `> 180`, not `>=` — and a half turn taken
        // the other way (-180) folds onto that same +180. One degree past the boundary in either
        // direction is where the answer jumps the full 358° to the opposite sign.
        [TestCase(0f, 180f, 180f)]
        [TestCase(0f, -180f, 180f)]
        [TestCase(0f, 179f, 179f)]
        [TestCase(0f, 181f, -179f)]
        [TestCase(0f, -181f, 179f)]
        [TestCase(90f, 270f, 180f)]
        [TestCase(90f, 271f, -179f)]
        [TestCase(90f, 269f, 179f)]
        public void AngleDifference_Signed_HalfATurnStaysPositiveAndFlipsOneDegreeLater(
            float reference,
            float value,
            float expected) =>
            Assert.AreEqual(expected, new AngleDifferenceConverter(reference).Convert(value), 1e-3f);

        // Unsigned is the magnitude of the signed answer, so the ±179 pair collapses onto one number
        // and the half turn survives as 180 — the largest value this converter can ever report.
        [TestCase(0f, 181f, 179f)]
        [TestCase(0f, -181f, 179f)]
        [TestCase(0f, 180f, 180f)]
        [TestCase(0f, 190f, 170f)]
        [TestCase(10f, 350f, 20f)]
        [TestCase(350f, 10f, 20f)]
        public void AngleDifference_Unsigned_ReportsHowFarOffWhicheverWay(float reference, float value, float expected) =>
            Assert.AreEqual(expected, new AngleDifferenceConverter(reference, signed: false).Convert(value), 1e-3f);

        [TestCase(45f, 45f)]
        [TestCase(-45f, -45f)]
        [TestCase(350f, -10f)]
        public void AngleDifference_DefaultConstructed_MeasuresFromZero(float value, float expected) =>
            Assert.AreEqual(expected, new AngleDifferenceConverter().Convert(value), 1e-3f);

        #endregion

        #region RadiansToDegreesConverter

        // The trap this converter exists to fall into: it is the mirror of DegreesToRadiansConverter
        // and a copy-paste that kept the old body would answer 0.0175 here instead of 57.3. The
        // numbers are large enough that the two directions cannot be confused.
        [TestCase(0f, 0f)]
        [TestCase(1f, 57.29578f)]
        [TestCase(3.1415927f, 180f)]
        [TestCase(0.7853982f, 45f)]
        [TestCase(-1.5707964f, -90f)]
        public void RadiansToDegrees_Convert_TurnsRadiansIntoDegrees(float value, float expected) =>
            Assert.AreEqual(expected, new RadiansToDegreesConverter().Convert(value), 1e-3f);

        [TestCase(0f, 0f)]
        [TestCase(180f, 3.1415927f)]
        [TestCase(90f, 1.5707964f)]
        [TestCase(-45f, -0.7853982f)]
        public void RadiansToDegrees_ConvertBack_TurnsDegreesIntoRadians(float value, float expected) =>
            Assert.AreEqual(expected, new RadiansToDegreesConverter().ConvertBack(value), 1e-6f);

        // Same statement from the other side, and the one that would catch a class wired up as an
        // alias of the converter it mirrors: the two must cross, not agree.
        [TestCase(1f)]
        [TestCase(90f)]
        [TestCase(-2.5f)]
        public void RadiansToDegrees_IsDegreesToRadiansTheOtherWayRound(float value)
        {
            var radiansToDegrees = new RadiansToDegreesConverter();
            var degreesToRadians = new DegreesToRadiansConverter();

            Assert.AreEqual(degreesToRadians.ConvertBack(value), radiansToDegrees.Convert(value), 1e-6f);
            Assert.AreEqual(degreesToRadians.Convert(value), radiansToDegrees.ConvertBack(value), 1e-6f);
        }

        [TestCase(0f)]
        [TestCase(1f)]
        [TestCase(-6.2831855f)]
        public void RadiansToDegrees_RoundTrips(float value)
        {
            var converter = new RadiansToDegreesConverter();

            Assert.AreEqual(value, converter.ConvertBack(converter.Convert(value)), 1e-5f);
        }

        #endregion

        #region QuaternionSlerpConverter

        [TestCase(0f, 0f)]
        [TestCase(0.5f, 45f)]
        [TestCase(1f, 90f)]
        public void QuaternionSlerp_ReadsTheRotationAtTheAmount(float amount, float expectedZ) =>
            AssertSameRotation(Quaternion.Euler(0f, 0f, expectedZ), Slerp().Convert(amount), $"an amount of {amount}");

        // The clamp is on and cannot be turned off from code. An unclamped slerp would answer 180°
        // for an amount of 2 and -90° for -1, so these two rows are what tells the branches apart.
        [TestCase(2f, 90f)]
        [TestCase(-1f, 0f)]
        public void QuaternionSlerp_HoldsTheAmountInsideZeroToOne(float amount, float expectedZ) =>
            AssertSameRotation(Quaternion.Euler(0f, 0f, expectedZ), Slerp().Convert(amount), $"an amount of {amount}");

        // The curve is consulted before the turn, so a constant curve pins the rotation wherever it
        // says regardless of the incoming amount — and a constant beyond the range is still clamped.
        [TestCase(1f, 0f, 90f)]
        [TestCase(0f, 1f, 0f)]
        [TestCase(0.5f, 0f, 45f)]
        [TestCase(2f, 0f, 90f)]
        [TestCase(-1f, 1f, 0f)]
        public void QuaternionSlerp_Curve_ShapesTheAmountBeforeTheTurn(float constant, float amount, float expectedZ) =>
            AssertSameRotation(
                Quaternion.Euler(0f, 0f, expectedZ),
                Slerp(AnimationCurve.Constant(0f, 1f, constant)).Convert(amount),
                $"a curve constant at {constant} evaluated at {amount}");

        [Test]
        public void QuaternionSlerp_LinearCurve_LeavesTheAmountAlone() =>
            AssertSameRotation(
                Quaternion.Euler(0f, 0f, 45f),
                Slerp(AnimationCurve.Linear(0f, 0f, 1f, 1f)).Convert(0.5f),
                "a linear curve at the halfway amount");

        // An unassigned curve deserializes as an empty one rather than as null, and Evaluate on an
        // empty curve returns zero — which would pin the rotation at the starting end for every
        // amount. Both spellings of "no curve" have to reach the same place, so an amount of 1 has
        // to land on the far end and not on the near one.
        [Test]
        public void QuaternionSlerp_EmptyCurve_IsTreatedAsNoCurve() =>
            AssertSameRotation(Quaternion.Euler(0f, 0f, 90f), Slerp(new AnimationCurve()).Convert(1f), "an empty curve");

        [Test]
        public void QuaternionSlerp_NullCurve_IsTreatedAsNoCurve() =>
            AssertSameRotation(Quaternion.Euler(0f, 0f, 90f), Slerp(null).Convert(1f), "a null curve");

        // Contradicts what "a gauge that sweeps between two stops" leads an author to expect: slerp
        // takes the short arc, so a dial authored 0° → 350° travels ten degrees backwards and its
        // midpoint is 355°, not the 175° that sweeping the long way round would give.
        [Test]
        public void QuaternionSlerp_TakesTheShortArcEvenWhenTheEndsSayOtherwise()
        {
            var converter = new QuaternionSlerpConverter(Vector3.zero, new Vector3(0f, 0f, 350f));

            AssertSameRotation(Quaternion.Euler(0f, 0f, 355f), converter.Convert(0.5f), "the midpoint of 0°→350°");
        }

        [TestCase(0f)]
        [TestCase(0.5f)]
        [TestCase(1f)]
        public void QuaternionSlerp_DefaultConstructed_TurnsNowhere(float amount) =>
            AssertSameRotation(
                Quaternion.identity,
                new QuaternionSlerpConverter().Convert(amount),
                $"an amount of {amount}");

        #endregion

        #region QuaternionToVector4Converter / Vector4ToQuaternionConverter

        // Four distinct numbers, none of them a rotation: the order has to be x, y, z, w and nothing
        // may be normalised on the way out. A w-first copy, or a normalising one, fails every row.
        [Test]
        public void QuaternionToVector4_CopiesTheFourNumbersInOrderWithoutNormalising()
        {
            var packed = new QuaternionToVector4Converter().Convert(new Quaternion(0.1f, 0.2f, 0.3f, 0.4f));

            Assert.AreEqual(0.1f, packed.x, 1e-6f);
            Assert.AreEqual(0.2f, packed.y, 1e-6f);
            Assert.AreEqual(0.3f, packed.z, 1e-6f);
            Assert.AreEqual(0.4f, packed.w, 1e-6f);
            Assert.AreEqual(0.3f, packed.sqrMagnitude, 1e-6f);
        }

        // Numbers off a lerp, a text field or a lossy wire format are rarely unit length. Both rows
        // describe the same 90° turn about Z at different scales, and normalising is what makes them
        // agree; without it the second would scale whatever it multiplies by three.
        [TestCase(0f, 0f, 0.5f, 0.5f)]
        [TestCase(0f, 0f, 3f, 3f)]
        public void Vector4ToQuaternion_NormalisesByDefault(float x, float y, float z, float w)
        {
            var rotation = new Vector4ToQuaternionConverter().Convert(new Vector4(x, y, z, w));

            Assert.AreEqual(0.70710678f, rotation.z, 1e-5f);
            Assert.AreEqual(0.70710678f, rotation.w, 1e-5f);
            Assert.AreEqual(90f, rotation.eulerAngles.z, 1e-2f);
        }

        [Test]
        public void Vector4ToQuaternion_WithoutNormalising_KeepsTheRawNumbers()
        {
            var rotation = new Vector4ToQuaternionConverter(normalize: false).Convert(new Vector4(0f, 0f, 0.5f, 0.5f));

            Assert.AreEqual(0.5f, rotation.z, 1e-6f);
            Assert.AreEqual(0.5f, rotation.w, 1e-6f);
        }

        [Test]
        public void Vector4ToQuaternion_ZeroWhileNormalising_IsTheIdentity() =>
            Assert.AreEqual(Quaternion.identity, new Vector4ToQuaternionConverter().Convert(Vector4.zero));

        // The guard belongs to the normalising path only. With the flag cleared, four zeroes come
        // through as a zero quaternion — not the identity — and a zero quaternion collapses whatever
        // it multiplies instead of leaving it alone.
        [Test]
        public void Vector4ToQuaternion_ZeroWithoutNormalising_IsADegenerateRotation()
        {
            var rotation = new Vector4ToQuaternionConverter(normalize: false).Convert(Vector4.zero);

            Assert.AreEqual(0f, rotation.w, 1e-6f);
            Assert.AreNotEqual(Quaternion.identity, rotation);
        }

        // The pair is meant to be a round trip for a save record or a network packet, so a unit
        // rotation has to survive it component for component — the sign of each number included,
        // which a rotation-space comparison would hide.
        [Test]
        public void QuaternionAndVector4_RoundTripAUnitRotation()
        {
            var rotation = Quaternion.Euler(30f, 45f, 60f);
            var packed = new QuaternionToVector4Converter().Convert(rotation);
            var restored = new Vector4ToQuaternionConverter().Convert(packed);

            Assert.AreEqual(rotation.x, restored.x, 1e-5f);
            Assert.AreEqual(rotation.y, restored.y, 1e-5f);
            Assert.AreEqual(rotation.z, restored.z, 1e-5f);
            Assert.AreEqual(rotation.w, restored.w, 1e-5f);
        }

        #endregion

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
