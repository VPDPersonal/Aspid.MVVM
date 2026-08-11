using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the mixing and listening binders: the <see cref="UnityEngine.Audio.AudioMixer"/> parameter and
    /// snapshot binders, and the two static <see cref="AudioListener"/> ones.
    /// </summary>
    /// <remarks>
    /// The package bound <see cref="AudioSource"/> thoroughly and the two things above it not at all, so a sound
    /// settings screen — the most common audio UI there is — had no binder to build on.
    /// <para/>
    /// A mixer cannot be created from a test: <see cref="UnityEngine.Audio.AudioMixer"/> assets are authored, not
    /// constructed. What is pinned here is the behaviour that does not need one — the listener properties, and that
    /// every missing reference is reported rather than silently ignored.
    /// </remarks>
    [TestFixture]
    public sealed class AudioMixingTests
    {
        private readonly List<GameObject> _spawned = new();

        private float _volume;
        private bool _paused;

        [SetUp]
        public void SetUp()
        {
            _volume = AudioListener.volume;
            _paused = AudioListener.pause;
        }

        [TearDown]
        public void TearDown()
        {
            AudioListener.volume = _volume;
            AudioListener.pause = _paused;

            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        #region AudioListener
        [Test]
        public void ListenerVolume_ReachesTheListener_AndIsClamped()
        {
            var binder = NewBinder<AudioListenerVolumeMonoBinder>(BindMode.OneWay);

            ((IBinder<float>)binder).SetValue(0.25f);
            Assert.AreEqual(0.25f, AudioListener.volume, 0.001f, "Громкость не доехала до слушателя");

            ((IBinder<float>)binder).SetValue(5f);
            Assert.AreEqual(1f, AudioListener.volume, 0.001f, "Громкость вне 0..1 не обрезана");

            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.IsFalse(float.IsNaN(AudioListener.volume), "NaN дошёл до слушателя");
        }

        [Test]
        public void ListenerVolume_OneWayToSource_ReportsTheCurrentVolume()
        {
            AudioListener.volume = 0.4f;

            var binder = new AudioListenerVolumeBinder(BindMode.OneWayToSource);
            var received = float.NaN;

            binder.Bind(new OneWayToSourceStructBindableMember<float>(value => received = value));

            Assert.AreEqual(0.4f, received, 0.001f, "ViewModel не получила текущую громкость");
        }

        [Test]
        public void ListenerPause_ReachesTheListener()
        {
            var binder = NewBinder<AudioListenerPauseMonoBinder>(BindMode.OneWay);

            ((IBinder<bool>)binder).SetValue(true);
            Assert.IsTrue(AudioListener.pause, "Пауза не доехала до слушателя");

            ((IBinder<bool>)binder).SetValue(false);
            Assert.IsFalse(AudioListener.pause, "Снятие паузы не доехало до слушателя");
        }

        /// <summary>
        /// The Invert option has to apply in both directions, or a ViewModel bound in
        /// <see cref="BindMode.OneWayToSource"/> would be told the opposite of what it would have sent.
        /// </summary>
        [Test]
        public void ListenerPause_Inverted_ReportsTheValueTheViewModelWouldHaveSent()
        {
            AudioListener.pause = true;

            var binder = new AudioListenerPauseBinder(isInvert: true, BindMode.OneWayToSource);
            var received = true;

            binder.Bind(new OneWayToSourceStructBindableMember<bool>(value => received = value));

            Assert.IsFalse(received, "Инверсия не применилась в обратном направлении");
        }

        [Test]
        public void ListenerPause_Inverted_AppliesTheOppositeValue()
        {
            var binder = new AudioListenerPauseBinder(isInvert: true);

            binder.SetValue(true);

            Assert.IsFalse(AudioListener.pause, "Инверсия не применилась");
        }
        #endregion

        #region Missing references are reported
        [Test]
        public void MixerFloat_WithoutAMixer_SaysSo()
        {
            var binder = NewBinder<AudioMixerFloatMonoBinder>(BindMode.OneWay);

            LogAssert.Expect(LogType.Error, new Regex("No mixer assigned"));
            ((IBinder<float>)binder).SetValue(1f);
        }

        [Test]
        public void MixerSnapshot_WithoutSnapshots_SaysSo()
        {
            var binder = NewBinder<AudioMixerSnapshotMonoBinder>(BindMode.OneWay);

            LogAssert.Expect(LogType.Error, new Regex("No snapshots assigned"));
            ((IBinder<int>)binder).SetValue(0);
        }

        [Test]
        public void MixerSnapshot_AnIndexOutsideTheList_SaysSo()
        {
            var binder = new AudioMixerSnapshotBinder(new UnityEngine.Audio.AudioMixerSnapshot[1]);

            LogAssert.Expect(LogType.Error, new Regex("outside the list"));
            binder.SetValue(7);
        }

        [Test]
        public void MixerSnapshot_AnEmptySlot_SaysSo()
        {
            var binder = new AudioMixerSnapshotBinder(new UnityEngine.Audio.AudioMixerSnapshot[1]);

            LogAssert.Expect(LogType.Error, new Regex("empty slot"));
            binder.SetValue(0);
        }

        /// <summary>
        /// A binder with nothing to transition must not bind: the alternative is an error on every value the
        /// ViewModel publishes.
        /// </summary>
        [Test]
        public void MixerSnapshot_WithoutSnapshots_RefusesToBind()
        {
            Assert.IsFalse(new AudioMixerSnapshotBinder(System.Array.Empty<UnityEngine.Audio.AudioMixerSnapshot>()).IsBind);
        }

        [Test]
        public void MixerSnapshot_ANullNameDoesNothing()
        {
            var binder = new AudioMixerSnapshotBinder(new UnityEngine.Audio.AudioMixerSnapshot[1]);

            Assert.DoesNotThrow(() => binder.SetValue((string)null), "Null-имя снапшота не должно ничего делать");
        }
        #endregion

        #region Modes
        [Test]
        public void TheStaticBinders_RefuseTwoWay()
        {
            Assert.Throws<System.ArgumentException>(() => _ = new AudioListenerVolumeBinder(BindMode.TwoWay));
            Assert.Throws<System.ArgumentException>(() => _ = new AudioListenerPauseBinder(mode: BindMode.TwoWay));
        }

        [Test]
        public void TheSnapshotBinder_AcceptsOnlyOneWayAndOneTime()
        {
            var snapshots = new UnityEngine.Audio.AudioMixerSnapshot[1];

            Assert.Throws<System.InvalidOperationException>(
                () => _ = new AudioMixerSnapshotBinder(snapshots, mode: BindMode.OneWayToSource),
                "OneWayToSource принят режимом, в котором нечего читать обратно");
        }
        #endregion

        private T NewBinder<T>(BindMode mode)
            where T : MonoBinder
        {
            var gameObject = new GameObject(typeof(T).Name);
            _spawned.Add(gameObject);

            var binder = gameObject.AddComponent<T>();
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)mode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }
    }
}
