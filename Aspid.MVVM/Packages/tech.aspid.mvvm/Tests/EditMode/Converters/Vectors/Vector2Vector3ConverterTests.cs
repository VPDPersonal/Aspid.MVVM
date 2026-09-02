using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using Mode = Aspid.MVVM.StarterKit.Vector2Vector3Converter.Mode;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="Vector2Vector3Converter"/>, which maps both ways off one mode.
    /// </summary>
    /// <remarks>
    /// The member names of <see cref="Vector2Vector3Converter.Mode"/> name the <i>destination</i>
    /// axes rather than the source components, which is why every mapping is spelled out below. The
    /// round trip keeps the two mapped axes and drops the constant one.
    /// </remarks>
    [TestFixture]
    public sealed class Vector2Vector3ConverterTests
    {
        [TestCase(Mode.XY, 1f, 2f)]
        [TestCase(Mode.XZ, 1f, 3f)]
        [TestCase(Mode.YX, 2f, 1f)]
        [TestCase(Mode.YZ, 2f, 3f)]
        [TestCase(Mode.ZX, 3f, 1f)]
        [TestCase(Mode.ZY, 3f, 2f)]
        public void Vector2ToVector3_ConvertBack_SelectsComponents(Mode mode, float x, float y) =>
            Assert.AreEqual(
                new Vector2(x, y),
                new Vector2Vector3Converter(mode).ConvertBack(new Vector3(1f, 2f, 3f)));

        [Test]
        public void Vector2ToVector3_ConvertBack_DefaultConstructed_TakesXY() =>
            Assert.AreEqual(
                new Vector2(1f, 2f),
                new Vector2Vector3Converter().ConvertBack(new Vector3(1f, 2f, 3f)));

        // The constant lands in the axis the name omits: XY puts it in z, XZ in y, YZ in x.
        [TestCase(Mode.XY, 1f, 2f, 9f)]
        [TestCase(Mode.XZ, 1f, 9f, 2f)]
        [TestCase(Mode.YZ, 9f, 1f, 2f)]
        public void Vector2ToVector3_Convert_PlacesTheConstantInTheMissingAxis(
            Mode mode,
            float x,
            float y,
            float z) =>
            Assert.AreEqual(
                new Vector3(x, y, z),
                new Vector2Vector3Converter(mode, thirdValue: 9f).Convert(new Vector2(1f, 2f)));

        // The remaining three modes swap the pair besides placing the constant, so they get their
        // own row rather than sharing the "places the constant" case above.
        [TestCase(Mode.YX, 2f, 1f, 9f)]
        [TestCase(Mode.ZX, 2f, 9f, 1f)]
        [TestCase(Mode.ZY, 9f, 2f, 1f)]
        public void Vector2ToVector3_Convert_SwappedModesAlsoPlaceTheConstant(
            Mode mode,
            float x,
            float y,
            float z) =>
            Assert.AreEqual(
                new Vector3(x, y, z),
                new Vector2Vector3Converter(mode, thirdValue: 9f).Convert(new Vector2(1f, 2f)));

        [Test]
        public void Vector2ToVector3_DefaultThirdValue_IsZero() =>
            Assert.AreEqual(
                new Vector3(1f, 2f, 0f),
                new Vector2Vector3Converter(Mode.XY).Convert(new Vector2(1f, 2f)));

        [Test]
        public void Vector2ToVector3_DefaultConstructed_TakesXY() =>
            Assert.AreEqual(
                new Vector3(1f, 2f, 0f),
                new Vector2Vector3Converter().Convert(new Vector2(1f, 2f)));

        // The setting is a serialized field rather than an argument, so an undeclared value is a
        // broken converter: reported on every push, with a zero vector rather than a guessed mapping.
        // The authored third value is 9 and the input is (1, 2), so falling back to XY would answer
        // (1, 2, 9) — none of the three numbers survives, which is what makes the zero readable.
        [Test]
        public void Vector2ToVector3_UndeclaredMode_ReportsItAndReturnsAZeroVector()
        {
            LogAssert.Expect(LogType.Error, new Regex("Vector2Vector3Converter.*not a declared Mode"));

            Assert.AreEqual(
                Vector3.zero,
                new Vector2Vector3Converter((Mode)99, thirdValue: 9f).Convert(new Vector2(1f, 2f)));
        }

        // The reverse mapping reads the same field, so it has the same hole and answers the same way.
        [Test]
        public void Vector2ToVector3_ConvertBack_UndeclaredMode_ReportsItAndReturnsAZeroVector()
        {
            LogAssert.Expect(LogType.Error, new Regex("Vector2Vector3Converter.*not a declared Mode"));

            Assert.AreEqual(
                Vector2.zero,
                new Vector2Vector3Converter((Mode)99).ConvertBack(new Vector3(1f, 2f, 3f)));
        }
    }
}
