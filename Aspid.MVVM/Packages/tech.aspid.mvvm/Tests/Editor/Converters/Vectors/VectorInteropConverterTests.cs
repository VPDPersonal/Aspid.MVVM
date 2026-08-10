using System;
using UnityEngine;
using NUnit.Framework;
using Comp = Aspid.MVVM.StarterKit.Vector4Component;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the interop converters that move numbers between differently shaped structs —
    /// <see cref="Vector3ToVector4Converter"/>, <see cref="Vector4ToVector3Converter"/>,
    /// <see cref="Vector4SwizzleConverter"/>, <see cref="Vector4ToRectConverter"/>,
    /// <see cref="RectToVector4Converter"/>, <see cref="BoundsCenterConverter"/>,
    /// <see cref="BoundsSizeConverter"/> and <see cref="BoundsToRectConverter"/>.
    /// </summary>
    /// <remarks>
    /// Guards against slot-shuffling mistakes, which are the failure mode this whole file invites:
    /// a converter that reads the wrong component still returns a value of the right type and never
    /// throws, so the bug surfaces only as a rectangle in the wrong place or a swapped axis on
    /// screen. Three particular traps are pinned here — Unity's implicit <see cref="Vector3"/> to
    /// <see cref="Vector4"/> conversion already zeroes <c>w</c>, so a default-<c>w</c> assertion
    /// alone cannot tell the converter from doing nothing; <see cref="Rect"/> stores a corner plus a
    /// size while <see cref="Bounds"/> stores a centre plus extents, so "the same four numbers"
    /// means different geometry on each side; and neither the rectangle nor the bounding-box
    /// converters normalise, so an inverted input stays inverted.
    /// </remarks>
    [TestFixture]
    internal sealed class VectorInteropConverterTests
    {
        [Test]
        public void Vector3ToVector4_Convert_WritesTheConfiguredW() =>
            Assert.AreEqual(
                new Vector4(1f, 2f, 3f, 9f),
                new Vector3ToVector4Converter(9f).Convert(new Vector3(1f, 2f, 3f)));

        // Unity's own implicit Vector3 -> Vector4 conversion already zeroes w, so this case would
        // pass against a converter that did nothing at all. It is the 9f case above that proves the
        // serialized field is read; this one only pins the default.
        [Test]
        public void Vector3ToVector4_DefaultConstructed_WritesZeroW() =>
            Assert.AreEqual(
                new Vector4(1f, 2f, 3f, 0f),
                new Vector3ToVector4Converter().Convert(new Vector3(1f, 2f, 3f)));

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
                new Vector4ToVector3Converter(drop).Convert(new Vector4(1f, 2f, 3f, 4f)));

        [Test]
        public void Vector4ToVector3_DefaultConstructed_DropsW() =>
            Assert.AreEqual(
                new Vector3(1f, 2f, 3f),
                new Vector4ToVector3Converter().Convert(new Vector4(1f, 2f, 3f, 4f)));

        // The class is documented as "the way back from Vector3ToVector4Converter", which only holds
        // for the W drop. Every other choice slides the survivors down a slot, so the padding value
        // the widening converter added comes back as part of the position.
        [Test]
        public void Vector4ToVector3_DroppingAnythingButW_DoesNotUndoTheWidening()
        {
            var widened = new Vector3ToVector4Converter(9f).Convert(new Vector3(1f, 2f, 3f));

            Assert.AreEqual(new Vector3(1f, 2f, 3f), new Vector4ToVector3Converter(Comp.W).Convert(widened));
            Assert.AreEqual(new Vector3(2f, 3f, 9f), new Vector4ToVector3Converter(Comp.X).Convert(widened));
        }

        // The default branch looks unreachable through the enum, but Unity keeps the raw int when a
        // serialized enum field outlives the member it named, so a renamed or reordered
        // Vector4Component lands here at runtime.
        [Test]
        public void Vector4ToVector3_UndeclaredComponent_Throws() =>
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Vector4ToVector3Converter((Comp)4).Convert(Vector4.zero));

        [Test]
        public void Vector4Swizzle_DefaultConstructed_ReordersNothing() =>
            Assert.AreEqual(
                new Vector4(1f, 2f, 3f, 4f),
                new Vector4SwizzleConverter().Convert(new Vector4(1f, 2f, 3f, 4f)));

        // The argument position names the destination slot and the enum value names the source. The
        // sibling Vector2ToVector3Converter.Mode uses its enum value for the destination axes
        // instead, so the family is not consistent and this direction is easy to invert by mistake.
        [TestCase(Comp.X, 1f)]
        [TestCase(Comp.Y, 2f)]
        [TestCase(Comp.Z, 3f)]
        [TestCase(Comp.W, 4f)]
        public void Vector4Swizzle_FirstArgument_NamesTheSourceOfX(Comp source, float expected) =>
            Assert.AreEqual(
                expected,
                new Vector4SwizzleConverter(source, Comp.Y, Comp.Z, Comp.W)
                    .Convert(new Vector4(1f, 2f, 3f, 4f)).x,
                1e-6f);

        [Test]
        public void Vector4Swizzle_Convert_Reverses() =>
            Assert.AreEqual(
                new Vector4(4f, 3f, 2f, 1f),
                new Vector4SwizzleConverter(Comp.W, Comp.Z, Comp.Y, Comp.X)
                    .Convert(new Vector4(1f, 2f, 3f, 4f)));

        // Reading one source into every slot is supported on purpose. A fixture that only tested
        // permutations would not notice a duplicate-rejecting guard being added later.
        [Test]
        public void Vector4Swizzle_RepeatedSource_BroadcastsIt() =>
            Assert.AreEqual(
                new Vector4(2f, 2f, 2f, 2f),
                new Vector4SwizzleConverter(Comp.Y, Comp.Y, Comp.Y, Comp.Y)
                    .Convert(new Vector4(1f, 2f, 3f, 4f)));

        // The undeclared value sits in the last slot, so this fails if only the first slot is
        // validated rather than every one.
        [Test]
        public void Vector4Swizzle_UndeclaredComponentInTheLastSlot_Throws() =>
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Vector4SwizzleConverter(Comp.X, Comp.Y, Comp.Z, (Comp)4).Convert(Vector4.zero));

        // z becomes the width and w the height: the four numbers are a corner plus a size, never a
        // pair of corners. xMax is asserted because that is the value that would differ had z been
        // read as the far edge instead.
        [Test]
        public void Vector4ToRect_Convert_ReadsXYWidthHeight()
        {
            var rect = new Vector4ToRectConverter().Convert(new Vector4(1f, 2f, 3f, 4f));

            Assert.AreEqual(new Rect(1f, 2f, 3f, 4f), rect);
            Assert.AreEqual(4f, rect.xMax, 1e-6f);
            Assert.AreEqual(6f, rect.yMax, 1e-6f);
        }

        [Test]
        public void RectToVector4_Convert_ReadsXYWidthHeight() =>
            Assert.AreEqual(
                new Vector4(1f, 2f, 3f, 4f),
                new RectToVector4Converter().Convert(new Rect(1f, 2f, 3f, 4f)));

        // MinMaxRect is built from two corners, but the vector carries a size, so the 5 and 6 that
        // went in come back out as 4 and 4.
        [Test]
        public void RectToVector4_MinMaxRect_CarriesTheSizeNotTheFarCorner() =>
            Assert.AreEqual(
                new Vector4(1f, 2f, 4f, 4f),
                new RectToVector4Converter().Convert(Rect.MinMaxRect(1f, 2f, 5f, 6f)));

        [Test]
        public void Vector4AndRect_RoundTripInBothDirections()
        {
            var vector = new Vector4(1f, 2f, 3f, 4f);
            var rect = new Rect(5f, 6f, 7f, 8f);

            Assert.AreEqual(
                vector,
                new RectToVector4Converter().Convert(new Vector4ToRectConverter().Convert(vector)));
            Assert.AreEqual(
                rect,
                new Vector4ToRectConverter().Convert(new RectToVector4Converter().Convert(rect)));
        }

        // Neither direction normalises, so an inverted rectangle survives intact rather than being
        // flipped positive — xMax ends up below xMin.
        [Test]
        public void Vector4AndRect_NegativeSize_IsNotNormalised()
        {
            var rect = new Vector4ToRectConverter().Convert(new Vector4(1f, 2f, -3f, -4f));

            Assert.AreEqual(new Rect(1f, 2f, -3f, -4f), rect);
            Assert.AreEqual(-2f, rect.xMax, 1e-6f);
            Assert.AreEqual(new Vector4(1f, 2f, -3f, -4f), new RectToVector4Converter().Convert(rect));
        }

        // Bounds' second constructor argument is the size, not the extents. Any test box whose size
        // equalled its extents would hide a converter reading the wrong one of the two.
        [Test]
        public void BoundsCenter_Convert_ReadsTheCentre() =>
            Assert.AreEqual(new Vector3(10f, 20f, 30f), new BoundsCenterConverter().Convert(Box()));

        [Test]
        public void BoundsSize_DefaultConstructed_ReadsTheFullSize() =>
            Assert.AreEqual(new Vector3(2f, 4f, 6f), new BoundsSizeConverter().Convert(Box()));

        [Test]
        public void BoundsSize_Extents_ReadsHalfTheSize() =>
            Assert.AreEqual(new Vector3(1f, 2f, 3f), new BoundsSizeConverter(extents: true).Convert(Box()));

        // Bounds stores a negative size as negative extents without clamping, and the converter
        // reports it as it found it.
        [Test]
        public void BoundsSize_NegativeSize_StaysNegative() =>
            Assert.AreEqual(
                new Vector3(-2f, -2f, -2f),
                new BoundsSizeConverter().Convert(new Bounds(Vector3.zero, new Vector3(-2f, -2f, -2f))));

        // For XZ and YZ the box's z lands in the rectangle's y, because a Rect has no third axis to
        // keep it in. The position comes from Bounds.min, not from the centre.
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
        public void BoundsToRect_DefaultConstructed_FlattensOntoXY() =>
            Assert.AreEqual(new Rect(9f, 18f, 2f, 4f), new BoundsToRectConverter().Convert(Box()));

        // Anchoring at the corner is exactly what makes the two centres agree; a rectangle placed at
        // Bounds.center would sit half a box off, at (11, 22).
        [Test]
        public void BoundsToRect_Centre_MatchesTheBoxCentreOnThePlane() =>
            Assert.AreEqual(
                new Vector2(10f, 20f),
                new BoundsToRectConverter(BoundsPlane.XY).Convert(Box()).center);

        // A negative size gives negative extents, which puts Bounds.min above Bounds.max. The
        // converter passes both through unchanged, so the documented "lower corner" is the upper one
        // here and the rectangle comes out inverted rather than normalised.
        [Test]
        public void BoundsToRect_NegativeSize_AnchorsAboveTheBoxAndStaysInverted() =>
            Assert.AreEqual(
                new Rect(1f, 1f, -2f, -2f),
                new BoundsToRectConverter(BoundsPlane.XY)
                    .Convert(new Bounds(Vector3.zero, new Vector3(-2f, -2f, -2f))));

        [Test]
        public void BoundsToRect_UndeclaredPlane_Throws() =>
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new BoundsToRectConverter((BoundsPlane)3).Convert(new Bounds()));

        // Centre (10, 20, 30) with size (2, 4, 6) puts min at (9, 18, 27). No two of the nine
        // numbers a flattening converter could pick up are equal, so a wrong axis cannot pass.
        private static Bounds Box() => new Bounds(new Vector3(10f, 20f, 30f), new Vector3(2f, 4f, 6f));
    }
}
