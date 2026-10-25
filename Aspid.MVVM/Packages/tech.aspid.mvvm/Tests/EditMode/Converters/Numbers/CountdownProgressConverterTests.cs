using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

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

        // [Min(0f)] holds the Inspector at zero, so the constructor is the only way a negative duration
        // gets in, and it refuses one.
        [Test]
        public void Constructor_NegativeDuration_Throws() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new CountdownProgressConverter(-1f));

        [Test]
        public void CountdownProgress_Double_ReadsTheSameProgress() =>
            Assert.AreEqual(
                0.5d,
                ((IConverter<double, double>)new CountdownProgressConverter(10f)).Convert(5d),
                1e-12d);
    }
}
