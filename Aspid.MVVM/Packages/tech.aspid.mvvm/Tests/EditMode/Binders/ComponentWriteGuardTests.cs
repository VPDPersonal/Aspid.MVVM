using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for the two numeric writes Unity does not police on the binder's behalf.
    /// </summary>
    /// <remarks>
    /// Most of the clamps the audit proposed turned out to be unnecessary, and the tests below draw the line by
    /// pinning Unity's own behaviour next to each fix. Unity refuses a negative, over-long or non-finite
    /// <see cref="AudioSource.time"/> and a non-finite collider extent, all on its own — so no binder guards those.
    /// It stores an out-of-range <see cref="AudioSource.timeSamples"/> and a negative collider extent exactly as
    /// given, and those are the two that are guarded.
    /// <para/>
    /// If a future Unity version starts rejecting them upstream, the pinning tests here fail and say the guards
    /// have become redundant.
    /// </remarks>
    [TestFixture]
    public sealed class ComponentWriteGuardTests
    {
        private const int SampleRate = 44100;

        private readonly List<Object> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var spawned in _spawned)
            {
                if (spawned) Object.DestroyImmediate(spawned);
            }

            _spawned.Clear();
        }

        [Test]
        public void UnityTimeSamples_StoresAPositionOutsideTheClip()
        {
            var (audioSource, clip) = NewAudioSource();

            audioSource.timeSamples = clip.samples + 1000;
            Assert.AreEqual(clip.samples + 1000, audioSource.timeSamples, "Unity начала отсекать позицию за концом клипа");

            audioSource.timeSamples = -5;
            Assert.AreEqual(-5, audioSource.timeSamples, "Unity начала отсекать отрицательную позицию");
        }

        [Test]
        public void SetTimeSamples_BeyondTheClip_StopsAtTheLastSample()
        {
            var (audioSource, clip) = NewAudioSource();

            audioSource.SetTimeSamples(clip.samples + 1000);

            Assert.AreEqual(clip.samples - 1, audioSource.timeSamples, "Позиция за концом клипа не обрезана");
        }

        [Test]
        public void SetTimeSamples_BelowZero_StopsAtTheStart()
        {
            var (audioSource, _) = NewAudioSource();

            audioSource.SetTimeSamples(-5);

            Assert.AreEqual(0, audioSource.timeSamples, "Отрицательная позиция не обрезана");
        }

        [Test]
        public void SetTimeSamples_WithNoClip_LeavesTheSourceAlone()
        {
            var gameObject = NewGameObject();
            var audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.SetTimeSamples(100);

            Assert.AreEqual(0, audioSource.timeSamples, "Позиция записана в источник без клипа");
        }

        /// <summary>
        /// Drives a real binder, not the helper: the three tests above would keep passing if the binders were
        /// never routed through <c>SetTimeSamples</c> at all.
        /// </summary>
        [Test]
        public void TimeSamplesBinder_BeyondTheClip_StopsAtTheLastSample()
        {
            var gameObject = NewGameObject();
            var audioSource = gameObject.AddComponent<AudioSource>();

            var clip = AudioClip.Create("guard", SampleRate, 1, SampleRate, false);
            _spawned.Add(clip);
            audioSource.clip = clip;

            var binder = gameObject.AddComponent<AudioSourceTimeSamplesMonoBinder>();
            ((IBinder<int>)binder).SetValue(clip.samples + 1000);

            Assert.AreEqual(clip.samples - 1, audioSource.timeSamples, "Биндер записал позицию за концом клипа");
        }

        [Test]
        public void UnityCollider_StoresANegativeExtent()
        {
            var box = NewGameObject().AddComponent<BoxCollider>();

            box.size = new Vector3(-1f, 2f, 3f);

            Assert.AreEqual(-1f, box.size.x, "Unity начала отсекать отрицательный размер коллайдера");
        }

        [Test]
        public void ColliderSizeBinder_WithANegativeComponent_RaisesItToZero()
        {
            var gameObject = NewGameObject();
            gameObject.AddComponent<BoxCollider>();

            var binder = gameObject.AddComponent<BoxColliderSizeMonoBinder>();
            var box = gameObject.GetComponent<BoxCollider>();

            ((IBinder<Vector3>)binder).SetValue(new Vector3(-1f, 2f, 3f));

            Assert.AreEqual(new Vector3(0f, 2f, 3f), box.size, "Отрицательная сторона не поднята до нуля");
        }

        [Test]
        public void ColliderRadiusBinder_WithANegativeValue_RaisesItToZero()
        {
            var gameObject = NewGameObject();
            gameObject.AddComponent<SphereCollider>();

            var binder = gameObject.AddComponent<SphereColliderRadiusMonoBinder>();
            var sphere = gameObject.GetComponent<SphereCollider>();

            ((IBinder<float>)binder).SetValue(-5f);

            Assert.AreEqual(0f, sphere.radius, "Отрицательный радиус не поднят до нуля");
        }

        private (AudioSource audioSource, AudioClip clip) NewAudioSource()
        {
            var audioSource = NewGameObject().AddComponent<AudioSource>();

            var clip = AudioClip.Create("guard", SampleRate, 1, SampleRate, false);
            _spawned.Add(clip);

            audioSource.clip = clip;
            return (audioSource, clip);
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("WriteGuard");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
