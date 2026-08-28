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
    /// <remarks>
    /// The binders used to hold four events each and cast between the types by hand, and the casts were
    /// direct: a value beyond the target type's range reached it through an undefined conversion, which on
    /// one platform saturates and on another wraps to the opposite bound. The channel saturates instead,
    /// matching what the forward direction already does through the binder interfaces.
    /// </remarks>
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

            Assert.AreEqual(new[] { int.MaxValue }, received, "Значение вне диапазона int не насыщено");
        }

        [Test]
        public void RaiseFloat_BelowLongRange_Saturates()
        {
            var channel = new NumberReverseChannel();
            var received = new List<long>();
            channel.LongValueChanged += value => received.Add(value);

            channel.Raise(float.MinValue);

            Assert.AreEqual(new[] { long.MinValue }, received, "Значение вне диапазона long не насыщено");
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

            Assert.AreEqual(new[] { 0 }, integers, "NaN не приведён к нулю на целочисленном канале");
            Assert.IsTrue(float.IsNaN(decimals[0]), "NaN не дошёл до вещественного канала как есть");
        }

        [Test]
        public void RaiseDouble_BeyondFloatRange_Saturates()
        {
            var channel = new NumberReverseChannel();
            var received = new List<float>();
            channel.FloatValueChanged += value => received.Add(value);

            channel.Raise(double.MaxValue);

            Assert.AreEqual(new[] { float.MaxValue }, received, "Значение вне диапазона float не насыщено");
        }

        [Test]
        public void RaiseLong_BeyondIntRange_Saturates()
        {
            var channel = new NumberReverseChannel();
            var received = new List<int>();
            channel.IntValueChanged += value => received.Add(value);

            channel.Raise(long.MaxValue);

            Assert.AreEqual(new[] { int.MaxValue }, received, "Значение вне диапазона int не насыщено");
        }

        [Test]
        public void RaiseIntegers_BeyondIntRange_SaturatesInsteadOfGoingSilent()
        {
            var channel = new NumberReverseChannel();
            var received = new List<int>();
            channel.IntValueChanged += value => received.Add(value);

            channel.RaiseIntegers(long.MaxValue);

            Assert.AreEqual(new[] { int.MaxValue }, received, "Целочисленный канал промолчал вместо насыщения");
        }

        [Test]
        public void RaiseDecimals_BeyondFloatRange_SaturatesInsteadOfGoingSilent()
        {
            var channel = new NumberReverseChannel();
            var received = new List<float>();
            channel.FloatValueChanged += value => received.Add(value);

            channel.RaiseDecimals(double.MaxValue);

            Assert.AreEqual(new[] { float.MaxValue }, received, "Вещественный канал промолчал вместо насыщения");
        }

        [Test]
        public void RaiseIntegers_LeavesTheDecimalEventsAlone()
        {
            var channel = new NumberReverseChannel();
            var decimals = new List<double>();
            channel.DoubleValueChanged += value => decimals.Add(value);

            channel.RaiseIntegers(5L);

            Assert.IsEmpty(decimals, "Целочисленный вызов задел вещественные каналы");
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

            Assert.AreEqual(new[] { "int", "long", "float", "double" }, reached, "Один Raise дошёл не до всех каналов");
        }

        [Test]
        public void HasListeners_ReportEachHalfSeparately()
        {
            var channel = new NumberReverseChannel();
            Assert.IsFalse(channel.HasIntegerListeners, "Пустой канал считает, что у него есть целочисленные подписчики");
            Assert.IsFalse(channel.HasDecimalListeners, "Пустой канал считает, что у него есть вещественные подписчики");

            channel.LongValueChanged += _ => { };

            Assert.IsTrue(channel.HasIntegerListeners, "Подписчик long не виден целочисленной половине");
            Assert.IsFalse(channel.HasDecimalListeners, "Подписчик long виден вещественной половине");
        }
    }
}
