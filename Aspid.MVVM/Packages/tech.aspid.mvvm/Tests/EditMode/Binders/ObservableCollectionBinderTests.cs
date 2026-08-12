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
    /// Tests for the binder that covers the three collection types the package left unbound: a set, a queue and a stack.
    /// </summary>
    /// <remarks>
    /// Lists and dictionaries were bound and these three were not, though a set of owned ids, a queue of pending requests
    /// and a stack of open screens are all things a View shows. One binder covers all three because that is what they
    /// have in common: membership that changes, and no index worth binding to.
    /// </remarks>
    [TestFixture]
    public sealed class ObservableCollectionBinderTests
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
        public void ASet_IsFollowed()
        {
            var binder = NewBinder();
            var set = new ObservableHashSet<string> { "a" };

            ((IBinder<IObservableCollection<string>>)binder).SetValue(set);
            Assert.AreEqual(new List<string> { "a" }, binder.Added, "Существующий элемент не проигран");

            set.Add("b");
            set.Remove("a");

            Assert.AreEqual(new List<string> { "a", "b" }, binder.Added, "Добавление не дошло до хука");
            Assert.AreEqual(new List<string> { "a" }, binder.Removed, "Удаление не дошло до хука");
        }

        [Test]
        public void AQueue_IsFollowed()
        {
            var binder = NewBinder();
            var queue = new ObservableQueue<string>();

            ((IBinder<IObservableCollection<string>>)binder).SetValue(queue);

            queue.Enqueue("first");
            queue.Dequeue();

            Assert.AreEqual(new List<string> { "first" }, binder.Added, "Постановка в очередь не дошла до хука");
            Assert.AreEqual(new List<string> { "first" }, binder.Removed, "Извлечение из очереди не дошло до хука");
        }

        [Test]
        public void AStack_IsFollowed()
        {
            var binder = NewBinder();
            var stack = new ObservableStack<string>();

            ((IBinder<IObservableCollection<string>>)binder).SetValue(stack);

            stack.Push("screen");
            stack.Pop();

            Assert.AreEqual(new List<string> { "screen" }, binder.Added, "Push не дошёл до хука");
            Assert.AreEqual(new List<string> { "screen" }, binder.Removed, "Pop не дошёл до хука");
        }

        [Test]
        public void AfterUnbinding_TheCollectionIsNoLongerFollowed()
        {
            var binder = NewBinder();
            var set = new ObservableHashSet<string>();

            binder.Bind(new OneWayBindableMember<IObservableCollection<string>>(set));
            binder.Unbind();

            binder.Added.Clear();
            set.Add("ignored");

            Assert.IsEmpty(binder.Added, "Биндер продолжил слушать коллекцию после отвязки");
        }

        private ProbeCollectionBinder NewBinder()
        {
            var gameObject = new GameObject("Collection");
            _spawned.Add(gameObject);

            var binder = gameObject.AddComponent<ProbeCollectionBinder>();
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }
    }

    /// <summary>
    /// Records what the base dispatches, which is all this binder is responsible for.
    /// </summary>
    internal sealed class ProbeCollectionBinder : ObservableCollectionMonoBinder<string>
    {
        public readonly List<string> Added = new();
        public readonly List<string> Removed = new();

        protected override void OnAdded(string newItem) => Added.Add(newItem);

        protected override void OnRemoved(string oldItem) => Removed.Add(oldItem);

        protected override void OnReset() { }
    }
}
