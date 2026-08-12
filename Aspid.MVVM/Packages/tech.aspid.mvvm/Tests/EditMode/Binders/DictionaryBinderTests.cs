using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using Aspid.Collections.Observable;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the MonoBehaviour half of the dictionary domain, which the list domain had and this one did not.
    /// </summary>
    /// <remarks>
    /// A dictionary could be shown from a View's own serialized field and not from a component dropped next to the
    /// objects it drives.
    /// </remarks>
    [TestFixture]
    public sealed class DictionaryBinderTests
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

        /// <summary>
        /// A View built after the data must still show it, so what the dictionary already holds is replayed through the
        /// add hook when it arrives.
        /// </summary>
        [Test]
        public void WhatTheDictionaryAlreadyHolds_IsReplayed()
        {
            var binder = NewBinder();
            var dictionary = new ObservableDictionary<string, string> { ["a"] = "one", ["b"] = "two" };

            ((IBinder<IReadOnlyObservableDictionary<string, string>>)binder).SetValue(dictionary);

            Assert.AreEqual(2, binder.Added.Count, "Существующие записи не проиграны заново");
        }

        [Test]
        public void AddedAndRemovedEntries_ReachTheHooks()
        {
            var binder = NewBinder();
            var dictionary = new ObservableDictionary<string, string>();

            ((IBinder<IReadOnlyObservableDictionary<string, string>>)binder).SetValue(dictionary);

            dictionary.Add("a", "one");
            dictionary.Remove("a");

            Assert.AreEqual(1, binder.Added.Count, "Добавление не дошло до хука");
            Assert.AreEqual(1, binder.Removed.Count, "Удаление не дошло до хука");
        }

        /// <summary>
        /// A binder that kept listening after unbinding would keep building views for a dictionary the View no longer
        /// shows.
        /// </summary>
        [Test]
        public void AfterUnbinding_TheDictionaryIsNoLongerFollowed()
        {
            var binder = NewBinder();
            var dictionary = new ObservableDictionary<string, string>();

            binder.Bind(new OneWayBindableMember<IReadOnlyObservableDictionary<string, string>>(dictionary));
            binder.Unbind();

            binder.Added.Clear();
            dictionary.Add("a", "one");

            Assert.IsEmpty(binder.Added, "Биндер продолжил слушать словарь после отвязки");
            Assert.AreEqual(1, binder.Resets, "Сброс при отвязке не произошёл");
        }

        private ProbeDictionaryBinder NewBinder()
        {
            var gameObject = new GameObject("Dictionary");
            _spawned.Add(gameObject);

            var binder = gameObject.AddComponent<ProbeDictionaryBinder>();
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }
    }

    /// <summary>
    /// Records what the base dispatches, which is all this half of the domain is responsible for.
    /// </summary>
    internal sealed class ProbeDictionaryBinder : ObservableDictionaryMonoBinder<string, string>
    {
        public readonly List<KeyValuePair<string, string>> Added = new();
        public readonly List<KeyValuePair<string, string>> Removed = new();

        public int Resets { get; private set; }

        protected override void OnAdded(KeyValuePair<string, string> newItem) => Added.Add(newItem);

        protected override void OnAdded(IReadOnlyList<KeyValuePair<string, string>> newItems) => Added.AddRange(newItems);

        protected override void OnRemoved(KeyValuePair<string, string> oldItem) => Removed.Add(oldItem);

        protected override void OnRemoved(IReadOnlyList<KeyValuePair<string, string>> oldItems) => Removed.AddRange(oldItems);

        protected override void OnReplace(KeyValuePair<string, string> oldItem, KeyValuePair<string, string> newItem)
        {
            Removed.Add(oldItem);
            Added.Add(newItem);
        }

        protected override void OnReset() => Resets++;
    }
}
