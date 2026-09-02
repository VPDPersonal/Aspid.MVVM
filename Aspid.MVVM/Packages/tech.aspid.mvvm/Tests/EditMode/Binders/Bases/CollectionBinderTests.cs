using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using Aspid.Collections.Observable;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="CollectionMonoBinder{T}"/> and <see cref="CollectionViewModelMonoBinder{TView}"/>.
    /// </summary>
    [TestFixture]
    public sealed class CollectionBinderTests : SceneFixture
    {
        [Test]
        public void CollectionMonoBinder_AppliesTheCollectionOnSetValue()
        {
            var binder = NewBinder();

            binder.SetValue(new ObservableList<string> { "a", "b" });

            CollectionAssert.AreEqual(new[] { "a", "b" }, binder.Applied);
        }

        [Test]
        public void CollectionMonoBinder_TracksAddAfterBinding()
        {
            var list = new ObservableList<string> { "a" };
            var binder = NewBinder();

            binder.SetValue(list);
            list.Add("b");

            CollectionAssert.AreEqual(new[] { "a", "b" }, binder.Applied, "The binder did not track an addition to the collection");
        }

        [Test]
        public void CollectionMonoBinder_TracksRemoveAfterBinding()
        {
            var list = new ObservableList<string> { "a", "b" };
            var binder = NewBinder();

            binder.SetValue(list);
            list.Remove("a");

            CollectionAssert.AreEqual(new[] { "b" }, binder.Applied, "The binder did not track a removal from the collection");
        }

        [Test]
        public void CollectionMonoBinder_StopsTrackingAfterANewCollectionIsAssigned()
        {
            var first = new ObservableList<string> { "a" };
            var binder = NewBinder();

            binder.SetValue(first);
            binder.SetValue(new ObservableList<string> { "z" });
            first.Add("b");

            CollectionAssert.AreEqual(new[] { "z" }, binder.Applied, "The binder stayed subscribed to the previous collection");
        }

        /// <summary>
        /// The serialized view list is fixed in the inspector; a longer collection used to run past its end.
        /// </summary>
        [Test]
        public void CollectionViewModelMonoBinder_WithMoreItemsThanViews_DoesNotOverrun()
        {
            var binder = Spawn<TestCollectionViewModelBinder>();

            binder.SetViews(Spawn<StubView>(), Spawn<StubView>());

            Assert.DoesNotThrow(() => binder.SetValue(new ObservableList<IViewModel>
            {
                new StubViewModel(), new StubViewModel(), new StubViewModel(),
            }));
        }

        private TestCollectionBinder NewBinder() =>
            Spawn<TestCollectionBinder>();
    }

    internal sealed class TestCollectionBinder : CollectionMonoBinder<string>
    {
        public List<string> Applied { get; } = new();

        protected override void OnAdded(IReadOnlyCollection<string> values) =>
            Applied.AddRange(values);

        protected override void OnReset() =>
            Applied.Clear();
    }

    internal sealed class TestCollectionViewModelBinder : CollectionViewModelMonoBinder<StubView>
    {
        public void SetViews(params StubView[] views) =>
            typeof(CollectionViewModelMonoBinder<StubView>)
                .GetField("_views", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                .SetValue(this, views);
    }
}
