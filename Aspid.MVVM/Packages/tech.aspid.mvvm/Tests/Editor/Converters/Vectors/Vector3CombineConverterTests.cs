using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;
using Mode = Aspid.MVVM.StarterKit.Vector3CombineConverter.Mode;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="Vector3CombineConverter"/> — all seven <see cref="Mode"/> branches,
    /// the pre/post converter hooks, both entry points (<c>Convert(Vector2)</c> and
    /// <c>Convert(Vector3)</c>), and the unassigned-target degrade path.
    /// </summary>
    /// <remarks>
    /// The class is abstract, so both the target component and the reference vector are supplied
    /// here by stubs. The <c>Convert(Vector2)</c> rows pin a known defect: the 2D entry point widens
    /// its argument before the mode is applied, so the source z is always zero.
    /// </remarks>
    [TestFixture]
    internal sealed class Vector3CombineConverterTests
    {
        private static readonly Vector3 From = new(1f, 2f, 3f);
        private static readonly Vector3 To = new(10f, 20f, 30f);

        private GameObject _target;

        [OneTimeSetUp]
        public void CreateTargetObject() =>
            _target = new GameObject(nameof(Vector3CombineConverterTests));

        [OneTimeTearDown]
        public void DestroyTargetObject() =>
            UnityEngine.Object.DestroyImmediate(_target);

        [TestCase(Mode.X, 1f, 20f, 30f)]
        [TestCase(Mode.Y, 10f, 2f, 30f)]
        [TestCase(Mode.Z, 10f, 20f, 3f)]
        [TestCase(Mode.XY, 1f, 2f, 30f)]
        [TestCase(Mode.XZ, 1f, 20f, 3f)]
        [TestCase(Mode.YZ, 10f, 2f, 3f)]
        [TestCase(Mode.XYZ, 1f, 2f, 3f)]
        public void Convert_Vector3_TakesTheNamedAxesFromTheInput(Mode mode, float x, float y, float z) =>
            Assert.AreEqual(new Vector3(x, y, z), NewStub(mode).Convert(From));

        [Test]
        public void Convert_DefaultMode_IsXyz() =>
            Assert.AreEqual(From, NewStub(Mode.XYZ).Convert(From));

        // Known defect: Convert(Vector2) has no dedicated combine path, so the argument widens to
        // (x, y, 0) before the mode runs and the source z is lost. Characterisation only — the fix
        // changes existing scenes and is deliberately out of the Phase 0 batch.
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
                NewStub(Mode.X, pre: new Offset(1f), post: null).Convert(From));

        [Test]
        public void Convert_PostConverter_RunsOnTheCombinedResult() =>
            Assert.AreEqual(
                new Vector3(2f, 21f, 31f),
                NewStub(Mode.X, pre: null, post: new Offset(1f)).Convert(From));

        [Test]
        public void Convert_BothHooks_RunInOrder() =>
            Assert.AreEqual(
                new Vector3(3f, 21f, 31f),
                NewStub(Mode.X, pre: new Offset(1f), post: new Offset(1f)).Convert(From));

        // An unassigned Inspector reference degrades to the input instead of throwing. VectorTo
        // throws in the stub to prove the guard short-circuits before the reference is ever read.
        [Test]
        public void Convert_MissingTarget_ReturnsTheInputUnchanged()
        {
            LogAssert.Expect(LogType.Error, new Regex("no target assigned"));

            Assert.AreEqual(From, new MissingTargetStub(Mode.XYZ).Convert(From));
        }

        [Test]
        public void Convert_MissingTarget_LogsOncePerInstance()
        {
            LogAssert.Expect(LogType.Error, new Regex("no target assigned"));

            var converter = new MissingTargetStub(Mode.XYZ);
            converter.Convert(From);
            converter.Convert(From);
            converter.Convert(From);
        }

        private Stub NewStub(Mode mode) =>
            new(_target.transform, To, mode);

        private Stub NewStub(Mode mode, IConverterVector3 pre, IConverterVector3 post) =>
            new(_target.transform, To, mode, pre, post);

        private sealed class Stub : Vector3CombineConverter
        {
            private readonly Vector3 _to;
            private readonly Component _target;

            public Stub(Component target, Vector3 to, Mode mode)
                : base(mode)
            {
                _target = target;
                _to = to;
            }

            public Stub(Component target, Vector3 to, Mode mode, IConverterVector3 pre, IConverterVector3 post)
                : base(mode, pre, post)
            {
                _target = target;
                _to = to;
            }

            protected override Component Target => _target;

            protected override Vector3 VectorTo => _to;
        }

        private sealed class MissingTargetStub : Vector3CombineConverter
        {
            public MissingTargetStub(Mode mode)
                : base(mode) { }

            protected override Component Target => null;

            protected override Vector3 VectorTo => throw new NullReferenceException();
        }

        private sealed class Offset : IConverterVector3
        {
            private readonly float _amount;

            public Offset(float amount) => _amount = amount;

            public Vector3 Convert(Vector3 value) => value + Vector3.one * _amount;
        }
    }
}
