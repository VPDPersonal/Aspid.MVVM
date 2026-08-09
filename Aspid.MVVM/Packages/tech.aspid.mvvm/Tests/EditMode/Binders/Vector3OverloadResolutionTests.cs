using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for <c>SetValue</c> overload resolution on the Vector3 binder bases.
    /// </summary>
    /// <remarks>
    /// <see cref="ComponentVector3MonoBinder{TComponent}"/> and <see cref="TargetVector3Binder{TTarget}"/> declare
    /// <c>SetValue(Vector2)</c> while inheriting <c>SetValue(Vector3)</c> from their base. C# builds the overload
    /// candidate set from the most derived type that declares an applicable member and looks no further, and
    /// <see cref="Vector3"/> converts implicitly to <see cref="Vector2"/> — so <c>SetValue(someVector3)</c> bound to
    /// the 2D overload and silently dropped Z. The binding path never saw this because it dispatches through
    /// <see cref="IBinder{T}"/>, whose interface map points at the base method; only direct calls from user code
    /// were affected. Each test therefore asserts both call shapes agree.
    /// </remarks>
    [TestFixture]
    public sealed class Vector3OverloadResolutionTests
    {
        private static readonly Vector3 Applied = new(2f, 3f, 4f);

        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        [Test]
        public void ComponentVector3MonoBinder_DirectSetValue_KeepsZ()
        {
            var binder = CreateMonoBinder();

            binder.SetValue(Applied);

            Assert.AreEqual(Applied, binder.Applied, "Прямой вызов потерял компоненту Z");
        }

        [Test]
        public void ComponentVector3MonoBinder_DirectSetValue_MatchesInterfaceDispatch()
        {
            var direct = CreateMonoBinder();
            var viaInterface = CreateMonoBinder();

            direct.SetValue(Applied);
            ((IBinder<Vector3>)viaInterface).SetValue(Applied);

            Assert.AreEqual(viaInterface.Applied, direct.Applied);
        }

        [Test]
        public void ComponentVector3MonoBinder_Vector2Overload_StillPromotesWithZeroZ()
        {
            var binder = CreateMonoBinder();

            binder.SetValue(new Vector2(2f, 3f));

            Assert.AreEqual(new Vector3(2f, 3f, 0f), binder.Applied);
        }

        [Test]
        public void TargetVector3Binder_DirectSetValue_KeepsZ()
        {
            var binder = new TestTargetVector3Binder(new Vector3Holder());

            binder.SetValue(Applied);

            Assert.AreEqual(Applied, binder.Applied, "Прямой вызов потерял компоненту Z");
        }

        [Test]
        public void TargetVector3Binder_Vector2Overload_StillPromotesWithZeroZ()
        {
            var binder = new TestTargetVector3Binder(new Vector3Holder());

            binder.SetValue(new Vector2(2f, 3f));

            Assert.AreEqual(new Vector3(2f, 3f, 0f), binder.Applied);
        }

        private TestComponentVector3Binder CreateMonoBinder()
        {
            var gameObject = new GameObject("Vector3Binder");
            _spawned.Add(gameObject);

            gameObject.AddComponent<Vector3Component>();
            return gameObject.AddComponent<TestComponentVector3Binder>();
        }
    }

    internal sealed class Vector3Component : MonoBehaviour
    {
        public Vector3 Value;
    }

    internal sealed class TestComponentVector3Binder : ComponentVector3MonoBinder<Vector3Component>
    {
        public Vector3 Applied => CachedComponent.Value;

        protected override Vector3 Property
        {
            get => CachedComponent.Value;
            set => CachedComponent.Value = value;
        }
    }

    internal sealed class Vector3Holder
    {
        public Vector3 Value;
    }

    internal sealed class TestTargetVector3Binder : TargetVector3Binder<Vector3Holder>
    {
        public TestTargetVector3Binder(Vector3Holder target)
            : base(target, converter: null) { }

        public Vector3 Applied => Target.Value;

        protected override Vector3 Property
        {
            get => Target.Value;
            set => Target.Value = value;
        }
    }
}
