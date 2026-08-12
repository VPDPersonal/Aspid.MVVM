using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the two families that had no ToSource binder while eight others did, and for the one property of
    /// <see cref="ToggleGroup"/>.
    /// </summary>
    [TestFixture]
    public sealed class ToSourceSymmetryTests
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
        public void SelectableToSource_HandsOverTheControl()
        {
            var gameObject = NewGameObject();
            var button = gameObject.AddComponent<Button>();
            var binder = gameObject.AddComponent<SelectableToSourceMonoBinder>();

            Selectable received = null;
            binder.Bind(new OneWayToSourceBindableMember<Selectable>(value => received = value));

            Assert.AreSame(button, received, "ViewModel не получила Selectable");
        }

        [Test]
        public void GameObjectToSource_HandsOverTheObject()
        {
            var gameObject = NewGameObject();
            var binder = gameObject.AddComponent<GameObjectToSourceMonoBinder>();

            GameObject received = null;
            binder.Bind(new OneWayToSourceBindableMember<GameObject>(value => received = value));

            Assert.AreSame(gameObject, received, "ViewModel не получила GameObject");
        }

        [Test]
        public void TheToSourceBinders_DefaultToTheOnlyModeTheySupport()
        {
            var gameObject = NewGameObject();
            gameObject.AddComponent<Button>();

            Assert.AreEqual(BindMode.OneWayToSource, gameObject.AddComponent<SelectableToSourceMonoBinder>().Mode);
            Assert.AreEqual(BindMode.OneWayToSource, gameObject.AddComponent<GameObjectToSourceMonoBinder>().Mode);
        }

        [Test]
        public void AllowSwitchOff_ReachesTheGroup()
        {
            var gameObject = NewGameObject();
            var group = gameObject.AddComponent<ToggleGroup>();
            var binder = gameObject.AddComponent<ToggleGroupAllowSwitchOffMonoBinder>();

            ((IBinder<bool>)binder).SetValue(true);
            Assert.IsTrue(group.allowSwitchOff, "Разрешение снять выбор не доехало");

            ((IBinder<bool>)binder).SetValue(false);
            Assert.IsFalse(group.allowSwitchOff, "Запрет снять выбор не доехал");
        }

        [Test]
        public void TheSerializableTwin_AcceptsItsTarget()
        {
            var group = NewGameObject().AddComponent<ToggleGroup>();

            Assert.IsTrue(new ToggleGroupAllowSwitchOffBinder(group).IsBind);
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("ToSource");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
