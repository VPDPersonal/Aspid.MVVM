using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="QuaternionToAngleConverter"/> — the signed/unsigned Euler reading, the
    /// <see cref="RotationAxis.Custom"/> branch, and why it is not the reverse of
    /// <see cref="AngleToQuaternionConverter"/>.
    /// </summary>
    [TestFixture]
    internal sealed class QuaternionToAngleConverterTests
    {
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
        public void Convert_ReadsTheChosenAxisAsSigned180(
            RotationAxis axis,
            float x,
            float y,
            float z,
            float expected) =>
            Assert.AreEqual(expected, new QuaternionToAngleConverter(axis).Convert(Quaternion.Euler(x, y, z)), 1e-2f);

        [TestCase(0f, 0f, 350f, 350f)]
        [TestCase(0f, 0f, -10f, 350f)]
        [TestCase(0f, 0f, 10f, 10f)]
        public void Convert_Unsigned_ReportsZeroToThreeSixty(float x, float y, float z, float expected) =>
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
        public void Convert_Custom_ReadsAroundTheDefaultUpAxis(float angle, bool signed, float expected) =>
            Assert.AreEqual(
                expected,
                new QuaternionToAngleConverter(RotationAxis.Custom, signed)
                    .Convert(Quaternion.AngleAxis(angle, Vector3.up)),
                1e-2f);

        // A rotation about Z carries no turn about up, and the converter reports its full 90° anyway
        // — nothing is projected onto the custom axis.
        [Test]
        public void Convert_Custom_PerpendicularRotation_ReportsTheWholeTurnNotZero() =>
            Assert.AreEqual(
                90f,
                new QuaternionToAngleConverter(RotationAxis.Custom).Convert(Quaternion.Euler(0f, 0f, 90f)),
                1e-2f);

        // This is not AngleToQuaternionConverter's ConvertBack under another name: it carries no
        // offset to undo, so the two disagree by exactly the offset — the trap for anyone who puts
        // this converter on the OneWay leg of a pair.
        [Test]
        public void Convert_DoesNotUndoAnAngleToQuaternionOffset()
        {
            var source = new AngleToQuaternionConverter(RotationAxis.Z, offset: 30f);
            var rotation = source.Convert(0f);

            Assert.AreEqual(0f, source.ConvertBack(rotation), 1e-2f);
            Assert.AreEqual(30f, new QuaternionToAngleConverter().Convert(rotation), 1e-2f);
        }

        // AngleToQuaternionConverter reads the Euler axes off eulerAngles, which Unity reports in
        // 0..360, so a clockwise 30° comes back as -(330) = -330 rather than the 30 that went in.
        // This converter folds to ±180 first and answers the -30 the rotation actually is.
        [Test]
        public void Convert_ReadsClockwiseWhereAngleToQuaternionOvershootsByAFullTurn()
        {
            var source = new AngleToQuaternionConverter(RotationAxis.Z, clockwise: true);
            var rotation = source.Convert(30f);

            Assert.AreEqual(-330f, source.ConvertBack(rotation), 1e-2f);
            Assert.AreEqual(-30f, new QuaternionToAngleConverter().Convert(rotation), 1e-2f);
        }

        // Passing an axis is the only way to select Custom without also naming it, so the ctor has to
        // set the axis itself — otherwise the converter still reads Z and the authored axis is never
        // consulted. A 90° turn about X reads 90 through Custom and 0 through the default Z.
        [Test]
        public void Convert_AxisCtor_SelectsCustomWithoutBeingTold() =>
            Assert.AreEqual(
                90f,
                new QuaternionToAngleConverter(Vector3.right).Convert(Quaternion.AngleAxis(90f, Vector3.right)),
                1e-2f);

        // An axis cleared in the Inspector has no direction to read a turn around. Left silent it
        // would write the ViewModel a constant zero that looks like a real reading, so it is reported
        // on every push and the documented fallback is that zero.
        [Test]
        public void Convert_Custom_ZeroAxis_ReportsItAndReadsZero()
        {
            LogAssert.Expect(LogType.Error, new Regex("custom axis is zero"));

            Assert.AreEqual(
                0f,
                new QuaternionToAngleConverter(Vector3.zero).Convert(Quaternion.AngleAxis(90f, Vector3.up)),
                1e-4f);
        }

        // The setting is a serialized field rather than an argument, so an undeclared value is a broken
        // converter: it is reported on every push and the angle reads zero, the same answer a cleared
        // custom axis gives.
        [Test]
        public void Convert_UndeclaredAxis_ReportsItAndReadsZero()
        {
            LogAssert.Expect(LogType.Error, new Regex("QuaternionToAngleConverter.*not a declared"));

            Assert.AreEqual(
                0f,
                new QuaternionToAngleConverter((RotationAxis)99).Convert(Quaternion.Euler(0f, 30f, 0f)),
                1e-4f);
        }
        [Test]
        public void QuaternionToAngle_Double_ReadsTheSameAngle() =>
            Assert.AreEqual(
                (double)new QuaternionToAngleConverter().Convert(Quaternion.Euler(0f, 90f, 0f)),
                ((IConverter<Quaternion, double>)new QuaternionToAngleConverter())
                    .Convert(Quaternion.Euler(0f, 90f, 0f)),
                1e-3d);

    }
}
