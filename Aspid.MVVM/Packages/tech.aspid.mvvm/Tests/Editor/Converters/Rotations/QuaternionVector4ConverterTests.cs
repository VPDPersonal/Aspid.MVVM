using UnityEngine;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the <see cref="QuaternionVector4Converter"/> /
    /// reverse pass — the raw component order, the default
    /// normalization and its opt-out, and the round trip.
    /// </summary>
    [TestFixture]
    internal sealed class QuaternionVector4ConverterTests
    {
        // Four distinct numbers, none of them a rotation: the order has to be x, y, z, w and nothing
        // may be normalized on the way out. A w-first copy, or a normalizing one, fails every row.
        [Test]
        public void QuaternionToVector4_CopiesTheFourNumbersInOrderWithoutNormalizing()
        {
            var packed = new QuaternionVector4Converter().Convert(new Quaternion(0.1f, 0.2f, 0.3f, 0.4f));

            Assert.AreEqual(0.1f, packed.x, 1e-6f);
            Assert.AreEqual(0.2f, packed.y, 1e-6f);
            Assert.AreEqual(0.3f, packed.z, 1e-6f);
            Assert.AreEqual(0.4f, packed.w, 1e-6f);
            Assert.AreEqual(0.3f, packed.sqrMagnitude, 1e-6f);
        }

        // Numbers off a lerp, a text field or a lossy wire format are rarely unit length. Both rows
        // describe the same 90° turn about Z at different scales, and normalizing is what makes them
        // agree; without it the second would scale whatever it multiplies by three.
        [TestCase(0f, 0f, 0.5f, 0.5f)]
        [TestCase(0f, 0f, 3f, 3f)]
        public void Vector4ToQuaternion_NormalizesByDefault(float x, float y, float z, float w)
        {
            var rotation = new QuaternionVector4Converter().ConvertBack(new Vector4(x, y, z, w));

            Assert.AreEqual(0.70710678f, rotation.z, 1e-5f);
            Assert.AreEqual(0.70710678f, rotation.w, 1e-5f);
            Assert.AreEqual(90f, rotation.eulerAngles.z, 1e-2f);
        }

        [Test]
        public void Vector4ToQuaternion_WithoutNormalizing_KeepsTheRawNumbers()
        {
            var rotation = new QuaternionVector4Converter(normalize: false).ConvertBack(new Vector4(0f, 0f, 0.5f, 0.5f));

            Assert.AreEqual(0.5f, rotation.z, 1e-6f);
            Assert.AreEqual(0.5f, rotation.w, 1e-6f);
        }

        [Test]
        public void Vector4ToQuaternion_ZeroWhileNormalizing_IsTheIdentity() =>
            Assert.AreEqual(Quaternion.identity, new QuaternionVector4Converter().ConvertBack(Vector4.zero));

        // The guard belongs to the normalizing path only. With the flag cleared, four zeroes come
        // through as a zero quaternion — not the identity — and a zero quaternion collapses whatever
        // it multiplies instead of leaving it alone.
        [Test]
        public void Vector4ToQuaternion_ZeroWithoutNormalizing_IsADegenerateRotation()
        {
            var rotation = new QuaternionVector4Converter(normalize: false).ConvertBack(Vector4.zero);

            Assert.AreEqual(0f, rotation.w, 1e-6f);
            Assert.AreNotEqual(Quaternion.identity, rotation);
        }

        // The pair is meant to be a round trip for a save record or a network packet, so a unit
        // rotation has to survive it component for component — the sign of each number included,
        // which a rotation-space comparison would hide.
        [Test]
        public void QuaternionAndVector4_RoundTripAUnitRotation()
        {
            var rotation = Quaternion.Euler(30f, 45f, 60f);
            var packed = new QuaternionVector4Converter().Convert(rotation);
            var restored = new QuaternionVector4Converter().ConvertBack(packed);

            Assert.AreEqual(rotation.x, restored.x, 1e-5f);
            Assert.AreEqual(rotation.y, restored.y, 1e-5f);
            Assert.AreEqual(rotation.z, restored.z, 1e-5f);
            Assert.AreEqual(rotation.w, restored.w, 1e-5f);
        }
    }
}
