using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the members added to <c>VectorMathConverters.cs</c> in the vector wave — the
    /// <see cref="Vector2ToFloatConverter"/> and its <see cref="Vector2Component.Dot"/> reading,
    /// <see cref="Vector2ArithmeticConverter"/>, the ordered-bounds fix shared by
    /// <see cref="VectorClampMagnitudeConverter"/> and <see cref="Vector2ClampMagnitudeConverter"/>,
    /// the component clamps, the round pair, the normalise pair,
    /// <see cref="VectorDistanceConverter"/> and the curve on <see cref="VectorLerpConverter"/>.
    /// </summary>
    /// <remarks>
    /// The class of mistake guarded here is a converter that silently stops honouring its own
    /// contract on input an Inspector can legitimately produce: a min/max pair typed the wrong way
    /// round, so a clamp breaks both of its bounds at once; a negative ceiling, so a length clamp
    /// turns the vector around; a negative grid step, so Floor rounds up; a direction that was never
    /// normalised, so a dot product silently scales its reading; a target Transform destroyed
    /// mid-session, so a distance either throws or measures to a corpse.
    /// <para>
    /// Every expectation below was taken from the arithmetic the source actually performs, not from
    /// the XML docs, and three of them contradict what a reader would assume. <c>Mathf.Round</c> is
    /// banker's rounding, so an exact half goes to the even neighbour rather than away from zero.
    /// Unity's <c>normalized</c> has a length floor of 1e-5, so a very short vector answers zero
    /// rather than a unit vector. And <see cref="VectorLerpConverter"/>'s "hold the incoming amount
    /// inside 0..1" is done by <c>Vector3.Lerp</c> on the amount the curve produced — the incoming
    /// value itself is never clamped. Each such test pins the behaviour and says so where it stands.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class VectorMathAdditionsTests
    {
        private static readonly Vector3 From = new(2f, 0f, 0f);
        private static readonly Vector3 To = new(12f, 0f, 0f);

        private readonly List<GameObject> _created = new();

        [TearDown]
        public void DestroyCreatedObjects()
        {
            // Unity's implicit bool is false for the one the destroyed-target test already removed.
            foreach (var gameObject in _created)
            {
                if (gameObject) UnityEngine.Object.DestroyImmediate(gameObject);
            }

            _created.Clear();
        }

        #region Vector2ToFloatConverter and the Dot component

        [TestCase(Vector2Component.X, 3f)]
        [TestCase(Vector2Component.Y, 4f)]
        [TestCase(Vector2Component.Magnitude, 5f)]
        [TestCase(Vector2Component.SqrMagnitude, 25f)]
        // Dot reached through the component-only ctor leaves the direction at its default, which is
        // up rather than right — so the reading is the y, and a converter that defaulted to right
        // would answer 3 here.
        [TestCase(Vector2Component.Dot, 4f)]
        public void Vector2ToFloat_Measures(Vector2Component component, float expected) =>
            Assert.AreEqual(expected, new Vector2ToFloatConverter(component).Convert(new Vector2(3f, 4f)), 1e-4f);

        [Test]
        public void Vector2ToFloat_DefaultConstructed_MeasuresLength() =>
            Assert.AreEqual(5f, new Vector2ToFloatConverter().Convert(new Vector2(3f, 4f)), 1e-4f);

        // The direction is used raw, not normalised: a direction of unit length reads as the signed
        // distance along it, one twice as long doubles that reading, and an unset one reads zero for
        // every input. The negative row is the one that proves the reading is signed rather than a
        // distance.
        [TestCase(1f, 0f, 3f)]
        [TestCase(0f, 1f, 4f)]
        [TestCase(-1f, 0f, -3f)]
        [TestCase(0f, 2f, 8f)]
        [TestCase(0f, 0f, 0f)]
        public void Vector2ToFloat_Dot_IsTheRawProductWithTheAuthoredDirection(float x, float y, float expected) =>
            Assert.AreEqual(
                expected,
                new Vector2ToFloatConverter(new Vector2(x, y)).Convert(new Vector2(3f, 4f)),
                1e-4f);

        // Passing a direction is the only way to select Dot without also naming the component, so the
        // ctor has to set the component itself — otherwise the converter still measures length and
        // the authored direction is never read.
        [Test]
        public void Vector2ToFloat_DirectionCtor_SelectsDotWithoutBeingTold() =>
            Assert.AreEqual(3f, new Vector2ToFloatConverter(Vector2.right).Convert(new Vector2(3f, 4f)), 1e-4f);

        [TestCase(0f, 1f, 0f, 4f)]
        [TestCase(0f, 0f, 2f, 10f)]
        [TestCase(-1f, 0f, 0f, -3f)]
        public void Vector3ToFloat_Dot_IsTheRawProductWithTheAuthoredDirection(
            float x,
            float y,
            float z,
            float expected) =>
            Assert.AreEqual(
                expected,
                new Vector3ToFloatConverter(new Vector3(x, y, z)).Convert(new Vector3(3f, 4f, 5f)),
                1e-4f);

        [Test]
        public void Vector3ToFloat_Dot_DefaultDirectionIsUp() =>
            Assert.AreEqual(4f, new Vector3ToFloatConverter(VectorComponent.Dot).Convert(new Vector3(3f, 4f, 5f)), 1e-4f);

        // Declaration order is the number Unity stores in the scene, so Dot had to be appended rather
        // than filed next to the axes it belongs with. Filing it after Z would repoint every field
        // already authored as Magnitude or SqrMagnitude, silently, on load.
        [TestCase(VectorComponent.X, 0)]
        [TestCase(VectorComponent.Y, 1)]
        [TestCase(VectorComponent.Z, 2)]
        [TestCase(VectorComponent.Magnitude, 3)]
        [TestCase(VectorComponent.SqrMagnitude, 4)]
        [TestCase(VectorComponent.Dot, 5)]
        public void VectorComponent_StoredValue_KeepsDotAppendedLast(VectorComponent component, int stored) =>
            Assert.AreEqual(stored, (int)component);

        [TestCase(Vector2Component.X, 0)]
        [TestCase(Vector2Component.Y, 1)]
        [TestCase(Vector2Component.Magnitude, 2)]
        [TestCase(Vector2Component.SqrMagnitude, 3)]
        [TestCase(Vector2Component.Dot, 4)]
        public void Vector2Component_StoredValue_HasNoZAxisToSkip(Vector2Component component, int stored) =>
            Assert.AreEqual(stored, (int)component);

        [Test]
        public void Vector2ToFloat_UndeclaredComponent_Throws() =>
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Vector2ToFloatConverter((Vector2Component)99).Convert(Vector2.one));

        #endregion

        #region Vector2ArithmeticConverter

        [TestCase(VectorOperation.Add, 11f, 22f)]
        [TestCase(VectorOperation.Subtract, -9f, -18f)]
        [TestCase(VectorOperation.Scale, 10f, 40f)]
        [TestCase(VectorOperation.Divide, 0.1f, 0.1f)]
        public void Vector2Arithmetic_AppliesTheOperation(VectorOperation operation, float x, float y) =>
            AssertClose(
                new Vector2(x, y),
                new Vector2ArithmeticConverter(operation, new Vector2(10f, 20f)).Convert(new Vector2(1f, 2f)));

        // What a freshly added [SerializeReference] entry does before anything is typed into it: Add
        // with a zero operand, which has to be an identity rather than a collapse to zero.
        [Test]
        public void Vector2Arithmetic_DefaultConstructed_LeavesTheVectorAlone() =>
            AssertClose(new Vector2(1f, 2f), new Vector2ArithmeticConverter().Convert(new Vector2(1f, 2f)));

        // Matches the Vector3 converter: a zero axis in the operand leaves that axis alone instead of
        // handing a binder an infinity. The x is in the case to prove the division still happens on
        // the axis that can take one.
        [Test]
        public void Vector2Arithmetic_DivideByAZeroAxis_LeavesThatAxisAlone() =>
            AssertClose(
                new Vector2(0.5f, 2f),
                new Vector2ArithmeticConverter(VectorOperation.Divide, new Vector2(2f, 0f)).Convert(new Vector2(1f, 2f)));

        // The zero guard is `== 0f`, so an ordinary negative divisor is not caught by it and the sign
        // travels through the division as arithmetic says it should.
        [Test]
        public void Vector2Arithmetic_DivideByANegativeAxis_KeepsTheSignChange() =>
            AssertClose(
                new Vector2(-0.5f, -0.5f),
                new Vector2ArithmeticConverter(VectorOperation.Divide, new Vector2(-2f, -4f))
                    .Convert(new Vector2(1f, 2f)));

        // The bounce a wall gives: the part along the normal is inverted, the part along the wall is
        // kept. A Reflect implemented as a plain negation would answer (-1, 1) here.
        [Test]
        public void Vector2Arithmetic_Reflect_BouncesOffTheOperandAsANormal() =>
            AssertClose(
                new Vector2(1f, 1f),
                new Vector2ArithmeticConverter(VectorOperation.Reflect, Vector2.up).Convert(new Vector2(1f, -1f)));

        // Vector2.Reflect does not normalise its normal, and neither does the converter, so a normal
        // authored twice as long as unit quadruples the part it reflects. Typing (0, 2) into the
        // operand field does not mean "up" — it means "up, four times over".
        [Test]
        public void Vector2Arithmetic_Reflect_DoesNotNormaliseTheOperand() =>
            AssertClose(
                new Vector2(1f, 7f),
                new Vector2ArithmeticConverter(VectorOperation.Reflect, new Vector2(0f, 2f))
                    .Convert(new Vector2(1f, -1f)));

        // An unset operand is a zero normal, whose reflection factor is zero, so the value walks
        // through unchanged — the opposite of what Scale does with the same unset operand.
        [Test]
        public void Vector2Arithmetic_Reflect_ZeroOperand_ReturnsTheInput() =>
            AssertClose(
                new Vector2(1f, -1f),
                new Vector2ArithmeticConverter(VectorOperation.Reflect, Vector2.zero).Convert(new Vector2(1f, -1f)));

        [Test]
        public void Vector2Arithmetic_Scale_ZeroOperand_CollapsesTheVector() =>
            AssertClose(
                Vector2.zero,
                new Vector2ArithmeticConverter(VectorOperation.Scale, Vector2.zero).Convert(new Vector2(1f, -1f)));

        [Test]
        public void Vector2Arithmetic_UndeclaredOperation_Throws() =>
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Vector2ArithmeticConverter((VectorOperation)99, Vector2.one).Convert(Vector2.one));

        #endregion

        #region ClampMagnitude — reversed bounds and a negative ceiling

        // The shared scale, which both magnitude clamps route through. Note the argument order is
        // (magnitude, min, max) here while the constructors take (max, min) — the rows name the
        // bounds so the two orders cannot be confused.
        //
        // Ordinary pairs first: too long is pulled back to the ceiling, too short is pushed out to
        // the floor, and anything between them is left alone.
        [TestCase(10f, 0f, 1f, 0.1f)]
        [TestCase(0.5f, 2f, 10f, 4f)]
        [TestCase(5f, 0f, 10f, 1f)]
        // The review fix. A pair typed the wrong way round is read in the order that holds the vector
        // inside both bounds: 1 and 5 mean 1..5 whichever field they were typed into. Taken raw, the
        // first of these rows would scale a length of 10 down to 1 — under the floor of 5 — so one
        // instance would break both of its own bounds at once.
        [TestCase(10f, 5f, 1f, 0.5f)]
        [TestCase(0.5f, 5f, 1f, 2f)]
        // The other half of the fix. Scaling by a negative ceiling turns the vector around, which is
        // the one thing a length clamp must never do; zero is the nearest legal length.
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
        public void VectorClampMagnitude_ReversedBounds_ClampsToTheLargerOfThePair() =>
            AssertClose(
                new Vector3(3f, 4f, 0f),
                new VectorClampMagnitudeConverter(maxMagnitude: 1f, minMagnitude: 5f).Convert(new Vector3(6f, 8f, 0f)));

        [Test]
        public void VectorClampMagnitude_ReversedBounds_RaisesToTheSmallerOfThePair() =>
            AssertClose(
                new Vector3(0.6f, 0.8f, 0f),
                new VectorClampMagnitudeConverter(maxMagnitude: 1f, minMagnitude: 5f)
                    .Convert(new Vector3(0.3f, 0.4f, 0f)));

        // Exactly zero, not the (-1.2, -1.6, 0) that scaling by -2/5 would produce: the result must
        // not point the other way.
        [Test]
        public void VectorClampMagnitude_NegativeCeiling_CollapsesToZeroRatherThanReversing() =>
            Assert.AreEqual(
                Vector3.zero,
                new VectorClampMagnitudeConverter(maxMagnitude: -2f).Convert(new Vector3(3f, 4f, 0f)));

        [Test]
        public void Vector2ClampMagnitude_ReversedBounds_ReadsThePairInOrder()
        {
            var converter = new Vector2ClampMagnitudeConverter(maxMagnitude: 1f, minMagnitude: 5f);

            AssertClose(new Vector2(3f, 4f), converter.Convert(new Vector2(6f, 8f)));
            AssertClose(new Vector2(0.6f, 0.8f), converter.Convert(new Vector2(0.3f, 0.4f)));
        }

        [Test]
        public void Vector2ClampMagnitude_NegativeCeiling_CollapsesToZeroRatherThanReversing() =>
            Assert.AreEqual(
                Vector2.zero,
                new Vector2ClampMagnitudeConverter(maxMagnitude: -2f).Convert(new Vector2(3f, 4f)));

        [Test]
        public void Vector2ClampMagnitude_KeepsTheDirectionWhileShorteningTheVector() =>
            AssertClose(new Vector2(0.6f, 0.8f), new Vector2ClampMagnitudeConverter(1f).Convert(new Vector2(3f, 4f)));

        // A floor of zero means "no floor" — the default — so a short vector stays short instead of
        // being stretched to the ceiling.
        [Test]
        public void Vector2ClampMagnitude_ZeroFloor_LeavesAShortVectorAlone() =>
            AssertClose(new Vector2(0.3f, 0.4f), new Vector2ClampMagnitudeConverter(10f).Convert(new Vector2(0.3f, 0.4f)));

        // A zero vector has no direction to stretch along, so the floor cannot be applied to it: the
        // converter hands it back rather than inventing an axis to grow on.
        [Test]
        public void Vector2ClampMagnitude_ZeroVector_StaysZeroEvenWithAFloor() =>
            Assert.AreEqual(Vector2.zero, new Vector2ClampMagnitudeConverter(10f, 2f).Convert(Vector2.zero));

        #endregion

        #region ClampComponents

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
        public void VectorClampComponents_OneAxisReversed_LeavesTheOtherAxesAlone() =>
            AssertClose(
                new Vector3(0f, 5f, 0.5f),
                new VectorClampComponentsConverter(new Vector3(0f, 5f, -1f), new Vector3(10f, -5f, 1f))
                    .Convert(new Vector3(-3f, 100f, 0.5f)));

        [Test]
        public void VectorClampComponents_ReversedBox_ClampsTheSameWayAsTheOrderedOne()
        {
            var value = new Vector3(5f, -5f, 0f);
            var ordered = new VectorClampComponentsConverter(-Vector3.one, Vector3.one).Convert(value);
            var reversed = new VectorClampComponentsConverter(Vector3.one, -Vector3.one).Convert(value);

            AssertClose(ordered, reversed);

            // Pinning the value as well as the agreement, so the pair cannot both be wrong together.
            AssertClose(new Vector3(1f, -1f, 0f), reversed);
        }

        [Test]
        public void Vector2ClampComponents_ReversedBox_ReadsThePairInOrder() =>
            AssertClose(
                new Vector2(1f, -1f),
                new Vector2ClampComponentsConverter(Vector2.one, -Vector2.one).Convert(new Vector2(5f, -5f)));

        [Test]
        public void Vector2ClampComponents_DefaultConstructed_HoldsBothAxesWithinOne() =>
            AssertClose(
                new Vector2(1f, -0.25f),
                new Vector2ClampComponentsConverter().Convert(new Vector2(5f, -0.25f)));

        #endregion

        #region Round

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

        // Nothing rejects a negative step, and dividing by it mirrors the rounding: Floor rounds the
        // result UP and Ceil rounds it down. An author who types -0.5 meaning "half a unit" gets the
        // opposite of the direction they picked.
        [TestCase(RoundMode.Floor, 0.5f, 0.5f)]
        [TestCase(RoundMode.Floor, -0.5f, 1f)]
        [TestCase(RoundMode.Ceil, 0.5f, 1f)]
        [TestCase(RoundMode.Ceil, -0.5f, 0.5f)]
        public void VectorRound_NegativeStep_MirrorsFloorAndCeil(RoundMode mode, float step, float expected) =>
            AssertClose(Vector3.one * expected, new VectorRoundConverter(mode, step).Convert(Vector3.one * 0.6f));

        [Test]
        public void Vector2Round_SnapsBothAxesToTheGrid() =>
            AssertClose(
                new Vector2(0.5f, -0.25f),
                new Vector2RoundConverter(RoundMode.Ceil, 0.25f).Convert(new Vector2(0.3f, -0.3f)));

        [Test]
        public void Vector2Round_DefaultConstructed_RoundsToWholeNumbers() =>
            AssertClose(new Vector2(1f, -2f), new Vector2RoundConverter().Convert(new Vector2(1.4f, -1.6f)));

        [Test]
        public void VectorRound_UndeclaredMode_Throws() =>
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new VectorRoundConverter((RoundMode)99).Convert(Vector3.one));

        [Test]
        public void Vector2Round_UndeclaredMode_Throws() =>
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Vector2RoundConverter((RoundMode)99).Convert(Vector2.one));

        #endregion

        #region Normalize

        [Test]
        public void Vector2Normalize_ReducesToUnitLength() =>
            AssertClose(new Vector2(0.6f, 0.8f), new Vector2NormalizeConverter().Convert(new Vector2(3f, 4f)));

        [Test]
        public void Vector2Normalize_NegativeDirection_KeepsItsSign() =>
            AssertClose(new Vector2(0f, -1f), new Vector2NormalizeConverter().Convert(new Vector2(0f, -4f)));

        // An already-unit vector must come back untouched rather than a hair off.
        [Test]
        public void Vector2Normalize_UnitInput_IsUnchanged() =>
            AssertClose(Vector2.up, new Vector2NormalizeConverter().Convert(Vector2.up));

        [Test]
        public void Vector2Normalize_ZeroStaysZeroRatherThanNaN() =>
            Assert.AreEqual(Vector2.zero, new Vector2NormalizeConverter().Convert(Vector2.zero));

        // Undocumented, and inherited from Unity rather than written here: `normalized` has a length
        // floor of 1e-5 (Vector2.kEpsilon / Vector3.kEpsilon), below which it answers zero instead of
        // a direction. A stick a hair off centre therefore aims nowhere rather than at full throw —
        // which is not what "or zero for a zero-length input" in the XML docs suggests.
        [Test]
        public void Vector2Normalize_BelowTheLengthEpsilon_IsZeroNotAUnitVector() =>
            Assert.AreEqual(Vector2.zero, new Vector2NormalizeConverter().Convert(new Vector2(1e-6f, 0f)));

        [Test]
        public void VectorNormalize_BelowTheLengthEpsilon_IsZeroNotAUnitVector() =>
            Assert.AreEqual(Vector3.zero, new VectorNormalizeConverter().Convert(new Vector3(1e-6f, 0f, 0f)));

        // The other side of the same threshold, which is what makes the two tests above a floor
        // rather than a blanket "short vectors are dropped": ten times the floor and the direction
        // survives.
        [Test]
        public void Vector2Normalize_JustAboveTheLengthEpsilon_KeepsTheDirection() =>
            AssertClose(Vector2.right, new Vector2NormalizeConverter().Convert(new Vector2(1e-4f, 0f)));

        #endregion

        #region VectorDistanceConverter

        // The flattened row is the one that matters: the height is dropped from the OFFSET, so a
        // position 10 above the target and 5 along the ground from it reads 5, not 11.18.
        [TestCase(3f, 4f, 0f, false, 5f)]
        [TestCase(3f, 10f, 4f, false, 11.18034f)]
        [TestCase(3f, 10f, 4f, true, 5f)]
        public void VectorDistance_MeasuresToTheAuthoredPoint(
            float x,
            float y,
            float z,
            bool flattenY,
            float expected) =>
            Assert.AreEqual(
                expected,
                new VectorDistanceConverter(Vector3.zero, flattenY).Convert(new Vector3(x, y, z)),
                1e-4f);

        [Test]
        public void VectorDistance_DefaultConstructed_MeasuresToTheOrigin() =>
            Assert.AreEqual(5f, new VectorDistanceConverter().Convert(new Vector3(3f, 4f, 0f)), 1e-4f);

        [Test]
        public void VectorDistance_AuthoredPoint_IsTheOtherEndOfTheMeasurement() =>
            Assert.AreEqual(
                5f,
                new VectorDistanceConverter(new Vector3(1f, 2f, 3f)).Convert(new Vector3(4f, 6f, 3f)),
                1e-4f);

        [Test]
        public void VectorDistance_Transform_MeasuresToItsPosition()
        {
            var target = NewTarget(new Vector3(10f, 0f, 0f));

            Assert.AreEqual(5f, new VectorDistanceConverter(target).Convert(new Vector3(13f, 4f, 0f)), 1e-4f);
        }

        // The position is read on every conversion rather than captured when the converter was built.
        // A waypoint marker has to follow the thing it points at, and a converter that cached the
        // position would pass the first assert and fail the second.
        [Test]
        public void VectorDistance_Transform_IsReReadOnEveryConversion()
        {
            var target = NewTarget(new Vector3(10f, 0f, 0f));
            var converter = new VectorDistanceConverter(target);

            Assert.AreEqual(0f, converter.Convert(new Vector3(10f, 0f, 0f)), 1e-4f);

            target.position = new Vector3(20f, 0f, 0f);

            Assert.AreEqual(10f, converter.Convert(new Vector3(10f, 0f, 0f)), 1e-4f);
        }

        [Test]
        public void VectorDistance_Transform_FlattenY_DropsTheHeightDifference()
        {
            var target = NewTarget(new Vector3(0f, 10f, 0f));

            Assert.AreEqual(
                5f,
                new VectorDistanceConverter(target, flattenY: true).Convert(new Vector3(3f, 0f, 4f)),
                1e-4f);
        }

        // The emptiness check is Unity's `== null`, not `is null`, so a destroyed target is seen as
        // empty and the converter measures to the authored point instead — zero for this ctor, hence
        // 5. Written with `is null` it would read `position` off a destroyed object and throw on the
        // frame the target dies; measuring to the old position would answer ~8.06.
        [Test]
        public void VectorDistance_DestroyedTarget_FallsBackToTheAuthoredPoint()
        {
            var target = NewTarget(new Vector3(10f, 0f, 0f));
            var converter = new VectorDistanceConverter(target);

            UnityEngine.Object.DestroyImmediate(target.gameObject);

            Assert.AreEqual(5f, converter.Convert(new Vector3(3f, 4f, 0f)), 1e-4f);

            // An empty target is an authoring choice, not a failure, so nothing may be reported.
            LogAssert.NoUnexpectedReceived();
        }

        #endregion

        #region VectorLerpConverter and the curve

        [Test]
        public void VectorLerp_NoCurve_MovesEvenly() =>
            AssertClose(new Vector3(7f, 0f, 0f), new VectorLerpConverter(From, To).Convert(0.5f));

        // Both spellings of "no curve" have to mean the same thing. An unassigned AnimationCurve
        // deserializes as an empty one rather than as null, and Evaluate on an empty curve answers 0
        // — which would pin every conversion at _from. The length guard is what stops that, and this
        // is the case that fails without it.
        [Test]
        public void VectorLerp_EmptyCurve_IsTreatedAsNoCurve() =>
            AssertClose(
                new Vector3(9f, 0f, 0f),
                new VectorLerpConverter(From, To, new AnimationCurve()).Convert(0.7f));

        // The curve shapes the amount before the move, so one that runs 1 down to 0 reverses the
        // travel: a quarter of the way in reads three quarters of the way along.
        [Test]
        public void VectorLerp_Curve_ShapesTheAmountBeforeTheMove() =>
            AssertClose(
                new Vector3(9.5f, 0f, 0f),
                new VectorLerpConverter(From, To, AnimationCurve.Linear(0f, 1f, 1f, 0f)).Convert(0.25f),
                1e-3f);

        // A constant curve proves the incoming amount is not consulted at all once a curve is
        // present — both conversions land on the same point.
        [Test]
        public void VectorLerp_ConstantCurve_IgnoresTheIncomingAmount()
        {
            var converter = new VectorLerpConverter(From, To, AnimationCurve.Constant(0f, 1f, 0.25f));

            AssertClose(new Vector3(4.5f, 0f, 0f), converter.Convert(0f));
            AssertClose(new Vector3(4.5f, 0f, 0f), converter.Convert(0.9f));
        }

        // The clamp belongs to Vector3.Lerp and applies to the amount the CURVE produced, not to the
        // amount that came in — the tooltip's "hold the incoming amount inside 0..1" describes the
        // wrong end. A curve that overshoots is held at the far end; one that undershoots, at the
        // near end. Unclamped these would read 22 and -8.
        [Test]
        public void VectorLerp_CurveAboveOne_IsHeldAtTheFarEnd() =>
            AssertClose(To, new VectorLerpConverter(From, To, AnimationCurve.Constant(0f, 1f, 2f)).Convert(0.5f));

        [Test]
        public void VectorLerp_CurveBelowZero_IsHeldAtTheNearEnd() =>
            AssertClose(From, new VectorLerpConverter(From, To, AnimationCurve.Constant(0f, 1f, -1f)).Convert(0.5f));

        [Test]
        public void VectorLerp_AmountOutsideZeroToOne_IsHeldAtTheEnds()
        {
            var converter = new VectorLerpConverter(From, To);

            AssertClose(To, converter.Convert(1.5f));
            AssertClose(From, converter.Convert(-0.5f));
        }

        // _clamp has no constructor parameter, so the LerpUnclamped branch is only reachable the way
        // a real scene reaches it — through deserialization of a [SerializeReference] field. The
        // first assert proves the JSON landed (and, incidentally, that a deserialized instance with
        // no curve key is not pinned at _from); the second is the branch itself, sailing half again
        // past _to instead of stopping on it.
        [Test]
        public void VectorLerp_Deserialized_WithClampOff_OvershootsTheFarEnd()
        {
            var converter = JsonUtility.FromJson<VectorLerpConverter>(
                "{\"_from\":{\"x\":2,\"y\":0,\"z\":0},\"_to\":{\"x\":12,\"y\":0,\"z\":0},\"_clamp\":false}");

            AssertClose(new Vector3(7f, 0f, 0f), converter.Convert(0.5f));
            AssertClose(new Vector3(17f, 0f, 0f), converter.Convert(1.5f));
        }

        #endregion

        private Transform NewTarget(Vector3 position)
        {
            var gameObject = new GameObject(nameof(VectorMathAdditionsTests));
            gameObject.transform.position = position;
            _created.Add(gameObject);

            return gameObject.transform;
        }

        // NUnit compares two vectors with Vector.Equals, which is exact float equality. Everything
        // expected above is the result of arithmetic, so the components are compared with a delta
        // instead — the exact-equality assertions are left for the cases that really are exact, such
        // as a collapse to zero.
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
    }
}
