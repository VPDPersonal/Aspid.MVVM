using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="DelegateTwoWayBinder{T}"/> and <see cref="DelegateTwoWayBinder{TTarget,T}"/>.
    /// </summary>
    [TestFixture]
    public sealed class DelegateTwoWayBinderTests
    {
        [Test]
        public void FactoryCtor_NullSetValue_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new DelegateTwoWayBinder<int>(setValue: null));

        [Test]
        public void SubscribeCtor_NullSubscribe_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => new DelegateTwoWayBinder<int>(subscribe: null, setValue: _ => { }));

        [Test]
        public void SetValue_ForwardsToTheSetter()
        {
            var received = 0;
            var binder = new DelegateTwoWayBinder<int>(value => received = value);

            ((IBinder<int>)binder).SetValue(7);

            Assert.AreEqual(7, received);
        }

        [Test]
        public void SubscribeCtor_InvokingTheCallback_RaisesValueChanged()
        {
            Action<int>? callback = null;
            var binder = new DelegateTwoWayBinder<int>(c => callback = c, _ => { });

            var received = new List<int>();
            binder.ValueChanged += received.Add;
            callback!.Invoke(5);

            Assert.AreEqual(new[] { 5 }, received);
        }

        [Test]
        public void GetValueOnBound_IsPushedOnBind()
        {
            var received = new List<int>();
            var binder = new DelegateTwoWayBinder<int>(_ => { }, getValueOnBound: () => 3);
            var member = new TwoWayBindableMember<int>(0, received.Add);

            binder.Bind(member);

            Assert.AreEqual(new[] { 3 }, received);
        }

        [Test]
        public void GetValueOnUnbinding_IsPushedOnUnbind()
        {
            var received = new List<int>();
            var binder = new DelegateTwoWayBinder<int>(_ => { }, getValueOnUnbinding: () => 8);
            var member = new TwoWayBindableMember<int>(0, received.Add);

            binder.Bind(member);
            binder.Unbind();

            Assert.AreEqual(new[] { 8 }, received);
        }

        [Test]
        public void TargetFactoryCtor_NullTarget_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => new DelegateTwoWayBinder<object, int>(null, (_, _) => { }, getValueOnBound: _ => 1));

        [Test]
        public void TargetFactoryCtor_NullSetValue_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => new DelegateTwoWayBinder<object, int>(new object(), (Action<object, int>)null, getValueOnBound: _ => 1));

        // Unlike the T-only factory ctor, the target-taking one rejects the case where neither
        // factory is provided — checked before the target or setValue are.
        [Test]
        public void TargetFactoryCtor_BothFactoriesNull_Throws() =>
            Assert.Throws<ArgumentException>(
                () => new DelegateTwoWayBinder<object, int>(new object(), (_, _) => { }));

        [Test]
        public void TargetSubscribeCtor_NullSubscribe_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => new DelegateTwoWayBinder<object, int>(new object(), null, (_, _) => { }));

        [Test]
        public void TargetSetValue_ForwardsTheTargetAndTheValue()
        {
            var target = new object();
            object? receivedTarget = null;
            var receivedValue = 0;
            var binder = new DelegateTwoWayBinder<object, int>(target, (t, value) =>
            {
                receivedTarget = t;
                receivedValue = value;
            }, getValueOnBound: _ => 0);

            ((IBinder<int>)binder).SetValue(9);

            Assert.AreSame(target, receivedTarget);
            Assert.AreEqual(9, receivedValue);
        }

        [Test]
        public void TargetSubscribeCtor_ReceivesTheTargetAndTheCallback()
        {
            var target = new object();
            object? receivedTarget = null;
            Action<int>? callback = null;
            var binder = new DelegateTwoWayBinder<object, int>(target, (t, c) =>
            {
                receivedTarget = t;
                callback = c;
            }, (_, _) => { });

            var received = new List<int>();
            binder.ValueChanged += received.Add;
            callback!.Invoke(4);

            Assert.AreSame(target, receivedTarget);
            Assert.AreEqual(new[] { 4 }, received);
        }

        [Test]
        public void TargetGetValueOnBound_IsPushedWithTheTargetOnBind()
        {
            var target = new object();
            object? receivedTarget = null;
            var received = new List<int>();
            var binder = new DelegateTwoWayBinder<object, int>(target, (_, _) => { }, t =>
            {
                receivedTarget = t;
                return 6;
            });
            var member = new TwoWayBindableMember<int>(0, received.Add);

            binder.Bind(member);

            Assert.AreSame(target, receivedTarget);
            Assert.AreEqual(new[] { 6 }, received);
        }
    }
}
