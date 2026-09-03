using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A converter that is deliberately not idempotent, so a value fed back through it is visible.
    /// </summary>
    internal sealed class DoublingConverter : IConverter<int, int>
    {
        public int Convert(int value) => value * 2;
    }

    /// <summary>
    /// Regression tests for <see cref="ValueTwoWayBinder{T}"/> feeding ViewModel updates straight back to the ViewModel.
    /// </summary>
    /// <remarks>
    /// <see cref="ValueTwoWayBinder{T}.Value"/> is the View-side entry point: its setter raises the reverse channel, so a
    /// write coming from the ViewModel must not go through it.
    /// </remarks>
    [TestFixture]
    public sealed class ValueBinderFeedbackTests
    {
        [Test]
        public void SetValue_FromTheViewModel_DoesNotBounceBack()
        {
            var binder = new ValueTwoWayBinder<int>(0);

            var received = new List<int>();
            var member = new OneWayToSourceBindableMember<int>(value => received.Add(value));

            ((IBinder)binder).Bind(member);
            received.Clear();

            ((IBinder<int>)binder).SetValue(7);

            Assert.IsEmpty(received, $"The update from the ViewModel bounced back: [{string.Join(", ", received)}]");
        }

        [Test]
        public void SetValue_WithAConverter_StoresTheConvertedValueWithoutReportingIt()
        {
            var binder = new ValueTwoWayBinder<int>(0, new DoublingConverter(), BindMode.TwoWay);

            var received = new List<int>();
            var member = new OneWayToSourceBindableMember<int>(value => received.Add(value));

            ((IBinder)binder).Bind(member);
            received.Clear();

            ((IBinder<int>)binder).SetValue(3);

            Assert.AreEqual(6, binder.Value, "The converter was not applied to the value from the ViewModel");
            Assert.IsEmpty(received, "The converted value travelled back to the ViewModel");
        }

        /// <summary>
        /// The View side must still reach the ViewModel — the guard above must not silence the whole channel.
        /// </summary>
        [Test]
        public void SettingValue_FromTheViewSide_StillReachesTheViewModel()
        {
            var binder = new ValueTwoWayBinder<int>(0);

            var received = new List<int>();
            var member = new OneWayToSourceBindableMember<int>(value => received.Add(value));

            ((IBinder)binder).Bind(member);
            received.Clear();

            binder.Value = 42;

            Assert.AreEqual(new[] { 42 }, received, "The value from the View side did not reach the ViewModel");
        }
    }
}
