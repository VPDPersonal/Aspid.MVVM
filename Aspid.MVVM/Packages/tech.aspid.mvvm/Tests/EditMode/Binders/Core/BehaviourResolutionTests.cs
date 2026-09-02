using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for a behaviour binder resolving itself as its own target.
    /// </summary>
    /// <remarks>
    /// These binders are typed on <see cref="Behaviour"/>, so the automatic <c>GetComponent</c> fallback matches
    /// every behaviour on the object, binders included, and the fallback now skips binders themselves.
    /// </remarks>
    [TestFixture]
    public sealed class BehaviourResolutionTests : SceneFixture
    {
        [Test]
        public void WithNoOtherBehaviour_TheBinderDoesNotTargetItself()
        {
            var binder = Spawn().AddComponent<BehaviourEnabledMonoBinder>();

            Assert.IsFalse(binder.CanBind, "The binder chose itself as the target");
        }

        [Test]
        public void WithAnotherBehaviour_ThatOneIsChosen()
        {
            var gameObject = Spawn();
            var image = gameObject.AddComponent<Image>();
            var binder = gameObject.AddComponent<BehaviourEnabledMonoBinder>();

            ((IBinder<bool>)binder).SetValue(false);

            Assert.IsFalse(image.enabled, "The value did not reach the neighbouring Behaviour");
            Assert.IsTrue(binder.enabled, "The binder disabled itself");
        }
    }
}
