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
    /// Regression tests for <see cref="TwoWayValue{T}"/> feeding ViewModel updates straight back to the ViewModel.
    /// </summary>
    /// <remarks>
    /// <c>IBinder&lt;T&gt;.SetValue</c> assigned through the <see cref="TwoWayValue{T}.Value"/> property, and that
    /// property's setter is the View-side entry point: it raises the reverse channel. So every update travelling
    /// ViewModel → View turned around immediately and travelled back — carrying the <em>converted</em> value, which
    /// overwrote the model with what the display shows.
    /// </remarks>
    [TestFixture]
    public sealed class ValueBinderFeedbackTests
    {
        [Test]
        public void SetValue_FromTheViewModel_DoesNotBounceBack()
        {
            var binder = new TwoWayValue<int>(0);

            var received = new List<int>();
            var member = new OneWayToSourceBindableMember<int>(value => received.Add(value));

            ((IBinder)binder).Bind(member);
            received.Clear();

            ((IBinder<int>)binder).SetValue(7);

            Assert.IsEmpty(received, $"Обновление из ViewModel вернулось обратно: [{string.Join(", ", received)}]");
        }

        [Test]
        public void SetValue_WithAConverter_StoresTheConvertedValueWithoutReportingIt()
        {
            var binder = new TwoWayValue<int>(0, new DoublingConverter(), BindMode.TwoWay);

            var received = new List<int>();
            var member = new OneWayToSourceBindableMember<int>(value => received.Add(value));

            ((IBinder)binder).Bind(member);
            received.Clear();

            ((IBinder<int>)binder).SetValue(3);

            Assert.AreEqual(6, binder.Value, "Конвертер не применился к значению из ViewModel");
            Assert.IsEmpty(received, "Сконвертированное значение уехало обратно во ViewModel");
        }

        /// <summary>
        /// The View side must still reach the ViewModel — the guard above must not silence the whole channel.
        /// </summary>
        [Test]
        public void SettingValue_FromTheViewSide_StillReachesTheViewModel()
        {
            var binder = new TwoWayValue<int>(0);

            var received = new List<int>();
            var member = new OneWayToSourceBindableMember<int>(value => received.Add(value));

            ((IBinder)binder).Bind(member);
            received.Clear();

            binder.Value = 42;

            Assert.AreEqual(new[] { 42 }, received, "Значение со стороны View не доехало до ViewModel");
        }
    }
}
