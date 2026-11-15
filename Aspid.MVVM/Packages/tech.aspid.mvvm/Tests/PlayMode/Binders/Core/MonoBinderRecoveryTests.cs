using System;
using NUnit.Framework;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class MonoBinderRecoveryTests : SceneFixture
    {
        [Test]
        public void ForeignOwnerCannotReleaseTheCurrentBinding()
        {
            var binder = Spawn<RuntimeTwoWayBinder>();
            var member = new TwoWayStructBindableMember<int>(1, _ => { });
            binder.Bind(new BindContext(member, this));
            binder.Unbind(new UnbindContext(binder));
            member.Value = 2;
            Assert.That(binder.IsBound, Is.True);
            Assert.That(binder.Value, Is.EqualTo(2));
            binder.Unbind(new UnbindContext(this));
            Assert.That(binder.IsBound, Is.False);
        }

        [TestCase("binding")]
        [TestCase("value")]
        [TestCase("attach")]
        [TestCase("bound")]
        public void DestructionInsideBindingCallbacksReleasesTheBinding(string stage)
        {
            var binder = Spawn<RuntimeTwoWayBinder>();
            var member = new TwoWayStructBindableMember<int>(1, _ => { });
            binder.Stage = "destroy-" + stage;
            binder.Bind(new BindContext(member, this));
            var calls = binder.Calls;
            member.Value = 2;
            Assert.That(binder.Calls, Is.EqualTo(calls));
            Assert.That(binder.IsBound || binder.Resource, Is.False);
        }

        [Test]
        public void RepeatedMonoBindPreservesTheOriginalSubscription()
        {
            var binder = Spawn<RuntimeTwoWayBinder>();
            var original = new TwoWayStructBindableMember<int>(1, _ => { });
            var other = new TwoWayStructBindableMember<int>(2, _ => { });
            binder.Bind(new BindContext(original, binder));
            Assert.Throws<InvalidOperationException>(() => binder.Bind(new BindContext(other, binder)));
            Assert.That(binder.IsBound, Is.True);
            original.Value = 3;
            Assert.That(binder.Value, Is.EqualTo(3));
            other.Value = 4;
            Assert.That(binder.Value, Is.EqualTo(3));
        }

        [Test]
        public void RuntimeDefaultsWorkWithoutEditorReset()
        {
            var forward = Spawn<RuntimeForwardBinder>();
            var source = Spawn<RuntimeSourceBinder>();
            var twoWay = Spawn<RuntimeTwoWayBinder>();
            Assert.That(forward.Mode, Is.EqualTo(BindMode.OneWay));
            Assert.That(source.Mode, Is.EqualTo(BindMode.OneWayToSource));
            Assert.That(twoWay.Mode, Is.EqualTo(BindMode.TwoWay));
            forward.Bind(new BindContext(new OneWayStructBindableMember<int>(5), forward));
            var received = 0;
            source.Bind(new BindContext(new OneWayToSourceStructBindableMember<int>(value => received = value), source));
            source.Emit(7);
            twoWay.Bind(new BindContext(new TwoWayStructBindableMember<int>(9, _ => { }), twoWay));
            Assert.That(forward.Value, Is.EqualTo(5));
            Assert.That(received, Is.EqualTo(7));
            Assert.That(twoWay.IsBound, Is.True);
        }

        [TestCase(BindMode.OneWay)]
        [TestCase(BindMode.TwoWay)]
        [TestCase(BindMode.OneTime)]
        [TestCase(BindMode.OneWayToSource)]
        public void AuthoredSerializedModeAndCodeConfigurationSurvive(BindMode mode)
        {
            var binder = Spawn<RuntimeTwoWayBinder>();
            JsonUtility.FromJsonOverwrite("{\"_mode\":" + (int)mode + "}", binder);
            Assert.That(binder.Mode, Is.EqualTo(mode));
            binder.Bind(new BindContext(new TwoWayStructBindableMember<int>(1, _ => { }), binder));
            Assert.Throws<InvalidOperationException>(() => binder.Mode = BindMode.OneWay);
            binder.Unbind(new UnbindContext(binder));
            binder.Mode = BindMode.OneTime;
            binder.Bind(new BindContext(new OneWayStructBindableMember<int>(2), binder));
            Assert.That(binder.Mode, Is.EqualTo(BindMode.OneTime));
            Assert.That(binder.Value, Is.EqualTo(2));
        }

        [TestCase("binding")]
        [TestCase("value")]
        [TestCase("attach")]
        [TestCase("bound")]
        [TestCase("mode")]
        [TestCase("reentrant-binding")]
        [TestCase("reentrant-value")]
        [TestCase("reentrant-attach")]
        [TestCase("reentrant-bound")]
        public void FailedMonoBindCanBeCleanedUpAndRetried(string stage)
        {
            var binder = Spawn<RuntimeTwoWayBinder>();
            var member = new TwoWayStructBindableMember<int>(1, _ => { });
            binder.Stage = stage;
            Assert.Throws<InvalidOperationException>(() => binder.Bind(new BindContext(member, binder)));
            binder.Unbind(new UnbindContext(binder));
            var calls = binder.Calls;
            member.Value = 2;
            Assert.That(binder.Calls, Is.EqualTo(calls));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.Resource, Is.False);
            binder.Stage = null;
            binder.Bind(new BindContext(member, binder));
            calls = binder.Calls;
            member.Value = 3;
            Assert.That(binder.Calls, Is.EqualTo(calls + 1));
        }

        [Test]
        public void FailedMonoUnbindDetachesEvenWhenBothHooksThrow()
        {
            var binder = Spawn<RuntimeTwoWayBinder>();
            var member = new TwoWayStructBindableMember<int>(1, _ => { });
            binder.Bind(new BindContext(member, binder));
            binder.Stage = "cleanup";
            var failures = Assert.Throws<AggregateException>(() => binder.Unbind(new UnbindContext(binder)));
            Assert.That(failures.InnerExceptions.Count, Is.EqualTo(2));
            Assert.That(failures.InnerExceptions[0].Message, Is.EqualTo("unbinding"));
            Assert.That(failures.InnerExceptions[1].Message, Is.EqualTo("unbound"));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.Resource, Is.False);
            binder.Unbind(new UnbindContext(binder));
            var calls = binder.Calls;
            member.Value = 3;
            Assert.That(binder.Calls, Is.EqualTo(calls));
        }
    }

    internal sealed class RuntimeForwardBinder : MonoBinder, IBinder<int>
    {
        public int Value;

        // Deliberately does not apply the Editor default, matching the player path.
        protected override void Reset() { }

        public void SetValue(int value) =>
            Value = value;
    }

    internal sealed class RuntimeSourceBinder : MonoBinder, IReverseBinder<int>
    {
        public event Action<int> ValueChanged;

        protected override BindMode DefaultMode =>
            BindMode.OneWayToSource;

        protected override void Reset() { }

        public void Emit(int value) =>
            ValueChanged?.Invoke(value);
    }

    internal sealed class RuntimeTwoWayBinder : MonoBinder, IBinder<int>, IReverseBinder<int>
    {
        public int Value;
        public int Calls;
        public bool Resource;
        public string Stage;

        public event Action<int> ValueChanged;

        protected override BindMode DefaultMode =>
            BindMode.TwoWay;

        protected override void Reset() { }

        public void SetValue(int value)
        {
            Calls++;
            Value = value;
            DestroyIfRequested("value");
            Rebind("value");
            if (Stage == "mode")
                Mode = BindMode.OneTime;
            if (Stage == "value")
                throw new InvalidOperationException("value");
        }

        protected override void OnBinding()
        {
            Resource = true;
            DestroyIfRequested("binding");
            Rebind("binding");
            if (Stage == "binding")
                throw new InvalidOperationException("binding");
        }

        protected override void OnAttach()
        {
            DestroyIfRequested("attach");
            Rebind("attach");
            if (Stage == "attach")
                throw new InvalidOperationException("attach");
        }

        protected override void OnBound()
        {
            DestroyIfRequested("bound");
            Rebind("bound");
            if (Stage == "bound")
                throw new InvalidOperationException("bound");
        }

        protected override void OnUnbinding()
        {
            if (Stage == "cleanup")
                throw new InvalidOperationException("unbinding");
        }

        protected override void OnDetach()
        {
            Resource = false;
            if (Stage == "cleanup")
                throw new InvalidOperationException("unbound");
        }

        private void DestroyIfRequested(string stage)
        {
            if (Stage == "destroy-" + stage)
                UnityEngine.Object.DestroyImmediate(this);
        }

        private void Rebind(string stage)
        {
            if (Stage == "reentrant-" + stage)
                Bind(new BindContext(new TwoWayStructBindableMember<int>(9, _ => { }), this));
        }
    }
}
