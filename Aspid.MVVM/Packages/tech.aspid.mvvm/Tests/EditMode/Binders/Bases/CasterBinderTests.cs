using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="CasterBinder{TFrom,TTo}"/> and <see cref="CasterBinder{TTarget,TFrom,TTo}"/>.
    /// </summary>
    [TestFixture]
    public sealed class CasterBinderTests
    {
        [Test]
        public void Ctor_NullSetValue_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new CasterBinder<int, string>(null, new IntToStringConverter()));

        [Test]
        public void Ctor_NullConverter_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new CasterBinder<int, string>(_ => { }, null));

        [Test]
        public void Ctor_TwoWayMode_Throws() =>
            Assert.Throws<InvalidOperationException>(
                () => new CasterBinder<int, string>(_ => { }, new IntToStringConverter(), BindMode.TwoWay));

        [Test]
        public void SetValue_ConvertsAndForwards()
        {
            string? received = null;
            var binder = new CasterBinder<int, string>(value => received = value, new IntToStringConverter());

            ((IBinder<int>)binder).SetValue(5);

            Assert.AreEqual("5", received);
        }

        [Test]
        public void TargetCtor_NullTarget_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => new CasterBinder<object, int, string>(null, (_, _) => { }, new IntToStringConverter()));

        [Test]
        public void TargetCtor_NullSetValue_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => new CasterBinder<object, int, string>(new object(), null, new IntToStringConverter()));

        [Test]
        public void TargetCtor_NullConverter_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => new CasterBinder<object, int, string>(new object(), (_, _) => { }, null));

        [Test]
        public void TargetSetValue_ConvertsAndForwardsWithTheTarget()
        {
            var target = new object();
            object? receivedTarget = null;
            string? received = null;
            var binder = new CasterBinder<object, int, string>(target, (t, value) =>
            {
                receivedTarget = t;
                received = value;
            }, new IntToStringConverter());

            ((IBinder<int>)binder).SetValue(5);

            Assert.AreSame(target, receivedTarget);
            Assert.AreEqual("5", received);
        }

        private sealed class IntToStringConverter : IConverter<int, string>
        {
            public string Convert(int value) => value.ToString();
        }
    }
}
