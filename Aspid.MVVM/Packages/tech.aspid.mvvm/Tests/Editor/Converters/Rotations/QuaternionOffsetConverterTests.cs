using UnityEngine;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="QuaternionOffsetConverter"/> — the apply-before/apply-after order and
    /// the round trip through <c>ConvertBack</c>.
    /// </summary>
    [TestFixture]
    internal sealed class QuaternionOffsetConverterTests
    {
        [Test]
        public void Convert_AppliesTheOffsetAfterTheBoundRotationByDefault() =>
            AssertSameRotation(
                Quaternion.Euler(0f, 90f, 0f) * Quaternion.Euler(0f, 30f, 0f),
                new QuaternionOffsetConverter(new Vector3(0f, 30f, 0f)).Convert(Quaternion.Euler(0f, 90f, 0f)));

        [Test]
        public void Convert_ApplyFirst_AppliesTheOffsetBeforeTheBoundRotation() =>
            AssertSameRotation(
                Quaternion.Euler(0f, 30f, 0f) * Quaternion.Euler(0f, 90f, 0f),
                new QuaternionOffsetConverter(new Vector3(0f, 30f, 0f), applyFirst: true).Convert(Quaternion.Euler(0f, 90f, 0f)));

        [TestCase(false)]
        [TestCase(true)]
        public void ConvertBack_UndoesConvert(bool applyFirst)
        {
            var converter = new QuaternionOffsetConverter(new Vector3(10f, 20f, 30f), applyFirst);
            var rotation = Quaternion.Euler(45f, 60f, 15f);

            AssertSameRotation(rotation, converter.ConvertBack(converter.Convert(rotation)));
        }

        [Test]
        public void Convert_DefaultConstructed_LeavesTheRotationUnchanged() =>
            AssertSameRotation(Quaternion.Euler(10f, 20f, 30f), new QuaternionOffsetConverter().Convert(Quaternion.Euler(10f, 20f, 30f)));

        private static void AssertSameRotation(Quaternion expected, Quaternion actual) =>
            Assert.AreEqual(0f, Quaternion.Angle(expected, actual), 1e-2f);
    }
}
