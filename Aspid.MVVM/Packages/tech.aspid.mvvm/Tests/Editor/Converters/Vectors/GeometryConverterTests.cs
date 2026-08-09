using UnityEngine;
using NUnit.Framework;

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
        public void QuaternionToEuler_NormalisesToSigned180()
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
            Assert.AreEqual(expected, new DirectionToAngleConverter(0f).Convert(new Vector2(x, y)), 1e-2f);

        [Test]
        public void DirectionToAngle_ZeroLengthReadsAsTheOffsetAlone() =>
            Assert.AreEqual(15f, new DirectionToAngleConverter(15f).Convert(Vector2.zero), 1e-4f);

        [Test]
        public void AngleToDirection_IsTheInverseOfDirectionToAngle()
        {
            var angle = new DirectionToAngleConverter(0f).Convert(new Vector2(0f, 1f));
            var direction = new AngleToDirectionConverter().Convert(angle);

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
            var converter = new DegreesToRadiansConverter();

            Assert.AreEqual(Mathf.PI, converter.Convert(180f), 1e-5f);
            Assert.AreEqual(180f, converter.ConvertBack(Mathf.PI), 1e-3f);
        }

        // LookRotation warns and returns identity on a zero vector; checking first keeps the console
        // clean when a target has not been picked yet.
        [Test]
        public void LookRotation_ZeroDirectionIsTheIdentityWithoutAWarning() =>
            Assert.AreEqual(Quaternion.identity, new LookRotationConverter().Convert(Vector3.zero));

        [Test]
        public void LookRotation_FlattensWhenAsked()
        {
            var rotation = new LookRotationConverter(Vector3.up, flatten: true).Convert(new Vector3(0f, 5f, 1f));

            Assert.AreEqual(0f, rotation.eulerAngles.x, 1e-2f);
        }

        [Test]
        public void FloatToVector3_WritesTheChosenAxes()
        {
            Assert.AreEqual(new Vector3(3f, 3f, 3f), new FloatToVector3Converter().Convert(3f));
            Assert.AreEqual(
                new Vector3(1f, 3f, 1f),
                new FloatToVector3Converter(AxisMask.Y, Vector3.one).Convert(3f));
        }

        [Test]
        public void FloatToVector2_WritesTheChosenAxes() =>
            Assert.AreEqual(new Vector2(1f, 3f), new FloatToVector2Converter(AxisMask.Y, Vector2.one).Convert(3f));

        [TestCase(VectorComponent.X, 3f)]
        [TestCase(VectorComponent.Y, 4f)]
        [TestCase(VectorComponent.Magnitude, 5f)]
        [TestCase(VectorComponent.SqrMagnitude, 25f)]
        public void Vector3ToFloat_Measures(VectorComponent component, float expected) =>
            Assert.AreEqual(expected, new Vector3ToFloatConverter(component).Convert(new Vector3(3f, 4f, 0f)), 1e-4f);

        [Test]
        public void VectorArithmetic_Adds() =>
            Assert.AreEqual(
                new Vector3(2f, 3f, 4f),
                new Vector3ArithmeticConverter(VectorOperation.Add, Vector3.one).Convert(new Vector3(1f, 2f, 3f)));

        [Test]
        public void VectorArithmetic_Scales() =>
            Assert.AreEqual(
                new Vector3(2f, 6f, 12f),
                new Vector3ArithmeticConverter(VectorOperation.Scale, new Vector3(2f, 3f, 4f))
                    .Convert(new Vector3(1f, 2f, 3f)));

        // A zero axis leaves that axis alone rather than producing an infinity.
        [Test]
        public void VectorArithmetic_DivideByAZeroAxisLeavesItAlone() =>
            Assert.AreEqual(
                new Vector3(0.5f, 2f, 3f),
                new Vector3ArithmeticConverter(VectorOperation.Divide, new Vector3(2f, 0f, 0f))
                    .Convert(new Vector3(1f, 2f, 3f)));

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

        [Test]
        public void VectorClampMagnitude_ZeroStaysZero() =>
            Assert.AreEqual(Vector3.zero, new VectorClampMagnitudeConverter(1f, 2f).Convert(Vector3.zero));

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
            var converter = new Vector3ToVector3IntConverter();

            Assert.AreEqual(new Vector3Int(1, 2, 3), converter.Convert(new Vector3(1.4f, 2.4f, 3.4f)));
            Assert.AreEqual(new Vector3(1f, 2f, 3f), converter.ConvertBack(new Vector3Int(1, 2, 3)));
        }

        [Test]
        public void Vector2ToVector2Int_Floors() =>
            Assert.AreEqual(
                new Vector2Int(1, 2),
                new Vector2ToVector2IntConverter(RoundMode.Floor).Convert(new Vector2(1.9f, 2.9f)));

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
