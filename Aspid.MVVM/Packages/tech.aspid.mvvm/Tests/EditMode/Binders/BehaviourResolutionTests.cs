using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for a behaviour binder resolving itself as its own target.
    /// </summary>
    /// <remarks>
    /// These binders are typed on <see cref="Behaviour"/>, so the automatic <c>GetComponent</c> fallback matches
    /// every behaviour on the object — binders included — and component order decides the winner. On an object
    /// carrying little else that is the binder itself, which then enables and disables itself, stops receiving
    /// values, and leaves nothing in the log to explain it. The fallback now skips binders; which of the remaining
    /// behaviours is meant is still the author's choice, made by filling the field.
    /// </remarks>
    [TestFixture]
    public sealed class BehaviourResolutionTests
    {
        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        [Test]
        public void WithNoOtherBehaviour_TheBinderDoesNotTargetItself()
        {
            var binder = NewGameObject().AddComponent<BehaviourEnabledMonoBinder>();

            Assert.IsFalse(binder.IsBind, "Биндер выбрал целью самого себя");
        }

        [Test]
        public void WithAnotherBehaviour_ThatOneIsChosen()
        {
            var gameObject = NewGameObject();
            var image = gameObject.AddComponent<Image>();
            var binder = gameObject.AddComponent<BehaviourEnabledMonoBinder>();

            ((IBinder<bool>)binder).SetValue(false);

            Assert.IsFalse(image.enabled, "Значение не доехало до соседнего Behaviour");
            Assert.IsTrue(binder.enabled, "Биндер выключил самого себя");
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("Behaviour");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
