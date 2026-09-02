using System;
using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Base fixture that owns the Unity objects a test creates and restores any global state it changed, both on
    /// tear-down.
    /// </summary>
    public abstract class SceneFixture
    {
        private readonly List<Object> _owned = new();
        private readonly List<Action> _restorers = new();

        /// <summary>
        /// Creates a GameObject that is destroyed after the test.
        /// </summary>
        /// <param name="name">The GameObject name.</param>
        /// <returns>The new GameObject.</returns>
        protected GameObject Spawn(string name = "Probe") =>
            Track(new GameObject(name));

        /// <summary>
        /// Creates a GameObject with a <typeparamref name="T"/> that is destroyed after the test.
        /// </summary>
        /// <typeparam name="T">The component to add.</typeparam>
        /// <param name="name">The GameObject name.</param>
        /// <returns>The added component.</returns>
        protected T Spawn<T>(string name = "Probe")
            where T : Component =>
            Spawn(name).AddComponent<T>();

        /// <summary>
        /// Registers any Unity object (asset, material, sprite, ScriptableObject) to be destroyed after the test.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="asset">The object to own.</param>
        /// <returns><paramref name="asset"/>.</returns>
        protected T Track<T>(T asset)
            where T : Object
        {
            _owned.Add(asset);
            return asset;
        }

        /// <summary>
        /// Destroys <paramref name="target"/> immediately, as a step of the test scenario.
        /// </summary>
        /// <param name="target">The object to destroy.</param>
        protected void Destroy(Object target)
        {
            _owned.Remove(target);
            if (target) Object.DestroyImmediate(target);
        }

        /// <summary>
        /// Queues <paramref name="restore"/> to run on tear-down, before owned objects are destroyed. For undoing a
        /// change to state that outlives the test on its own — a static or engine-global value a test overwrote.
        /// </summary>
        /// <param name="restore">The action that puts the changed state back.</param>
        protected void RestoreOnTearDown(Action restore) =>
            _restorers.Add(restore);

        [TearDown]
        public void TearDownFixture()
        {
            for (var i = _restorers.Count - 1; i >= 0; i--)
            {
                _restorers[i]();
            }

            _restorers.Clear();

            for (var i = _owned.Count - 1; i >= 0; i--)
            {
                if (_owned[i]) Object.DestroyImmediate(_owned[i]);
            }

            _owned.Clear();
        }
    }
}
