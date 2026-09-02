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
        public void WithoutItsComponent_TheBinderRefusesToBind()
        {
            var binder = Spawn().AddComponent<AudioSourceVolumeMonoBinder>();

            Assert.IsFalse(binder.CanBind, "The binder agreed to bind without its component");
        }

        [Test]
        public void WithoutItsComponent_BindingDeliversNothingInsteadOfThrowing()
        {
            var binder = Spawn().AddComponent<AudioSourceVolumeMonoBinder>();
            var member = new OneWayBindableMember<float>(0.5f);

            Assert.DoesNotThrow(() => ((IBinder)binder).Bind(member),
                "Binding without a component still throws");
        }

        [Test]
        public void WithItsComponent_TheBinderStillBinds()
        {
            var gameObject = Spawn();
            var audioSource = gameObject.AddComponent<AudioSource>();
            var binder = gameObject.AddComponent<AudioSourceVolumeMonoBinder>();

            Assert.IsTrue(binder.CanBind, "The binder refused to bind with its component present");

            ((IBinder<float>)binder).SetValue(0.25f);
            Assert.AreEqual(0.25f, audioSource.volume, 0.001f, "The value did not reach the component");
        }
    }
}
