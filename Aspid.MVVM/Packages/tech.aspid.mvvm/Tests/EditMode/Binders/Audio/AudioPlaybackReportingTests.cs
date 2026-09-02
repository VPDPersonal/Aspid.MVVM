using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for playing a clip the ViewModel supplies and reporting that a source has finished playing.
    /// </summary>
    [TestFixture]
    public sealed class AudioPlaybackReportingTests : SceneFixture
    {
        [Test]
        public void PlayOneShot_ANullClipDoesNothing()
        {
            var binder = NewPlayOneShot();

            Assert.DoesNotThrow(() => ((IBinder<AudioClip>)binder).SetValue(null), "A null clip threw");
        }

        /// <summary>
        /// A destroyed clip must be treated as no clip: Unity logs an error of its own for one, and a ViewModel that
        /// held a reference to an unloaded asset would produce it on every event.
        /// </summary>
        [Test]
        public void PlayOneShot_ADestroyedClipDoesNothing()
        {
            var binder = NewPlayOneShot();
            var clip = NewClip();

            Destroy(clip);

            Assert.DoesNotThrow(() => ((IBinder<AudioClip>)binder).SetValue(clip), "A destroyed clip threw");
        }

        [Test]
        public void PlayOneShot_ALiveClipIsAccepted()
        {
            var binder = NewPlayOneShot();
            var clip = NewClip();

            Assert.DoesNotThrow(() => ((IBinder<AudioClip>)binder).SetValue(clip), "A live clip did not play");
        }

        /// <summary>
        /// The state is reported at binding time, so a ViewModel starts in step with a source that is already silent
        /// rather than waiting for the first change that may never come.
        /// </summary>
        [Test]
        public void IsPlaying_ReportsTheCurrentStateWhenBound()
        {
            var source = NewSource();
            var binder = source.gameObject.AddComponent<AudioSourceIsPlayingToSourceMonoBinder>();

            var received = new List<bool>();
            binder.Bind(new OneWayToSourceStructBindableMember<bool>(received.Add));

            Assert.AreEqual(new List<bool> { false }, received, "The state was not reported on binding");
        }

        [Test]
        public void IsPlaying_TheDefaultModeIsTheOnlyOneItSupports()
        {
            var source = NewSource();
            var binder = source.gameObject.AddComponent<AudioSourceIsPlayingToSourceMonoBinder>();

            Assert.AreEqual(BindMode.OneWayToSource, binder.Mode, "The default mode is not OneWayToSource");
        }

        private AudioSourcePlayOneShotMonoBinder NewPlayOneShot() =>
            NewSource().gameObject.AddComponent<AudioSourcePlayOneShotMonoBinder>();

        private AudioSource NewSource() =>
            Spawn<AudioSource>("AudioSource");

        private AudioClip NewClip() =>
            Track(AudioClip.Create("Clip", 128, 1, 44100, stream: false));
    }
}
