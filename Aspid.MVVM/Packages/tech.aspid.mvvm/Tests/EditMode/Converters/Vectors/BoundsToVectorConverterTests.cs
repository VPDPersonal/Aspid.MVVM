using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="BoundsToVectorConverter"/>'s center, size and extents modes.
    /// </summary>
    [TestFixture]
    public sealed class BoundsToVectorConverterTests
    {
        // Bounds' second constructor argument is the size, not the extents. Any test box whose size
        // equalled its extents would hide a converter reading the wrong one of the two.
        [Test]
        public void DefaultConstructed_ReadsTheCenter() =>
            Assert.AreEqual(new Vector3(10f, 20f, 30f), new BoundsToVectorConverter().Convert(Box()));

        [Test]
        public void Size_ReadsTheFullSize() =>
            Assert.AreEqual(new Vector3(2f, 4f, 6f), new BoundsToVectorConverter(BoundsVector.Size).Convert(Box()));

        [Test]
        public void Extents_ReadsHalfTheSize() =>
            Assert.AreEqual(new Vector3(1f, 2f, 3f), new BoundsToVectorConverter(BoundsVector.Extents).Convert(Box()));

        // Bounds stores a negative size as negative extents without clamping, and the converter
        // reports it as it found it.
        [Test]
        public void NegativeSize_StaysNegative() =>
            Assert.AreEqual(
                new Vector3(-2f, -2f, -2f),
                new BoundsToVectorConverter(BoundsVector.Size).Convert(new Bounds(Vector3.zero, new Vector3(-2f, -2f, -2f))));

        // No two of center, size and extents share a component, so a converter reading the wrong one
        // of the three cannot pass by accident.
        private static Bounds Box() => new Bounds(new Vector3(10f, 20f, 30f), new Vector3(2f, 4f, 6f));
    }
}
