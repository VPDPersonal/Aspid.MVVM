using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="Vector2CombineConverter"/>'s missing-target degrade path, exercised
    /// through <see cref="BoxCollider2DOffsetCombineConverter"/> and a stub subclass.
    /// </summary>
    [TestFixture]
    public sealed class Vector2CombineConverterTests : SceneFixture
    {
        // An unassigned Inspector reference is the normal state of a half-built prefab. Reading the
        // collider's offset would throw and take every binder queued behind this one down with it.
        [Test]
        public void MissingTarget_ReturnsTheInputRatherThanThrowing()
        {
            // Named in full: the message has to say which converter is empty, and GetType().Name is
            // what makes it the subclass rather than the shared base.
            LogAssert.Expect(LogType.Error, new Regex("BoxCollider2DOffsetCombineConverter.*no target assigned"));

            var value = new Vector2(1f, 2f);

            Assert.AreEqual(value, new BoxCollider2DOffsetCombineConverter().Convert(value));
        }

        // Every push, not once per converter: an empty Inspector reference stays loud until somebody
        // fills it in. LogAssert fails the test on any error the fixture did not ask for, so three
        // expectations against three pushes is the assertion that the diagnostic is not muted after
        // the first one.
        [Test]
        public void MissingTarget_ReportsOnEveryPush()
        {
            LogAssert.Expect(LogType.Error, new Regex("no target assigned"));
            LogAssert.Expect(LogType.Error, new Regex("no target assigned"));
            LogAssert.Expect(LogType.Error, new Regex("no target assigned"));

            var converter = new BoxCollider2DOffsetCombineConverter();
            converter.Convert(new Vector2(1f, 2f));
            converter.Convert(new Vector2(3f, 4f));
            converter.Convert(new Vector2(5f, 6f));
        }

        // The Vector3 entry point degrades to the *narrowed* input: z is dropped at the call, so what
        // comes back is not the value that was pushed.
        [Test]
        public void Vector3_MissingTarget_ReturnsTheInputWithoutItsZ()
        {
            LogAssert.Expect(LogType.Error, new Regex("no target assigned"));

            Assert.AreEqual(
                new Vector2(1f, 2f),
                new BoxCollider2DOffsetCombineConverter().Convert(new Vector3(1f, 2f, 3f)));
        }

        // The pair below is what stops the degrade assertion from being vacuous. Every shipped 2D
        // converter keeps its target in a private [SerializeField] and exposes no mode, so a live
        // target and a mode other than XY can only come from a stub.
        [Test]
        public void ModeX_LiveTarget_TakesYFromTheReferenceVector()
        {
            var target = Spawn(nameof(ModeX_LiveTarget_TakesYFromTheReferenceVector));
            var converter = new CombineStub(target.transform, new Vector2(10f, 20f), Vector2CombineConverter.Mode.X);

            Assert.AreEqual(new Vector2(1f, 20f), converter.Convert(new Vector2(1f, 2f)));
        }

        // Same mode, no target: the y that a live reference would have supplied stays as it arrived,
        // so "returns the input" is a genuine degrade and not the identity XY gives for free.
        [Test]
        public void ModeX_MissingTarget_KeepsTheInputY()
        {
            LogAssert.Expect(LogType.Error, new Regex("no target assigned"));

            var converter = new CombineStub(null, new Vector2(10f, 20f), Vector2CombineConverter.Mode.X);

            Assert.AreEqual(new Vector2(1f, 2f), converter.Convert(new Vector2(1f, 2f)));
        }

        private sealed class CombineStub : Vector2CombineConverter
        {
            private readonly Vector2 _to;
            private readonly Component _target;

            public CombineStub(Component target, Vector2 to, Mode mode)
                : base(mode)
            {
                _target = target;
                _to = to;
            }

            protected override Component Target => _target;

            // Throws when there is no target, so a guard that ran after the read would fail here
            // instead of passing by accident.
            protected override Vector2 VectorTo =>
                _target == null ? throw new NullReferenceException(nameof(VectorTo)) : _to;
        }
    }
}
