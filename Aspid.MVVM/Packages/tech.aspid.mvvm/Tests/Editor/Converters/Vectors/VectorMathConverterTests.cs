using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the vector math converters — the <see cref="VectorToFloatConverter"/> and its
    /// <see cref="VectorComponent.Dot"/> reading, <see cref="VectorArithmeticConverter"/>, the
    /// ordered bounds of <see cref="VectorClampMagnitudeConverter"/>, the component clamps, the
    /// rounding, the normalizing, <see cref="VectorDistanceConverter"/> and the curve on
    /// <see cref="VectorLerpConverter"/>.
    /// </summary>
    /// <remarks>
    /// The class of mistake guarded here is a converter that stops honouring its own contract on input an
    /// Inspector can legitimately produce: a min/max pair typed the wrong way round, a negative ceiling,
    /// a negative grid step, a direction that was never normalized, a target destroyed mid-session.
    /// <para>
    /// Expectations come from the arithmetic the source performs, and three contradict what a reader would
    /// assume: <c>Mathf.Round</c> is banker's rounding, Unity's <c>normalized</c> has a length floor of
    /// 1e-5, and <see cref="VectorLerpConverter"/> clamps the amount the curve produced rather than the
    /// incoming value. Each such test says so where it stands.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class VectorMathConverterTests
    {
        private static readonly Vector3 _from = new(2f, 0f, 0f);
        private static readonly Vector3 _to = new(12f, 0f, 0f);

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

        #region VectorToFloatConverter and the Dot component

        [TestCase(VectorComponent.X, 3f)]
        [TestCase(VectorComponent.Y, 4f)]
        [TestCase(VectorComponent.Magnitude, 5f)]
        [TestCase(VectorComponent.SqrMagnitude, 25f)]
        // Dot reached through the component-only ctor leaves the direction at its default, which is
        // up rather than right — so the reading is the y, and a converter that defaulted to right
        // would answer 3 here.
        [TestCase(VectorComponent.Dot, 4f)]
        public void VectorToFloat_Vector2_Measures(VectorComponent component, float expected) =>
            Assert.AreEqual(
                expected,
                AsVector2(new VectorToFloatConverter(component)).Convert(new Vector2(3f, 4f)),
                1e-4f);

        [Test]
        public void VectorToFloat_Vector2_DefaultConstructed_MeasuresLength() =>
            Assert.AreEqual(
                5f,
                AsVector2(new VectorToFloatConverter()).Convert(new Vector2(3f, 4f)),
                1e-4f);

        // The direction is used raw, not normalized: a direction of unit length reads as the signed
        // distance along it, one twice as long doubles that reading, and an unset one reads zero for
        // every input. The negative row is the one that proves the reading is signed rather than a
        // distance.
        [TestCase(1f, 0f, 3f)]
        [TestCase(0f, 1f, 4f)]
        [TestCase(-1f, 0f, -3f)]
        [TestCase(0f, 2f, 8f)]
        [TestCase(0f, 0f, 0f)]
        public void VectorToFloat_Vector2Dot_IsTheRawProductWithTheAuthoredDirection(
            float x,
            float y,
            float expected) =>
            Assert.AreEqual(
                expected,
                AsVector2(new VectorToFloatConverter(new Vector4(x, y, 0f, 0f))).Convert(new Vector2(3f, 4f)),
                1e-4f);

        // Passing a direction is the only way to select Dot without also naming the component, so the
        // ctor has to set the component itself — otherwise the converter still measures length and
        // the authored direction is never read.
        [Test]
        public void VectorToFloat_DirectionCtor_SelectsDotWithoutBeingTold() =>
            Assert.AreEqual(
                3f,
                AsVector2(new VectorToFloatConverter(new Vector4(1f, 0f, 0f, 0f))).Convert(new Vector2(3f, 4f)),
                1e-4f);

        [TestCase(0f, 1f, 0f, 4f)]
        [TestCase(0f, 0f, 2f, 10f)]
        [TestCase(-1f, 0f, 0f, -3f)]
        public void VectorToFloat_Dot_IsTheRawProductWithTheAuthoredDirection(
            float x,
            float y,
            float z,
            float expected) =>
            Assert.AreEqual(
                expected,
                new VectorToFloatConverter(new Vector4(x, y, z, 0f)).Convert(new Vector3(3f, 4f, 5f)),
                1e-4f);

        [Test]
        public void VectorToFloat_Dot_DefaultDirectionIsUp() =>
            Assert.AreEqual(
                4f,
                new VectorToFloatConverter(VectorComponent.Dot).Convert(new Vector3(3f, 4f, 5f)),
                1e-4f);

        [Test]
        public void VectorToFloat_Vector4_ReadsTheFourthComponent() =>
            Assert.AreEqual(
                6f,
                ((IConverter<Vector4, float>)new VectorToFloatConverter(VectorComponent.W))
                    .Convert(new Vector4(3f, 4f, 5f, 6f)),
                1e-4f);

        // Declaration order is the number Unity stores in the scene, so Dot had to be appended rather
        // than filed next to the axes it belongs with, and W after it when the converter grew a
        // four-component width. Filing either in place would repoint every field already authored.
        [TestCase(VectorComponent.X, 0)]
        [TestCase(VectorComponent.Y, 1)]
        [TestCase(VectorComponent.Z, 2)]
        [TestCase(VectorComponent.Magnitude, 3)]
        [TestCase(VectorComponent.SqrMagnitude, 4)]
        [TestCase(VectorComponent.Dot, 5)]
        [TestCase(VectorComponent.W, 6)]
        public void VectorComponent_StoredValue_KeepsDotAndWAppendedLast(VectorComponent component, int stored) =>
            Assert.AreEqual(stored, (int)component);

        // A component the bound width does not carry is a misconfiguration, not a measurement: it is
        // reported on every push and reads as zero rather than as a length.
        [Test]
        public void VectorToFloat_ComponentTheWidthLacks_IsReportedAndReadsZero()
        {
            LogAssert.Expect(LogType.Error, new Regex("VectorToFloatConverter.*not one a Vector2 carries"));

            Assert.AreEqual(
                0f,
                AsVector2(new VectorToFloatConverter(VectorComponent.Z)).Convert(new Vector2(3f, 4f)),
                1e-6f);
        }

        // The setting is a serialized field rather than an argument, so an undeclared value — corrupted
        // YAML or a stray cast — is reported on every push and zero is read instead, rather than
        // throwing the binding down.
        [Test]
        public void VectorToFloat_UndeclaredComponent_ReportsItAndReadsZero()
        {
            LogAssert.Expect(LogType.Error, new Regex("VectorToFloatConverter.*not a declared VectorComponent"));

            Assert.AreEqual(
                0f,
                new VectorToFloatConverter((VectorComponent)99).Convert(new Vector3(3f, 4f, 0f)),
                1e-6f);
        }

        private static IConverter<Vector2, float> AsVector2(VectorToFloatConverter converter) => converter;

        #endregion

        #region VectorArithmeticConverter

        [TestCase(VectorOperation.Add, 11f, 22f)]
        [TestCase(VectorOperation.Subtract, -9f, -18f)]
        [TestCase(VectorOperation.Scale, 10f, 40f)]
        [TestCase(VectorOperation.Divide, 0.1f, 0.1f)]
        public void Vector2Arithmetic_AppliesTheOperation(VectorOperation operation, float x, float y) =>
            AssertClose(
                new Vector2(x, y),
                AsWidth<Vector2>(new VectorArithmeticConverter(operation, new Vector4(10f, 20f, 0f, 0f)))
                    .Convert(new Vector2(1f, 2f)));

        // What a freshly added [SerializeReference] entry does before anything is typed into it: Add
        // with a zero operand, which has to be an identity rather than a collapse to zero.
        [Test]
        public void Vector2Arithmetic_DefaultConstructed_LeavesTheVectorAlone() =>
            AssertClose(
                new Vector2(1f, 2f),
                AsWidth<Vector2>(new VectorArithmeticConverter()).Convert(new Vector2(1f, 2f)));

        // A zero axis in the operand leaves that axis alone instead of handing a binder an infinity.
        // The x is in the case to prove the division still happens on the axis that can take one.
        [Test]
        public void Vector2Arithmetic_DivideByAZeroAxis_LeavesThatAxisAlone() =>
            AssertClose(
                new Vector2(0.5f, 2f),
                AsWidth<Vector2>(new VectorArithmeticConverter(VectorOperation.Divide, new Vector4(2f, 0f, 0f, 0f)))
                    .Convert(new Vector2(1f, 2f)));

        // The zero guard is `== 0f`, so an ordinary negative divisor is not caught by it and the sign
        // travels through the division as arithmetic says it should.
        [Test]
        public void Vector2Arithmetic_DivideByANegativeAxis_KeepsTheSignChange() =>
            AssertClose(
                new Vector2(-0.5f, -0.5f),
                AsWidth<Vector2>(new VectorArithmeticConverter(VectorOperation.Divide, new Vector4(-2f, -4f, 0f, 0f)))
                    .Convert(new Vector2(1f, 2f)));

        // The bounce a wall gives: the part along the normal is inverted, the part along the wall is
        // kept. A Reflect implemented as a plain negation would answer (-1, 1) here.
        [Test]
        public void Vector2Arithmetic_Reflect_BouncesOffTheOperandAsANormal() =>
            AssertClose(
                new Vector2(1f, 1f),
                AsWidth<Vector2>(new VectorArithmeticConverter(VectorOperation.Reflect, new Vector4(0f, 1f, 0f, 0f)))
                    .Convert(new Vector2(1f, -1f)));

        // Vector2.Reflect does not normalize its normal, and neither does the converter, so a normal
        // authored twice as long as unit quadruples the part it reflects. Typing (0, 2) into the
        // operand field does not mean "up" — it means "up, four times over".
        [Test]
        public void Vector2Arithmetic_Reflect_DoesNotNormalizeTheOperand() =>
            AssertClose(
                new Vector2(1f, 7f),
                AsWidth<Vector2>(new VectorArithmeticConverter(VectorOperation.Reflect, new Vector4(0f, 2f, 0f, 0f)))
                    .Convert(new Vector2(1f, -1f)));

        // An unset operand is a zero normal, whose reflection factor is zero, so the value walks
        // through unchanged — the opposite of what Scale does with the same unset operand.
        [Test]
        public void Vector2Arithmetic_Reflect_ZeroOperand_ReturnsTheInput() =>
            AssertClose(
                new Vector2(1f, -1f),
                AsWidth<Vector2>(new VectorArithmeticConverter(VectorOperation.Reflect, Vector4.zero))
                    .Convert(new Vector2(1f, -1f)));

        [Test]
        public void Vector2Arithmetic_Scale_ZeroOperand_CollapsesTheVector() =>
            AssertClose(
                Vector2.zero,
                AsWidth<Vector2>(new VectorArithmeticConverter(VectorOperation.Scale, Vector4.zero))
                    .Convert(new Vector2(1f, -1f)));

        // The setting is a serialized field rather than an argument, so an undeclared value — corrupted
        // YAML or a stray cast — is reported on every push and the operand is left out of it, rather
        // than throwing the binding down.
        [Test]
        public void Vector2Arithmetic_UndeclaredOperation_ReportsItAndPassesTheVectorThrough()
        {
            LogAssert.Expect(LogType.Error, new Regex("VectorArithmeticConverter.*not a declared VectorOperation"));

            AssertClose(
                new Vector2(1f, -1f),
                AsWidth<Vector2>(new VectorArithmeticConverter((VectorOperation)99, Vector4.one))
                    .Convert(new Vector2(1f, -1f)));
        }

        // Reflect is the one operation Unity has no Vector4 method for, so the four-component path is
        // the only hand-written arithmetic in the converter. The normal is W, which puts the whole
        // reflection on the component the narrower widths do not carry: 4 flips to -4 and nothing
        // else moves. A reflection that ignored W would answer the input unchanged.
        [Test]
        public void Vector4Arithmetic_Reflect_TurnsTheFourthComponentAround() =>
            AssertClose(
                new Vector4(1f, 2f, 3f, -4f),
                AsWidth<Vector4>(new VectorArithmeticConverter(VectorOperation.Reflect, new Vector4(0f, 0f, 0f, 1f)))
                    .Convert(new Vector4(1f, 2f, 3f, 4f)));

        #endregion

        #region ClampMagnitude — reversed bounds and a negative ceiling

        // The shared scale, which all three widths route through. Note the argument order is
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

        #endregion

        #region Normalize

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

        // Undocumented, and inherited from Unity rather than written here: `normalized` has a length
        // floor of 1e-5 (Vector2.kEpsilon / Vector3.kEpsilon), below which it answers zero instead of
        // a direction. A stick a hair off center therefore aims nowhere rather than at full throw —
        // which is not what "or zero for a zero-length input" in the XML docs suggests.
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
            AssertClose(new Vector3(7f, 0f, 0f), new VectorLerpConverter(_from, _to).Convert(0.5f));

        // Both spellings of "no curve" have to mean the same thing. An unassigned AnimationCurve
        // deserializes as an empty one rather than as null, and Evaluate on an empty curve answers 0
        // — which would pin every conversion at _from. The length guard is what stops that, and this
        // is the case that fails without it.
        [Test]
        public void VectorLerp_EmptyCurve_IsTreatedAsNoCurve() =>
            AssertClose(
                new Vector3(9f, 0f, 0f),
                new VectorLerpConverter(_from, _to, new AnimationCurve()).Convert(0.7f));

        // The curve shapes the amount before the move, so one that runs 1 down to 0 reverses the
        // travel: a quarter of the way in reads three quarters of the way along.
        [Test]
        public void VectorLerp_Curve_ShapesTheAmountBeforeTheMove() =>
            AssertClose(
                new Vector3(9.5f, 0f, 0f),
                new VectorLerpConverter(_from, _to, AnimationCurve.Linear(0f, 1f, 1f, 0f)).Convert(0.25f),
                1e-3f);

        // A constant curve proves the incoming amount is not consulted at all once a curve is
        // present — both conversions land on the same point.
        [Test]
        public void VectorLerp_ConstantCurve_IgnoresTheIncomingAmount()
        {
            var converter = new VectorLerpConverter(_from, _to, AnimationCurve.Constant(0f, 1f, 0.25f));

            AssertClose(new Vector3(4.5f, 0f, 0f), converter.Convert(0f));
            AssertClose(new Vector3(4.5f, 0f, 0f), converter.Convert(0.9f));
        }

        // The clamp belongs to Vector3.Lerp and applies to the amount the CURVE produced, not to the
        // amount that came in — the tooltip's "hold the incoming amount inside 0..1" describes the
        // wrong end. A curve that overshoots is held at the far end; one that undershoots, at the
        // near end. Unclamped these would read 22 and -8.
        [Test]
        public void VectorLerp_CurveAboveOne_IsHeldAtTheFarEnd() =>
            AssertClose(_to, new VectorLerpConverter(_from, _to, AnimationCurve.Constant(0f, 1f, 2f)).Convert(0.5f));

        [Test]
        public void VectorLerp_CurveBelowZero_IsHeldAtTheNearEnd() =>
            AssertClose(_from, new VectorLerpConverter(_from, _to, AnimationCurve.Constant(0f, 1f, -1f)).Convert(0.5f));

        [Test]
        public void VectorLerp_AmountOutsideZeroToOne_IsHeldAtTheEnds()
        {
            var converter = new VectorLerpConverter(_from, _to);

            AssertClose(_to, converter.Convert(1.5f));
            AssertClose(_from, converter.Convert(-0.5f));
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

        // Both clamp families report a half-authored pair on every push, so the fixture has to say so
        // for each conversion it makes with one — LogAssert fails the test on any error it did not ask
        // for, and on any expectation nothing produced.
        private static readonly Regex _invalidLengthBounds =
            new("are not two ordered non-negative lengths");

        private static readonly Regex _invertedAxisBounds =
            new("on at least one axis");

        private Transform NewTarget(Vector3 position)
        {
            var gameObject = new GameObject(nameof(VectorMathConverterTests));
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

        // A 2D scene measures the same distance without the depth the target carries.
        [Test]
        public void Distance_Vector2_MeasuresWithoutTheDepth() =>
            Assert.AreEqual(
                5f,
                ((IConverter<Vector2, float>)new VectorDistanceConverter(new Vector3(0f, 0f, 99f)))
                    .Convert(new Vector2(3f, 4f)),
                1e-4f);

        [Test]
        public void VectorLerp_Vector2_ReadsTheFirstTwoComponents() =>
            Assert.AreEqual(
                new Vector2(5f, 5f),
                ((IConverter<float, Vector2>)new VectorLerpConverter(Vector4.zero, new Vector4(10f, 10f, 10f, 10f)))
                    .Convert(0.5f));

        [Test]
        public void VectorLerp_Vector4_ReadsTheFourth() =>
            Assert.AreEqual(
                new Vector4(5f, 5f, 5f, 5f),
                ((IConverter<float, Vector4>)new VectorLerpConverter(Vector4.zero, new Vector4(10f, 10f, 10f, 10f)))
                    .Convert(0.5f));

        private static void AssertClose(Vector4 expected, Vector4 actual, float delta = 1e-4f)
        {
            Assert.AreEqual(expected.x, actual.x, delta, $"x of {actual}, expected {expected}");
            Assert.AreEqual(expected.y, actual.y, delta, $"y of {actual}, expected {expected}");
            Assert.AreEqual(expected.z, actual.z, delta, $"z of {actual}, expected {expected}");
            Assert.AreEqual(expected.w, actual.w, delta, $"w of {actual}, expected {expected}");
        }

        // Each vector converter serves Vector2, Vector3 and Vector4, and only the Vector3 width is a
        // public method — the other two are explicit interface implementations. Calling Convert on the
        // class with a Vector2 would compile and quietly widen it to the Vector3 width, so a test that
        // means one of the narrow widths has to ask for its interface, which is what this does.
        private static IConverter<T, T> AsWidth<T>(IConverter<T, T> converter) => converter;
    }
}
