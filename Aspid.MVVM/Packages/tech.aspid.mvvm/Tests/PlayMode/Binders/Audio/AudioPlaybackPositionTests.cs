using UnityEngine;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for seeking an <see cref="AudioSource"/> past the end of its clip.
    /// </summary>
    /// <remarks>
    /// These run in play mode because outside it <see cref="AudioSource.time"/> reads <c>0</c> no matter what is
    /// written, so an EditMode test would observe nothing at all.
    /// </remarks>
    [TestFixture]
    public sealed class AudioPlaybackPositionTests : SceneFixture
    {
        private const int SampleRate = 44100;

        /// <summary>
        /// Pins the premise: this is Unity's behaviour, not the binder's.
        /// </summary>
        [UnityTest]
        public IEnumerator UnityTime_RefusesAPositionOutsideTheClip_AndSaysSo()
        {
            var audioSource = NewPlayingSource();
            yield return null;

            Assert.IsTrue(audioSource.isPlaying, "The source is not playing — time stores nothing while it is not");

            audioSource.time = 0.5f;
            Assert.AreEqual(0.5f, audioSource.time, 0.01f, "A valid position was not kept");

            LogAssert.Expect(LogType.Error, new Regex("invalid seek position"));
            audioSource.time = 100f;

            Assert.AreEqual(0.5f, audioSource.time, 0.01f,
                "Unity changed behaviour: a refused seek no longer leaves the position untouched");
        }

        [UnityTest]
        public IEnumerator TimeBinder_BeyondTheClip_StopsAtTheEndWithoutComplaining()
        {
            var audioSource = NewPlayingSource();
            var binder = audioSource.gameObject.AddComponent<AudioSourceTimeMonoBinder>();
            yield return null;

            ((IBinder<float>)binder).SetValue(100f);

            // The upper bound is not exact: the source is playing, and the playhead can advance toward the clip's
            // end between the assignment and the read.
            Assert.Greater(audioSource.time, 0.9f, "The position was not driven to the end of the clip");
            Assert.LessOrEqual(audioSource.time, 1f, "The position went past the end of the clip");
        }

        [UnityTest]
        public IEnumerator TimeBinder_WithANegativePosition_StopsAtTheStart()
        {
            var audioSource = NewPlayingSource();
            var binder = audioSource.gameObject.AddComponent<AudioSourceTimeMonoBinder>();
            yield return null;

            ((IBinder<float>)binder).SetValue(-10f);

            Assert.AreEqual(0f, audioSource.time, "A negative position was not clamped");
        }

        private AudioSource NewPlayingSource()
        {
            var gameObject = Spawn("Audio");
            var clip = Track(AudioClip.Create("guard", SampleRate, 1, SampleRate, false));

            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = 0f;
            audioSource.Play();

            return audioSource;
        }
    }
}
