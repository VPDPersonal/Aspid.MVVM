using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="IFloatBinder"/>: every numeric type it is pushed through eventually reaches its
    /// <see langword="float"/> <c>SetValue</c>, saturating a <see langword="double"/> at the bounds.
    /// </summary>
    [TestFixture]
    public sealed class FloatBinderTests
    {
        [Test]
        public void Native_ForwardsTheValue()
        {
            var binder = new RecordingFloatBinder();
            ((IBinder<float>)binder).SetValue(1.5f);

            Assert.AreEqual(1.5f, binder.Last);
        }

        [Test]
        public void Int_RoutesDirectly()
        {
            var binder = new RecordingFloatBinder();
            ((IBinder<int>)binder).SetValue(7);

            Assert.AreEqual(7f, binder.Last);
        }

        [Test]
        public void Long_RoutesDirectly()
        {
            var binder = new RecordingFloatBinder();
            ((IBinder<long>)binder).SetValue(7L);

            Assert.AreEqual(7f, binder.Last);
        }

        [Test]
        public void Double_BeyondFloatRange_Saturates()
        {
            var binder = new RecordingFloatBinder();
            ((IBinder<double>)binder).SetValue(double.MaxValue);

            Assert.AreEqual(float.MaxValue, binder.Last);
        }

        /// <summary>
        /// A NaN or an infinity is representable as a <see langword="float"/>, so clamping it would turn
        /// "no bound" into a specific number instead of leaving it as the same statement about the value.
        /// </summary>
        [Test]
        public void Double_NaNAndInfinity_PassThroughUnclamped()
        {
            var binder = new RecordingFloatBinder();

            ((IBinder<double>)binder).SetValue(double.NaN);
            Assert.IsTrue(float.IsNaN(binder.Last), "NaN was clamped instead of passing through");

            ((IBinder<double>)binder).SetValue(double.PositiveInfinity);
            Assert.IsTrue(float.IsPositiveInfinity(binder.Last), "Infinity was clamped instead of passing through");
        }

        private sealed class RecordingFloatBinder : Binder, IFloatBinder
        {
            public float Last { get; private set; }

            public void SetValue(float value) =>
                Last = value;
        }
    }
}
