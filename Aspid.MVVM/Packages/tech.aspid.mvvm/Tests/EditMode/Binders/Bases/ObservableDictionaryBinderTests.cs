using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using Aspid.Collections.Observable;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="ObservableDictionaryMonoBinder{TKey, TValue}"/>, the MonoBehaviour half of the
    /// dictionary domain the list domain already had.
    /// </summary>
    [TestFixture]
    public sealed class ObservableDictionaryBinderTests : SceneFixture
    {
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

            Assert.AreEqual(2, binder.Added.Count, "The existing entries were not replayed");
        }

        [Test]
        public void AddedAndRemovedEntries_ReachTheHooks()
        {
            var binder = NewBinder();
            var dictionary = new ObservableDictionary<string, string>();

            ((IBinder<IReadOnlyObservableDictionary<string, string>>)binder).SetValue(dictionary);

            dictionary.Add("a", "one");
            dictionary.Remove("a");

            Assert.AreEqual(1, binder.Added.Count, "The addition did not reach the hook");
            Assert.AreEqual(1, binder.Removed.Count, "The removal did not reach the hook");
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

            Assert.IsEmpty(binder.Added, "The binder kept listening to the dictionary after unbinding");
            Assert.AreEqual(1, binder.Resets, "No reset happened on unbinding");
        }

        private ProbeDictionaryBinder NewBinder()
        {
            var binder = Spawn<ProbeDictionaryBinder>("Dictionary");
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }
    }
}
