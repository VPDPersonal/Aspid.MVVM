using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;
using Aspid.MVVM.StarterKit;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class VirtualizedListLifecycleTests : SceneFixture
    {
        [UnityTest]
        public IEnumerator NullSourceStaysEmptyAfterFramesAndScroll()
        {
            var list = Create();
            list.ItemsSource = Source();
            yield return Ready();
            list.ItemsSource = null;
            list.ItemsSource = null;
            yield return Ready();
            Scroll(list, 10);
            Assert.That(list.content.sizeDelta.y, Is.Zero);
            Assert.That(Views(list).All(view => view.ViewModel == null && !view.gameObject.activeSelf), Is.True);
            var source = Source();
            list.ItemsSource = source;
            yield return Ready();
            Scroll(list, 0);
            AssertSlots(list, source, 0);
        }

        [UnityTest]
        public IEnumerator DisableAndEnablePauseSourceEventsAndRecoverPendingInitialization()
        {
            var list = Create();
            var source = Source();
            list.ItemsSource = source;
            list.enabled = false;
            source.Add(new VirtualizedLifecycleModel());
            Assert.That(list.AddCalls, Is.Zero);
            yield return Ready();
            Assert.That(Views(list), Is.Empty);
            list.enabled = true;
            yield return Ready();
            Scroll(list, 10);
            list.enabled = false;
            source.Insert(0, new VirtualizedLifecycleModel());
            Assert.That(list.AddCalls, Is.Zero);
            list.enabled = true;
            yield return Ready();
            AssertSlots(list, source, 10);
            source.Add(new VirtualizedLifecycleModel());
            Assert.That(list.AddCalls, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator DestroyingOnlyTheComponentReleasesExternalContentAndSource()
        {
            var list = Create();
            var source = Source();
            list.ItemsSource = source;
            yield return Ready();
            var content = list.content;
            var views = Views(list);
            Object.Destroy(list);
            yield return Ready();
            Assert.That(views.All(view => !view), Is.True);
            Assert.That(content.childCount, Is.Zero);
            source.Add(new VirtualizedLifecycleModel());
            Assert.That(list.AddCalls, Is.Zero);
        }

        [UnityTest]
        public IEnumerator DestroyingTheOwnerAlsoDisconnectsTheSource()
        {
            var list = Create();
            var source = Source();
            list.ItemsSource = source;
            yield return Ready();
            var content = list.content;
            Object.Destroy(list.gameObject);
            yield return Ready();
            Assert.That(content.childCount, Is.Zero);
            source.Add(new VirtualizedLifecycleModel());
            Assert.That(list.AddCalls, Is.Zero);
        }

        [UnityTest]
        public IEnumerator MoveRefreshesEveryAffectedSlotOnBothAxes()
        {
            foreach (var horizontal in new[] { false, true })
            {
                var list = Create(horizontal);
                var source = Source();
                source[11] = null;
                list.ItemsSource = source;
                yield return Ready();
                Scroll(list, 10);
                foreach (var pair in new[] { (0, 20), (20, 0), (10, 12), (9, 10), (14, 15), (0, 2), (25, 30) })
                {
                    source.Move(pair.Item1, pair.Item2);
                    AssertSlots(list, source, 10);
                }
                Scroll(list, 18);
                AssertSlots(list, source, 18);
                Scroll(list, 2);
                AssertSlots(list, source, 2);
                list.ItemsSource = null;
            }
        }

        [UnityTest]
        public IEnumerator FilteredSourceAndNullItemsCanBeReplacedAndCleared()
        {
            var list = Create();
            var source = Source();
            source[1] = null;
            using var filtered = new FilteredList<IViewModel>(source, (IComparer<IViewModel>)null, null);
            list.ItemsSource = filtered;
            yield return Ready();
            Scroll(list, 0);
            Assert.That(Views(list).Count(view => view.ViewModel == source[0]), Is.EqualTo(1));
            Assert.That(Views(list).Any(view => view.ViewModel == null && !view.gameObject.activeSelf), Is.True);
            source.RemoveAt(0);
            Assert.That(Views(list).All(view => view.ViewModel != source.Last()), Is.True);
            list.ItemsSource = null;
            source.Add(new VirtualizedLifecycleModel());
            yield return Ready();
            Assert.That(Views(list).All(view => view.ViewModel == null), Is.True);
        }

        [UnityTest]
        public IEnumerator SourceChangedDuringInitializationDoesNotResumeTheOldRefresh()
        {
            var list = Create();
            var replacement = Source();
            var calls = 0;
            VirtualizedLifecycleView.Initialized = () =>
            {
                calls++;
                VirtualizedLifecycleView.Initialized = null;
                list.ItemsSource = replacement;
            };
            RestoreOnTearDown(() => VirtualizedLifecycleView.Initialized = null);
            list.ItemsSource = Source();
            yield return Ready();
            yield return Ready();
            Assert.That(calls, Is.EqualTo(1));
            Scroll(list, 0);
            AssertSlots(list, replacement, 0);
        }

        [UnityTest]
        public IEnumerator SourceChangedDuringCleanupWinsOverTheOuterAssignment()
        {
            var list = Create();
            list.ItemsSource = Source();
            yield return Ready();
            var replacement = Source();
            VirtualizedLifecycleView.Cleaning = () =>
            {
                VirtualizedLifecycleView.Cleaning = null;
                list.ItemsSource = replacement;
            };
            RestoreOnTearDown(() => VirtualizedLifecycleView.Cleaning = null);
            list.ItemsSource = Source();
            yield return Ready();
            Assert.That(list.ItemsSource, Is.SameAs(replacement));
            Scroll(list, 0);
            AssertSlots(list, replacement, 0);
        }

        [UnityTest]
        public IEnumerator CleanupFailureStillClearsEverySlotAndDetachesSource()
        {
            var list = Create();
            var source = Source();
            list.ItemsSource = source;
            yield return Ready();
            VirtualizedLifecycleView.Cleaning = () =>
            {
                VirtualizedLifecycleView.Cleaning = null;
                throw new InvalidOperationException("Cleanup failed.");
            };
            RestoreOnTearDown(() => VirtualizedLifecycleView.Cleaning = null);
            Assert.Throws<InvalidOperationException>(() => list.ItemsSource = null);
            Assert.That(Views(list).All(view => view.ViewModel == null && !view.gameObject.activeSelf), Is.True);
            source.Add(new VirtualizedLifecycleModel());
            Assert.That(list.AddCalls, Is.Zero);
            list.ItemsSource = Source();
            yield return Ready();
            Assert.That(Views(list).All(view => view.ViewModel != null), Is.True);
        }

        [UnityTest]
        public IEnumerator ItemSourceBindersClearTheirTargetOnUnbind()
        {
            foreach (var mono in new[] { false, true })
            {
                var list = Create();
                IBinder binder = mono
                    ? list.gameObject.AddComponent<VirtualizedListItemSourceMonoBinder>()
                    : new VirtualizedListItemSourceBinder(list);
                var source = Source();
                binder.Bind(new BindContext(new OneWayBindableMember<IReadOnlyList<IViewModel>>(source), this));
                yield return Ready();
                Assert.That(Views(list).Any(view => view.ViewModel != null), Is.True);
                binder.Unbind(new UnbindContext(this));
                yield return Ready();
                Assert.That(list.ItemsSource, Is.Null);
                Scroll(list, 3);
                Assert.That(Views(list).All(view => view.ViewModel == null), Is.True);
            }
        }

        [UnityTest]
        public IEnumerator ReassigningTheSameSourceDoesNotDuplicateSubscriptionsOrViews()
        {
            var list = Create();
            var source = Source();
            list.ItemsSource = source;
            yield return Ready();
            var views = Views(list);
            list.ItemsSource = source;
            yield return Ready();
            source.Add(new VirtualizedLifecycleModel());
            Assert.That(list.AddCalls, Is.EqualTo(1));
            CollectionAssert.AreEquivalent(views, Views(list));
        }

        [UnityTest]
        public IEnumerator FailedSecondInitializationClearsThePoolAndAllowsRetry()
        {
            var list = Create();
            var source = Source();
            var calls = 0;
            VirtualizedLifecycleView.Initialized = () =>
            {
                if (++calls == 2) throw new InvalidOperationException("Second initialization failed.");
            };
            RestoreOnTearDown(() => VirtualizedLifecycleView.Initialized = null);
            LogAssert.Expect(LogType.Exception, "InvalidOperationException: Second initialization failed.");
            list.ItemsSource = source;
            yield return Ready();
            Assert.That(list.ItemsSource, Is.Null);
            Assert.That(Views(list).All(view => view.ViewModel == null && !view.gameObject.activeSelf), Is.True);
            source.Add(new VirtualizedLifecycleModel());
            Assert.That(list.AddCalls, Is.Zero);
            VirtualizedLifecycleView.Initialized = null;
            list.ItemsSource = Source();
            yield return Ready();
            AssertSlots(list, list.ItemsSource, 0);
        }

        [UnityTest]
        public IEnumerator FailedFactoryReleaseHidesTheViewBeforeDeferredDestructionAndAllowsRebind()
        {
            foreach (var mono in new[] { false, true })
            {
                var prefab = Spawn<MonoView>();
                var parent = Spawn().transform;
                var factory = new ReleaseFailureFactory(prefab, parent);
                IBinder binder;
                if (mono)
                {
                    binder = Spawn<ObservableListViewModelMonoBinder>();
                    typeof(ObservableListViewModelMonoBinder<MonoView>)
                        .GetField("_viewFactory", BindingFlags.Instance | BindingFlags.NonPublic)
                        .SetValue(binder, factory);
                }
                else binder = new ObservableListViewModelBinder(factory);
                var source = new IViewModel[] { new VirtualizedLifecycleModel(), new VirtualizedLifecycleModel() };
                var member = new OneWayBindableMember<IReadOnlyList<IViewModel>>(source);
                binder.Bind(new BindContext(member, this));
                var views = parent.GetComponentsInChildren<MonoView>(true);
                factory.FailNext = true;
                var exception = Assert.Throws<InvalidOperationException>(() => binder.Unbind(new UnbindContext(this)));
                Assert.That(exception.InnerException.Message, Is.EqualTo("Release before cleanup failed."));
                Assert.That(views[1].gameObject.activeSelf, Is.False);
                Assert.That(views[1].ViewModel, Is.Null);
                yield return Ready();
                Assert.That(views.All(view => !view), Is.True);
                Assert.That(parent.childCount, Is.Zero);
                binder.Bind(new BindContext(member, this));
                Assert.That(parent.GetComponentsInChildren<MonoView>().All(view => view.ViewModel != null), Is.True);
                Assert.That(parent.childCount, Is.EqualTo(2));
                binder.Unbind(new UnbindContext(this));
                yield return Ready();
                Assert.That(parent.childCount, Is.Zero);
            }
        }

        private sealed class ReleaseFailureFactory : PrefabViewFactory<MonoView>
        {
            public bool FailNext;

            public ReleaseFailureFactory(
                MonoView prefab,
                Transform parent)
                : base(prefab, parent) { }

            public override void Release(MonoView view)
            {
                if (FailNext)
                {
                    FailNext = false;
                    throw new InvalidOperationException("Release before cleanup failed.");
                }
                base.Release(view);
            }
        }

        private VirtualizedLifecycleProbe Create(bool horizontal = false)
        {
            var canvas = Track(new GameObject("CollectionCanvas", typeof(RectTransform), typeof(Canvas)));
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var owner = Track(new GameObject("VirtualizedList", typeof(RectTransform)));
            owner.transform.SetParent(canvas.transform, false);
            var rect = (RectTransform)owner.transform;
            rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(150, 150);
            var content = Track(new GameObject("ExternalContent", typeof(RectTransform)));
            content.transform.SetParent(canvas.transform, false);
            var prefab = Track(new GameObject("ItemPrefab", typeof(RectTransform)));
            prefab.SetActive(false);
            ((RectTransform)prefab.transform).sizeDelta = new Vector2(50, 50);
            var view = prefab.AddComponent<VirtualizedLifecycleView>();
            var list = owner.AddComponent<VirtualizedLifecycleProbe>();
            list.enabled = false;
            list.viewport = rect;
            list.content = (RectTransform)content.transform;
            list.horizontal = horizontal;
            list.vertical = !horizontal;
            list.inertia = false;
            list.movementType = ScrollRect.MovementType.Unrestricted;
            typeof(VirtualizedList).GetField("_viewPrefab", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(list, view);
            list.enabled = true;
            return list;
        }

        private static ObservableList<IViewModel> Source()
        {
            var source = new ObservableList<IViewModel>();
            for (var i = 0; i < 40; i++) source.Add(new VirtualizedLifecycleModel());
            return source;
        }

        private static IEnumerator Ready()
        {
            yield return null;
            yield return null;
            yield return null;
        }

        private static MonoView[] Views(VirtualizedList list) =>
            list.content.GetComponentsInChildren<MonoView>(true);

        private static void Scroll(VirtualizedList list, int index)
        {
            list.content.anchoredPosition = list.horizontal
                ? new Vector2(-50 * index, 0)
                : new Vector2(0, 50 * index);
            list.onValueChanged.Invoke(Vector2.zero);
        }

        private static void AssertSlots(VirtualizedList list, IReadOnlyList<IViewModel> source, int top)
        {
            var views = Views(list).OrderBy(view => list.horizontal
                ? view.transform.localPosition.x
                : -view.transform.localPosition.y).ToArray();
            Assert.That(views.Length, Is.EqualTo(5));
            for (var i = 0; i < views.Length; i++)
            {
                Assert.That(views[i].ViewModel, Is.SameAs(source[top + i]), $"Slot {i}, top {top}");
                Assert.That(views[i].gameObject.activeSelf, Is.EqualTo(source[top + i] != null));
                var position = list.horizontal ? views[i].transform.localPosition.x : -views[i].transform.localPosition.y;
                Assert.That(position, Is.EqualTo(50 * (top + i)).Within(0.01f));
            }
        }
    }

    internal sealed class VirtualizedLifecycleProbe : VirtualizedList
    {
        public int AddCalls;
        protected override void OnAdded(IViewModel newItem, int newStartingIndex)
        {
            AddCalls++;
            base.OnAdded(newItem, newStartingIndex);
        }
    }

    internal sealed class VirtualizedLifecycleModel : IViewModel
    {
        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters) =>
            default;
    }

    [View]
    internal sealed partial class VirtualizedLifecycleView : MonoView
    {
        public static Action Initialized;
        public static Action Cleaning;
        partial void OnInitializedInternal(IViewModel viewModel) =>
            Initialized?.Invoke();
        partial void OnDeinitializingInternal() =>
            Cleaning?.Invoke();
    }
}
