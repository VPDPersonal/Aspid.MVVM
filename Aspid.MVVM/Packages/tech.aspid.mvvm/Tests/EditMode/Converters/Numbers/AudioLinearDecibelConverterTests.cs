using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="AudioLinearDecibelConverter"/> — the linear-to-decibel curve, the
    /// inverted direction, the round trip, and the misconfigured-range guard.
    /// </summary>
    [TestFixture]
    public sealed class AudioLinearDecibelConverterTests
    {
        // A linear slider wired straight to a mixer sounds wrong: half the slider is nearly silent.
        [Test]
        public void AudioLinearToDecibel_HalfVolumeIsAboutMinusSixDecibels() =>
            Assert.AreEqual(-6.02f, new AudioLinearDecibelConverter().Convert(0.5f), delta: 0.05f);

        [Test]
        public void AudioLinearToDecibel_SilenceIsTheFloor() =>
            Assert.AreEqual(-80f, new AudioLinearDecibelConverter().Convert(0f), delta: 1e-4f);

        [Test]
        public void AudioLinearToDecibel_FullVolumeIsZero() =>
            Assert.AreEqual(0f, new AudioLinearDecibelConverter().Convert(1f), delta: 1e-4f);

        [Test]
        public void AudioLinearToDecibel_RoundTrips()
        {
            var converter = new AudioLinearDecibelConverter();

            Assert.AreEqual(0.5f, converter.ConvertBack(converter.Convert(0.5f)), delta: 1e-3f);
        }

        [Test]
        public void AudioLinearToDecibel_Inverted_IsTheOtherDirection()
        {
            var toDecibels = new AudioLinearDecibelConverter();
            var toLinear = new AudioLinearDecibelConverter(isInvert: true);

            Assert.AreEqual(0.5f, toLinear.Convert(toDecibels.Convert(0.5f)), delta: 1e-3f);
        }

        // Silence has to sit below full volume or the clamp collapses and every slider position
        // answers the same number. The default range is what keeps the fader working.
        [Test]
        public void AudioLinearToDecibel_RangeThatIsNotARange_ReportsAndUsesTheDefaultRange()
        {
            LogAssert.Expect(LogType.Error, new Regex("AudioLinearDecibelConverter.*decibel range is not a range"));

            Assert.AreEqual(-6.02f, new AudioLinearDecibelConverter(0f, -80f).Convert(0.5f), delta: 0.05f);
        }

        // The pair is read on every push rather than once at load, so a broken range keeps saying so
        // instead of going quiet after the first conversion.
        [Test]
        public void AudioLinearToDecibel_RangeThatIsNotARange_ReportsOnEveryPush()
        {
            var converter = new AudioLinearDecibelConverter(0f, -80f);

            LogAssert.Expect(LogType.Error, new Regex("AudioLinearDecibelConverter.*decibel range is not a range"));
            LogAssert.Expect(LogType.Error, new Regex("AudioLinearDecibelConverter.*decibel range is not a range"));

            converter.Convert(0.5f);
            converter.Convert(0.25f);
        }

        [Test]
        public void AudioLinearToDecibel_Double_ReadsTheSameCurve() =>
            Assert.AreEqual(
                -6.02d,
                ((IConverter<double, double>)new AudioLinearDecibelConverter()).Convert(0.5d),
                0.05d);

        [Test]
        public void AudioLinearToDecibel_Double_ConvertBack_ReadsTheSliderPosition() =>
            Assert.AreEqual(
                0.5d,
                ((ITwoWayConverter<double, double>)new AudioLinearDecibelConverter()).ConvertBack(-6.02d),
                1e-3d);
    }
}
