using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for the two numeric writes Unity does not police on the binder's behalf.
    /// </summary>
    /// <remarks>
    /// Unity refuses a negative, over-long or non-finite <see cref="AudioSource.time"/> and a non-finite collider
    /// extent, all on its own — so no binder guards those. It stores an out-of-range
    /// <see cref="AudioSource.timeSamples"/> and a negative collider extent exactly as given, and those are the
    /// two that are guarded.
    /// <para/>
    /// If a future Unity version starts rejecting them upstream, the pinning tests here fail and say the guards
    /// have become redundant.
    /// </remarks>
    [TestFixture]
    public sealed class ComponentWriteGuardTests : SceneFixture
    {
        private const int SampleRate = 44100;

        [Test]
        public void UnityTimeSamples_StoresAPositionOutsideTheClip()
        {
            var (audioSource, clip) = NewAudioSource();

            audioSource.timeSamples = clip.samples + 1000;
            Assert.AreEqual(clip.samples + 1000, audioSource.timeSamples, "Unity started clipping a position past the clip's end");

            audioSource.timeSamples = -5;
            Assert.AreEqual(-5, audioSource.timeSamples, "Unity started clipping a negative position");
        }

        [Test]
        public void SetTimeSamples_BeyondTheClip_StopsAtTheLastSample()
        {
            var (audioSource, clip) = NewAudioSource();

            audioSource.SetTimeSamples(clip.samples + 1000);

            Assert.AreEqual(clip.samples - 1, audioSource.timeSamples, "The position past the clip's end was not clamped");
        }

        [Test]
        public void SetTimeSamples_BelowZero_StopsAtTheStart()
        {
            var (audioSource, _) = NewAudioSource();

            audioSource.SetTimeSamples(-5);

            Assert.AreEqual(0, audioSource.timeSamples, "The negative position was not clamped");
        }

        [Test]
        public void SetTimeSamples_WithNoClip_LeavesTheSourceAlone()
        {
            var audioSource = Spawn<AudioSource>();

            audioSource.SetTimeSamples(100);

            Assert.AreEqual(0, audioSource.timeSamples, "A position was written into a source with no clip");
        }

        /// <summary>
        /// Drives a real binder, not the helper: the three tests above would keep passing if the binders were
        /// never routed through <c>SetTimeSamples</c> at all.
        /// </summary>
        [Test]
        public void TimeSamplesBinder_BeyondTheClip_StopsAtTheLastSample()
        {
            var audioSource = Spawn<AudioSource>();

            var clip = Track(AudioClip.Create("guard", SampleRate, 1, SampleRate, false));
            audioSource.clip = clip;

            var binder = audioSource.gameObject.AddComponent<AudioSourceTimeSamplesMonoBinder>();
            ((IBinder<int>)binder).SetValue(clip.samples + 1000);

            Assert.AreEqual(clip.samples - 1, audioSource.timeSamples, "The binder wrote a position past the clip's end");
        }

        [Test]
        public void UnityCollider_StoresANegativeExtent()
        {
            var box = Spawn<BoxCollider>();

            box.size = new Vector3(-1f, 2f, 3f);

            Assert.AreEqual(-1f, box.size.x, "Unity started clipping a negative collider size");
        }

        [Test]
        public void ColliderSizeBinder_WithANegativeComponent_RaisesItToZero()
        {
            var box = Spawn<BoxCollider>();
            var binder = box.gameObject.AddComponent<BoxColliderSizeMonoBinder>();

            ((IBinder<Vector3>)binder).SetValue(new Vector3(-1f, 2f, 3f));

            Assert.AreEqual(new Vector3(0f, 2f, 3f), box.size, "The negative side was not raised to zero");
        }

        [Test]
        public void ColliderRadiusBinder_WithANegativeValue_RaisesItToZero()
        {
            var sphere = Spawn<SphereCollider>();
            var binder = sphere.gameObject.AddComponent<SphereColliderRadiusMonoBinder>();

            ((IBinder<float>)binder).SetValue(-5f);

            Assert.AreEqual(0f, sphere.radius, "The negative radius was not raised to zero");
        }

        private (AudioSource audioSource, AudioClip clip) NewAudioSource()
        {
            var audioSource = Spawn<AudioSource>();
            var clip = Track(AudioClip.Create("guard", SampleRate, 1, SampleRate, false));

            audioSource.clip = clip;
            return (audioSource, clip);
        }
    }
}
