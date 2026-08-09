using UnityEngine;
using NUnit.Framework;
using V2Mode = Aspid.MVVM.StarterKit.Vector2SubstitutionConverter.Mode;
using V3Mode = Aspid.MVVM.StarterKit.Vector3SubstitutionConverter.Mode;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Exhaustive coverage for <see cref="Vector3SubstitutionConverter"/> (all 27 modes) and
    /// <see cref="Vector2SubstitutionConverter"/> (all 4) — one row per enum member, so a mis-mapped
    /// component cannot pass unnoticed.
    /// </summary>
    [TestFixture]
    internal sealed class VectorSubstitutionConverterTests
    {
        [TestCase(V3Mode.XYZ, 1f, 2f, 3f)]
        [TestCase(V3Mode.XZY, 1f, 3f, 2f)]
        [TestCase(V3Mode.YXZ, 2f, 1f, 3f)]
        [TestCase(V3Mode.YZX, 2f, 3f, 1f)]
        [TestCase(V3Mode.ZXY, 3f, 1f, 2f)]
        [TestCase(V3Mode.ZYX, 3f, 2f, 1f)]
        [TestCase(V3Mode.XXY, 1f, 1f, 2f)]
        [TestCase(V3Mode.XYX, 1f, 2f, 1f)]
        [TestCase(V3Mode.YXX, 2f, 1f, 1f)]
        [TestCase(V3Mode.XXZ, 1f, 1f, 3f)]
        [TestCase(V3Mode.XZX, 1f, 3f, 1f)]
        [TestCase(V3Mode.ZXX, 3f, 1f, 1f)]
        [TestCase(V3Mode.YYX, 2f, 2f, 1f)]
        [TestCase(V3Mode.YXY, 2f, 1f, 2f)]
        [TestCase(V3Mode.XYY, 1f, 2f, 2f)]
        [TestCase(V3Mode.YYZ, 2f, 2f, 3f)]
        [TestCase(V3Mode.YZY, 2f, 3f, 2f)]
        [TestCase(V3Mode.ZYY, 3f, 2f, 2f)]
        [TestCase(V3Mode.ZZX, 3f, 3f, 1f)]
        [TestCase(V3Mode.ZXZ, 3f, 1f, 3f)]
        [TestCase(V3Mode.XZZ, 1f, 3f, 3f)]
        [TestCase(V3Mode.ZZY, 3f, 3f, 2f)]
        [TestCase(V3Mode.ZYZ, 3f, 2f, 3f)]
        [TestCase(V3Mode.YZZ, 2f, 3f, 3f)]
        [TestCase(V3Mode.XXX, 1f, 1f, 1f)]
        [TestCase(V3Mode.YYY, 2f, 2f, 2f)]
        [TestCase(V3Mode.ZZZ, 3f, 3f, 3f)]
        public void Vector3_Convert_RearrangesComponents(V3Mode mode, float x, float y, float z) =>
            Assert.AreEqual(
                new Vector3(x, y, z),
                new Vector3SubstitutionConverter(mode).Convert(new Vector3(1f, 2f, 3f)));

        [Test]
        public void Vector3_DefaultConstructed_IsIdentity() =>
            Assert.AreEqual(
                new Vector3(1f, 2f, 3f),
                new Vector3SubstitutionConverter().Convert(new Vector3(1f, 2f, 3f)));

        [TestCase(V2Mode.XY, 1f, 2f)]
        [TestCase(V2Mode.YX, 2f, 1f)]
        [TestCase(V2Mode.YY, 2f, 2f)]
        [TestCase(V2Mode.XX, 1f, 1f)]
        public void Vector2_Convert_RearrangesComponents(V2Mode mode, float x, float y) =>
            Assert.AreEqual(
                new Vector2(x, y),
                new Vector2SubstitutionConverter(mode).Convert(new Vector2(1f, 2f)));

        [Test]
        public void Vector2_DefaultConstructed_IsIdentity() =>
            Assert.AreEqual(
                new Vector2(1f, 2f),
                new Vector2SubstitutionConverter().Convert(new Vector2(1f, 2f)));
    }
}
