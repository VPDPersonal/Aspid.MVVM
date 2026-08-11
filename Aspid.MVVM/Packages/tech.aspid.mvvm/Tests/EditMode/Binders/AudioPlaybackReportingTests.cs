using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the last two gaps in the AudioSource domain: playing a clip the ViewModel supplies, and telling the
    /// ViewModel that a sound has finished.
    /// </summary>
    /// <remarks>
    /// The playback binders could start and stop the clip a source was configured with. A sound per event had to reach
    /// for the component by hand, and nothing could report that a voice line had ended — so a button that should
    /// re-enable itself afterwards had nothing to listen to.
    /// </remarks>
    [TestFixture]
    public sealed class AudioPlaybackReportingTests
    {
        private readonly List<GameObject> _spawned = new();
        private readonly List<Object> _assets = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            foreach (var asset in _assets)
            {
                if (asset) Object.DestroyImmediate(asset);
            }

            _spawned.Clear();
            _assets.Clear();
        }

        [Test]
        public void PlayOneShot_ANullClipDoesNothing()
        {
            var binder = NewPlayOneShot();

            Assert.DoesNotThrow(() => ((IBinder<AudioClip>)binder).SetValue(null), "Null-клип уронил биндер");
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

            Object.DestroyImmediate(clip);

            Assert.DoesNotThrow(() => ((IBinder<AudioClip>)binder).SetValue(clip), "Уничтоженный клип уронил биндер");
        }

        [Test]
        public void PlayOneShot_ALiveClipIsAccepted()
        {
            var binder = NewPlayOneShot();
            var clip = NewClip();

            Assert.DoesNotThrow(() => ((IBinder<AudioClip>)binder).SetValue(clip), "Живой клип не проиграл");
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

            Assert.AreEqual(new List<bool> { false }, received, "Состояние при установке связи не сообщено");
        }

        [Test]
        public void IsPlaying_TheDefaultModeIsTheOnlyOneItSupports()
        {
            var source = NewSource();
            var binder = source.gameObject.AddComponent<AudioSourceIsPlayingToSourceMonoBinder>();

            Assert.AreEqual(BindMode.OneWayToSource, binder.Mode, "Режим по умолчанию не OneWayToSource");
        }

        private AudioSourcePlayOneShotMonoBinder NewPlayOneShot() =>
            NewSource().gameObject.AddComponent<AudioSourcePlayOneShotMonoBinder>();

        private AudioSource NewSource()
        {
            var gameObject = new GameObject("AudioSource");
            _spawned.Add(gameObject);

            return gameObject.AddComponent<AudioSource>();
        }

        private AudioClip NewClip()
        {
            var clip = AudioClip.Create("Clip", 128, 1, 44100, stream: false);
            _assets.Add(clip);

            return clip;
        }
    }
}
