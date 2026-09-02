using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="IIntBinder"/>: every numeric type it is pushed through eventually reaches its
    /// <see langword="int"/> <c>SetValue</c>, saturating rather than wrapping when it is out of range.
    /// </summary>
    [TestFixture]
    public sealed class IntBinderTests
    {
        [Test]
        public void Native_ForwardsTheValue()
        {
            var binder = new RecordingIntBinder();
            ((IBinder<int>)binder).SetValue(7);

            Assert.AreEqual(7, binder.Last);
        }

        [Test]
        public void Long_BeyondIntRange_Saturates()
        {
            var binder = new RecordingIntBinder();

            ((IBinder<long>)binder).SetValue(long.MaxValue);
            Assert.AreEqual(int.MaxValue, binder.Last, "A long above int range was not saturated");

            ((IBinder<long>)binder).SetValue(long.MinValue);
            Assert.AreEqual(int.MinValue, binder.Last, "A long below int range was not saturated");
        }

        [Test]
        public void Float_BeyondIntRange_Saturates()
        {
            var binder = new RecordingIntBinder();
            ((IBinder<float>)binder).SetValue(float.MaxValue);

            Assert.AreEqual(int.MaxValue, binder.Last);
        }

        [Test]
        public void Double_BeyondIntRange_Saturates()
        {
            var binder = new RecordingIntBinder();
            ((IBinder<double>)binder).SetValue(double.MinValue);

            Assert.AreEqual(int.MinValue, binder.Last);
        }

        [Test]
        public void Double_ANaN_RoutesAsZero()
        {
            var binder = new RecordingIntBinder();
            ((IBinder<double>)binder).SetValue(double.NaN);

            Assert.AreEqual(0, binder.Last);
        }

        [Test]
        public void NarrowTypes_RouteThroughToInt()
        {
            var binder = new RecordingIntBinder();

            ((IBinder<uint>)binder).SetValue(100u);
            Assert.AreEqual(100, binder.Last, "uint did not reach the int SetValue");

            ((IBinder<byte>)binder).SetValue(5);
            Assert.AreEqual(5, binder.Last, "byte did not reach the int SetValue");
        }

        [Test]
        public void Sbyte_ANegativeValue_RoutesToInt()
        {
            var binder = new RecordingIntBinder();
            ((IBinder<sbyte>)binder).SetValue(-12);

            Assert.AreEqual(-12, binder.Last, "sbyte did not reach the int SetValue");
        }

        [Test]
        public void Short_RoutesToInt()
        {
            var binder = new RecordingIntBinder();
            ((IBinder<short>)binder).SetValue(-1234);

            Assert.AreEqual(-1234, binder.Last, "short did not reach the int SetValue");
        }

        [Test]
        public void Ushort_RoutesToInt()
        {
            var binder = new RecordingIntBinder();
            ((IBinder<ushort>)binder).SetValue(9);

            Assert.AreEqual(9, binder.Last, "ushort did not reach the int SetValue");
        }

        [Test]
        public void Ulong_BeyondLongMax_SaturatesThroughTheLongStepFirst()
        {
            var binder = new RecordingIntBinder();
            ((IBinder<ulong>)binder).SetValue(ulong.MaxValue);

            Assert.AreEqual(int.MaxValue, binder.Last, "ulong beyond long range did not end up at int.MaxValue");
        }

        private sealed class RecordingIntBinder : Binder, IIntBinder
        {
            public int Last { get; private set; }

            public void SetValue(int value) =>
                Last = value;
        }
    }
}
