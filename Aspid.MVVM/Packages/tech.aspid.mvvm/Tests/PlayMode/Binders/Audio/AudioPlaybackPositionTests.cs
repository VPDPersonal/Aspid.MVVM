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

        // The source is playing, so the playhead can advance between an assignment and the read that follows it: by one
        // DSP buffer (1024 samples, 21 ms at 48 kHz) on 6000.0 to 6000.2 in a headless container, by nothing elsewhere.
        private const float PlayheadDrift = 0.05f;

        /// <summary>
        /// Pins the premise: this is Unity's behaviour, not the binder's.
        /// </summary>
        /// <remarks>
        /// Where the refusal leaves the playhead depends on the audio output: with a device it stays where it was, and
        /// on a machine without one (a headless CI container) it reads as the end of the clip. It never takes the
        /// position asked for, which is what the binder's clamp is there for.
        /// </remarks>
        [UnityTest]
        public IEnumerator UnityTime_RefusesAPositionOutsideTheClip_AndSaysSo()
        {
            var audioSource = NewPlayingSource();
            yield return null;

            Assert.IsTrue(audioSource.isPlaying, "The source is not playing — time stores nothing while it is not");

            audioSource.time = 0.5f;
            Assert.That(audioSource.time, Is.InRange(0.5f, 0.5f + PlayheadDrift), "A valid position was not kept");

            LogAssert.Expect(LogType.Error, new Regex("invalid seek position"));
            audioSource.time = 100f;

            Assert.That(
                audioSource.time,
                Is.InRange(0.5f, 0.5f + PlayheadDrift).Or.EqualTo(audioSource.clip.length).Within(0.01f),
                "Unity changed behaviour: a refused seek moved the position somewhere other than where it was or the end of the clip");
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

            Assert.That(audioSource.time, Is.InRange(0f, PlayheadDrift), "A negative position was not clamped");
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
