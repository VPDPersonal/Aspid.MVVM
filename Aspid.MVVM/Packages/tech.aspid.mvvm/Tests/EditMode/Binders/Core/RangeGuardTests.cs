using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the range guards on paired and bounded component properties.
    /// </summary>
    /// <remarks>
    /// Unity does not enforce <c>minValue &lt;= maxValue</c> on <see cref="Slider"/> or <c>minDistance &lt;= maxDistance</c> on
    /// <see cref="AudioSource"/>, and stores a <c>NaN</c> <c>dopplerLevel</c>; the binders guard exactly these.
    /// </remarks>
    [TestFixture]
    public sealed class RangeGuardTests : SceneFixture
    {
        [Test]
        public void SetMinMax_WithAnInvertedRange_SwapsTheEndpoints()
        {
            var slider = Spawn<Slider>();

            LogAssert.Expect(LogType.Error, new Regex("is inverted"));
            slider.SetMinMax(new Vector2(10f, 2f), SliderRangeMode.Range);

            Assert.AreEqual(2f, slider.minValue);
            Assert.AreEqual(10f, slider.maxValue);
        }

        [Test]
        public void SetMinMax_WithANonFiniteRange_LeavesTheSliderAlone()
        {
            var slider = Spawn().AddComponent<Slider>();
            slider.minValue = 0f;
            slider.maxValue = 1f;

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            slider.SetMinMax(new Vector2(float.NaN, 1f), SliderRangeMode.Range);

            Assert.AreEqual(0f, slider.minValue);
            Assert.AreEqual(1f, slider.maxValue);
        }

        [Test]
        public void SetMinMaxDistance_WithAnInvertedRange_SwapsTheEndpoints()
        {
            var audioSource = Spawn<AudioSource>();

            LogAssert.Expect(LogType.Error, new Regex("is inverted"));
            audioSource.SetMinMaxDistance(new Vector2(50f, 5f), AudioSourceDistanceMode.Range);

            Assert.AreEqual(5f, audioSource.minDistance);
            Assert.AreEqual(50f, audioSource.maxDistance);
        }

        /// <summary>
        /// Pins Unity's own behaviour, not ours: <see cref="AudioSource.dopplerLevel"/> enforces its 0..5 range
        /// inside the property setter, but lets <c>NaN</c> straight through.
        /// </summary>
        /// <remarks>
        /// This is why the binder still needs a guard, and why that guard is about <c>NaN</c> rather than the range.
        /// If a future Unity version starts rejecting <c>NaN</c> here, this test fails and says so.
        /// </remarks>
        [Test]
        public void UnityDopplerLevel_ClampsTheRangeButNotNaN()
        {
            var audioSource = Spawn<AudioSource>();

            audioSource.dopplerLevel = 42f;
            Assert.AreEqual(5f, audioSource.dopplerLevel, "Unity stopped clamping the range");

            audioSource.dopplerLevel = float.NaN;
            Assert.IsNaN(audioSource.dopplerLevel, "Unity started rejecting NaN itself — the binder's clamp is no longer needed");
        }

        [Test]
        public void DopplerLevelBinder_ReplacesNaNWithTheLowerBound()
        {
            var audioSource = Spawn<AudioSource>();
            var binder = audioSource.gameObject.AddComponent<AudioSourceDopplerLevelMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.AreEqual(0f, audioSource.dopplerLevel, "NaN reached the component");
        }
    }
}
