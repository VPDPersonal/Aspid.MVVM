using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="VectorToFloatConverter"/> — the component readings, the
    /// <see cref="VectorComponent.Dot"/> direction, and the undeclared-value guards.
    /// </summary>
    [TestFixture]
    public sealed class VectorToFloatConverterTests
    {
        [TestCase(VectorComponent.X, 3f)]
        [TestCase(VectorComponent.Y, 4f)]
        [TestCase(VectorComponent.Magnitude, 5f)]
        [TestCase(VectorComponent.SqrMagnitude, 25f)]
        // Dot reached through the component-only ctor leaves the direction at its default, which is
        // up rather than right — so the reading is the y, and a converter that defaulted to right
        // would answer 3 here.
        [TestCase(VectorComponent.Dot, 4f)]
        public void VectorToFloat_Vector2_Measures(VectorComponent component, float expected) =>
            Assert.AreEqual(
                expected,
                AsVector2(new VectorToFloatConverter(component)).Convert(new Vector2(3f, 4f)),
                1e-4f);

        [Test]
        public void VectorToFloat_Vector2_DefaultConstructed_MeasuresLength() =>
            Assert.AreEqual(
                5f,
                AsVector2(new VectorToFloatConverter()).Convert(new Vector2(3f, 4f)),
                1e-4f);

        // The direction is used raw, not normalized: a direction of unit length reads as the signed
        // distance along it, one twice as long doubles that reading, and an unset one reads zero for
        // every input. The negative row is the one that proves the reading is signed rather than a
        // distance.
        [TestCase(1f, 0f, 3f)]
        [TestCase(0f, 1f, 4f)]
        [TestCase(-1f, 0f, -3f)]
        [TestCase(0f, 2f, 8f)]
        [TestCase(0f, 0f, 0f)]
        public void VectorToFloat_Vector2Dot_IsTheRawProductWithTheAuthoredDirection(
            float x,
            float y,
            float expected) =>
            Assert.AreEqual(
                expected,
                AsVector2(new VectorToFloatConverter(new Vector4(x, y, 0f, 0f))).Convert(new Vector2(3f, 4f)),
                1e-4f);

        // Passing a direction is the only way to select Dot without also naming the component, so the
        // ctor has to set the component itself — otherwise the converter still measures length and
        // the authored direction is never read.
        [Test]
        public void VectorToFloat_DirectionCtor_SelectsDotWithoutBeingTold() =>
            Assert.AreEqual(
                3f,
                AsVector2(new VectorToFloatConverter(new Vector4(1f, 0f, 0f, 0f))).Convert(new Vector2(3f, 4f)),
                1e-4f);

        [TestCase(0f, 1f, 0f, 4f)]
        [TestCase(0f, 0f, 2f, 10f)]
        [TestCase(-1f, 0f, 0f, -3f)]
        public void VectorToFloat_Dot_IsTheRawProductWithTheAuthoredDirection(
            float x,
            float y,
            float z,
            float expected) =>
            Assert.AreEqual(
                expected,
                new VectorToFloatConverter(new Vector4(x, y, z, 0f)).Convert(new Vector3(3f, 4f, 5f)),
                1e-4f);

        [Test]
        public void VectorToFloat_Dot_DefaultDirectionIsUp() =>
            Assert.AreEqual(
                4f,
                new VectorToFloatConverter(VectorComponent.Dot).Convert(new Vector3(3f, 4f, 5f)),
                1e-4f);

        [Test]
        public void VectorToFloat_Vector4_ReadsTheFourthComponent() =>
            Assert.AreEqual(
                6f,
                ((IConverter<Vector4, float>)new VectorToFloatConverter(VectorComponent.W))
                    .Convert(new Vector4(3f, 4f, 5f, 6f)),
                1e-4f);

        // Declaration order is the number Unity stores in the scene, so Dot had to be appended rather
        // than filed next to the axes it belongs with, and W after it when the converter grew a
        // four-component width. Filing either in place would repoint every field already authored.
        [TestCase(VectorComponent.X, 0)]
        [TestCase(VectorComponent.Y, 1)]
        [TestCase(VectorComponent.Z, 2)]
        [TestCase(VectorComponent.Magnitude, 3)]
        [TestCase(VectorComponent.SqrMagnitude, 4)]
        [TestCase(VectorComponent.Dot, 5)]
        [TestCase(VectorComponent.W, 6)]
        public void VectorComponent_StoredValue_KeepsDotAndWAppendedLast(VectorComponent component, int stored) =>
            Assert.AreEqual(stored, (int)component);

        // A component the bound width does not carry is a misconfiguration, not a measurement: it is
        // reported on every push and reads as zero rather than as a length.
        [Test]
        public void VectorToFloat_ComponentTheWidthLacks_IsReportedAndReadsZero()
        {
            LogAssert.Expect(LogType.Error, new Regex("VectorToFloatConverter.*not one a Vector2 carries"));

            Assert.AreEqual(
                0f,
                AsVector2(new VectorToFloatConverter(VectorComponent.Z)).Convert(new Vector2(3f, 4f)),
                1e-6f);
        }

        // The setting is a serialized field rather than an argument, so an undeclared value — corrupted
        // YAML or a stray cast — is reported on every push and zero is read instead, rather than
        // throwing the binding down.
        [Test]
        public void VectorToFloat_UndeclaredComponent_ReportsItAndReadsZero()
        {
            LogAssert.Expect(LogType.Error, new Regex("VectorToFloatConverter.*not a declared VectorComponent"));

            Assert.AreEqual(
                0f,
                new VectorToFloatConverter((VectorComponent)99).Convert(new Vector3(3f, 4f, 0f)),
                1e-6f);
        }

        [TestCase(VectorComponent.X, 3f)]
        [TestCase(VectorComponent.Y, 4f)]
        [TestCase(VectorComponent.Magnitude, 5f)]
        [TestCase(VectorComponent.SqrMagnitude, 25f)]
        public void VectorToFloat_Vector3_Measures(VectorComponent component, float expected) =>
            Assert.AreEqual(
                expected,
                new VectorToFloatConverter(component).Convert(new Vector3(3f, 4f, 0f)),
                1e-4f);

        private static IConverter<Vector2, float> AsVector2(VectorToFloatConverter converter) => converter;
    }
}
