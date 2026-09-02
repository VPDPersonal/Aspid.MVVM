using System;
using UnityEngine;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the new playback binders, which hand the ViewModel one operation on an <see cref="AudioSource"/>.
    /// </summary>
    /// <remarks>
    /// These run in play mode because <c>AudioSource.isPlaying</c> only means anything while the game is running.
    /// </remarks>
    [TestFixture]
    public sealed class AudioPlaybackBinderTests : SceneFixture
    {
        [UnityTest]
        public IEnumerator PlayBinder_StartsTheSource()
        {
            var (audioSource, play) = Create<AudioSourcePlayMonoBinder>();
            yield return null;

            Invoke(play);

            Assert.IsTrue(audioSource.isPlaying, "The source did not start");
        }

        [UnityTest]
        public IEnumerator StopBinder_StopsTheSource()
        {
            var (audioSource, stop) = Create<AudioSourceStopMonoBinder>();
            audioSource.Play();
            yield return null;

            Invoke(stop);

            Assert.IsFalse(audioSource.isPlaying, "The source did not stop");
        }

        [UnityTest]
        public IEnumerator PauseAndUnPause_SuspendAndResume()
        {
            var gameObject = NewAudioObject(out var audioSource);
            var pause = gameObject.AddComponent<AudioSourcePauseMonoBinder>();
            var unpause = gameObject.AddComponent<AudioSourceUnPauseMonoBinder>();

            audioSource.Play();
            yield return null;

            Invoke(pause);
            Assert.IsFalse(audioSource.isPlaying, "Pause did not take effect");

            Invoke(unpause);
            Assert.IsTrue(audioSource.isPlaying, "Resume did not take effect");
        }

        /// <summary>
        /// The operation must not run on an inactive object, and the command must say so before it is called.
        /// </summary>
        [UnityTest]
        public IEnumerator OnAnInactiveObject_TheCommandRefuses()
        {
            var (audioSource, play) = Create<AudioSourcePlayMonoBinder>();
            yield return null;

            play.gameObject.SetActive(false);

            IRelayCommand received = null;
            ((IBinder)play).Bind(new OneWayToSourceBindableMember<IRelayCommand>(value => received = value));

            Assert.IsNotNull(received, "The command did not reach the ViewModel");
            Assert.IsFalse(received.CanExecute(), "The command agreed to run on a disabled object");

            received.Execute();
            Assert.IsFalse(audioSource.isPlaying, "The source started on a disabled object");
        }

        private static void Invoke(AudioSourcePlaybackMonoBinder binder)
        {
            Action received = null;
            ((IBinder)binder).Bind(new OneWayToSourceBindableMember<Action>(value => received = value));

            Assert.IsNotNull(received, "The binder did not hand an action to the ViewModel");
            received.Invoke();
        }

        private (AudioSource audioSource, TBinder binder) Create<TBinder>()
            where TBinder : AudioSourcePlaybackMonoBinder
        {
            var gameObject = NewAudioObject(out var audioSource);
            return (audioSource, gameObject.AddComponent<TBinder>());
        }

        private GameObject NewAudioObject(out AudioSource audioSource)
        {
            var gameObject = Spawn("Audio");
            var clip = Track(AudioClip.Create("playback", 44100, 1, 44100, false));

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = 0f;

            return gameObject;
        }
    }
}
