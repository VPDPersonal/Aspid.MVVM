using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="UnityEngine.Audio.AudioMixer"/> parameter and snapshot binders.
    /// </summary>
    /// <remarks>
    /// A mixer cannot be created from a test: <see cref="UnityEngine.Audio.AudioMixer"/> assets are authored, not
    /// constructed. What is pinned here is the behaviour that does not need one — that every missing reference is
    /// reported rather than silently ignored.
    /// </remarks>
    [TestFixture]
    public sealed class AudioMixerBinderTests : SceneFixture
    {
        [Test]
        public void MixerFloat_WithoutAMixer_SaysSo()
        {
            var binder = NewBinder<AudioMixerFloatMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("no mixer is assigned"));
            ((IBinder<float>)binder).SetValue(1f);
        }

        [Test]
        public void MixerSnapshot_WithoutSnapshots_SaysSo()
        {
            var binder = NewBinder<AudioMixerSnapshotMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("no snapshots are assigned"));
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
            Assert.IsFalse(new AudioMixerSnapshotBinder(System.Array.Empty<UnityEngine.Audio.AudioMixerSnapshot>()).CanBind);
        }

        [Test]
        public void MixerSnapshot_ANullNameDoesNothing()
        {
            var binder = new AudioMixerSnapshotBinder(new UnityEngine.Audio.AudioMixerSnapshot[1]);

            Assert.DoesNotThrow(() => binder.SetValue((string)null), "A null snapshot name did something");
        }

        [Test]
        public void TheSnapshotBinder_AcceptsOnlyOneWayAndOneTime()
        {
            var snapshots = new UnityEngine.Audio.AudioMixerSnapshot[1];

            Assert.Throws<System.InvalidOperationException>(
                () => _ = new AudioMixerSnapshotBinder(snapshots, mode: BindMode.OneWayToSource),
                "OneWayToSource was accepted by a mode with nothing to read back");
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
