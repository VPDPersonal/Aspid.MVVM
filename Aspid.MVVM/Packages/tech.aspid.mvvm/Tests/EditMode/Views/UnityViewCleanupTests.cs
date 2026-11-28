using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.Tests
{
    internal sealed class UnityViewCleanupTests : SceneFixture
    {
        [Test]
        public void SafeLifecycleHelpersSkipDestroyedUnityViews()
        {
            var view = ScriptableObject.CreateInstance<DisposableScriptableProbe>();
            var model = new Model();
            view.Initialize(model);
            Object.DestroyImmediate(view);

            var replacement = new Model();
            view.InitializeSafely(replacement);
            Assert.That(((IView)view).ReinitializeSafely(replacement), Is.Null);
            Assert.That(view.DeinitializeViewSafely(), Is.Null);
            Assert.That(view.DisposeViewSafely(), Is.Null);
            Assert.That(((IView)view).DisposeViewSafely(), Is.Null);
            Assert.That(view.ViewModel, Is.SameAs(model));
            Assert.That(view.DisposeCalls, Is.Zero);
        }

        [Test]
        public void StrictLifecycleHelpersRejectDestroyedUnityViews()
        {
            var view = ScriptableObject.CreateInstance<DisposableScriptableProbe>();
            Object.DestroyImmediate(view);

            Assert.That(Assert.Throws<ArgumentNullException>(() => view.DeinitializeView()).ParamName, Is.EqualTo("view"));
            Assert.That(Assert.Throws<ArgumentNullException>(() => view.Reinitialize(null)).ParamName, Is.EqualTo("view"));
            Assert.That(Assert.Throws<ArgumentNullException>(() => view.DisposeView()).ParamName, Is.EqualTo("view"));
            Assert.That(Assert.Throws<ArgumentNullException>(() => ((IView)view).DisposeView()).ParamName, Is.EqualTo("view"));
            Assert.That(view.DisposeCalls, Is.Zero);
        }

        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void DestructionContinuesAfterCleanupFails(bool entireObject, bool throughInterface)
        {
            var view = Spawn<Probe>();
            var owner = view.gameObject;
            view.Failure = new InvalidOperationException("Cleanup failed.");
            var failure = Assert.Throws<InvalidOperationException>(() =>
            {
                if (entireObject)
                {
                    if (throughInterface) ((IView)view).DestroyViewAndGameObject();
                    else view.DestroyViewAndGameObject();
                }
                else
                {
                    if (throughInterface) ((IView)view).DestroyView();
                    else view.DestroyView();
                }
            });
            Assert.That(failure, Is.SameAs(view.Failure));
            Assert.That(view == null, Is.True);
            Assert.That(owner == null, Is.EqualTo(entireObject));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void SafeDestructionStillDestroysUnityViewAfterFailure(bool wholeObject)
        {
            var view = Spawn<Probe>();
            var owner = view.gameObject;
            view.Failure = new InvalidOperationException("Safe view cleanup failed.");
            var failure = Assert.Throws<InvalidOperationException>(() =>
            {
                if (wholeObject) view.DestroyViewAndGameObjectSafely();
                else view.DestroyViewSafely();
            });

            Assert.That(failure, Is.SameAs(view.Failure));
            Assert.That(view == null, Is.True);
            Assert.That(owner == null, Is.EqualTo(wholeObject));
        }

        [Test]
        public void DestroyViewsRejectsNullCollections()
        {
            IView[] array = null;
            List<IView> list = null;
            IEnumerable<IView> sequence = null;

            Assert.That(Assert.Throws<ArgumentNullException>(() => array.DestroyViews()).ParamName, Is.EqualTo("views"));
            Assert.That(Assert.Throws<ArgumentNullException>(() => list.DestroyViews()).ParamName, Is.EqualTo("views"));
            Assert.That(Assert.Throws<ArgumentNullException>(() => sequence.DestroyViews()).ParamName, Is.EqualTo("views"));
            Assert.That(Assert.Throws<ArgumentNullException>(() => array.DestroyViewsAndGameObjects()).ParamName, Is.EqualTo("views"));
            Assert.That(Assert.Throws<ArgumentNullException>(() => list.DestroyViewsAndGameObjects()).ParamName, Is.EqualTo("views"));
            Assert.That(Assert.Throws<ArgumentNullException>(() => sequence.DestroyViewsAndGameObjects()).ParamName, Is.EqualTo("views"));
        }

        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void DestroyViewsContinuesAfterMissingView(bool destroyGameObjects, bool destroyed)
        {
            var first = Spawn<Probe>();
            var last = Spawn<Probe>();
            IView missing = null;
            if (destroyed)
            {
                var view = Spawn<Probe>();
                Object.DestroyImmediate(view);
                missing = view;
            }

            var views = new[] { first, missing, last };
            var failure = Assert.Throws<ArgumentNullException>(() =>
            {
                if (destroyGameObjects)
                    views.DestroyViewsAndGameObjects();
                else views.DestroyViews();
            });

            Assert.That(failure.ParamName, Is.EqualTo("view"));
            Assert.That(first == null, Is.True);
            Assert.That(last == null, Is.True);
        }

        [Test]
        public void DestroyViewsSafelyPreservesGameObjectsAndSkipsMissingViews()
        {
            var first = Spawn<Probe>();
            var last = Spawn<Probe>();
            var firstOwner = first.gameObject;
            var lastOwner = last.gameObject;
            var destroyed = Spawn<Probe>();
            Object.DestroyImmediate(destroyed);
            var views = new[] { first, null, destroyed, last };

            views.DestroyViewsSafely();

            Assert.That(first == null && last == null, Is.True);
            Assert.That(firstOwner != null && lastOwner != null, Is.True);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DestroyViewsAndGameObjectsContinuesAfterCleanupFails(bool safely)
        {
            var first = Spawn<Probe>();
            var last = Spawn<Probe>();
            var firstOwner = first.gameObject;
            var lastOwner = last.gameObject;
            first.Failure = new InvalidOperationException("Batch cleanup failed.");
            var views = new[] { first, last };

            var failure = Assert.Throws<InvalidOperationException>(() =>
            {
                if (safely)
                    views.DestroyViewsAndGameObjectsSafely();
                else
                    views.DestroyViewsAndGameObjects();
            });
            Assert.That(failure, Is.SameAs(first.Failure));

            Assert.That(firstOwner == null && lastOwner == null, Is.True);
        }

        [Test]
        public void BaseDisposeReleasesBindingsWithoutDestroyingObjectsOrModel()
        {
            var view = Spawn<MonoView>();
            var model = new Model();
            view.Initialize(model);
            view.Dispose();
            Assert.That(view.ViewModel, Is.Null);
            Assert.That(view != null, Is.True);
            Assert.That(model.Disposed, Is.False);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void FailedInitializationDestroysTheWholeClone(int overload)
        {
            var original = Spawn<Probe>();
            original.FailInitialization = true;
            Probe.LastInitialized = null;
            Assert.Throws<InvalidOperationException>(() => Create(overload, original, new Model()));
            Assert.That(Probe.LastInitialized, Is.Not.Null);
            Assert.That(Probe.LastInitialized == null, Is.True);
            Assert.That(Probe.LastOwner == null, Is.True);
            Assert.That(original != null, Is.True);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void NullModelIsRejectedBeforeCloning(int overload)
        {
            var original = Spawn<Probe>();
            Probe.LastInitialized = null;
            Assert.Throws<ArgumentNullException>(() => Create(overload, original, null));
            Assert.That(Probe.LastInitialized, Is.Null);
        }

        [Test]
        public void DestroyedModelIsRejectedBeforeCloning([NUnit.Framework.Range(0, 6)] int overload)
        {
            var original = Spawn<Probe>();
            var model = ScriptableObject.CreateInstance<ScriptableModel>();
            Object.DestroyImmediate(model);
            Probe.LastInitialized = null;

            var error = Assert.Throws<ArgumentNullException>(() => Create(overload, original, model));

            Assert.That(error.ParamName, Is.EqualTo("viewModel"));
            Assert.That(Probe.LastInitialized, Is.Null);
        }

        [Test]
        public void DestroyingAScriptableViewReleasesItsModel()
        {
            var view = Track(ScriptableObject.CreateInstance<EmptyScriptableView>());
            var model = new Model();
            view.Initialize(model);

            Object.DestroyImmediate(view);

            Assert.That(view.ViewModel, Is.Null);
            Assert.That(model.Disposed, Is.False);
        }

        [Test]
        public void FailedScriptableInitializationDestroysTheClone()
        {
            var original = ScriptableObject.CreateInstance<ScriptableProbe>();
            try
            {
                Assert.Throws<InvalidOperationException>(() => MonoView.Instantiate(original, new Model()));
                Assert.That(ScriptableProbe.Last == null, Is.True);
                Assert.That(original != null, Is.True);
            }
            finally
            {
                Object.DestroyImmediate(original);
            }
        }

        [TestCase(false, false, false)]
        [TestCase(false, false, true)]
        [TestCase(false, true, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, false)]
        [TestCase(true, false, true)]
        [TestCase(true, true, false)]
        [TestCase(true, true, true)]
        public void DestroyViewDestroysScriptableViewsEvenWhenCleanupFails(bool wholeObject, bool throughInterface, bool failCleanup)
        {
            var view = ScriptableObject.CreateInstance<DisposableScriptableProbe>();
            var model = new Model();
            view.Initialize(model);
            var failure = new InvalidOperationException("Cleanup failed.");
            view.Failure = failCleanup ? failure : null;
            try
            {
                IViewModel Destroy() => wholeObject
                    ? throughInterface ? ((IView)view).DestroyViewAndGameObject() : view.DestroyViewAndGameObject()
                    : throughInterface ? ((IView)view).DestroyView() : view.DestroyView();

                if (failCleanup)
                    Assert.That(Assert.Throws<InvalidOperationException>(() => Destroy()), Is.SameAs(failure));
                else
                    Assert.That(Destroy(), Is.SameAs(model));

                Assert.That(view == null, Is.True);
                Assert.That(view.DisposeCalls, Is.EqualTo(1));
                Assert.That(model.Disposed, Is.False);
                Assert.That(Assert.Throws<ArgumentNullException>(() => Destroy()).ParamName, Is.EqualTo("view"));
                Assert.That(wholeObject ? view.DestroyViewAndGameObjectSafely() : view.DestroyViewSafely(), Is.Null);
                Assert.That(view.DisposeCalls, Is.EqualTo(1));
            }
            finally
            {
                if (view) Object.DestroyImmediate(view);
            }
        }

        [Test]
        public void DestroyViewRejectsNullAndSafelySkipsIt()
        {
            IView missing = null;
            Assert.That(Assert.Throws<ArgumentNullException>(() => missing.DestroyView()).ParamName, Is.EqualTo("view"));
            Assert.That(Assert.Throws<ArgumentNullException>(() => missing.DestroyViewAndGameObject()).ParamName, Is.EqualTo("view"));
            Assert.That(missing.DestroyViewSafely(), Is.Null);
            Assert.That(missing.DestroyViewAndGameObjectSafely(), Is.Null);
        }

        [Test]
        public void DestroyViewCleansUpManagedViews()
        {
            var model = new Model();
            var view = new ManagedView();
            view.Initialize(model);
            Assert.That(view.DestroyView(), Is.SameAs(model));
            Assert.That(view.ViewModel, Is.Null);
            view.Initialize(model);
            Assert.That(view.DestroyViewAndGameObject(), Is.SameAs(model));
            Assert.That(view.ViewModel, Is.Null);
            Assert.That(model.Disposed, Is.False);
        }

        [Test]
        public void RollbackPreservesInitializationFailureWhenScriptableCleanupAlsoFails()
        {
            var original = ScriptableObject.CreateInstance<FailingScriptableProbe>();
            var failure = new InvalidOperationException("Initialization failed.");
            FailingScriptableProbe.InitializationFailure = failure;
            try
            {
                LogAssert.Expect(LogType.Exception, "InvalidOperationException: Cleanup failed.");
                Assert.That(Assert.Throws<InvalidOperationException>(() =>
                    MonoView.Instantiate(original, new Model())), Is.SameAs(failure));
                Assert.That(FailingScriptableProbe.Last == null, Is.True);
                Assert.That(original != null, Is.True);
            }
            finally
            {
                Object.DestroyImmediate(original);
            }
        }

        private sealed class ScriptableModel : ScriptableViewModel { }

        private sealed class ManagedView : IView
        {
            public IViewModel ViewModel { get; private set; }

            public void Initialize(IViewModel viewModel) =>
                ViewModel = viewModel;

            public void Deinitialize() =>
                ViewModel = null;
        }

        private sealed class DisposableScriptableProbe : ScriptableObject, IView, IDisposable
        {
            public Exception Failure;
            public int DisposeCalls;
            public IViewModel ViewModel { get; private set; }

            public void Initialize(IViewModel viewModel) =>
                ViewModel = viewModel;

            public void Deinitialize() =>
                ViewModel = null;

            public void Dispose()
            {
                DisposeCalls++;
                Deinitialize();
                if (Failure is not null)
                    throw Failure;
            }
        }

        private sealed class FailingScriptableProbe : ScriptableObject, IView
        {
            public static Exception InitializationFailure;
            public static FailingScriptableProbe Last;
            public IViewModel ViewModel => null;

            public void Initialize(IViewModel viewModel)
            {
                Last = this;
                throw InitializationFailure;
            }

            public void Deinitialize() =>
                throw new InvalidOperationException("Cleanup failed.");
        }

        private static Probe Create(int overload, Probe original, IViewModel model) => overload switch
        {
            0 => MonoView.Instantiate(original, model),
            1 => MonoView.Instantiate(original, null, model),
            2 => MonoView.Instantiate(original, null, true, model),
            3 => MonoView.Instantiate(original, Vector3.zero, Quaternion.identity, model),
            4 => MonoView.Instantiate(original, Vector3.zero, Quaternion.identity, null, model),
            5 => MonoView.Instantiate(original, model, default),
            _ => MonoView.Instantiate(original, Vector3.zero, Quaternion.identity, model, default)
        };

        private sealed class Probe : MonoBehaviour, IView, IDisposable
        {
            public static Probe LastInitialized;
            public static GameObject LastOwner;
            [NonSerialized] public Exception Failure;
            public bool FailInitialization;
            public IViewModel ViewModel { get; private set; }

            public void Initialize(IViewModel viewModel)
            {
                LastInitialized = this;
                LastOwner = gameObject;
                ViewModel = viewModel;
                if (FailInitialization)
                    throw new InvalidOperationException("Initialization failed.");
            }

            public void Deinitialize() => ViewModel = null;
            public void Dispose()
            {
                Deinitialize();
                if (Failure is not null) throw Failure;
            }
        }

        private sealed class ScriptableProbe : ScriptableObject, IView
        {
            public static ScriptableProbe Last;
            public IViewModel ViewModel => null;
            public void Initialize(IViewModel viewModel)
            {
                Last = this;
                throw new InvalidOperationException("Initialization failed.");
            }
            public void Deinitialize() { }
        }

        private sealed class Model : IViewModel, IDisposable
        {
            public bool Disposed;
            public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters) => default;
            public void Dispose() => Disposed = true;
        }
    }
}
