using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="TwoWayValue{T}"/> that are not already covered by <see cref="ValueBinderFeedbackTests"/>.
    /// </summary>
    [TestFixture]
    public sealed class TwoWayValueTests
    {
        [Test]
        public void Ctor_NoneMode_Throws() =>
            Assert.Throws<ArgumentException>(() => new TwoWayValue<int>(0, BindMode.None));

        [Test]
        public void DefaultCtor_StoresTheDefaultValueAndTwoWayMode()
        {
            var binder = new TwoWayValue<int>();

            Assert.AreEqual(0, binder.Value);
            Assert.AreEqual(BindMode.TwoWay, binder.Mode);
        }

        [Test]
        public void SetValue_FromTheViewModel_UpdatesValueAndRaisesChangedWithTheRawValue()
        {
            var binder = new TwoWayValue<int>(0);

            var received = new List<int>();
            binder.Changed += received.Add;
            ((IBinder<int>)binder).SetValue(5);

            Assert.AreEqual(5, binder.Value);
            Assert.AreEqual(new[] { 5 }, received);
        }

        [Test]
        public void ValueSetter_WithATwoWayConverter_NotifiesTheViewModelThroughConvertBack()
        {
            var binder = new TwoWayValue<int>(0, new DoublingConverter());

            var received = new List<int>();
            ((IReverseBinder<int>)binder).ValueChanged += received.Add;
            binder.Value = 6;

            Assert.AreEqual(new[] { 3 }, received, "The ViewModel expects the value undone by ConvertBack.");
        }

        [Test]
        public void ImplicitOperator_ReturnsTheValue()
        {
            var binder = new TwoWayValue<int>(9);

            int value = binder;

            Assert.AreEqual(9, value);
        }

        private sealed class DoublingConverter : ITwoWayConverter<int, int>
        {
            public int Convert(int value) => value * 2;

            public int ConvertBack(int value) => value / 2;
        }
    }
}
