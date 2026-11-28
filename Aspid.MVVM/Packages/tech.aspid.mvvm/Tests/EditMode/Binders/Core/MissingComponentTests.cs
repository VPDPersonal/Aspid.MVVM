using System;
using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for a <c>*MonoBinder</c> placed on a GameObject that has no component for it to drive.
    /// </summary>
    [TestFixture]
    public sealed class MissingComponentTests : SceneFixture
    {
        [Test]
        public void Bind_WithoutAComponent_ThrowsAndCanRetry()
        {
            var binder = Spawn().AddComponent<AudioSourceVolumeMonoBinder>();
            var context = new BindContext(new OneWayBindableMember<float>(0.5f), this);

            Assert.Throws<InvalidOperationException>(() => binder.Bind(context));
            Assert.That(binder.IsBound, Is.False);

            var audioSource = binder.gameObject.AddComponent<AudioSource>();
            binder.Bind(context);

            Assert.That(binder.IsBound, Is.True);
            Assert.That(audioSource.volume, Is.EqualTo(0.5f));
            binder.Unbind(new UnbindContext(this));
        }

        [Test]
        public void WithItsComponent_TheBinderStillBinds()
        {
            var gameObject = Spawn();
            var audioSource = gameObject.AddComponent<AudioSource>();
            var binder = gameObject.AddComponent<AudioSourceVolumeMonoBinder>();

            ((IBinder<float>)binder).SetValue(0.25f);
            Assert.AreEqual(0.25f, audioSource.volume, 0.001f, "The value did not reach the component");
        }
    }
}
