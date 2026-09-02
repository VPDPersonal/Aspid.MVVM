using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.Events;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using Aspid.Collections.Observable;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the collection count binder: "Items: 12" and an empty-state panel, both derived from a collection the
    /// View already has.
    /// </summary>
    [TestFixture]
    public sealed class CollectionCountTests : SceneFixture
    {
        [Test]
        public void APlainList_IsCountedOnce()
        {
            var (binder, counts, empties) = New();

            ((IBinder<IReadOnlyList<object>>)binder).SetValue(new List<object> { "a", "b" });

            Assert.AreEqual(new List<int> { 2 }, counts, "The count was not reported");
            Assert.AreEqual(new List<bool> { false }, empties, "Emptiness was reported incorrectly");
        }

        /// <summary>
        /// The point of the binder: an observable collection is followed, so the count answers every insert and removal
        /// without the ViewModel keeping a second field in step.
        /// </summary>
        [Test]
        public void AnObservableList_IsFollowed()
        {
            var (binder, counts, _) = New();
            var list = new ObservableList<object>();

            ((IBinder<IReadOnlyObservableList<object>>)binder).SetValue(list);
            counts.Clear();

            list.Add("first");
            list.Add("second");
            list.RemoveAt(0);

            Assert.AreEqual(new List<int> { 1, 2, 1 }, counts, "The count did not follow the collection's changes");
        }

        /// <summary>
        /// A panel that says "nothing here" is the right answer to a list that has not arrived, so a null collection
        /// reports zero rather than nothing at all.
        /// </summary>
        [Test]
        public void ANullCollection_ReportsZeroAndEmpty()
        {
            var (binder, counts, empties) = New();

            ((IBinder<IReadOnlyList<object>>)binder).SetValue(null);

            Assert.AreEqual(new List<int> { 0 }, counts, "The zero count was not reported");
            Assert.AreEqual(new List<bool> { true }, empties, "Emptiness was not reported");
        }

        /// <summary>
        /// A binder that kept listening after unbinding would keep answering for a collection the View no longer shows —
        /// and would hold it alive.
        /// </summary>
        [Test]
        public void AfterUnbinding_TheCollectionIsNoLongerFollowed()
        {
            var (binder, counts, _) = New();
            var list = new ObservableList<object>();

            // Through Bind, not SetValue: the unsubscription lives in OnUnbound, which only a bound binder calls —
            // a direct SetValue would have left the subscription in place and the test would check the wrong thing.
            binder.Bind(new OneWayBindableMember<IReadOnlyObservableList<object>>(list));
            binder.Unbind();
            counts.Clear();

            list.Add("ignored");

            Assert.IsEmpty(counts, "The binder kept listening to the collection after unbinding");
        }

        private (ObjectCollectionCountMonoBinder Binder, List<int> Counts, List<bool> Empties) New()
        {
            var binder = Spawn<ObjectCollectionCountMonoBinder>("Count");

            // A component added from code skips the inspector's Reset and stays in TwoWay, which this binder
            // does not accept: set the mode the way the inspector does.
            var serializedObject = new UnityEditor.SerializedObject(binder);
            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            var counts = new List<int>();
            var empties = new List<bool>();

            Listen<int>(binder, "_count", counts.Add);
            Listen<bool>(binder, "_isEmpty", empties.Add);

            return (binder, counts, empties);
        }

        private static void Listen<TValue>(MonoBehaviour owner, string fieldName, UnityAction<TValue> listener)
        {
            for (var type = owner.GetType(); type is not null; type = type.BaseType)
            {
                var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
                if (field is null) continue;

                if (field.GetValue(owner) is not UnityEvent<TValue> unityEvent)
                {
                    unityEvent = new UnityEvent<TValue>();
                    field.SetValue(owner, unityEvent);
                }

                unityEvent.AddListener(listener);
                return;
            }

            Assert.Fail($"The binder has no {fieldName} field");
        }
    }
}
