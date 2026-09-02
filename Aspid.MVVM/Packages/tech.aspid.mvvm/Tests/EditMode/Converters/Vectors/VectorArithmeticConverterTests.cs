using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="VectorArithmeticConverter"/> — each <see cref="VectorOperation"/>, the
    /// zero-operand identities, and the undeclared-operation guard.
    /// </summary>
    [TestFixture]
    public sealed class VectorArithmeticConverterTests
    {
        [TestCase(VectorOperation.Add, 11f, 22f)]
        [TestCase(VectorOperation.Subtract, -9f, -18f)]
        [TestCase(VectorOperation.Scale, 10f, 40f)]
        [TestCase(VectorOperation.Divide, 0.1f, 0.1f)]
        public void Vector2Arithmetic_AppliesTheOperation(VectorOperation operation, float x, float y) =>
            AssertClose(
                new Vector2(x, y),
                AsWidth<Vector2>(new VectorArithmeticConverter(operation, new Vector4(10f, 20f, 0f, 0f)))
                    .Convert(new Vector2(1f, 2f)));

        // What a freshly added [SerializeReference] entry does before anything is typed into it: Add
        // with a zero operand, which has to be an identity rather than a collapse to zero.
        [Test]
        public void Vector2Arithmetic_DefaultConstructed_LeavesTheVectorAlone() =>
            AssertClose(
                new Vector2(1f, 2f),
                AsWidth<Vector2>(new VectorArithmeticConverter()).Convert(new Vector2(1f, 2f)));

        // A zero axis in the operand leaves that axis alone instead of handing a binder an infinity.
        // The x is in the case to prove the division still happens on the axis that can take one.
        [Test]
        public void Vector2Arithmetic_DivideByAZeroAxis_LeavesThatAxisAlone() =>
            AssertClose(
                new Vector2(0.5f, 2f),
                AsWidth<Vector2>(new VectorArithmeticConverter(VectorOperation.Divide, new Vector4(2f, 0f, 0f, 0f)))
                    .Convert(new Vector2(1f, 2f)));

        // The zero guard is `== 0f`, so an ordinary negative divisor is not caught by it and the sign
        // travels through the division as arithmetic says it should.
        [Test]
        public void Vector2Arithmetic_DivideByANegativeAxis_KeepsTheSignChange() =>
            AssertClose(
                new Vector2(-0.5f, -0.5f),
                AsWidth<Vector2>(new VectorArithmeticConverter(VectorOperation.Divide, new Vector4(-2f, -4f, 0f, 0f)))
                    .Convert(new Vector2(1f, 2f)));

        // The bounce a wall gives: the part along the normal is inverted, the part along the wall is
        // kept. A Reflect implemented as a plain negation would answer (-1, 1) here.
        [Test]
        public void Vector2Arithmetic_Reflect_BouncesOffTheOperandAsANormal() =>
            AssertClose(
                new Vector2(1f, 1f),
                AsWidth<Vector2>(new VectorArithmeticConverter(VectorOperation.Reflect, new Vector4(0f, 1f, 0f, 0f)))
                    .Convert(new Vector2(1f, -1f)));

        // Vector2.Reflect does not normalize its normal, and neither does the converter, so a normal
        // authored twice as long as unit quadruples the part it reflects. Typing (0, 2) into the
        // operand field does not mean "up" — it means "up, four times over".
        [Test]
        public void Vector2Arithmetic_Reflect_DoesNotNormalizeTheOperand() =>
            AssertClose(
                new Vector2(1f, 7f),
                AsWidth<Vector2>(new VectorArithmeticConverter(VectorOperation.Reflect, new Vector4(0f, 2f, 0f, 0f)))
                    .Convert(new Vector2(1f, -1f)));

        // An unset operand is a zero normal, whose reflection factor is zero, so the value walks
        // through unchanged — the opposite of what Scale does with the same unset operand.
        [Test]
        public void Vector2Arithmetic_Reflect_ZeroOperand_ReturnsTheInput() =>
            AssertClose(
                new Vector2(1f, -1f),
                AsWidth<Vector2>(new VectorArithmeticConverter(VectorOperation.Reflect, Vector4.zero))
                    .Convert(new Vector2(1f, -1f)));

        [Test]
        public void Vector2Arithmetic_Scale_ZeroOperand_CollapsesTheVector() =>
            AssertClose(
                Vector2.zero,
                AsWidth<Vector2>(new VectorArithmeticConverter(VectorOperation.Scale, Vector4.zero))
                    .Convert(new Vector2(1f, -1f)));

        // The setting is a serialized field rather than an argument, so an undeclared value — corrupted
        // YAML or a stray cast — is reported on every push and the operand is left out of it, rather
        // than throwing the binding down.
        [Test]
        public void Vector2Arithmetic_UndeclaredOperation_ReportsItAndPassesTheVectorThrough()
        {
            LogAssert.Expect(LogType.Error, new Regex("VectorArithmeticConverter.*not a declared VectorOperation"));

            AssertClose(
                new Vector2(1f, -1f),
                AsWidth<Vector2>(new VectorArithmeticConverter((VectorOperation)99, Vector4.one))
                    .Convert(new Vector2(1f, -1f)));
        }

        // Reflect is the one operation Unity has no Vector4 method for, so the four-component path is
        // the only hand-written arithmetic in the converter. The normal is W, which puts the whole
        // reflection on the component the narrower widths do not carry: 4 flips to -4 and nothing
        // else moves. A reflection that ignored W would answer the input unchanged.
        [Test]
        public void Vector4Arithmetic_Reflect_TurnsTheFourthComponentAround() =>
            AssertClose(
                new Vector4(1f, 2f, 3f, -4f),
                AsWidth<Vector4>(new VectorArithmeticConverter(VectorOperation.Reflect, new Vector4(0f, 0f, 0f, 1f)))
                    .Convert(new Vector4(1f, 2f, 3f, 4f)));

        // NUnit compares two vectors with Vector.Equals, which is exact float equality. Everything
        // expected above is the result of arithmetic, so the components are compared with a delta
        // instead — the exact-equality assertions are left for the cases that really are exact, such
        // as a collapse to zero.
        private static void AssertClose(Vector2 expected, Vector2 actual, float delta = 1e-4f)
        {
            Assert.AreEqual(expected.x, actual.x, delta, $"x of {actual}, expected {expected}");
            Assert.AreEqual(expected.y, actual.y, delta, $"y of {actual}, expected {expected}");
        }

        private static void AssertClose(Vector4 expected, Vector4 actual, float delta = 1e-4f)
        {
            Assert.AreEqual(expected.x, actual.x, delta, $"x of {actual}, expected {expected}");
            Assert.AreEqual(expected.y, actual.y, delta, $"y of {actual}, expected {expected}");
            Assert.AreEqual(expected.z, actual.z, delta, $"z of {actual}, expected {expected}");
            Assert.AreEqual(expected.w, actual.w, delta, $"w of {actual}, expected {expected}");
        }

        // Each vector converter serves Vector2, Vector3 and Vector4, and only the Vector3 width is a
        // public method — the other two are explicit interface implementations. Calling Convert on the
        // class with a Vector2 would compile and quietly widen it to the Vector3 width, so a test that
        // means one of the narrow widths has to ask for its interface, which is what this does.
        private static IConverter<T, T> AsWidth<T>(IConverter<T, T> converter) => converter;
    }
}
