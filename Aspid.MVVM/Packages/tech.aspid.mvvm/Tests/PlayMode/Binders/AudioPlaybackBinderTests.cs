#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the new playback binders, which hand the ViewModel one operation on an <see cref="AudioSource"/>.
    /// </summary>
    /// <remarks>
    /// The package bound every AudioSource property and none of its operations, so a ViewModel could set the volume
    /// of a sound it had no way to start. These run in play mode because <c>AudioSource.isPlaying</c> only means
    /// anything while the game is running.
    /// </remarks>
    [TestFixture]
    public sealed class AudioPlaybackBinderTests
    {
        private readonly List<UnityEngine.Object> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var spawned in _spawned)
            {
                if (spawned) UnityEngine.Object.Destroy(spawned);
            }

            _spawned.Clear();
        }

        [UnityTest]
        public IEnumerator PlayBinder_StartsTheSource()
        {
            var (audioSource, play) = Create<AudioSourcePlayMonoBinder>();
            yield return null;

            Invoke(play);

            Assert.IsTrue(audioSource.isPlaying, "Источник не запустился");
        }

        [UnityTest]
        public IEnumerator StopBinder_StopsTheSource()
        {
            var (audioSource, stop) = Create<AudioSourceStopMonoBinder>();
            audioSource.Play();
            yield return null;

            Invoke(stop);

            Assert.IsFalse(audioSource.isPlaying, "Источник не остановился");
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
            Assert.IsFalse(audioSource.isPlaying, "Пауза не сработала");

            Invoke(unpause);
            Assert.IsTrue(audioSource.isPlaying, "Возобновление не сработало");
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

            Assert.IsNotNull(received, "Команда не доехала до ViewModel");
            Assert.IsFalse(received.CanExecute(), "Команда согласилась выполниться на выключенном объекте");

            received.Execute();
            Assert.IsFalse(audioSource.isPlaying, "Источник запустился на выключенном объекте");
        }

        private static void Invoke(AudioSourcePlaybackMonoBinder binder)
        {
            Action received = null;
            ((IBinder)binder).Bind(new OneWayToSourceBindableMember<Action>(value => received = value));

            Assert.IsNotNull(received, "Биндер не отдал действие во ViewModel");
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
            var gameObject = new GameObject("Audio");
            _spawned.Add(gameObject);

            var clip = AudioClip.Create("playback", 44100, 1, 44100, false);
            _spawned.Add(clip);

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = 0f;

            return gameObject;
        }
    }
}
#endif
