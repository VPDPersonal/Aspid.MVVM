using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;
using Comp = Aspid.MVVM.StarterKit.Vector4Component;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the interop converters that move numbers between differently shaped structs —
    /// <see cref="Vector3Vector4Converter"/> in both directions,
    /// <see cref="RectVector4Converter"/>, <see cref="BoundsToVectorConverter"/> and
    /// <see cref="BoundsToRectConverter"/>.
    /// </summary>
    /// <remarks>
    /// A converter that reads the wrong component still returns a value of the right type and never
    /// throws, so the bug surfaces only as a rectangle in the wrong place. Three traps are pinned here:
    /// Unity's implicit <see cref="Vector3"/> to <see cref="Vector4"/> conversion already zeroes
    /// <c>w</c>; <see cref="Rect"/> stores a corner plus a size while <see cref="Bounds"/> stores a
    /// center plus extents; and neither converter normalizes, so an inverted input stays inverted.
    /// </remarks>
    [TestFixture]
    internal sealed class VectorInteropConverterTests
    {
        [Test]
        public void Vector3ToVector4_Convert_WritesTheConfiguredW() =>
            Assert.AreEqual(
                new Vector4(1f, 2f, 3f, 9f),
                new Vector3Vector4Converter(9f).Convert(new Vector3(1f, 2f, 3f)));

        // Unity's own implicit Vector3 -> Vector4 conversion already zeroes w, so this case would
        // pass against a converter that did nothing at all. It is the 9f case above that proves the
        // serialized field is read; this one only pins the default.
        [Test]
        public void Vector3ToVector4_DefaultConstructed_WritesZeroW() =>
            Assert.AreEqual(
                new Vector4(1f, 2f, 3f, 0f),
                new Vector3Vector4Converter().Convert(new Vector3(1f, 2f, 3f)));

        [TestCase(Comp.X, 2f, 3f, 4f)]
        [TestCase(Comp.Y, 1f, 3f, 4f)]
        [TestCase(Comp.Z, 1f, 2f, 4f)]
        [TestCase(Comp.W, 1f, 2f, 3f)]
        public void Vector4ToVector3_Convert_DropsTheNamedComponentAndKeepsTheRestInOrder(
            Comp drop,
            float x,
            float y,
            float z) =>
            Assert.AreEqual(
                new Vector3(x, y, z),
                new Vector3Vector4Converter(w: 0f, drop).ConvertBack(new Vector4(1f, 2f, 3f, 4f)));

        [Test]
        public void Vector4ToVector3_DefaultConstructed_DropsW() =>
            Assert.AreEqual(
                new Vector3(1f, 2f, 3f),
                new Vector3Vector4Converter().ConvertBack(new Vector4(1f, 2f, 3f, 4f)));

        // Undoing Vector3Vector4Converter only holds for the W drop. Every other choice slides the
        // survivors down a slot, so the padding value the widening converter added comes back as
        // part of the position.
        [Test]
        public void Vector4ToVector3_DroppingAnythingButW_DoesNotUndoTheWidening()
        {
            var widened = new Vector3Vector4Converter(9f).Convert(new Vector3(1f, 2f, 3f));

            Assert.AreEqual(new Vector3(1f, 2f, 3f), new Vector3Vector4Converter(w: 0f, Comp.W).ConvertBack(widened));
            Assert.AreEqual(new Vector3(2f, 3f, 9f), new Vector3Vector4Converter(w: 0f, Comp.X).ConvertBack(widened));
        }

        // The default branch looks unreachable through the enum, but Unity keeps the raw int when a
        // serialized enum field outlives the member it named, so a renamed or reordered
        // Vector4Component lands here at runtime — reported on every push, with W dropped.
        [Test]
        public void Vector4ToVector3_UndeclaredComponent_ReportsItAndDropsW()
        {
            LogAssert.Expect(LogType.Error, new Regex("Vector3Vector4Converter.*not a declared Vector4Component"));

            Assert.AreEqual(
                new Vector3(1f, 2f, 3f),
                new Vector3Vector4Converter(w: 0f, (Comp)4).ConvertBack(new Vector4(1f, 2f, 3f, 4f)));
        }

        // z becomes the width and w the height: the four numbers are a corner plus a size, never a
        // pair of corners. xMax is asserted because that is the value that would differ had z been
        // read as the far edge instead.
        [Test]
        public void Vector4ToRect_Convert_ReadsXYWidthHeight()
        {
            var rect = new RectVector4Converter().Convert(new Vector4(1f, 2f, 3f, 4f));

            Assert.AreEqual(new Rect(1f, 2f, 3f, 4f), rect);
            Assert.AreEqual(4f, rect.xMax, 1e-6f);
            Assert.AreEqual(6f, rect.yMax, 1e-6f);
        }

        [Test]
        public void RectToVector4_Convert_ReadsXYWidthHeight() =>
            Assert.AreEqual(
                new Vector4(1f, 2f, 3f, 4f),
                new RectVector4Converter().Convert(new Rect(1f, 2f, 3f, 4f)));

        // MinMaxRect is built from two corners, but the vector carries a size, so the 5 and 6 that
        // went in come back out as 4 and 4.
        [Test]
        public void RectToVector4_MinMaxRect_CarriesTheSizeNotTheFarCorner() =>
            Assert.AreEqual(
                new Vector4(1f, 2f, 4f, 4f),
                new RectVector4Converter().Convert(Rect.MinMaxRect(1f, 2f, 5f, 6f)));

        [Test]
        public void Vector4AndRect_RoundTripInBothDirections()
        {
            var vector = new Vector4(1f, 2f, 3f, 4f);
            var rect = new Rect(5f, 6f, 7f, 8f);

            Assert.AreEqual(
                vector,
                new RectVector4Converter().Convert(new RectVector4Converter().Convert(vector)));
            Assert.AreEqual(
                rect,
                new RectVector4Converter().Convert(new RectVector4Converter().Convert(rect)));
        }

        // Neither direction normalizes, so an inverted rectangle survives intact rather than being
        // flipped positive — xMax ends up below xMin.
        [Test]
        public void Vector4AndRect_NegativeSize_IsNotNormalized()
        {
            var rect = new RectVector4Converter().Convert(new Vector4(1f, 2f, -3f, -4f));

            Assert.AreEqual(new Rect(1f, 2f, -3f, -4f), rect);
            Assert.AreEqual(-2f, rect.xMax, 1e-6f);
            Assert.AreEqual(new Vector4(1f, 2f, -3f, -4f), new RectVector4Converter().Convert(rect));
        }

        // Bounds' second constructor argument is the size, not the extents. Any test box whose size
        // equalled its extents would hide a converter reading the wrong one of the two.
        [Test]
        public void BoundsToVector_DefaultConstructed_ReadsTheCenter() =>
            Assert.AreEqual(new Vector3(10f, 20f, 30f), new BoundsToVectorConverter().Convert(Box()));

        [Test]
        public void BoundsToVector_Size_ReadsTheFullSize() =>
            Assert.AreEqual(new Vector3(2f, 4f, 6f), new BoundsToVectorConverter(BoundsVector.Size).Convert(Box()));

        [Test]
        public void BoundsToVector_Extents_ReadsHalfTheSize() =>
            Assert.AreEqual(new Vector3(1f, 2f, 3f), new BoundsToVectorConverter(BoundsVector.Extents).Convert(Box()));

        // Bounds stores a negative size as negative extents without clamping, and the converter
        // reports it as it found it.
        [Test]
        public void BoundsToVector_NegativeSize_StaysNegative() =>
            Assert.AreEqual(
                new Vector3(-2f, -2f, -2f),
                new BoundsToVectorConverter(BoundsVector.Size).Convert(new Bounds(Vector3.zero, new Vector3(-2f, -2f, -2f))));

        // For XZ and YZ the box's z lands in the rectangle's y, because a Rect has no third axis to
        // keep it in. The position comes from Bounds.min, not from the center.
        [TestCase(BoundsPlane.XY, 9f, 18f, 2f, 4f)]
        [TestCase(BoundsPlane.XZ, 9f, 27f, 2f, 6f)]
        [TestCase(BoundsPlane.YZ, 18f, 27f, 4f, 6f)]
        public void BoundsToRect_Convert_AnchorsAtTheMinCornerOfTheChosenPlane(
            BoundsPlane plane,
            float x,
            float y,
            float width,
            float height) =>
            Assert.AreEqual(new Rect(x, y, width, height), new BoundsToRectConverter(plane).Convert(Box()));

        [Test]
        public void BoundsToRect_XY_FlattensOntoXY() =>
            Assert.AreEqual(new Rect(9f, 18f, 2f, 4f), new BoundsToRectConverter(BoundsPlane.XY).Convert(Box()));

        // Anchoring at the corner is exactly what makes the two centers agree; a rectangle placed at
        // Bounds.center would sit half a box off, at (11, 22).
        [Test]
        public void BoundsToRect_Center_MatchesTheBoxCenterOnThePlane() =>
            Assert.AreEqual(
                new Vector2(10f, 20f),
                new BoundsToRectConverter(BoundsPlane.XY).Convert(Box()).center);

        // A negative size gives negative extents, which puts Bounds.min above Bounds.max. The
        // converter passes both through unchanged, so the documented "lower corner" is the upper one
        // here and the rectangle comes out inverted rather than normalized.
        [Test]
        public void BoundsToRect_NegativeSize_AnchorsAboveTheBoxAndStaysInverted() =>
            Assert.AreEqual(
                new Rect(1f, 1f, -2f, -2f),
                new BoundsToRectConverter(BoundsPlane.XY)
                    .Convert(new Bounds(Vector3.zero, new Vector3(-2f, -2f, -2f))));

        // The setting is a serialized field rather than an argument, so an undeclared value is a broken
        // converter: it is reported on every push and the box is flattened onto XY.
        [Test]
        public void BoundsToRect_UndeclaredPlane_ReportsItAndFlattensOntoXY()
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
