using System;
using NUnit.Framework;
using UnityEngine;
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

            Assert.That(((IView)view).ReinitializeSafely(new Model()), Is.Null);
            Assert.That(view.DeinitializeViewSafely(), Is.Null);
            Assert.That(view.DisposeViewSafely(), Is.Null);
            Assert.That(((IView)view).DisposeViewSafely(), Is.Null);
            Assert.That(view.ViewModel, Is.SameAs(model));
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
        public void NullModelIsRejectedBeforeCloning(int overload)
        {
            var original = Spawn<Probe>();
            Probe.LastInitialized = null;
            Assert.Throws<ArgumentNullException>(() => Create(overload, original, null));
            Assert.That(Probe.LastInitialized, Is.Null);
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
        public void ScriptableViewsAreDestroyedEvenWhenCleanupFails(bool wholeObject, bool throughInterface, bool failCleanup)
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
                Assert.That(Destroy(), Is.Null);
                Assert.That(view.DisposeCalls, Is.EqualTo(1));
            }
            finally
            {
                if (view) Object.DestroyImmediate(view);
            }
        }

        [Test]
        public void NullAndManagedViewsDoNotRequireUnityObjects()
        {
            IView missing = null;
            Assert.That(missing.DestroyView(), Is.Null);
            Assert.That(missing.DestroyViewAndGameObject(), Is.Null);
            Assert.That(MonoViewExtensions.DestroyView<DisposableScriptableProbe>(null), Is.Null);
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
            _ => MonoView.Instantiate(original, Vector3.zero, Quaternion.identity, null, model)
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
