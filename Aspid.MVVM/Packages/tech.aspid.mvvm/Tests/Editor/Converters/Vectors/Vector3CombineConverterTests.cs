using UnityEngine;
using NUnit.Framework;
using Mode = Aspid.MVVM.StarterKit.Vector3CombineConverter.Mode;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="Vector3CombineConverter"/> — all seven <see cref="Mode"/> branches,
    /// the pre/post converter hooks, and both entry points (<c>Convert(Vector2)</c> and
    /// <c>Convert(Vector3)</c>).
    /// </summary>
    /// <remarks>
    /// The class is abstract, so the reference vector is supplied here by a stub rather than by a
    /// scene component. The <c>Convert(Vector2)</c> row pins a known defect: the 2D entry point
    /// widens its argument before the mode is applied, so the source z is always zero.
    /// </remarks>
    [TestFixture]
    internal sealed class Vector3CombineConverterTests
    {
        private static readonly Vector3 From = new(1f, 2f, 3f);
        private static readonly Vector3 To = new(10f, 20f, 30f);

        [TestCase(Mode.X, 1f, 20f, 30f)]
        [TestCase(Mode.Y, 10f, 2f, 30f)]
        [TestCase(Mode.Z, 10f, 20f, 3f)]
        [TestCase(Mode.XY, 1f, 2f, 30f)]
        [TestCase(Mode.XZ, 1f, 20f, 3f)]
        [TestCase(Mode.YZ, 10f, 2f, 3f)]
        [TestCase(Mode.XYZ, 1f, 2f, 3f)]
        public void Convert_Vector3_TakesTheNamedAxesFromTheInput(Mode mode, float x, float y, float z) =>
            Assert.AreEqual(new Vector3(x, y, z), new Stub(To, mode).Convert(From));

        [Test]
        public void Convert_DefaultConstructed_UsesXyz() =>
            Assert.AreEqual(From, new Stub(To).Convert(From));

        // Known defect: Convert(Vector2) has no dedicated combine path, so the argument widens to
        // (x, y, 0) before the mode runs and the source z is lost. Characterisation only — the fix
        // changes existing scenes and is deliberately out of the Phase 0 batch.
        [TestCase(Mode.XYZ, 1f, 2f, 0f)]
        [TestCase(Mode.Z, 10f, 20f, 0f)]
        [TestCase(Mode.XZ, 1f, 20f, 0f)]
        [TestCase(Mode.XY, 1f, 2f, 30f)]
        public void Convert_Vector2_WidensBeforeCombining(Mode mode, float x, float y, float z) =>
            Assert.AreEqual(new Vector3(x, y, z), new Stub(To, mode).Convert(new Vector2(1f, 2f)));

        [Test]
        public void Convert_PreConverter_RunsBeforeTheModeSelection() =>
            Assert.AreEqual(
                new Vector3(2f, 20f, 30f),
                new Stub(To, Mode.X, pre: new Offset(1f), post: null).Convert(From));

        [Test]
        public void Convert_PostConverter_RunsOnTheCombinedResult() =>
            Assert.AreEqual(
                new Vector3(2f, 21f, 31f),
                new Stub(To, Mode.X, pre: null, post: new Offset(1f)).Convert(From));

        [Test]
        public void Convert_BothHooks_RunInOrder() =>
            Assert.AreEqual(
                new Vector3(3f, 21f, 31f),
                new Stub(To, Mode.X, pre: new Offset(1f), post: new Offset(1f)).Convert(From));

        [Test]
        [Ignore("Fixed in PR 2 — null-guards. An unassigned Inspector reference must not throw.")]
        public void Convert_MissingTarget_ReturnsTheInputUnchanged() =>
            Assert.AreEqual(From, new MissingTargetStub(Mode.XYZ).Convert(From));

        private sealed class Stub : Vector3CombineConverter
        {
            private readonly Vector3 _to;

            public Stub(Vector3 to)
                : base(Mode.XYZ) => _to = to;

            public Stub(Vector3 to, Mode mode)
                : base(mode) => _to = to;

            public Stub(Vector3 to, Mode mode, IConverterVector3 pre, IConverterVector3 post)
                : base(mode, pre, post) => _to = to;

            protected override Vector3 VectorTo => _to;
        }

        private sealed class MissingTargetStub : Vector3CombineConverter
        {
            public MissingTargetStub(Mode mode)
                : base(mode) { }

            protected override Vector3 VectorTo => throw new System.NullReferenceException();
        }

        private sealed class Offset : IConverterVector3
        {
            private readonly float _amount;

            public Offset(float amount) => _amount = amount;

            public Vector3 Convert(Vector3 value) => value + Vector3.one * _amount;
        }
    }
}
