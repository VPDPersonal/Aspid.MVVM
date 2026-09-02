using System;
using NUnit.Framework;
using UnityEngine.Events;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

#pragma warning disable CS0618 // the type under test is itself obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="UnityGenericOneWayToSourceBinder{T}"/> and <see cref="UnityGenericOneWayToSourceBinder{TTarget,T}"/>.
    /// </summary>
    [TestFixture]
    public sealed class UnityGenericOneWayToSourceBinderTests
    {
        [Test]
        public void SubscribeCtor_InvokingTheCallback_RaisesValueChanged()
        {
            UnityAction<int>? callback = null;
            var binder = new UnityGenericOneWayToSourceBinder<int>(subscribe: c => callback = c);

            var received = new List<int>();
            binder.ValueChanged += received.Add;
            callback!.Invoke(5);

            Assert.AreEqual(new[] { 5 }, received);
        }

        [Test]
        public void FactoryCtor_BothFactoriesNull_Throws() =>
            Assert.Throws<ArgumentException>(() => new UnityGenericOneWayToSourceBinder<int>());

        [Test]
        public void FactoryCtor_GetValueOnBound_IsPushedOnBind()
        {
            var binder = new UnityGenericOneWayToSourceBinder<int>(getValueOnBound: () => 3);
            var member = new OneWayToSourceBindableMember<int>(_ => { });

            binder.Bind(member);

            Assert.AreEqual(3, member.Value);
        }

        [Test]
        public void FactoryCtor_GetValueOnUnbinding_IsPushedOnUnbind()
        {
            var binder = new UnityGenericOneWayToSourceBinder<int>(getValueOnUnbinding: () => 8);
            var member = new OneWayToSourceBindableMember<int>(_ => { });

            binder.Bind(member);
            binder.Unbind();

            Assert.AreEqual(8, member.Value);
        }

        [Test]
        public void TargetSubscribeCtor_NullTarget_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => new UnityGenericOneWayToSourceBinder<object, int>(null, (UnityAction<object, UnityAction<int>>)null));

        [Test]
        public void TargetSubscribeCtor_ReceivesTheTargetAndTheCallback()
        {
            var target = new object();
            object? receivedTarget = null;
            UnityAction<int>? callback = null;
            var binder = new UnityGenericOneWayToSourceBinder<object, int>(target, (t, c) =>
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
                () => new UnityGenericOneWayToSourceBinder<object, int>(null, getValueOnBound: _ => 1));

        [Test]
        public void TargetFactoryCtor_BothFactoriesNull_Throws() =>
            Assert.Throws<ArgumentException>(() => new UnityGenericOneWayToSourceBinder<object, int>(new object()));

        [Test]
        public void TargetFactoryCtor_GetValueOnBound_IsPushedWithTheTargetOnBind()
        {
            var target = new object();
            object? receivedTarget = null;
            var binder = new UnityGenericOneWayToSourceBinder<object, int>(target, getValueOnBound: t =>
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
