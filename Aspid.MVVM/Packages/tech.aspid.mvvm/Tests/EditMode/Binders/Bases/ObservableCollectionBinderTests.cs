using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using Aspid.Collections.Observable;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="ObservableCollectionMonoBinder{T}"/>, which covers a set, a queue and a stack — the
    /// three <see cref="IObservableCollection{T}"/> shapes the package left unbound.
    /// </summary>
    /// <remarks>
    /// One binder covers all three because that is what they have in common: membership that changes, and no
    /// index worth binding to.
    /// </remarks>
    [TestFixture]
    public sealed class ObservableCollectionBinderTests : SceneFixture
    {
        [Test]
        public void ASet_IsFollowed()
        {
            var binder = NewBinder();
            var set = new ObservableHashSet<string> { "a" };

            ((IBinder<IObservableCollection<string>>)binder).SetValue(set);
            Assert.AreEqual(new List<string> { "a" }, binder.Added, "The existing element was not replayed");

            set.Add("b");
            set.Remove("a");

            Assert.AreEqual(new List<string> { "a", "b" }, binder.Added, "The addition did not reach the hook");
            Assert.AreEqual(new List<string> { "a" }, binder.Removed, "The removal did not reach the hook");
        }

        [Test]
        public void AQueue_IsFollowed()
        {
            var binder = NewBinder();
            var queue = new ObservableQueue<string>();

            ((IBinder<IObservableCollection<string>>)binder).SetValue(queue);

            queue.Enqueue("first");
            queue.Dequeue();

            Assert.AreEqual(new List<string> { "first" }, binder.Added, "The enqueue did not reach the hook");
            Assert.AreEqual(new List<string> { "first" }, binder.Removed, "The dequeue did not reach the hook");
        }

        [Test]
        public void AStack_IsFollowed()
        {
            var binder = NewBinder();
            var stack = new ObservableStack<string>();

            ((IBinder<IObservableCollection<string>>)binder).SetValue(stack);

            stack.Push("screen");
            stack.Pop();

            Assert.AreEqual(new List<string> { "screen" }, binder.Added, "The push did not reach the hook");
            Assert.AreEqual(new List<string> { "screen" }, binder.Removed, "The pop did not reach the hook");
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

            Assert.IsEmpty(binder.Added, "The binder kept listening to the collection after unbinding");
        }

        private ProbeCollectionBinder NewBinder()
        {
            var binder = Spawn<ProbeCollectionBinder>("Collection");
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }
    }
}
