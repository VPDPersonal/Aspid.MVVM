using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class CollectionLifecycleTests : SceneFixture
    {
        [TestCase(false)]
        [TestCase(true)]
        public void UnbindReleasesChildBindingsAndCanBindAgain(bool mono)
        {
            var view = Spawn<CollectionLifecycleView>();
            var model = new CollectionLifecycleModel();
            var source = new ObservableList<IViewModel> { model };
            var binder = Fixed(mono, view);
            var member = new OneWayBindableMember<IReadOnlyCollection<IViewModel>>(source);
            binder.Bind(new BindContext(member, this));
            model.Member.Value = 4;
            Assert.That(view.Value, Is.EqualTo(4));
            binder.Unbind(new UnbindContext(this));
            model.Member.Value = 8;
            Assert.That(view.ViewModel, Is.Null);
            Assert.That(view.Value, Is.EqualTo(4));
            Assert.That(view.gameObject.activeSelf, Is.False);
            binder.Bind(new BindContext(member, this));
            Assert.That(view.Value, Is.EqualTo(8));
            binder.Unbind(new UnbindContext(this));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void InitialEmptyAndNullCollectionsHideFixedViews(bool mono)
        {
            var view = Spawn<CollectionLifecycleView>();
            var binder = Fixed(mono, view);
            ((IBinder<IReadOnlyCollection<IViewModel>>)binder).SetValue(Array.Empty<IViewModel>());
            Assert.That(view.gameObject.activeSelf, Is.False);
            view.gameObject.SetActive(true);
            ((IBinder<IReadOnlyCollection<IViewModel>>)binder).SetValue(null);
            Assert.That(view.gameObject.activeSelf, Is.False);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void FixedCleanupContinuesAfterAChildThrows(bool mono)
        {
            var first = Spawn<CollectionLifecycleView>();
            var second = Spawn<CollectionLifecycleView>();
            var binder = Fixed(mono, first, second);
            var source = new ObservableList<IViewModel> { new CollectionLifecycleModel(), new CollectionLifecycleModel() };
            binder.Bind(new BindContext(new OneWayBindableMember<IReadOnlyCollection<IViewModel>>(source), this));
            first.ThrowOnCleanup = true;
            Assert.Throws<InvalidOperationException>(() => binder.Unbind(new UnbindContext(this)));
            Assert.That(second.ViewModel, Is.Null);
            Assert.That(first.gameObject.activeSelf || second.gameObject.activeSelf, Is.False);
            source.Add(new CollectionLifecycleModel());
            Assert.That(second.ViewModel, Is.Null);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void NullSlotsKeepIndexesAcrossAllListChanges(bool mono)
        {
            var factory = new CollectionLifecycleFactory(Spawn().transform);
            var binder = List(mono, factory);
            var a = new CollectionLifecycleModel();
            var b = new CollectionLifecycleModel();
            var source = new ObservableList<IViewModel> { null, a, null };
            binder.Bind(new BindContext(new OneWayBindableMember<IReadOnlyList<IViewModel>>(source), this));
            AssertSlots(factory, source);
            source.Insert(0, b);
            AssertSlots(factory, source);
            source[1] = a;
            AssertSlots(factory, source);
            source[2] = null;
            AssertSlots(factory, source);
            source.Move(0, 3);
            AssertSlots(factory, source);
            source.RemoveAt(1);
            AssertSlots(factory, source);
            source.AddRange(new IViewModel[] { null, a, b });
            AssertSlots(factory, source);
            source.Clear();
            Assert.That(factory.Live, Is.Empty);
            source.Add(a);
            binder.Unbind(new UnbindContext(this));
            Assert.That(factory.Live, Is.Empty);
            Assert.That(factory.Created, Is.EqualTo(factory.Released));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void FailedInitialBatchReleasesEveryAcquiredView(bool mono)
        {
            var factory = new CollectionLifecycleFactory(Spawn().transform) { FailCreateAt = 2, FailFirstRelease = true };
            var binder = List(mono, factory);
            var source = new IViewModel[] { new CollectionLifecycleModel(), new CollectionLifecycleModel() };
            var exception = Assert.Throws<AggregateException>(() => binder.Bind(
                new BindContext(new OneWayBindableMember<IReadOnlyList<IViewModel>>(source), this)));
            Assert.That(exception.InnerExceptions[0].Message, Is.EqualTo("Create failed."));
            Assert.That(exception.InnerExceptions[0].StackTrace, Does.Contain("CollectionLifecycleFactory.Create"));
            Assert.That(exception.ToString(), Does.Contain("Release failed."));
            Assert.That(exception.ToString(), Does.Contain("index 0"));
            Assert.That(factory.Live, Is.Empty);
            Assert.That(factory.Created, Is.EqualTo(factory.Released));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void ReleaseFailureDoesNotRetainOtherViews(bool mono)
        {
            var factory = new CollectionLifecycleFactory(Spawn().transform);
            var binder = List(mono, factory);
            var source = new IViewModel[] { new CollectionLifecycleModel(), null, new CollectionLifecycleModel() };
            binder.Bind(new BindContext(new OneWayBindableMember<IReadOnlyList<IViewModel>>(source), this));
            factory.FailFirstRelease = true;
            Assert.Throws<InvalidOperationException>(() => binder.Unbind(new UnbindContext(this)));
            Assert.That(factory.Live, Is.Empty);
            Assert.That(factory.Released, Is.EqualTo(3));
            binder.Unbind(new UnbindContext(this));
            Assert.That(factory.Released, Is.EqualTo(3));
        }

        [Test]
        public void PrefabPoolReusesClearedNullAndNonNullSlots()
        {
            var prefab = Spawn<CollectionLifecycleView>();
            var parent = Spawn().transform;
            var pool = new PrefabViewPool<CollectionLifecycleView>(prefab, parent);
            var binder = new ObservableListViewModelBinder<CollectionLifecycleView>(pool);
            var source = new ObservableList<IViewModel> { null, new CollectionLifecycleModel() };
            binder.Bind(new BindContext(new OneWayBindableMember<IReadOnlyList<IViewModel>>(source), this));
            var count = parent.childCount;
            binder.Unbind(new UnbindContext(this));
            foreach (Transform child in parent)
            {
                Assert.That(child.gameObject.activeSelf, Is.False);
                Assert.That(child.GetComponent<CollectionLifecycleView>().ViewModel, Is.Null);
            }
            binder.Bind(new BindContext(new OneWayBindableMember<IReadOnlyList<IViewModel>>(source), this));
            Assert.That(parent.childCount, Is.EqualTo(count));
            binder.Unbind(new UnbindContext(this));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void FailedIncrementalBatchDetachesAndReleasesTrackedViews(bool mono)
        {
            var factory = new CollectionLifecycleFactory(Spawn().transform);
            var binder = List(mono, factory);
            var source = new ObservableList<IViewModel> { new CollectionLifecycleModel() };
            binder.Bind(new BindContext(new OneWayBindableMember<IReadOnlyList<IViewModel>>(source), this));
            factory.FailCreateAt = 3;
            Assert.Throws<InvalidOperationException>(() => source.AddRange(new IViewModel[]
            {
                new CollectionLifecycleModel(), new CollectionLifecycleModel()
            }));
            Assert.That(factory.Live, Is.Empty);
            Assert.That(factory.Created, Is.EqualTo(factory.Released));
            factory.FailCreateAt = 0;
            source.Add(new CollectionLifecycleModel());
            Assert.That(factory.Live, Is.Empty);
            binder.Unbind(new UnbindContext(this));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void FailedPrefabInitializationDoesNotLeakAnAcquiredView(bool pooled)
        {
            var prefab = Spawn<CollectionLifecycleView>();
            var parent = Spawn().transform;
            IViewFactory<CollectionLifecycleView> factory = pooled
                ? new PrefabViewPool<CollectionLifecycleView>(prefab, parent)
                : new PrefabViewFactory<CollectionLifecycleView>(prefab, parent);
            prefab.ThrowOnCleanup = true;
            var exception = Assert.Throws<AggregateException>(() => factory.Create(
                new CollectionLifecycleModel { ThrowOnInitialize = true }));
            Assert.That(exception.InnerExceptions[0].Message, Is.EqualTo("Initialization failed."));
            Assert.That(exception.InnerExceptions[0].StackTrace, Does.Contain("CollectionLifecycleView.Initialize"));
            Assert.That(exception.ToString(), Does.Contain("Child cleanup failed."));
            prefab.ThrowOnCleanup = false;
            foreach (var view in parent.GetComponentsInChildren<CollectionLifecycleView>(true))
            {
                Assert.That(view.ViewModel, Is.Null);
                Assert.That(view.gameObject.activeSelf, Is.False);
            }
            var healthy = factory.Create(new CollectionLifecycleModel());
            Assert.That(healthy.ViewModel, Is.Not.Null);
            factory.Release(healthy);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void FilteredListIsDisposedAfterReleaseFailure(bool mono)
        {
            var factory = new CollectionLifecycleFactory(Spawn().transform);
            var binder = List(mono, factory);
            var filter = new CollectionCountingFilter();
            var type = mono
                ? typeof(ObservableListViewModelMonoBinder<CollectionLifecycleView>)
                : typeof(ObservableListViewModelBinder<CollectionLifecycleView>);
            SetField(type, binder, "_filter", filter);
            var source = new ObservableList<IViewModel> { new CollectionLifecycleModel() };
            binder.Bind(new BindContext(new OneWayBindableMember<IReadOnlyList<IViewModel>>(source), this));
            factory.FailFirstRelease = true;
            Assert.Throws<InvalidOperationException>(() => binder.Unbind(new UnbindContext(this)));
            var calls = filter.Calls;
            source.Add(new CollectionLifecycleModel());
            Assert.That(filter.Calls, Is.EqualTo(calls));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void AcquiredViewsReturnToTheirOriginalFactory(bool mono)
        {
            var original = new CollectionLifecycleFactory(Spawn().transform);
            var replacement = new CollectionLifecycleFactory(Spawn().transform);
            var binder = List(mono, original);
            var source = new IViewModel[] { new CollectionLifecycleModel(), null };
            binder.Bind(new BindContext(new OneWayBindableMember<IReadOnlyList<IViewModel>>(source), this));
            var type = mono
                ? typeof(ObservableListViewModelMonoBinder<CollectionLifecycleView>)
                : typeof(ObservableListViewModelBinder<CollectionLifecycleView>);
            SetField(type, binder, "_viewFactory", replacement);
            binder.Unbind(new UnbindContext(this));
            Assert.That(original.Released, Is.EqualTo(2));
            Assert.That(replacement.Released, Is.Zero);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void FailedFilteredReplayDetachesAndReleasesEveryAcquiredView(bool mono)
        {
            var factory = new CollectionLifecycleFactory(Spawn().transform);
            var binder = List(mono, factory);
            var source = new ObservableList<IViewModel> { new CollectionLifecycleModel() };
            using var filtered = new FilteredList<IViewModel>(source, (IComparer<IViewModel>)null, null);
            binder.Bind(new BindContext(new OneWayBindableMember<IReadOnlyList<IViewModel>>(filtered), this));
            factory.FailCreateAt = 3;
            Assert.Throws<InvalidOperationException>(() => source.Add(new CollectionLifecycleModel()));
            Assert.That(factory.Live, Is.Empty);
            Assert.That(factory.Created, Is.EqualTo(factory.Released));
            factory.FailCreateAt = 0;
            source.Add(new CollectionLifecycleModel());
            Assert.That(factory.Live, Is.Empty);
            binder.Unbind(new UnbindContext(this));
        }

        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void ReleaseFailureBeforeCleanupDestroysViewsAndAllowsRebind(bool mono, bool cleanupFails)
        {
            var factory = new CollectionLifecycleFactory(Spawn().transform);
            var binder = List(mono, factory);
            var model = new CollectionLifecycleModel();
            var source = new ObservableList<IViewModel> { model, model, model };
            var member = new OneWayBindableMember<IReadOnlyList<IViewModel>>(source);
            binder.Bind(new BindContext(member, this));
            var views = factory.Live.ToArray();
            if (cleanupFails) views[2].ThrowOnCleanup = true;
            factory.FailBeforeRelease = true;
            var exception = Assert.Catch(() => binder.Unbind(new UnbindContext(this)));
            Assert.That(exception.ToString(), Does.Contain("Release before cleanup failed."));
            Assert.That(exception.ToString(), Does.Contain("CollectionLifecycleFactory.Release"));
            Assert.That(exception.ToString(), Does.Contain("index 2"));
            if (cleanupFails) Assert.That(exception.ToString(), Does.Contain("Child cleanup failed."));
            Assert.That(factory.ReleaseAttempts, Is.EqualTo(3));
            Assert.That(factory.Parent.childCount, Is.Zero);
            model.Member.Value = 77;
            foreach (var view in views)
            {
                Assert.That((bool)view, Is.False);
                Assert.That(view.Value, Is.EqualTo(1));
            }
            binder.Unbind(new UnbindContext(this));
            Assert.That(factory.ReleaseAttempts, Is.EqualTo(3));
            binder.Bind(new BindContext(member, this));
            AssertSlots(factory, source);
            binder.Unbind(new UnbindContext(this));
            Assert.That(factory.Parent.childCount, Is.Zero);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void FailedRemoveClearsTheRemainingViewsAndAllowsRebind(bool mono)
        {
            var factory = new CollectionLifecycleFactory(Spawn().transform);
            var binder = List(mono, factory);
            var source = new ObservableList<IViewModel>
            {
                new CollectionLifecycleModel(), new CollectionLifecycleModel(), new CollectionLifecycleModel()
            };
            var member = new OneWayBindableMember<IReadOnlyList<IViewModel>>(source);
            binder.Bind(new BindContext(member, this));
            factory.FailBeforeRelease = true;
            var exception = Assert.Throws<InvalidOperationException>(() => source.RemoveAt(0));
            Assert.That(exception.Message, Does.Contain("index 0"));
            Assert.That(exception.InnerException.Message, Is.EqualTo("Release before cleanup failed."));
            Assert.That(factory.Parent.childCount, Is.Zero);
            source.Add(new CollectionLifecycleModel());
            Assert.That(factory.Parent.childCount, Is.Zero);
            binder.Unbind(new UnbindContext(this));
            binder.Bind(new BindContext(member, this));
            AssertSlots(factory, source);
            binder.Unbind(new UnbindContext(this));
        }

        private IBinder Fixed(bool mono, params CollectionLifecycleView[] views)
        {
            if (!mono) return new CollectionViewModelBinder<CollectionLifecycleView>(views);
            var binder = Spawn<CollectionFixedMonoProbe>();
            SetField(typeof(CollectionViewModelMonoBinder<CollectionLifecycleView>), binder, "_views", views);
            return binder;
        }

        private IBinder List(bool mono, IViewFactory<CollectionLifecycleView> factory)
        {
            if (!mono) return new ObservableListViewModelBinder<CollectionLifecycleView>(factory);
            var binder = Spawn<CollectionListMonoProbe>();
            SetField(typeof(ObservableListViewModelMonoBinder<CollectionLifecycleView>), binder, "_viewFactory", factory);
            return binder;
        }

        private static void SetField(Type type, object target, string name, object value) =>
            type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, value);

        private static void AssertSlots(CollectionLifecycleFactory factory, IReadOnlyList<IViewModel> source)
        {
            var views = factory.Parent.GetComponentsInChildren<CollectionLifecycleView>(true);
            Assert.That(views.Length, Is.EqualTo(source.Count));
            for (var i = 0; i < source.Count; i++)
            {
                Assert.That(views[i].ViewModel, Is.SameAs(source[i]), $"Slot {i}");
                Assert.That(views[i].gameObject.activeSelf, Is.EqualTo(source[i] != null), $"Slot {i}");
            }
        }
    }

    internal sealed class CollectionFixedMonoProbe : CollectionViewModelMonoBinder<CollectionLifecycleView> { }
    internal sealed class CollectionListMonoProbe : ObservableListViewModelMonoBinder<CollectionLifecycleView> { }

    internal sealed class CollectionLifecycleModel : IViewModel
    {
        public readonly OneWayStructBindableMember<int> Member = new(1);
        public bool ThrowOnInitialize;
        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters) =>
            default;
    }

    internal sealed class CollectionLifecycleView : MonoBehaviour, IView
    {
        private ValueProbe _binder;
        public bool ThrowOnCleanup;
        public int Value;
        public IViewModel ViewModel { get; private set; }

        public void Initialize(IViewModel viewModel)
        {
            _binder ??= new ValueProbe(value => Value = value);
            ViewModel = viewModel;
            _binder.Bind(new BindContext(((CollectionLifecycleModel)viewModel).Member, this));
            if (((CollectionLifecycleModel)viewModel).ThrowOnInitialize)
                throw new InvalidOperationException("Initialization failed.");
        }

        public void Deinitialize()
        {
            _binder?.Unbind(new UnbindContext(this));
            ViewModel = null;
            if (!ThrowOnCleanup) return;
            ThrowOnCleanup = false;
            throw new InvalidOperationException("Child cleanup failed.");
        }

        private sealed class ValueProbe : Binder, IBinder<int>
        {
            private readonly Action<int> _set;
            public ValueProbe(Action<int> set) : base(BindMode.OneWay) =>
                _set = set;
            public void SetValue(int value) =>
                _set(value);
        }
    }

    internal sealed class CollectionCountingFilter : ICollectionFilter<IViewModel>
    {
        public int Calls;
        public bool Matches(IViewModel value)
        {
            Calls++;
            return true;
        }
    }

    internal sealed class CollectionLifecycleFactory : IViewFactory<CollectionLifecycleView>
    {
        public readonly Transform Parent;
        public readonly List<CollectionLifecycleView> Live = new();
        public int Created;
        public int Released;
        public int FailCreateAt;
        public bool FailFirstRelease;
        public bool FailBeforeRelease;
        public int ReleaseAttempts;
        public CollectionLifecycleFactory(Transform parent) =>
            Parent = parent;

        public CollectionLifecycleView Create(IViewModel model)
        {
            if (Created + 1 == FailCreateAt) throw new InvalidOperationException("Create failed.");
            var view = new GameObject("CollectionItem").AddComponent<CollectionLifecycleView>();
            view.transform.SetParent(Parent, false);
            if (model != null) view.Initialize(model);
            Live.Add(view);
            Created++;
            return view;
        }

        public void Release(CollectionLifecycleView view)
        {
            ReleaseAttempts++;
            if (FailBeforeRelease)
            {
                FailBeforeRelease = false;
                throw new InvalidOperationException("Release before cleanup failed.");
            }
            view.Deinitialize();
            Live.Remove(view);
            Released++;
            UnityEngine.Object.DestroyImmediate(view.gameObject);
            if (!FailFirstRelease) return;
            FailFirstRelease = false;
            throw new InvalidOperationException("Release failed.");
        }
    }
}
