using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the binder that hands the GameObject itself back to the ViewModel.
    /// </summary>
    [TestFixture]
    public sealed class GameObjectToSourceBinderTests : SceneFixture
    {
        [Test]
        public void GameObjectToSourceMonoBinder_HandsOverTheObject()
        {
            var gameObject = Spawn();
            var binder = gameObject.AddComponent<GameObjectToSourceMonoBinder>();

            GameObject received = null;
            binder.Bind(new OneWayToSourceBindableMember<GameObject>(value => received = value));

            Assert.AreSame(gameObject, received, "The ViewModel did not receive the GameObject");
        }
    }
}
