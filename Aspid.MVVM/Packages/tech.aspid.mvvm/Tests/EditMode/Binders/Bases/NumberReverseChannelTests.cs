using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="NumberReverseChannel"/>, the single place the View → ViewModel numeric
    /// conversions happen.
    /// </summary>
    [TestFixture]
    public sealed class NumberReverseChannelTests
    {
        [Test]
        public void RaiseFloat_BeyondIntRange_Saturates()
        {
            var channel = new NumberReverseChannel();
            var received = new List<int>();
            channel.IntValueChanged += value => received.Add(value);

            channel.Raise(float.MaxValue);

            Assert.AreEqual(new[] { int.MaxValue }, received, "Value outside the int range was not saturated");
        }

        [Test]
        public void RaiseFloat_BelowLongRange_Saturates()
        {
            var channel = new NumberReverseChannel();
            var received = new List<long>();
            channel.LongValueChanged += value => received.Add(value);

            channel.Raise(float.MinValue);

            Assert.AreEqual(new[] { long.MinValue }, received, "Value outside the long range was not saturated");
        }

        [Test]
        public void RaiseFloat_ANaN_ReachesTheIntegerEventsAsZero()
        {
            var channel = new NumberReverseChannel();
            var integers = new List<int>();
            var decimals = new List<float>();
            channel.IntValueChanged += value => integers.Add(value);
            channel.FloatValueChanged += value => decimals.Add(value);

            channel.Raise(float.NaN);

            Assert.AreEqual(new[] { 0 }, integers, "NaN did not reach the integer channel as zero");
            Assert.IsTrue(float.IsNaN(decimals[0]), "NaN did not reach the floating-point channel unchanged");
        }

        [Test]
        public void RaiseDouble_BeyondFloatRange_Saturates()
        {
            var channel = new NumberReverseChannel();
            var received = new List<float>();
            channel.FloatValueChanged += value => received.Add(value);

            channel.Raise(double.MaxValue);

            Assert.AreEqual(new[] { float.MaxValue }, received, "Value outside the float range was not saturated");
        }

        [Test]
        public void RaiseLong_BeyondIntRange_Saturates()
        {
            var channel = new NumberReverseChannel();
            var received = new List<int>();
            channel.IntValueChanged += value => received.Add(value);

            channel.Raise(long.MaxValue);

            Assert.AreEqual(new[] { int.MaxValue }, received, "Value outside the int range was not saturated");
        }

        [Test]
        public void RaiseIntegers_BeyondIntRange_SaturatesInsteadOfGoingSilent()
        {
            var channel = new NumberReverseChannel();
            var received = new List<int>();
            channel.IntValueChanged += value => received.Add(value);

            channel.RaiseIntegers(long.MaxValue);

            Assert.AreEqual(new[] { int.MaxValue }, received, "The integer channel stayed silent instead of saturating");
        }

        [Test]
        public void RaiseFloatingPoint_BeyondFloatRange_SaturatesInsteadOfGoingSilent()
        {
            var channel = new NumberReverseChannel();
            var received = new List<float>();
            channel.FloatValueChanged += value => received.Add(value);

            channel.RaiseFloatingPoint(double.MaxValue);

            Assert.AreEqual(new[] { float.MaxValue }, received, "The floating-point channel stayed silent instead of saturating");
        }

        [Test]
        public void RaiseIntegers_LeavesTheDecimalEventsAlone()
        {
            var channel = new NumberReverseChannel();
            var decimals = new List<double>();
            channel.DoubleValueChanged += value => decimals.Add(value);

            channel.RaiseIntegers(5L);

            Assert.IsEmpty(decimals, "The integer call reached the floating-point channels");
        }

        [Test]
        public void Raise_ReachesEveryNumericEvent()
        {
            var channel = new NumberReverseChannel();
            var reached = new List<string>();
            channel.IntValueChanged += _ => reached.Add("int");
            channel.LongValueChanged += _ => reached.Add("long");
            channel.FloatValueChanged += _ => reached.Add("float");
            channel.DoubleValueChanged += _ => reached.Add("double");

            channel.Raise(1.5f);

            Assert.AreEqual(new[] { "int", "long", "float", "double" }, reached, "A single Raise did not reach every channel");
        }

        [Test]
        public void HasListeners_ReportEachHalfSeparately()
        {
            var channel = new NumberReverseChannel();
            Assert.IsFalse(channel.HasIntegerListeners, "An empty channel reports integer subscribers");
            Assert.IsFalse(channel.HasFloatingPointListeners, "An empty channel reports floating-point subscribers");

            channel.LongValueChanged += _ => { };

            Assert.IsTrue(channel.HasIntegerListeners, "A long subscriber is not visible to the integer half");
            Assert.IsFalse(channel.HasFloatingPointListeners, "A long subscriber is visible to the floating-point half");
        }
    }
}
