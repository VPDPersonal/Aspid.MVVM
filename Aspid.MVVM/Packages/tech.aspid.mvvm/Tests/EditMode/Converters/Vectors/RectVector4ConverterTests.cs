using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="RectVector4Converter"/>, which packs a <see cref="Rect"/> as
    /// (x, y, width, height) rather than as two corners.
    /// </summary>
    [TestFixture]
    public sealed class RectVector4ConverterTests
    {
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
    }
}
