using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="GenericOneWayToSourceBinder{T}"/> and <see cref="GenericOneWayToSourceBinder{TTarget,T}"/>.
    /// </summary>
    [TestFixture]
    public sealed class GenericOneWayToSourceBinderTests
    {
        [Test]
        public void SubscribeCtor_NullSubscribe_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new GenericOneWayToSourceBinder<int>(subscribe: null));

        [Test]
        public void SubscribeCtor_InvokingTheCallback_RaisesValueChanged()
        {
            Action<int>? callback = null;
            var binder = new GenericOneWayToSourceBinder<int>(subscribe: c => callback = c);

            var received = new List<int>();
            binder.ValueChanged += received.Add;
            callback!.Invoke(5);

            Assert.AreEqual(new[] { 5 }, received);
        }

        [Test]
        public void FactoryCtor_BothFactoriesNull_Throws() =>
            Assert.Throws<ArgumentException>(() => new GenericOneWayToSourceBinder<int>());

        [Test]
        public void FactoryCtor_GetValueOnBound_IsPushedOnBind()
        {
            var binder = new GenericOneWayToSourceBinder<int>(getValueOnBound: () => 3);
            var member = new OneWayToSourceBindableMember<int>(_ => { });

            binder.Bind(member);

            Assert.AreEqual(3, member.Value);
        }

        [Test]
        public void FactoryCtor_GetValueOnUnbinding_IsPushedOnUnbind()
        {
            var binder = new GenericOneWayToSourceBinder<int>(getValueOnUnbinding: () => 8);
            var member = new OneWayToSourceBindableMember<int>(_ => { });

            binder.Bind(member);
            binder.Unbind();

            Assert.AreEqual(8, member.Value);
        }

        [Test]
        public void TargetSubscribeCtor_NullTarget_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => new GenericOneWayToSourceBinder<object, int>(null, (_, _) => { }));

        [Test]
        public void TargetSubscribeCtor_NullSubscribe_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => new GenericOneWayToSourceBinder<object, int>(new object(), (Action<object, Action<int>>)null));

        [Test]
        public void TargetSubscribeCtor_ReceivesTheTargetAndTheCallback()
        {
            var target = new object();
            object? receivedTarget = null;
            Action<int>? callback = null;
            var binder = new GenericOneWayToSourceBinder<object, int>(target, (t, c) =>
            {
                receivedTarget = t;
                callback = c;
            });

            var received = new List<int>();
            binder.ValueChanged += received.Add;
            callback!.Invoke(4);

            Assert.AreSame(target, receivedTarget);
            Assert.AreEqual(new[] { 4 }, received);
        }

        [Test]
        public void TargetFactoryCtor_NullTarget_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => new GenericOneWayToSourceBinder<object, int>(null, getValueOnBound: _ => 1));

        [Test]
        public void TargetFactoryCtor_BothFactoriesNull_Throws() =>
            Assert.Throws<ArgumentException>(() => new GenericOneWayToSourceBinder<object, int>(new object()));

        [Test]
        public void TargetFactoryCtor_GetValueOnBound_IsPushedWithTheTargetOnBind()
        {
            var target = new object();
            object? receivedTarget = null;
            var binder = new GenericOneWayToSourceBinder<object, int>(target, getValueOnBound: t =>
            {
                receivedTarget = t;
                return 6;
            });
            var member = new OneWayToSourceBindableMember<int>(_ => { });

            binder.Bind(member);

            Assert.AreSame(target, receivedTarget);
            Assert.AreEqual(6, member.Value);
        }
    }
}
