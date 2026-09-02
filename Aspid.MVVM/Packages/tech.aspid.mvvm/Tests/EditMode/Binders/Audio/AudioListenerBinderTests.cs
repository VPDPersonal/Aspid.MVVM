using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the two static <see cref="AudioListener"/> binders: volume and pause.
    /// </summary>
    [TestFixture]
    public sealed class AudioListenerBinderTests : SceneFixture
    {
        [SetUp]
        public void SetUp()
        {
            var volume = AudioListener.volume;
            var paused = AudioListener.pause;

            RestoreOnTearDown(() =>
            {
                AudioListener.volume = volume;
                AudioListener.pause = paused;
            });
        }

        [Test]
        public void Volume_ReachesTheListener_AndIsClamped()
        {
            var binder = NewBinder<AudioListenerVolumeMonoBinder>();

            ((IBinder<float>)binder).SetValue(0.25f);
            Assert.AreEqual(0.25f, AudioListener.volume, 0.001f, "The volume did not reach the listener");

            ((IBinder<float>)binder).SetValue(5f);
            Assert.AreEqual(1f, AudioListener.volume, 0.001f, "A volume outside 0..1 was not clamped");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.IsFalse(float.IsNaN(AudioListener.volume), "NaN reached the listener");
        }

        [Test]
        public void Volume_OneWayToSource_ReportsTheCurrentVolume()
        {
            AudioListener.volume = 0.4f;

            var binder = new AudioListenerVolumeBinder(mode: BindMode.OneWayToSource);
            var received = float.NaN;

            binder.Bind(new OneWayToSourceStructBindableMember<float>(value => received = value));

            Assert.AreEqual(0.4f, received, 0.001f, "The ViewModel did not receive the current volume");
        }

        [Test]
        public void Pause_ReachesTheListener()
        {
            var binder = NewBinder<AudioListenerPauseMonoBinder>();

            ((IBinder<bool>)binder).SetValue(true);
            Assert.IsTrue(AudioListener.pause, "The pause did not reach the listener");

            ((IBinder<bool>)binder).SetValue(false);
            Assert.IsFalse(AudioListener.pause, "Lifting the pause did not reach the listener");
        }

        /// <summary>
        /// A two-way converter has to apply in both directions, or a ViewModel bound in
        /// <see cref="BindMode.OneWayToSource"/> would be told the opposite of what it would have sent.
        /// </summary>
        [Test]
        public void Pause_Inverted_ReportsTheValueTheViewModelWouldHaveSent()
        {
            AudioListener.pause = true;

            var binder = new AudioListenerPauseBinder(new BoolInvertConverter(), BindMode.OneWayToSource);
            var received = true;

            binder.Bind(new OneWayToSourceStructBindableMember<bool>(value => received = value));

            Assert.IsFalse(received, "The inversion did not apply in the reverse direction");
        }

        [Test]
        public void Pause_Inverted_AppliesTheOppositeValue()
        {
            var binder = new AudioListenerPauseBinder(new BoolInvertConverter());

            binder.SetValue(true);

            Assert.IsFalse(AudioListener.pause, "The inversion did not apply");
        }

        [Test]
        public void TheStaticBinders_RefuseTwoWay()
        {
            Assert.Throws<System.ArgumentException>(() => _ = new AudioListenerVolumeBinder(mode: BindMode.TwoWay));
            Assert.Throws<System.ArgumentException>(() => _ = new AudioListenerPauseBinder(mode: BindMode.TwoWay));
        }

        private T NewBinder<T>()
            where T : MonoBinder
        {
            var binder = Spawn(typeof(T).Name).AddComponent<T>();
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }
    }
}
