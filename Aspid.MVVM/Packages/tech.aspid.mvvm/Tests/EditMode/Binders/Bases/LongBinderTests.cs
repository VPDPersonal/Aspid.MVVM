using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="ILongBinder"/>: every numeric type it is pushed through eventually reaches its
    /// <see langword="long"/> <c>SetValue</c>, saturating rather than wrapping when it is out of range.
    /// </summary>
    [TestFixture]
    public sealed class LongBinderTests
    {
        [Test]
        public void Native_ForwardsTheValue()
        {
            var binder = new RecordingLongBinder();
            ((IBinder<long>)binder).SetValue(7L);

            Assert.AreEqual(7L, binder.Last);
        }

        [Test]
        public void Int_RoutesDirectly()
        {
            var binder = new RecordingLongBinder();
            ((IBinder<int>)binder).SetValue(int.MinValue);

            Assert.AreEqual((long)int.MinValue, binder.Last);
        }

        [Test]
        public void Float_BeyondLongRange_Saturates()
        {
            var binder = new RecordingLongBinder();
            ((IBinder<float>)binder).SetValue(float.MaxValue);

            Assert.AreEqual(long.MaxValue, binder.Last);
        }

        [Test]
        public void Double_BeyondLongRange_Saturates()
        {
            var binder = new RecordingLongBinder();
            ((IBinder<double>)binder).SetValue(double.MinValue);

            Assert.AreEqual(long.MinValue, binder.Last);
        }

        [Test]
        public void Double_ANaN_RoutesAsZero()
        {
            var binder = new RecordingLongBinder();
            ((IBinder<double>)binder).SetValue(double.NaN);

            Assert.AreEqual(0L, binder.Last);
        }

        /// <summary>
        /// A <see langword="ulong"/> above <see cref="long.MaxValue"/> has no exact <see langword="long"/>
        /// representation, so an unchecked cast wraps it into a negative value instead of the nearer bound.
        /// </summary>
        [Test]
        public void Ulong_BeyondLongMax_SaturatesToLongMax()
        {
            var binder = new RecordingLongBinder();
            ((IBinder<ulong>)binder).SetValue(ulong.MaxValue);

            Assert.AreEqual(long.MaxValue, binder.Last);
        }

        [Test]
        public void Uint_RoutesDirectly()
        {
            var binder = new RecordingLongBinder();
            ((IBinder<uint>)binder).SetValue(uint.MaxValue);

            Assert.AreEqual((long)uint.MaxValue, binder.Last);
        }

        private sealed class RecordingLongBinder : Binder, ILongBinder
        {
            public long Last { get; private set; }

            public void SetValue(long value) =>
                Last = value;
        }
    }
}
