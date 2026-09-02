using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="GameObject.layer"/> binder.
    /// </summary>
    [TestFixture]
    public sealed class GameObjectLayerBinderTests : SceneFixture
    {
        [Test]
        public void Layer_ReachesTheObject()
        {
            var gameObject = Spawn("Object");
            var binder = gameObject.AddComponent<GameObjectLayerMonoBinder>();

            ((IBinder<int>)binder).SetValue(5);

            Assert.AreEqual(5, gameObject.layer, "The layer did not reach the object");
        }

        /// <summary>
        /// Unity has 32 layers and silently keeps the previous one for an index outside them, so the binder says so
        /// instead.
        /// </summary>
        [Test]
        public void ALayerThatDoesNotExist_IsReported()
        {
            var gameObject = Spawn("Object");
            var binder = gameObject.AddComponent<GameObjectLayerMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("Layer 40 does not exist"));
            ((IBinder<int>)binder).SetValue(40);

            Assert.AreEqual(0, gameObject.layer, "A non-existent layer was written anyway");
        }

        [Test]
        public void Layer_OneWayToSource_ReportsTheCurrentLayer()
        {
            var gameObject = Spawn("Object");
            gameObject.layer = 9;

            var binder = new GameObjectLayerBinder(gameObject, mode: BindMode.OneWayToSource);
            var received = -1;

            binder.Bind(new OneWayToSourceStructBindableMember<int>(value => received = value));

            Assert.AreEqual(9, received, "The ViewModel did not receive the current layer");
        }
    }
}
