using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using Aspid.Collections.Observable;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the MonoBehaviour collection binders.
    /// </summary>
    /// <remarks>
    /// <see cref="CollectionMonoBinder{T}"/> was a stripped-down copy of <see cref="CollectionBinderBase{T}"/>: it
    /// applied the collection once at <c>SetValue</c> and never subscribed to <c>CollectionChanged</c>, so the View
    /// stopped tracking the list the moment it was bound. Its serializable twin had subscribed from the start.
    /// </remarks>
    [TestFixture]
    public sealed class CollectionBinderTests
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

            CollectionAssert.AreEqual(new[] { "a", "b" }, binder.Applied, "Биндер не отследил добавление в коллекцию");
        }

        [Test]
        public void CollectionMonoBinder_TracksRemoveAfterBinding()
        {
            var list = new ObservableList<string> { "a", "b" };
            var binder = NewBinder();

            binder.SetValue(list);
            list.Remove("a");

            CollectionAssert.AreEqual(new[] { "b" }, binder.Applied, "Биндер не отследил удаление из коллекции");
        }

        [Test]
        public void CollectionMonoBinder_StopsTrackingAfterANewCollectionIsAssigned()
        {
            var first = new ObservableList<string> { "a" };
            var binder = NewBinder();

            binder.SetValue(first);
            binder.SetValue(new ObservableList<string> { "z" });
            first.Add("b");

            CollectionAssert.AreEqual(new[] { "z" }, binder.Applied, "Биндер остался подписан на прежнюю коллекцию");
        }

        /// <summary>
        /// The serialized view list is fixed in the inspector; a longer collection used to run past its end.
        /// </summary>
        [Test]
        public void CollectionViewModelMonoBinder_WithMoreItemsThanViews_DoesNotOverrun()
        {
            var gameObject = NewGameObject();
            var binder = gameObject.AddComponent<TestCollectionViewModelBinder>();

            binder.SetViews(NewStubView(), NewStubView());

            Assert.DoesNotThrow(() => binder.SetValue(new ObservableList<IViewModel>
            {
                new StubViewModel(), new StubViewModel(), new StubViewModel(),
            }));
        }


        /// <summary>
        /// The factory parents each new view last in the hierarchy, so an insert anywhere but the end left the
        /// visual order — and any LayoutGroup driven by it — out of step with the model.
        /// </summary>
        [Test]
        public void ObservableListViewModelMonoBinder_InsertAtTheFront_PutsTheViewFirstInTheHierarchy()
        {
            var parent = NewGameObject().transform;
            var binder = NewGameObject().AddComponent<TestObservableListBinder>();
            binder.UseFactory(new StubViewFactory(parent));

            var list = new ObservableList<IViewModel> { new StubViewModel(), new StubViewModel() };
            binder.SetValue(list);

            var inserted = new StubViewModel();
            list.Insert(0, inserted);

            Assert.AreEqual(3, parent.childCount);
            Assert.AreSame(
                inserted,
                parent.GetChild(0).GetComponent<StubView>().ViewModel,
                "Вставленный в начало элемент не встал первым в иерархии");
        }

        private TestCollectionBinder NewBinder() =>
            NewGameObject().AddComponent<TestCollectionBinder>();

        private StubView NewStubView() =>
            NewGameObject().AddComponent<StubView>();

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("CollectionBinder");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }

    internal sealed class TestCollectionBinder : CollectionMonoBinder<string>
    {
        public List<string> Applied { get; } = new();

        protected override void OnAdded(IReadOnlyCollection<string> values) =>
            Applied.AddRange(values);

        protected override void OnReset() =>
            Applied.Clear();
    }

    internal sealed class StubViewModel : IViewModel
    {
        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters) => default;
    }

    /// <summary>
    /// Minimal <see cref="IView"/> so the test exercises the binder's indexing rather than MonoView's own
    /// initialization requirements.
    /// </summary>
    internal sealed class StubView : MonoBehaviour, IView
    {
        public IViewModel ViewModel { get; private set; }

        public void Initialize(IViewModel viewModel) => ViewModel = viewModel;

        public void Deinitialize() => ViewModel = null;
    }

    internal sealed class TestCollectionViewModelBinder : CollectionViewModelMonoBinder<StubView>
    {
        public void SetViews(params StubView[] views) =>
            typeof(CollectionViewModelMonoBinder<StubView>)
                .GetField("_views", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                .SetValue(this, views);
    }

    internal sealed class StubViewFactory : IViewFactory<StubView>
    {
        private readonly Transform _parent;

        public StubViewFactory(Transform parent) => _parent = parent;

        public StubView Create(IViewModel viewModel)
        {
            var view = new GameObject("StubView").AddComponent<StubView>();

            view.transform.SetParent(_parent, worldPositionStays: false);
            view.Initialize(viewModel);

            return view;
        }

        public StubView Create<TKey>(IViewModel viewModel, TKey key) => Create(viewModel);

        public void Release(StubView view) => Object.DestroyImmediate(view.gameObject);
    }

    internal sealed class TestObservableListBinder : ObservableListViewModelMonoBinder<StubView, StubViewFactory>
    {
        public void UseFactory(StubViewFactory factory) =>
            typeof(ObservableListViewModelMonoBinder<StubView, StubViewFactory>)
                .GetField("_viewFactory", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                .SetValue(this, factory);
    }
}
