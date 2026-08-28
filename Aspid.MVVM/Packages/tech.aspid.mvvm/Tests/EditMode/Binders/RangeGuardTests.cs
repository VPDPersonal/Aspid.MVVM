using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the range guards on paired and bounded component properties.
    /// </summary>
    /// <remarks>
    /// Unity does not enforce <c>minValue &lt;= maxValue</c> on <see cref="Slider"/> or
    /// <c>minDistance &lt;= maxDistance</c> on <see cref="AudioSource"/>: assigning them in order is enough to leave
    /// the component inverted, after which <c>Slider.normalizedValue</c> reads backwards and an audio source is
    /// silent at every distance. Separately, <c>dopplerLevel</c> was the one bounded AudioSource property the
    /// binders never clamped — Unity enforces its range in the setter, so what leaked through was <c>NaN</c>.
    /// </remarks>
    [TestFixture]
    public sealed class RangeGuardTests
    {
        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        [Test]
        public void SetMinMax_WithAnInvertedRange_SwapsTheEndpoints()
        {
            var slider = NewGameObject().AddComponent<Slider>();

            LogAssert.Expect(LogType.Error, new Regex("is inverted"));
            slider.SetMinMax(new Vector2(10f, 2f), SliderValueMode.Range);

            Assert.AreEqual(2f, slider.minValue);
            Assert.AreEqual(10f, slider.maxValue);
        }

        [Test]
        public void SetMinMax_WithANonFiniteRange_LeavesTheSliderAlone()
        {
            var slider = NewGameObject().AddComponent<Slider>();
            slider.minValue = 0f;
            slider.maxValue = 1f;

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            slider.SetMinMax(new Vector2(float.NaN, 1f), SliderValueMode.Range);

            Assert.AreEqual(0f, slider.minValue);
            Assert.AreEqual(1f, slider.maxValue);
        }

        [Test]
        public void SetMinMaxDistance_WithAnInvertedRange_SwapsTheEndpoints()
        {
            var audioSource = NewGameObject().AddComponent<AudioSource>();

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
            var audioSource = NewGameObject().AddComponent<AudioSource>();

            audioSource.dopplerLevel = 42f;
            Assert.AreEqual(5f, audioSource.dopplerLevel, "Unity перестал клампить диапазон");

            audioSource.dopplerLevel = float.NaN;
            Assert.IsNaN(audioSource.dopplerLevel, "Unity начал отсекать NaN сам — клампинг в биндере больше не нужен");
        }

        [Test]
        public void DopplerLevelBinder_ReplacesNaNWithTheLowerBound()
        {
            var gameObject = NewGameObject();
            var audioSource = gameObject.AddComponent<AudioSource>();
            var binder = gameObject.AddComponent<AudioSourceDopplerLevelMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.AreEqual(0f, audioSource.dopplerLevel, "NaN дошёл до компонента");
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("RangeGuard");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
