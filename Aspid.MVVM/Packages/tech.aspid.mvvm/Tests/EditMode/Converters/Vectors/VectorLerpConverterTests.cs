using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="VectorLerpConverter"/> — the shaping curve, the empty-curve guard, and
    /// the unclamped path reached only through deserialization.
    /// </summary>
    /// <remarks>
    /// The clamp belongs to <c>Vector3.Lerp</c> and applies to the amount the curve produced, not to
    /// the amount that came in.
    /// </remarks>
    [TestFixture]
    public sealed class VectorLerpConverterTests
    {
        private static readonly Vector3 _from = new(2f, 0f, 0f);
        private static readonly Vector3 _to = new(12f, 0f, 0f);

        [Test]
        public void VectorLerp_NoCurve_MovesEvenly() =>
            AssertClose(new Vector3(7f, 0f, 0f), new VectorLerpConverter(_from, _to).Convert(0.5f));

        // Both spellings of "no curve" have to mean the same thing. An unassigned AnimationCurve
        // deserializes as an empty one rather than as null, and Evaluate on an empty curve answers 0
        // — which would pin every conversion at _from. The length guard is what stops that, and this
        // is the case that fails without it.
        [Test]
        public void VectorLerp_EmptyCurve_IsTreatedAsNoCurve() =>
            AssertClose(
                new Vector3(9f, 0f, 0f),
                new VectorLerpConverter(_from, _to, new AnimationCurve()).Convert(0.7f));

        // The curve shapes the amount before the move, so one that runs 1 down to 0 reverses the
        // travel: a quarter of the way in reads three quarters of the way along.
        [Test]
        public void VectorLerp_Curve_ShapesTheAmountBeforeTheMove() =>
            AssertClose(
                new Vector3(9.5f, 0f, 0f),
                new VectorLerpConverter(_from, _to, AnimationCurve.Linear(0f, 1f, 1f, 0f)).Convert(0.25f),
                1e-3f);

        // A constant curve proves the incoming amount is not consulted at all once a curve is
        // present — both conversions land on the same point.
        [Test]
        public void VectorLerp_ConstantCurve_IgnoresTheIncomingAmount()
        {
            var converter = new VectorLerpConverter(_from, _to, AnimationCurve.Constant(0f, 1f, 0.25f));

            AssertClose(new Vector3(4.5f, 0f, 0f), converter.Convert(0f));
            AssertClose(new Vector3(4.5f, 0f, 0f), converter.Convert(0.9f));
        }

        // The clamp belongs to Vector3.Lerp and applies to the amount the CURVE produced, not to the
        // amount that came in — the tooltip's "hold the incoming amount inside 0..1" describes the
        // wrong end. A curve that overshoots is held at the far end; one that undershoots, at the
        // near end. Unclamped these would read 22 and -8.
        [Test]
        public void VectorLerp_CurveAboveOne_IsHeldAtTheFarEnd() =>
            AssertClose(_to, new VectorLerpConverter(_from, _to, AnimationCurve.Constant(0f, 1f, 2f)).Convert(0.5f));

        [Test]
        public void VectorLerp_CurveBelowZero_IsHeldAtTheNearEnd() =>
            AssertClose(_from, new VectorLerpConverter(_from, _to, AnimationCurve.Constant(0f, 1f, -1f)).Convert(0.5f));

        [Test]
        public void VectorLerp_AmountOutsideZeroToOne_IsHeldAtTheEnds()
        {
            var converter = new VectorLerpConverter(_from, _to);

            AssertClose(_to, converter.Convert(1.5f));
            AssertClose(_from, converter.Convert(-0.5f));
        }

        // _clamp has no constructor parameter, so the LerpUnclamped branch is only reachable the way
        // a real scene reaches it — through deserialization of a [SerializeReference] field. The
        // first assert proves the JSON landed (and, incidentally, that a deserialized instance with
        // no curve key is not pinned at _from); the second is the branch itself, sailing half again
        // past _to instead of stopping on it.
        [Test]
        public void VectorLerp_Deserialized_WithClampOff_OvershootsTheFarEnd()
        {
            var converter = JsonUtility.FromJson<VectorLerpConverter>(
                "{\"_from\":{\"x\":2,\"y\":0,\"z\":0},\"_to\":{\"x\":12,\"y\":0,\"z\":0},\"_clamp\":false}");

            AssertClose(new Vector3(7f, 0f, 0f), converter.Convert(0.5f));
            AssertClose(new Vector3(17f, 0f, 0f), converter.Convert(1.5f));
        }

        [Test]
        public void VectorLerp_Vector2_ReadsTheFirstTwoComponents() =>
            Assert.AreEqual(
                new Vector2(5f, 5f),
                ((IConverter<float, Vector2>)new VectorLerpConverter(Vector4.zero, new Vector4(10f, 10f, 10f, 10f)))
                    .Convert(0.5f));

        [Test]
        public void VectorLerp_Vector4_ReadsTheFourth() =>
            Assert.AreEqual(
                new Vector4(5f, 5f, 5f, 5f),
                ((IConverter<float, Vector4>)new VectorLerpConverter(Vector4.zero, new Vector4(10f, 10f, 10f, 10f)))
                    .Convert(0.5f));

        private static void AssertClose(Vector3 expected, Vector3 actual, float delta = 1e-4f)
        {
            Assert.AreEqual(expected.x, actual.x, delta, $"x of {actual}, expected {expected}");
            Assert.AreEqual(expected.y, actual.y, delta, $"y of {actual}, expected {expected}");
            Assert.AreEqual(expected.z, actual.z, delta, $"z of {actual}, expected {expected}");
        }
    }
}
