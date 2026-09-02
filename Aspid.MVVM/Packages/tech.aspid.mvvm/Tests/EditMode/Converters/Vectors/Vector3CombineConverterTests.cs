using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using Mode = Aspid.MVVM.StarterKit.Vector3CombineConverter.Mode;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="Vector3CombineConverter"/> — all seven <see cref="Mode"/> branches,
    /// the pre/post converter hooks, both entry points (<c>Convert(Vector2)</c> and
    /// <c>Convert(Vector3)</c>), and the unassigned-target degrade path of the base.
    /// </summary>
    /// <remarks>
    /// The class is abstract, so the reference vector is supplied here by a stub. The
    /// <c>Convert(Vector2)</c> rows pin a known defect: the 2D entry point widens its argument
    /// before the mode is applied, so the source z is always zero.
    /// </remarks>
    [TestFixture]
    public sealed class Vector3CombineConverterTests : SceneFixture
    {
        private static readonly Vector3 _from = new(1f, 2f, 3f);
        private static readonly Vector3 _to = new(10f, 20f, 30f);

        [TestCase(Mode.X, 1f, 20f, 30f)]
        [TestCase(Mode.Y, 10f, 2f, 30f)]
        [TestCase(Mode.Z, 10f, 20f, 3f)]
        [TestCase(Mode.XY, 1f, 2f, 30f)]
        [TestCase(Mode.XZ, 1f, 20f, 3f)]
        [TestCase(Mode.YZ, 10f, 2f, 3f)]
        [TestCase(Mode.XYZ, 1f, 2f, 3f)]
        public void Convert_Vector3_TakesTheNamedAxesFromTheInput(Mode mode, float x, float y, float z) =>
            Assert.AreEqual(new Vector3(x, y, z), NewStub(mode).Convert(_from));

        [Test]
        public void Convert_DefaultMode_IsXyz() =>
            Assert.AreEqual(_from, NewStub(Mode.XYZ).Convert(_from));

        // Known defect, characterisation only: Convert(Vector2) has no dedicated combine path, so the
        // argument widens to (x, y, 0) before the mode runs and the source z is lost.
        [TestCase(Mode.XYZ, 1f, 2f, 0f)]
        [TestCase(Mode.Z, 10f, 20f, 0f)]
        [TestCase(Mode.XZ, 1f, 20f, 0f)]
        [TestCase(Mode.XY, 1f, 2f, 30f)]
        public void Convert_Vector2_WidensBeforeCombining(Mode mode, float x, float y, float z) =>
            Assert.AreEqual(new Vector3(x, y, z), NewStub(mode).Convert(new Vector2(1f, 2f)));

        [Test]
        public void Convert_PreConverter_RunsBeforeTheModeSelection() =>
            Assert.AreEqual(
                new Vector3(2f, 20f, 30f),
                NewStub(Mode.X, pre: new Offset(1f), post: null).Convert(_from));

        [Test]
        public void Convert_PostConverter_RunsOnTheCombinedResult() =>
            Assert.AreEqual(
                new Vector3(2f, 21f, 31f),
                NewStub(Mode.X, pre: null, post: new Offset(1f)).Convert(_from));

        [Test]
        public void Convert_BothHooks_RunInOrder() =>
            Assert.AreEqual(
                new Vector3(3f, 21f, 31f),
                NewStub(Mode.X, pre: new Offset(1f), post: new Offset(1f)).Convert(_from));

        // The null guard lives in the base: an unassigned target logs an error and the input
        // comes back unchanged.
        [Test]
        public void Convert_MissingTarget_LogsAndReturnsTheInput()
        {
            LogAssert.Expect(LogType.Error, new Regex("no target assigned"));

            Assert.AreEqual(_from, new BoxColliderCenterCombineConverter().Convert(_from));
        }

        private Stub NewStub(Mode mode) =>
            new(Spawn("Vector3CombineHost").transform, _to, mode);

        private Stub NewStub(Mode mode, IConverter<Vector3, Vector3> pre, IConverter<Vector3, Vector3> post) =>
            new(Spawn("Vector3CombineHost").transform, _to, mode, pre, post);

        private sealed class Stub : Vector3CombineConverter
        {
            private readonly Vector3 _vectorTo;
            private readonly Component _target;

            public Stub(Component target, Vector3 to, Mode mode)
                : base(mode)
            {
                _target = target;
                _vectorTo = to;
            }

            public Stub(Component target, Vector3 to, Mode mode, IConverter<Vector3, Vector3> pre, IConverter<Vector3, Vector3> post)
                : base(mode, pre, post)
            {
                _target = target;
                _vectorTo = to;
            }

            protected override Component Target => _target;

            protected override Vector3 VectorTo => _vectorTo;
        }

        private sealed class Offset : IConverter<Vector3, Vector3>
        {
            private readonly float _amount;

            public Offset(float amount) => _amount = amount;

            public Vector3 Convert(Vector3 value) => value + Vector3.one * _amount;
        }
    }
}
