using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="VectorDistanceConverter"/> — the fixed and transform targets, the
    /// flattened-Y measurement, and the destroyed-target fallback.
    /// </summary>
    [TestFixture]
    public sealed class VectorDistanceConverterTests : SceneFixture
    {
        // The flattened row is the one that matters: the height is dropped from the OFFSET, so a
        // position 10 above the target and 5 along the ground from it reads 5, not 11.18.
        [TestCase(3f, 4f, 0f, false, 5f)]
        [TestCase(3f, 10f, 4f, false, 11.18034f)]
        [TestCase(3f, 10f, 4f, true, 5f)]
        public void VectorDistance_MeasuresToTheAuthoredPoint(
            float x,
            float y,
            float z,
            bool flattenY,
            float expected) =>
            Assert.AreEqual(
                expected,
                new VectorDistanceConverter(Vector3.zero, flattenY).Convert(new Vector3(x, y, z)),
                1e-4f);

        [Test]
        public void VectorDistance_DefaultConstructed_MeasuresToTheOrigin() =>
            Assert.AreEqual(5f, new VectorDistanceConverter().Convert(new Vector3(3f, 4f, 0f)), 1e-4f);

        [Test]
        public void VectorDistance_AuthoredPoint_IsTheOtherEndOfTheMeasurement() =>
            Assert.AreEqual(
                5f,
                new VectorDistanceConverter(new Vector3(1f, 2f, 3f)).Convert(new Vector3(4f, 6f, 3f)),
                1e-4f);

        [Test]
        public void VectorDistance_Transform_MeasuresToItsPosition()
        {
            var target = NewTarget(new Vector3(10f, 0f, 0f));

            Assert.AreEqual(5f, new VectorDistanceConverter(target).Convert(new Vector3(13f, 4f, 0f)), 1e-4f);
        }

        // The position is read on every conversion rather than captured when the converter was built.
        // A waypoint marker has to follow the thing it points at, and a converter that cached the
        // position would pass the first assert and fail the second.
        [Test]
        public void VectorDistance_Transform_IsReReadOnEveryConversion()
        {
            var target = NewTarget(new Vector3(10f, 0f, 0f));
            var converter = new VectorDistanceConverter(target);

            Assert.AreEqual(0f, converter.Convert(new Vector3(10f, 0f, 0f)), 1e-4f);

            target.position = new Vector3(20f, 0f, 0f);

            Assert.AreEqual(10f, converter.Convert(new Vector3(10f, 0f, 0f)), 1e-4f);
        }

        [Test]
        public void VectorDistance_Transform_FlattenY_DropsTheHeightDifference()
        {
            var target = NewTarget(new Vector3(0f, 10f, 0f));

            Assert.AreEqual(
                5f,
                new VectorDistanceConverter(target, flattenY: true).Convert(new Vector3(3f, 0f, 4f)),
                1e-4f);
        }

        // The emptiness check is Unity's `== null`, not `is null`, so a destroyed target is seen as
        // empty and the converter measures to the authored point instead — zero for this ctor, hence
        // 5. Written with `is null` it would read `position` off a destroyed object and throw on the
        // frame the target dies; measuring to the old position would answer ~8.06.
        [Test]
        public void VectorDistance_DestroyedTarget_FallsBackToTheAuthoredPoint()
        {
            var target = NewTarget(new Vector3(10f, 0f, 0f));
            var converter = new VectorDistanceConverter(target);

            Destroy(target.gameObject);

            Assert.AreEqual(5f, converter.Convert(new Vector3(3f, 4f, 0f)), 1e-4f);

            // An empty target is an authoring choice, not a failure, so nothing may be reported.
            LogAssert.NoUnexpectedReceived();
        }

        // A 2D scene measures the same distance without the depth the target carries.
        [Test]
        public void Distance_Vector2_MeasuresWithoutTheDepth() =>
            Assert.AreEqual(
                5f,
                ((IConverter<Vector2, float>)new VectorDistanceConverter(new Vector3(0f, 0f, 99f)))
                    .Convert(new Vector2(3f, 4f)),
                1e-4f);

        private Transform NewTarget(Vector3 position)
        {
            var transform = Spawn(nameof(VectorDistanceConverterTests)).transform;
            transform.position = position;

            return transform;
        }
    }
}
