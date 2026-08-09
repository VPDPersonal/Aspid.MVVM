using UnityEngine;
using NUnit.Framework;
using To2 = Aspid.MVVM.StarterKit.Vector3ToVector2Converter.Values;
using To3 = Aspid.MVVM.StarterKit.Vector2ToVector3Converter.Values;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the dimension-changing pair, <see cref="Vector3ToVector2Converter"/> and
    /// <see cref="Vector2ToVector3Converter"/>.
    /// </summary>
    /// <remarks>
    /// The pair does not round-trip: the 3→2 direction offers all six orderings, the 2→3 direction
    /// only three. The member names of <see cref="Vector2ToVector3Converter.Values"/> also name the
    /// <i>destination</i> axes rather than the source components, which is why every mapping is
    /// spelled out below.
    /// </remarks>
    [TestFixture]
    internal sealed class VectorDimensionConverterTests
    {
        [TestCase(To2.XY, 1f, 2f)]
        [TestCase(To2.XZ, 1f, 3f)]
        [TestCase(To2.YX, 2f, 1f)]
        [TestCase(To2.YZ, 2f, 3f)]
        [TestCase(To2.ZX, 3f, 1f)]
        [TestCase(To2.ZY, 3f, 2f)]
        public void Vector3ToVector2_Convert_SelectsComponents(To2 values, float x, float y) =>
            Assert.AreEqual(
                new Vector2(x, y),
                new Vector3ToVector2Converter(values).Convert(new Vector3(1f, 2f, 3f)));

        [Test]
        public void Vector3ToVector2_DefaultConstructed_TakesXY() =>
            Assert.AreEqual(
                new Vector2(1f, 2f),
                new Vector3ToVector2Converter().Convert(new Vector3(1f, 2f, 3f)));

        // The constant lands in the axis the name omits: XY puts it in z, XZ in y, YZ in x.
        [TestCase(To3.XY, 1f, 2f, 9f)]
        [TestCase(To3.XZ, 1f, 9f, 2f)]
        [TestCase(To3.YZ, 9f, 1f, 2f)]
        public void Vector2ToVector3_Convert_PlacesTheConstantInTheMissingAxis(
            To3 values,
            float x,
            float y,
            float z) =>
            Assert.AreEqual(
                new Vector3(x, y, z),
                new Vector2ToVector3Converter(values, thirdValue: 9f).Convert(new Vector2(1f, 2f)));

        [Test]
        public void Vector2ToVector3_DefaultThirdValue_IsZero() =>
            Assert.AreEqual(
                new Vector3(1f, 2f, 0f),
                new Vector2ToVector3Converter(To3.XY).Convert(new Vector2(1f, 2f)));

        [Test]
        public void Vector2ToVector3_DefaultConstructed_TakesXY() =>
            Assert.AreEqual(
                new Vector3(1f, 2f, 0f),
                new Vector2ToVector3Converter().Convert(new Vector2(1f, 2f)));
    }
}
