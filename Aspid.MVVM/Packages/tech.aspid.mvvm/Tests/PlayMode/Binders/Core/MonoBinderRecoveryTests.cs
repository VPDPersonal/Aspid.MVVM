using System;
using NUnit.Framework;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class MonoBinderRecoveryTests : SceneFixture
    {
        [Test]
        public void RuntimeDefaultsWorkWithoutEditorReset()
        {
            var forward = Spawn<RuntimeForwardBinder>();
            var source = Spawn<RuntimeSourceBinder>();
            var twoWay = Spawn<RuntimeTwoWayBinder>();
            Assert.That(forward.Mode, Is.EqualTo(BindMode.OneWay));
            Assert.That(source.Mode, Is.EqualTo(BindMode.OneWayToSource));
            Assert.That(twoWay.Mode, Is.EqualTo(BindMode.TwoWay));
            forward.Bind(new OneWayStructBindableMember<int>(5));
            var received = 0;
            source.Bind(new OneWayToSourceStructBindableMember<int>(value => received = value));
            source.Emit(7);
            twoWay.Bind(new TwoWayStructBindableMember<int>(9, _ => { }));
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
            binder.Bind(new TwoWayStructBindableMember<int>(1, _ => { }));
            Assert.Throws<InvalidOperationException>(() => binder.Mode = BindMode.OneWay);
            binder.Unbind();
            binder.Mode = BindMode.OneTime;
            binder.Bind(new OneWayStructBindableMember<int>(2));
            Assert.That(binder.Mode, Is.EqualTo(BindMode.OneTime));
            Assert.That(binder.Value, Is.EqualTo(2));
        }

        [TestCase("binding")]
        [TestCase("value")]
        [TestCase("bound")]
        [TestCase("mode")]
        public void FailedMonoBindRollsBackAndCanRetry(string stage)
        {
            var binder = Spawn<RuntimeTwoWayBinder>();
            var member = new TwoWayStructBindableMember<int>(1, _ => { });
            binder.Stage = stage;
            Assert.Throws<InvalidOperationException>(() => binder.Bind(member));
            binder.Unbind();
            var calls = binder.Calls;
            member.Value = 2;
            Assert.That(binder.Calls, Is.EqualTo(calls));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.Resource, Is.False);
            binder.Stage = null;
            binder.Bind(member);
            calls = binder.Calls;
            member.Value = 3;
            Assert.That(binder.Calls, Is.EqualTo(calls + 1));
        }

        [Test]
        public void FailedMonoUnbindDetachesEvenWhenBothHooksThrow()
        {
            var binder = Spawn<RuntimeTwoWayBinder>();
            var member = new TwoWayStructBindableMember<int>(1, _ => { });
            binder.Bind(member);
            binder.Stage = "cleanup";
            Assert.That(Assert.Throws<InvalidOperationException>(() => binder.Unbind()).Message, Is.EqualTo("unbinding"));
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.Resource, Is.False);
            binder.Unbind();
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
        protected override BindMode DefaultMode => BindMode.OneWayToSource;
        protected override void Reset() { }
        public event Action<int> ValueChanged;
        public void Emit(int value) =>
            ValueChanged?.Invoke(value);
    }

    internal sealed class RuntimeTwoWayBinder : MonoBinder, IBinder<int>, IReverseBinder<int>
    {
        public int Value;
        public int Calls;
        public bool Resource;
        public string Stage;
        protected override BindMode DefaultMode => BindMode.TwoWay;
        protected override void Reset() { }
        public event Action<int> ValueChanged;
        public void SetValue(int value)
        {
            Calls++;
            Value = value;
            if (Stage == "mode") Mode = BindMode.OneTime;
            if (Stage == "value") throw new InvalidOperationException("value");
        }
        protected override void OnBinding()
        {
            Resource = true;
            if (Stage == "binding") throw new InvalidOperationException("binding");
        }
        protected override void OnBound()
        {
            if (Stage == "bound") throw new InvalidOperationException("bound");
        }
        protected override void OnUnbinding()
        {
            if (Stage == "cleanup") throw new InvalidOperationException("unbinding");
        }
        protected override void OnUnbound()
        {
            Resource = false;
            if (Stage == "cleanup") throw new InvalidOperationException("unbound");
        }
    }
}
