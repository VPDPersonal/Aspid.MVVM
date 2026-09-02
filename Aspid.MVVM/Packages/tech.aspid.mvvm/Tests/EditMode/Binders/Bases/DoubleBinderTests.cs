using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="IDoubleBinder"/>: every other numeric type it is pushed through widens straight
    /// into its <see langword="double"/> <c>SetValue</c>, with nothing left to saturate.
    /// </summary>
    [TestFixture]
    public sealed class DoubleBinderTests
    {
        [Test]
        public void Native_ForwardsTheValue()
        {
            var binder = new RecordingDoubleBinder();
            ((IBinder<double>)binder).SetValue(1.5d);

            Assert.AreEqual(1.5d, binder.Last);
        }

        [Test]
        public void Int_WidensDirectly()
        {
            var binder = new RecordingDoubleBinder();
            ((IBinder<int>)binder).SetValue(int.MinValue);

            Assert.AreEqual((double)int.MinValue, binder.Last);
        }

        [Test]
        public void Long_WidensDirectly()
        {
            var binder = new RecordingDoubleBinder();
            ((IBinder<long>)binder).SetValue(long.MaxValue);

            Assert.AreEqual((double)long.MaxValue, binder.Last);
        }

        [Test]
        public void Float_WidensDirectly()
        {
            var binder = new RecordingDoubleBinder();
            ((IBinder<float>)binder).SetValue(float.MaxValue);

            Assert.AreEqual((double)float.MaxValue, binder.Last);
        }

        [Test]
        public void Float_NaNAndInfinity_WidenUnchanged()
        {
            var binder = new RecordingDoubleBinder();

            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.IsTrue(double.IsNaN(binder.Last));

            ((IBinder<float>)binder).SetValue(float.PositiveInfinity);
            Assert.IsTrue(double.IsPositiveInfinity(binder.Last));
        }

        private sealed class RecordingDoubleBinder : Binder, IDoubleBinder
        {
            public double Last { get; private set; }

            public void SetValue(double value) =>
                Last = value;
        }
    }
}
