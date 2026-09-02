using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="BoundsToRectConverter"/>, which flattens a <see cref="Bounds"/> onto
    /// one plane and anchors the rectangle at the box's min corner rather than its center.
    /// </summary>
    [TestFixture]
    public sealed class BoundsToRectConverterTests
    {
        // For XZ and YZ the box's z lands in the rectangle's y, because a Rect has no third axis to
        // keep it in. The position comes from Bounds.min, not from the center.
        [TestCase(BoundsPlane.XY, 9f, 18f, 2f, 4f)]
        [TestCase(BoundsPlane.XZ, 9f, 27f, 2f, 6f)]
        [TestCase(BoundsPlane.YZ, 18f, 27f, 4f, 6f)]
        public void Convert_AnchorsAtTheMinCornerOfTheChosenPlane(
            BoundsPlane plane,
            float x,
            float y,
            float width,
            float height) =>
            Assert.AreEqual(new Rect(x, y, width, height), new BoundsToRectConverter(plane).Convert(Box()));

        [Test]
        public void XY_FlattensOntoXY() =>
            Assert.AreEqual(new Rect(9f, 18f, 2f, 4f), new BoundsToRectConverter(BoundsPlane.XY).Convert(Box()));

        // Anchoring at the corner is exactly what makes the two centers agree; a rectangle placed at
        // Bounds.center would sit half a box off, at (11, 22).
        [Test]
        public void Center_MatchesTheBoxCenterOnThePlane() =>
            Assert.AreEqual(
                new Vector2(10f, 20f),
                new BoundsToRectConverter(BoundsPlane.XY).Convert(Box()).center);

        // A negative size gives negative extents, which puts Bounds.min above Bounds.max. The
        // converter passes both through unchanged, so the documented "lower corner" is the upper one
        // here and the rectangle comes out inverted rather than normalized.
        [Test]
        public void NegativeSize_AnchorsAboveTheBoxAndStaysInverted() =>
            Assert.AreEqual(
                new Rect(1f, 1f, -2f, -2f),
                new BoundsToRectConverter(BoundsPlane.XY)
                    .Convert(new Bounds(Vector3.zero, new Vector3(-2f, -2f, -2f))));

        // The setting is a serialized field rather than an argument, so an undeclared value is a broken
        // converter: it is reported on every push and the box is flattened onto XY.
        [Test]
        public void UndeclaredPlane_ReportsItAndFlattensOntoXY()
        {
            LogAssert.Expect(LogType.Error, new Regex("BoundsToRectConverter.*not a declared"));

            Assert.AreEqual(
                new Rect(9f, 18f, 2f, 4f),
                new BoundsToRectConverter((BoundsPlane)3).Convert(Box()));
        }

        // Center (10, 20, 30) with size (2, 4, 6) puts min at (9, 18, 27). No two of the nine
        // numbers a flattening converter could pick up are equal, so a wrong axis cannot pass.
        private static Bounds Box() => new Bounds(new Vector3(10f, 20f, 30f), new Vector3(2f, 4f, 6f));
    }
}
