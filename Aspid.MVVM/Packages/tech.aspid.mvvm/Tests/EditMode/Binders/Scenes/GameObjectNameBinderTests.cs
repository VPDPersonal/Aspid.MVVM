using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="GameObject.name"/> binder.
    /// </summary>
    [TestFixture]
    public sealed class GameObjectNameBinderTests : SceneFixture
    {
        [Test]
        public void Name_ReachesTheObject_AndNullClearsIt()
        {
            var gameObject = Spawn("Named");
            var binder = gameObject.AddComponent<GameObjectNameMonoBinder>();

            ((IBinder<string>)binder).SetValue("Slot 3");
            Assert.AreEqual("Slot 3", gameObject.name, "The name did not reach the object");

            ((IBinder<string>)binder).SetValue(null);
            Assert.AreEqual(string.Empty, gameObject.name, "A null value did not clear the object's name");
        }
    }
}
