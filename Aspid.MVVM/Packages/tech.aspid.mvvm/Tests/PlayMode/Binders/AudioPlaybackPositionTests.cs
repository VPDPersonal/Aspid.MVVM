using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for seeking an <see cref="AudioSource"/> past the end of its clip.
    /// </summary>
    /// <remarks>
    /// <see cref="AudioSource.time"/> does not store an out-of-range position — but it does not ignore one
    /// quietly either. It refuses the seek with an audio-engine error and leaves the playhead where it was, so a
    /// binder driving it from a seek slider or a <c>progress * duration</c> calculation with a stale duration
    /// produces an error per frame while the control it is bound to appears not to work at all.
    /// <para/>
    /// These run in play mode because outside it <see cref="AudioSource.time"/> reads <c>0</c> no matter what is
    /// written, so an EditMode test would observe nothing at all. Its sibling <c>timeSamples</c> does store the
    /// value and is covered in the EditMode suite.
    /// </remarks>
    [TestFixture]
    public sealed class AudioPlaybackPositionTests
    {
        private const int SampleRate = 44100;

        private readonly List<Object> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var spawned in _spawned)
            {
                if (spawned) Object.Destroy(spawned);
            }

            _spawned.Clear();
        }

        /// <summary>
        /// Pins the premise: this is Unity's behaviour, not the binder's.
        /// </summary>
        [UnityTest]
        public IEnumerator UnityTime_RefusesAPositionOutsideTheClip_AndSaysSo()
        {
            var audioSource = NewPlayingSource();
            yield return null;

            Assert.IsTrue(audioSource.isPlaying, "Источник не играет — свойство time ничего не хранит");

            audioSource.time = 0.5f;
            Assert.AreEqual(0.5f, audioSource.time, 0.01f, "Корректная позиция не сохранена");

            LogAssert.Expect(LogType.Error, new Regex("invalid seek position"));
            audioSource.time = 100f;

            Assert.AreEqual(0.5f, audioSource.time, 0.01f,
                "Unity изменила поведение: отвергнутая перемотка больше не оставляет позицию прежней");
        }

        [UnityTest]
        public IEnumerator TimeBinder_BeyondTheClip_StopsAtTheEndWithoutComplaining()
        {
            var audioSource = NewPlayingSource();
            var binder = audioSource.gameObject.AddComponent<AudioSourceTimeMonoBinder>();
            yield return null;

            ((IBinder<float>)binder).SetValue(100f);

            Assert.Greater(audioSource.time, 0.9f, "Позиция не доведена до конца клипа");
            Assert.Less(audioSource.time, 1f, "Позиция вышла за пределы клипа");
        }

        [UnityTest]
        public IEnumerator TimeBinder_WithANegativePosition_StopsAtTheStart()
        {
            var audioSource = NewPlayingSource();
            var binder = audioSource.gameObject.AddComponent<AudioSourceTimeMonoBinder>();
            yield return null;

            ((IBinder<float>)binder).SetValue(-10f);

            Assert.AreEqual(0f, audioSource.time, "Отрицательная позиция не обрезана");
        }

        private AudioSource NewPlayingSource()
        {
            var gameObject = new GameObject("Audio");
            _spawned.Add(gameObject);

            var clip = AudioClip.Create("guard", SampleRate, 1, SampleRate, false);
            _spawned.Add(clip);

            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = 0f;
            audioSource.Play();

            return audioSource;
        }
    }
}
