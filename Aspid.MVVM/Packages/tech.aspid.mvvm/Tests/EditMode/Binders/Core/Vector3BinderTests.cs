using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for <c>SetValue</c> overload resolution on the <see cref="IVector3Binder"/> families.
    /// </summary>
    /// <remarks>
    /// <see cref="Vector3"/> converts implicitly to <see cref="Vector2"/>, so a class-level <c>SetValue(Vector2)</c> would capture
    /// <c>SetValue(Vector3)</c> calls and drop Z; the 2D entry point must stay a default interface implementation.
    /// </remarks>
    [TestFixture]
    public sealed class Vector3BinderTests : SceneFixture
    {
        private static readonly Vector3 Applied = new(2f, 3f, 4f);

        [Test]
        public void ComponentVector3MonoBinder_DirectSetValue_KeepsZ()
        {
            var binder = CreateMonoBinder();

            binder.SetValue(Applied);

            Assert.AreEqual(Applied, binder.Applied, "The direct call lost the Z component");
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
        public void ComponentVector3MonoBinder_Vector2Channel_StillPromotesWithZeroZ()
        {
            var binder = CreateMonoBinder();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(2f, 3f));

            Assert.AreEqual(new Vector3(2f, 3f, 0f), binder.Applied);
        }

        [Test]
        public void TargetVector3Binder_DirectSetValue_KeepsZ()
        {
            var binder = new TestTargetVector3Binder(new Vector3Holder());

            binder.SetValue(Applied);

            Assert.AreEqual(Applied, binder.Applied, "The direct call lost the Z component");
        }

        [Test]
        public void TargetVector3Binder_Vector2Channel_StillPromotesWithZeroZ()
        {
            var binder = new TestTargetVector3Binder(new Vector3Holder());

            ((IBinder<Vector2>)binder).SetValue(new Vector2(2f, 3f));

            Assert.AreEqual(new Vector3(2f, 3f, 0f), binder.Applied);
        }

        private TestComponentVector3Binder CreateMonoBinder()
        {
            var gameObject = Spawn("Vector3Binder");

            gameObject.AddComponent<Vector3Component>();
            return gameObject.AddComponent<TestComponentVector3Binder>();
        }
    }

    internal sealed class TestComponentVector3Binder : ComponentMonoBinder<Vector3Component, Vector3>, IVector3Binder
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

    internal sealed class TestTargetVector3Binder : TargetBinder<Vector3Holder, Vector3>, IVector3Binder
    {
        public TestTargetVector3Binder(Vector3Holder target)
            : base(target, converter: null, BindMode.OneWay) { }

        public Vector3 Applied => Target.Value;

        protected override Vector3 Property
        {
            get => Target.Value;
            set => Target.Value = value;
        }
    }
}
