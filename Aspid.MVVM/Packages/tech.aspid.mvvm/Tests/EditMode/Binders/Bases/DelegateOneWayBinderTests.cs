using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="DelegateOneWayBinder{T}"/> and <see cref="DelegateOneWayBinder{TTarget,T}"/>.
    /// </summary>
    [TestFixture]
    public sealed class DelegateOneWayBinderTests
    {
        [Test]
        public void Ctor_NullSetValue_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new DelegateOneWayBinder<int>(null));

        [Test]
        public void Ctor_TwoWayMode_Throws() =>
            Assert.Throws<InvalidOperationException>(() => new DelegateOneWayBinder<int>(_ => { }, BindMode.TwoWay));

        [Test]
        public void Ctor_OneWayToSourceMode_Throws() =>
            Assert.Throws<InvalidOperationException>(() => new DelegateOneWayBinder<int>(_ => { }, BindMode.OneWayToSource));

        [Test]
        public void SetValue_ForwardsToTheSetter()
        {
            var received = 0;
            var binder = new DelegateOneWayBinder<int>(value => received = value);

            ((IBinder<int>)binder).SetValue(7);

            Assert.AreEqual(7, received);
        }

        [Test]
        public void TargetCtor_NullTarget_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new DelegateOneWayBinder<object, int>(null, (_, _) => { }));

        [Test]
        public void TargetCtor_NullSetValue_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new DelegateOneWayBinder<object, int>(new object(), null));

        [Test]
        public void TargetCtor_TwoWayMode_Throws() =>
            Assert.Throws<InvalidOperationException>(
                () => new DelegateOneWayBinder<object, int>(new object(), (_, _) => { }, BindMode.TwoWay));

        [Test]
        public void TargetSetValue_ForwardsTheTargetAndTheValue()
        {
            var target = new object();
            object? receivedTarget = null;
            var receivedValue = 0;
            var binder = new DelegateOneWayBinder<object, int>(target, (t, value) =>
            {
                receivedTarget = t;
                receivedValue = value;
            });

            ((IBinder<int>)binder).SetValue(9);

            Assert.AreSame(target, receivedTarget);
            Assert.AreEqual(9, receivedValue);
        }
    }
}
