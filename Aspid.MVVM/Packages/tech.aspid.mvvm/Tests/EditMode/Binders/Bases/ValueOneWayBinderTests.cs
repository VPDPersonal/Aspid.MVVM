using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="ValueOneWayBinder{T}"/>.
    /// </summary>
    [TestFixture]
    public sealed class ValueOneWayBinderTests
    {
        [Test]
        public void Ctor_TwoWayMode_Throws() =>
            Assert.Throws<InvalidOperationException>(() => new ValueOneWayBinder<int>(0, BindMode.TwoWay));

        [Test]
        public void Ctor_OneWayToSourceMode_Throws() =>
            Assert.Throws<InvalidOperationException>(() => new ValueOneWayBinder<int>(0, BindMode.OneWayToSource));

        [Test]
        public void DefaultCtor_StoresTheDefaultValue()
        {
            var binder = new ValueOneWayBinder<int>();

            Assert.AreEqual(0, binder.Value);
            Assert.AreEqual(BindMode.OneWay, binder.Mode);
        }

        [Test]
        public void ValueCtor_StoresTheGivenValue()
        {
            var binder = new ValueOneWayBinder<int>(7);

            Assert.AreEqual(7, binder.Value);
        }

        [Test]
        public void SetValue_UpdatesValueAndRaisesChangedWithTheRawValue()
        {
            var binder = new ValueOneWayBinder<int>(0);

            var received = new List<int>();
            binder.Changed += received.Add;
            ((IBinder<int>)binder).SetValue(5);

            Assert.AreEqual(5, binder.Value);
            Assert.AreEqual(new[] { 5 }, received);
        }

        [Test]
        public void SetValue_WithAConverter_StoresTheConvertedValueButRaisesTheRawOne()
        {
            var binder = new ValueOneWayBinder<int>(0, new DoublingConverter());

            var received = new List<int>();
            binder.Changed += received.Add;
            ((IBinder<int>)binder).SetValue(3);

            Assert.AreEqual(6, binder.Value, "The converter must be applied to the stored value.");
            Assert.AreEqual(new[] { 3 }, received, "Changed must carry the raw ViewModel value, not the converted one.");
        }

        [Test]
        public void ImplicitOperator_ReturnsTheValue()
        {
            var binder = new ValueOneWayBinder<int>(9);

            int value = binder;

            Assert.AreEqual(9, value);
        }

        private sealed class DoublingConverter : IConverter<int, int>
        {
            public int Convert(int value) => value * 2;
        }
    }
}
