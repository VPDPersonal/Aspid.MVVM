using UnityEngine;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="VectorLerpConverter"/> — the clamped default, the shaping curve, and
    /// the empty-curve guard.
    /// </summary>
    [TestFixture]
    internal sealed class VectorLerpConverterTests
    {
        [Test]
        public void Convert_MovesBetweenTheTwoVectors() =>
            Assert.AreEqual(
                new Vector3(5f, 5f, 5f),
                new VectorLerpConverter(Vector3.zero, new Vector3(10f, 10f, 10f)).Convert(0.5f));

        [Test]
        public void Convert_Clamped_HoldsTheAmountInsideZeroToOne() =>
            Assert.AreEqual(
                Vector3.one,
                new VectorLerpConverter(Vector3.zero, Vector3.one).Convert(2f));

        [Test]
        public void Convert_Curve_ShapesTheAmount() =>
            Assert.AreEqual(
                Vector3.one,
                new VectorLerpConverter(Vector3.zero, Vector3.one, AnimationCurve.Constant(0f, 1f, 1f)).Convert(0f));

        // An unassigned curve deserializes as an empty one, and evaluating that returns zero — which
        // would pin the result at _from if not guarded.
        [Test]
        public void Convert_EmptyCurve_IsTreatedAsNoCurve() =>
            Assert.AreEqual(
                new Vector3(5f, 5f, 5f),
                new VectorLerpConverter(Vector3.zero, new Vector3(10f, 10f, 10f), new AnimationCurve()).Convert(0.5f));
    }
}
