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
    /// Coverage for <see cref="Vector3Vector4Converter"/> in both directions.
    /// </summary>
    [TestFixture]
    public sealed class Vector3Vector4ConverterTests
    {
        [Test]
        public void Vector3ToVector4_Convert_WritesTheConfiguredW() =>
            Assert.AreEqual(
                new Vector4(1f, 2f, 3f, 9f),
                new Vector3Vector4Converter(9f).Convert(new Vector3(1f, 2f, 3f)));

        // Unity's own implicit Vector3 -> Vector4 conversion already zeroes w, so this case would
        // pass against a converter that did nothing at all. It is the 9f case above that proves the
        // serialized field is read; this one only pins the default.
        [Test]
        public void Vector3ToVector4_DefaultConstructed_WritesZeroW() =>
            Assert.AreEqual(
                new Vector4(1f, 2f, 3f, 0f),
                new Vector3Vector4Converter().Convert(new Vector3(1f, 2f, 3f)));

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
                new Vector3Vector4Converter(w: 0f, drop).ConvertBack(new Vector4(1f, 2f, 3f, 4f)));

        [Test]
        public void Vector4ToVector3_DefaultConstructed_DropsW() =>
            Assert.AreEqual(
                new Vector3(1f, 2f, 3f),
                new Vector3Vector4Converter().ConvertBack(new Vector4(1f, 2f, 3f, 4f)));

        // Undoing Vector3Vector4Converter only holds for the W drop. Every other choice slides the
        // survivors down a slot, so the padding value the widening converter added comes back as
        // part of the position.
        [Test]
        public void Vector4ToVector3_DroppingAnythingButW_DoesNotUndoTheWidening()
        {
            var widened = new Vector3Vector4Converter(9f).Convert(new Vector3(1f, 2f, 3f));

            Assert.AreEqual(new Vector3(1f, 2f, 3f), new Vector3Vector4Converter(w: 0f, Comp.W).ConvertBack(widened));
            Assert.AreEqual(new Vector3(2f, 3f, 9f), new Vector3Vector4Converter(w: 0f, Comp.X).ConvertBack(widened));
        }

        // The default branch looks unreachable through the enum, but Unity keeps the raw int when a
        // serialized enum field outlives the member it named, so a renamed or reordered
        // Vector4Component lands here at runtime — reported on every push, with W dropped.
        [Test]
        public void Vector4ToVector3_UndeclaredComponent_ReportsItAndDropsW()
        {
            LogAssert.Expect(LogType.Error, new Regex("Vector3Vector4Converter.*not a declared Vector4Component"));

            Assert.AreEqual(
                new Vector3(1f, 2f, 3f),
                new Vector3Vector4Converter(w: 0f, (Comp)4).ConvertBack(new Vector4(1f, 2f, 3f, 4f)));
        }
    }
}
