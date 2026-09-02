using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="CountdownProgressConverter"/> — the remaining and elapsed readings,
    /// a zero-duration timer, and the negative-duration guard.
    /// </summary>
    [TestFixture]
    public sealed class CountdownProgressConverterTests
    {
        [TestCase(30f, 0.5f)]
        [TestCase(0f, 0f)]
        [TestCase(90f, 1f)]
        public void Countdown_ReportsWhatIsLeft(float secondsLeft, float expected) =>
            Assert.AreEqual(expected, new CountdownProgressConverter(60f).Convert(secondsLeft), delta: 1e-6f);

        [Test]
        public void Countdown_ReportsWhatIsGoneWhenAsked() =>
            Assert.AreEqual(0.5f, new CountdownProgressConverter(60f, elapsed: true).Convert(30f), delta: 1e-6f);

        [Test]
        public void Countdown_ZeroDurationIsAFinishedTimer() =>
            Assert.AreEqual(0f, new CountdownProgressConverter(0f).Convert(10f), delta: 1e-6f);

        // [Min(0f)] holds the Inspector at zero, so a negative duration is older authored data. A
        // finished timer is the safe read: dividing by it would drive a fill bar past its own end.
        [TestCase(false, 0f)]
        [TestCase(true, 1f)]
        public void Countdown_NegativeDuration_ReportsAndReadsAsFinished(bool elapsed, float expected)
        {
            LogAssert.Expect(LogType.Error, new Regex("CountdownProgressConverter.*duration -1 is negative"));

            Assert.AreEqual(expected, new CountdownProgressConverter(-1f, elapsed).Convert(10f), delta: 1e-6f);
        }

        [Test]
        public void CountdownProgress_Double_ReadsTheSameProgress() =>
            Assert.AreEqual(
                0.5d,
                ((IConverter<double, double>)new CountdownProgressConverter(10f)).Convert(5d),
                1e-12d);
    }
}
