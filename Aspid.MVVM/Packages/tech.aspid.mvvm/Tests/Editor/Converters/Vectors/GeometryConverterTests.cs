using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the rotation, vector and layout converters.
    /// </summary>
    [TestFixture]
    internal sealed class GeometryConverterTests
    {
        [Test]
        public void AngleToQuaternion_TurnsAroundTheChosenAxis() =>
            Assert.AreEqual(90f, new AngleToQuaternionConverter().Convert(90f).eulerAngles.z, 1e-3f);

        [Test]
        public void AngleToQuaternion_RoundTrips()
        {
            var converter = new AngleToQuaternionConverter(RotationAxis.Z, offset: 30f);

            Assert.AreEqual(45f, converter.ConvertBack(converter.Convert(45f)), 1e-2f);
        }

        [Test]
        public void EulerToQuaternion_RoundTrips()
        {
            var converter = new EulerToQuaternionConverter();
            var euler = new Vector3(0f, 45f, 0f);

            Assert.AreEqual(euler.y, converter.ConvertBack(converter.Convert(euler)).y, 1e-2f);
        }

        // Unity reports Euler angles in 0..360, so a needle a little past zero reads as 359 rather
        // than -1 — which makes a "below zero" test fail exactly when it matters.
        [Test]
        public void QuaternionToEuler_NormalizesToSigned180()
        {
            var rotation = Quaternion.Euler(0f, 0f, 350f);

            Assert.AreEqual(-10f, new QuaternionToEulerConverter().Convert(rotation).z, 1e-2f);
            Assert.AreEqual(350f, new QuaternionToEulerConverter(false).Convert(rotation).z, 1e-2f);
        }

        [Test]
        public void QuaternionOffset_RoundTrips()
        {
            var converter = new QuaternionOffsetConverter(new Vector3(0f, 90f, 0f));
            var rotation = Quaternion.Euler(0f, 30f, 0f);

            Assert.AreEqual(30f, converter.ConvertBack(converter.Convert(rotation)).eulerAngles.y, 1e-2f);
        }

        [TestCase(1f, 0f, 0f)]
        [TestCase(0f, 1f, 90f)]
        [TestCase(-1f, 0f, 180f)]
        public void DirectionToAngle_ReadsTheAngle(float x, float y, float expected) =>
            Assert.AreEqual(expected, new DirectionAngleConverter(0f).Convert(new Vector2(x, y)), 1e-2f);

        [Test]
        public void DirectionToAngle_ZeroLengthReadsAsTheOffsetAlone() =>
            Assert.AreEqual(15f, new DirectionAngleConverter(15f).Convert(Vector2.zero), 1e-4f);

        // The unit is a serialized setting the constructor now reaches, so the radians mode does not
        // have to go through the Inspector to be tested.
        [Test]
        public void DirectionToAngle_ReportsRadiansWhenAsked() =>
            Assert.AreEqual(
                Mathf.PI / 2f,
                new DirectionAngleConverter(0f, degrees: false).Convert(new Vector2(0f, 1f)),
                1e-4f);

        [Test]
        public void DirectionToAngle_MeasuresClockwiseWhenAsked() =>
            Assert.AreEqual(
                -90f,
                new DirectionAngleConverter(0f, clockwise: true).Convert(new Vector2(0f, 1f)),
                1e-2f);

        [Test]
        public void AngleToDirection_IsTheInverseOfDirectionToAngle()
        {
            var angle = new DirectionAngleConverter(0f).Convert(new Vector2(0f, 1f));
            var direction = new DirectionAngleConverter().ConvertBack(angle);

            Assert.AreEqual(0f, direction.x, 1e-4f);
            Assert.AreEqual(1f, direction.y, 1e-4f);
        }

        [TestCase(AngleRange.Zero360, 370f, 10f)]
        [TestCase(AngleRange.Zero360, -10f, 350f)]
        [TestCase(AngleRange.Signed180, 350f, -10f)]
        [TestCase(AngleRange.Signed180, 190f, -170f)]
        public void AngleWrap_FoldsIntoTheRange(AngleRange range, float value, float expected) =>
            Assert.AreEqual(expected, new AngleWrapConverter(range).Convert(value), 1e-3f);

        [Test]
        public void DegreesToRadians_RoundTrips()
        {
            var converter = new DegreesRadiansConverter();

            Assert.AreEqual(Mathf.PI, converter.Convert(180f), 1e-5f);
            Assert.AreEqual(180f, converter.ConvertBack(Mathf.PI), 1e-3f);
        }

        // LookRotation warns and returns identity on a zero vector; checking first keeps the console
        // clean when a target has not been picked yet.
        [Test]
        public void LookRotation_ZeroDirectionIsTheIdentityWithoutAWarning() =>
            Assert.AreEqual(Quaternion.identity, new LookRotationConverter().Convert(Vector3.zero));

        // An up vector cleared in the Inspector leaves LookRotation with no plane to level against.
        // The converter reports it and looks with world up, so the result is the ordinary one rather
        // than whatever Unity does with a degenerate pair.
        [Test]
        public void LookRotation_ZeroUp_ReportsItAndLooksWithWorldUp()
        {
            LogAssert.Expect(LogType.Error, new Regex("up vector is zero"));

            var rotation = new LookRotationConverter(Vector3.zero).Convert(Vector3.forward);

            Assert.AreEqual(0f, Quaternion.Angle(Quaternion.identity, rotation), 1e-2f);
        }

        [Test]
        public void LookRotation_FlattensWhenAsked()
        {
            var rotation = new LookRotationConverter(Vector3.up, flatten: true).Convert(new Vector3(0f, 5f, 1f));

            Assert.AreEqual(0f, rotation.eulerAngles.x, 1e-2f);
        }

        [Test]
        public void FloatToVector3_WritesTheChosenAxes()
        {
            Assert.AreEqual(new Vector3(3f, 3f, 3f), new FloatToVectorConverter().Convert(3f));
            Assert.AreEqual(
                new Vector3(1f, 3f, 1f),
                new FloatToVectorConverter(AxisMask.Y, Vector4.one).Convert(3f));
        }

        [Test]
        public void FloatToVector2_WritesTheChosenAxes() =>
            Assert.AreEqual(new Vector2(1f, 3f), ((IConverter<float, Vector2>)new FloatToVectorConverter(AxisMask.Y, Vector4.one)).Convert(3f));

        [TestCase(VectorComponent.X, 3f)]
        [TestCase(VectorComponent.Y, 4f)]
        [TestCase(VectorComponent.Magnitude, 5f)]
        [TestCase(VectorComponent.SqrMagnitude, 25f)]
        public void Vector3ToFloat_Measures(VectorComponent component, float expected) =>
            Assert.AreEqual(expected, new VectorToFloatConverter(component).Convert(new Vector3(3f, 4f, 0f)), 1e-4f);

        [Test]
        public void VectorArithmetic_Adds() =>
            Assert.AreEqual(
                new Vector3(2f, 3f, 4f),
                new VectorArithmeticConverter(VectorOperation.Add, new Vector4(1f, 1f, 1f, 0f))
                    .Convert(new Vector3(1f, 2f, 3f)));

        [Test]
        public void VectorArithmetic_Scales() =>
            Assert.AreEqual(
                new Vector3(2f, 6f, 12f),
                new VectorArithmeticConverter(VectorOperation.Scale, new Vector4(2f, 3f, 4f, 0f))
                    .Convert(new Vector3(1f, 2f, 3f)));

        // A zero axis leaves that axis alone rather than producing an infinity.
        [Test]
        public void VectorArithmetic_DivideByAZeroAxisLeavesItAlone() =>
            Assert.AreEqual(
                new Vector3(0.5f, 2f, 3f),
                new VectorArithmeticConverter(VectorOperation.Divide, new Vector4(2f, 0f, 0f, 0f))
                    .Convert(new Vector3(1f, 2f, 3f)));

        [Test]
        public void VectorArithmetic_UndeclaredOperation_ReportsItAndPassesTheVectorThrough()
        {
            LogAssert.Expect(LogType.Error, new Regex("VectorArithmeticConverter.*not a declared VectorOperation"));

            Assert.AreEqual(
                new Vector3(1f, 2f, 3f),
                new VectorArithmeticConverter((VectorOperation)99, new Vector4(1f, 1f, 1f, 0f))
                    .Convert(new Vector3(1f, 2f, 3f)));
        }

        [Test]
        public void VectorClampMagnitude_HoldsTheLength() =>
            Assert.AreEqual(
                1f,
                new VectorClampMagnitudeConverter(1f).Convert(new Vector3(3f, 4f, 0f)).magnitude,
                1e-4f);

        [Test]
        public void VectorClampMagnitude_RaisesToTheMinimum() =>
            Assert.AreEqual(
                2f,
                new VectorClampMagnitudeConverter(10f, 2f).Convert(new Vector3(0.3f, 0.4f, 0f)).magnitude,
                1e-4f);

        // The pair reads (max, min), so this instance has a floor of 2 above a ceiling of 1 and is
        // reported before the zero vector short-circuits the clamp.
        [Test]
        public void VectorClampMagnitude_ZeroStaysZero()
        {
            LogAssert.Expect(LogType.Error, new Regex("are not two ordered non-negative lengths"));

            Assert.AreEqual(Vector3.zero, new VectorClampMagnitudeConverter(1f, 2f).Convert(Vector3.zero));
        }

        [Test]
        public void VectorRound_SnapsToAGrid() =>
            Assert.AreEqual(
                new Vector3(0.5f, 1f, 1.5f),
                new VectorRoundConverter(RoundMode.Round, 0.5f).Convert(new Vector3(0.6f, 0.9f, 1.4f)));

        [Test]
        public void VectorNormalize_ZeroStaysZero() =>
            Assert.AreEqual(Vector3.zero, new VectorNormalizeConverter().Convert(Vector3.zero));

        [Test]
        public void VectorLerp_MovesBetweenTheStops()
        {
            var converter = new VectorLerpConverter(Vector3.zero, new Vector3(10f, 0f, 0f));

            Assert.AreEqual(new Vector3(5f, 0f, 0f), converter.Convert(0.5f));
        }

        [Test]
        public void Vector3ToVector3Int_RoundTrips()
        {
            var converter = (ITwoWayConverter<Vector3, Vector3Int>)new VectorToVectorIntConverter();

            Assert.AreEqual(new Vector3Int(1, 2, 3), converter.Convert(new Vector3(1.4f, 2.4f, 3.4f)));
            Assert.AreEqual(new Vector3(1f, 2f, 3f), converter.ConvertBack(new Vector3Int(1, 2, 3)));
        }

        [Test]
        public void Vector2ToVector2Int_Floors() =>
            Assert.AreEqual(
                new Vector2Int(1, 2),
                new VectorToVectorIntConverter(RoundMode.Floor).Convert(new Vector2(1.9f, 2.9f)));

        // The mode is a serialized field, so an undeclared value survives a reordered enum. Rounding
        // to nearest is the mode a new converter starts in, and the inputs separate it from the other
        // three: 1.4 rounds down where Ceil would raise it, 2.6 rounds up where Floor and Truncate
        // would drop it. No other mode answers this pair.
        [Test]
        public void Vector2ToVector2Int_UndeclaredMode_ReportsItAndRoundsToNearest()
        {
            LogAssert.Expect(LogType.Error, new Regex("VectorToVectorIntConverter.*not a declared RoundMode"));

            Assert.AreEqual(
                new Vector2Int(1, 3),
                new VectorToVectorIntConverter((RoundMode)42).Convert(new Vector2(1.4f, 2.6f)));
        }

        // The third axis is negative because that is where Floor and Truncate part company, so the
        // wider overload rules out all three of the others rather than two of them.
        [Test]
        public void Vector3ToVector3Int_UndeclaredMode_ReportsItAndRoundsToNearest()
        {
            LogAssert.Expect(LogType.Error, new Regex("VectorToVectorIntConverter.*not a declared RoundMode"));

            Assert.AreEqual(
                new Vector3Int(1, 3, -1),
                ((IConverter<Vector3, Vector3Int>)new VectorToVectorIntConverter((RoundMode)42))
                    .Convert(new Vector3(1.4f, 2.6f, -1.4f)));
        }

        [Test]
        public void IntToRectOffset_WritesTheChosenSides()
        {
            var padding = new IntToRectOffsetConverter(RectSides.Horizontal).Convert(8);

            Assert.AreEqual(8, padding.left);
            Assert.AreEqual(8, padding.right);
            Assert.AreEqual(0, padding.top);
        }

        // RectOffset is a class, so a new one per push would allocate once per notification.
        [Test]
        public void IntToRectOffset_ReusesOneInstance()
        {
            var converter = new IntToRectOffsetConverter();

            Assert.AreSame(converter.Convert(4), converter.Convert(8));
            Assert.AreEqual(8, converter.Convert(8).left);
        }

        [Test]
        public void RectOffsetScale_ScalesTheChosenSides()
        {
            var scaled = new RectOffsetScaleConverter(2f, RectSides.Vertical)
                .Convert(new RectOffset(3, 3, 3, 3));

            Assert.AreEqual(3, scaled.left);
            Assert.AreEqual(6, scaled.top);
            Assert.AreEqual(6, scaled.bottom);
        }

        // A null padding is the reset a binder pushes, and reading it must not allocate a throwaway
        // RectOffset to take four zeroes off. Zero scaled by anything is still zero.
        [Test]
        public void RectOffsetScale_NullPadding_ReadsAsNoPadding()
        {
            var scaled = new RectOffsetScaleConverter(2f).Convert(null);

            Assert.AreEqual(0, scaled.left);
            Assert.AreEqual(0, scaled.right);
            Assert.AreEqual(0, scaled.top);
            Assert.AreEqual(0, scaled.bottom);
        }

        // The rounding is a serialized setting the constructor now reaches, so a fractional scale
        // does not have to go through the Inspector to be tested. 3 * 1.5 is 4.5, which Ceil takes
        // to 5 and Floor to 4.
        [TestCase(RoundMode.Ceil, 5)]
        [TestCase(RoundMode.Floor, 4)]
        public void RectOffsetScale_Rounding_DecidesWhereTheFractionGoes(RoundMode rounding, int expected) =>
            Assert.AreEqual(
                expected,
                new RectOffsetScaleConverter(1.5f, RectSides.All, rounding)
                    .Convert(new RectOffset(3, 3, 3, 3)).left);

        // A plain (int) cast of an out-of-range float is undefined in C#, so the scaled side is held
        // at the bounds of what a padding can carry instead of wrapping to a negative number.
        [Test]
        public void RectOffsetScale_ScaleBeyondWhatAnIntHolds_SaturatesRatherThanWraps() =>
            Assert.AreEqual(
                int.MaxValue,
                new RectOffsetScaleConverter(1e12f).Convert(new RectOffset(3, 3, 3, 3)).left);

        [Test]
        public void Vector4ToRectOffset_ReadsTheFourNumbersInOrder()
        {
            var padding = new Vector4ToRectOffsetConverter().Convert(new Vector4(1f, 2f, 3f, 4f));

            Assert.AreEqual(1, padding.left);
            Assert.AreEqual(2, padding.right);
            Assert.AreEqual(3, padding.top);
            Assert.AreEqual(4, padding.bottom);
        }
    }
}
