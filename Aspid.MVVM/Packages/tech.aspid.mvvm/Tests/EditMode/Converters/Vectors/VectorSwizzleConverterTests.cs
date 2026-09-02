using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using Comp = Aspid.MVVM.StarterKit.Vector4Component;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Exhaustive coverage for <see cref="VectorSwizzleConverter"/> at all three widths — every
    /// three-component rearrangement (27) and every two-component one (4) gets a row, so a
    /// mis-mapped component cannot pass unnoticed.
    /// </summary>
    /// <remarks>
    /// Unity converts a <see cref="Vector2"/> or <see cref="Vector3"/> to <see cref="Vector4"/>
    /// implicitly, so calling <c>Convert</c> on the converter directly would bind to the public
    /// four-component overload and test the wrong width. The narrow widths are reached through their
    /// interfaces by the <c>Swizzle</c> helpers below.
    /// </remarks>
    [TestFixture]
    public sealed class VectorSwizzleConverterTests
    {
        [TestCase(Comp.X, Comp.Y, Comp.Z, 1f, 2f, 3f)]
        [TestCase(Comp.X, Comp.Z, Comp.Y, 1f, 3f, 2f)]
        [TestCase(Comp.Y, Comp.X, Comp.Z, 2f, 1f, 3f)]
        [TestCase(Comp.Y, Comp.Z, Comp.X, 2f, 3f, 1f)]
        [TestCase(Comp.Z, Comp.X, Comp.Y, 3f, 1f, 2f)]
        [TestCase(Comp.Z, Comp.Y, Comp.X, 3f, 2f, 1f)]
        [TestCase(Comp.X, Comp.X, Comp.Y, 1f, 1f, 2f)]
        [TestCase(Comp.X, Comp.Y, Comp.X, 1f, 2f, 1f)]
        [TestCase(Comp.Y, Comp.X, Comp.X, 2f, 1f, 1f)]
        [TestCase(Comp.X, Comp.X, Comp.Z, 1f, 1f, 3f)]
        [TestCase(Comp.X, Comp.Z, Comp.X, 1f, 3f, 1f)]
        [TestCase(Comp.Z, Comp.X, Comp.X, 3f, 1f, 1f)]
        [TestCase(Comp.Y, Comp.Y, Comp.X, 2f, 2f, 1f)]
        [TestCase(Comp.Y, Comp.X, Comp.Y, 2f, 1f, 2f)]
        [TestCase(Comp.X, Comp.Y, Comp.Y, 1f, 2f, 2f)]
        [TestCase(Comp.Y, Comp.Y, Comp.Z, 2f, 2f, 3f)]
        [TestCase(Comp.Y, Comp.Z, Comp.Y, 2f, 3f, 2f)]
        [TestCase(Comp.Z, Comp.Y, Comp.Y, 3f, 2f, 2f)]
        [TestCase(Comp.Z, Comp.Z, Comp.X, 3f, 3f, 1f)]
        [TestCase(Comp.Z, Comp.X, Comp.Z, 3f, 1f, 3f)]
        [TestCase(Comp.X, Comp.Z, Comp.Z, 1f, 3f, 3f)]
        [TestCase(Comp.Z, Comp.Z, Comp.Y, 3f, 3f, 2f)]
        [TestCase(Comp.Z, Comp.Y, Comp.Z, 3f, 2f, 3f)]
        [TestCase(Comp.Y, Comp.Z, Comp.Z, 2f, 3f, 3f)]
        [TestCase(Comp.X, Comp.X, Comp.X, 1f, 1f, 1f)]
        [TestCase(Comp.Y, Comp.Y, Comp.Y, 2f, 2f, 2f)]
        [TestCase(Comp.Z, Comp.Z, Comp.Z, 3f, 3f, 3f)]
        public void Vector3_Convert_RearrangesComponents(
            Comp x,
            Comp y,
            Comp z,
            float resultX,
            float resultY,
            float resultZ) =>
            Assert.AreEqual(
                new Vector3(resultX, resultY, resultZ),
                Swizzle(new VectorSwizzleConverter(x, y, z, Comp.W), new Vector3(1f, 2f, 3f)));

        // The W slot holds its default here, which a Vector3 cannot source. It is never read, so no
        // error is reported: an unread slot cannot be misconfigured.
        [Test]
        public void Vector3_DefaultConstructed_IsIdentity() =>
            Assert.AreEqual(
                new Vector3(1f, 2f, 3f),
                Swizzle(new VectorSwizzleConverter(), new Vector3(1f, 2f, 3f)));

        [TestCase(Comp.X, Comp.Y, 1f, 2f)]
        [TestCase(Comp.Y, Comp.X, 2f, 1f)]
        [TestCase(Comp.Y, Comp.Y, 2f, 2f)]
        [TestCase(Comp.X, Comp.X, 1f, 1f)]
        public void Vector2_Convert_RearrangesComponents(Comp x, Comp y, float resultX, float resultY) =>
            Assert.AreEqual(
                new Vector2(resultX, resultY),
                Swizzle(new VectorSwizzleConverter(x, y, Comp.Z, Comp.W), new Vector2(1f, 2f)));

        [Test]
        public void Vector2_DefaultConstructed_IsIdentity() =>
            Assert.AreEqual(
                new Vector2(1f, 2f),
                Swizzle(new VectorSwizzleConverter(), new Vector2(1f, 2f)));

        [Test]
        public void Vector4_DefaultConstructed_ReordersNothing() =>
            Assert.AreEqual(
                new Vector4(1f, 2f, 3f, 4f),
                new VectorSwizzleConverter().Convert(new Vector4(1f, 2f, 3f, 4f)));

        // The argument position names the destination slot and the enum value names the source. The
        // sibling Vector2Vector3Converter.Mode uses its enum value for the destination axes
        // instead, so the family is not consistent and this direction is easy to invert by mistake.
        [TestCase(Comp.X, 1f)]
        [TestCase(Comp.Y, 2f)]
        [TestCase(Comp.Z, 3f)]
        [TestCase(Comp.W, 4f)]
        public void Vector4_FirstArgument_NamesTheSourceOfX(Comp source, float expected) =>
            Assert.AreEqual(
                expected,
                new VectorSwizzleConverter(source, Comp.Y, Comp.Z, Comp.W)
                    .Convert(new Vector4(1f, 2f, 3f, 4f)).x,
                1e-6f);

        [Test]
        public void Vector4_Convert_Reverses() =>
            Assert.AreEqual(
                new Vector4(4f, 3f, 2f, 1f),
                new VectorSwizzleConverter(Comp.W, Comp.Z, Comp.Y, Comp.X)
                    .Convert(new Vector4(1f, 2f, 3f, 4f)));

        // Reading one source into every slot is supported on purpose. A fixture that only tested
        // permutations would not notice a duplicate-rejecting guard being added later.
        [Test]
        public void Vector4_RepeatedSource_BroadcastsIt() =>
            Assert.AreEqual(
                new Vector4(2f, 2f, 2f, 2f),
                new VectorSwizzleConverter(Comp.Y, Comp.Y, Comp.Y, Comp.Y)
                    .Convert(new Vector4(1f, 2f, 3f, 4f)));

        // A Vector2 carries no Z, so the X slot is a misconfiguration: it is reported and X keeps
        // the incoming x. The Y slot still reads X, which is what separates this from an untouched
        // vector.
        [Test]
        public void Vector2_SlotOutOfRange_ReportsItAndPassesThatSlotThrough()
        {
            LogAssert.Expect(
                LogType.Error,
                new Regex("VectorSwizzleConverter.*X slot reads Z, which a Vector2 does not carry"));

            Assert.AreEqual(
                new Vector2(1f, 1f),
                Swizzle(new VectorSwizzleConverter(Comp.Z, Comp.X, Comp.Z, Comp.W), new Vector2(1f, 2f)));
        }

        [Test]
        public void Vector3_SlotOutOfRange_ReportsItAndPassesThatSlotThrough()
        {
            LogAssert.Expect(
                LogType.Error,
                new Regex("VectorSwizzleConverter.*X slot reads W, which a Vector3 does not carry"));

            Assert.AreEqual(
                new Vector3(1f, 3f, 2f),
                Swizzle(new VectorSwizzleConverter(Comp.W, Comp.Z, Comp.Y, Comp.W), new Vector3(1f, 2f, 3f)));
        }

        // The default branch looks unreachable through the enum, but Unity keeps the raw int when a
        // serialized enum field outlives the member it named, so a renamed or reordered
        // Vector4Component lands here at runtime. The undeclared value sits in the last slot and the
        // three good slots are all rearranged, so this fails both if only the first slot is checked
        // and if the whole vector is passed through instead of the broken slot alone.
        [Test]
        public void Vector4_UndeclaredComponentInTheLastSlot_ReportsItAndPassesThatSlotThrough()
        {
            LogAssert.Expect(
                LogType.Error,
                new Regex("VectorSwizzleConverter.*not a declared Vector4Component"));

            Assert.AreEqual(
                new Vector4(4f, 3f, 2f, 4f),
                new VectorSwizzleConverter(Comp.W, Comp.Z, Comp.Y, (Comp)4)
                    .Convert(new Vector4(1f, 2f, 3f, 4f)));
        }

        private static Vector2 Swizzle(VectorSwizzleConverter converter, Vector2 value) =>
            ((IConverter<Vector2, Vector2>)converter).Convert(value);

        private static Vector3 Swizzle(VectorSwizzleConverter converter, Vector3 value) =>
            ((IConverter<Vector3, Vector3>)converter).Convert(value);
    }
}
