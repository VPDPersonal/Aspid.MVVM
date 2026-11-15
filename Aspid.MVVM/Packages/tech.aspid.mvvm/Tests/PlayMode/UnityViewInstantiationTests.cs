using System;
using System.Collections;
using System.Threading;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Aspid.MVVM.Tests
{
    internal sealed class UnityViewInstantiationTests : SceneFixture
    {
        [UnityTest]
        public IEnumerator EveryAsyncOverloadInitializesItsClonesWithTheSharedModel()
        {
            var original = Spawn<Probe>();
            var model = new Model();
            for (var overload = 0; overload < 12; overload++)
            {
                Probe.InitializeCalls = 0;
                Probe.FailAt = 0;
                var operation = Create(overload, original, model);
                yield return operation;
                yield return null;
                Assert.That(operation.Result.Length, Is.EqualTo(overload < 4 ? 1 : 3));
                foreach (var view in operation.Result)
                {
                    Assert.That(view.ViewModel, Is.SameAs(model));
                    view.DestroyViewAndGameObject();
                }
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator FailedBatchRollsBackInitializedAndUninitializedClones()
        {
            var original = Spawn<Probe>();
            var model = new Model();
            Probe.InitializeCalls = 0;
            Probe.FailAt = 2;
            Probe.DeinitializeCalls = 0;
            LogAssert.Expect(LogType.Exception, new Regex("InvalidOperationException: Initialization failed"));
            var operation = MonoView.InstantiateAsync(original, 3, model);
            yield return operation;
            yield return null;
            var clones = operation.Result;
            Assert.That(Probe.InitializeCalls, Is.EqualTo(2));
            Assert.That(Probe.DeinitializeCalls, Is.EqualTo(3));
            yield return null;
            foreach (var clone in clones)
                Assert.That(clone == null, Is.True);
            Assert.That(model.Disposed, Is.False);
        }

        [UnityTest]
        public IEnumerator CancelledOperationDoesNotInitializeClones()
        {
            var original = Spawn<Probe>();
            Probe.InitializeCalls = 0;
            Probe.FailAt = 0;
            var operation = MonoView.InstantiateAsync(original, 3, new Model());
            operation.Cancel();
            yield return operation;
            yield return null;
            Assert.That(Probe.InitializeCalls, Is.Zero);
        }

        [UnityTest]
        public IEnumerator CancellationTokenDoesNotInitializeClones()
        {
            var original = Spawn<Probe>();
            Probe.InitializeCalls = 0;
            Probe.FailAt = 0;
            using var cancellation = new CancellationTokenSource();
            var operation = MonoView.InstantiateAsync(
                original, 3, null, Vector3.zero, Quaternion.identity, new Model(), cancellation.Token);
            cancellation.Cancel();
            yield return operation;
            yield return null;
            Assert.That(Probe.InitializeCalls, Is.Zero);
        }

        [UnityTest]
        public IEnumerator DestructionAfterFailureIsDeferredUntilTheNextFrame()
        {
            var view = Spawn<Probe>();
            var owner = view.gameObject;
            view.FailCleanup = true;
            Assert.Throws<InvalidOperationException>(() => view.DestroyViewAndGameObject());
            Assert.That(view != null, Is.True);
            yield return null;
            Assert.That(view == null, Is.True);
            Assert.That(owner == null, Is.True);
        }

        [UnityTest]
        public IEnumerator ScriptableViewDestructionIsDeferred() =>
            CheckScriptableDestruction(wholeObject: true, failCleanup: false);

        [UnityTest]
        public IEnumerator ScriptableViewDestructionContinuesAfterCleanupFailure() =>
            CheckScriptableDestruction(wholeObject: true, failCleanup: true);

        [UnityTest]
        public IEnumerator DestroyViewDestroysScriptableObjects() =>
            CheckScriptableDestruction(wholeObject: false, failCleanup: false);

        [UnityTest]
        public IEnumerator DestroyViewDestroysScriptableObjectsAfterCleanupFailure() =>
            CheckScriptableDestruction(wholeObject: false, failCleanup: true);

        private static IEnumerator CheckScriptableDestruction(bool wholeObject, bool failCleanup)
        {
            var view = ScriptableObject.CreateInstance<ScriptableProbe>();
            view.FailCleanup = failCleanup;
            try
            {
                IViewModel Destroy() => wholeObject
                    ? view.DestroyViewAndGameObject()
                    : view.DestroyView();
                if (failCleanup)
                    Assert.Throws<InvalidOperationException>(() => Destroy());
                else
                    Destroy();

                Assert.That(view != null, Is.True);
                Assert.That(view.DeinitializeCalls, Is.EqualTo(1));
                yield return null;
                Assert.That(view == null, Is.True);
            }
            finally
            {
                if (view) UnityEngine.Object.Destroy(view);
            }
        }

        private sealed class ScriptableProbe : ScriptableObject, IView
        {
            public bool FailCleanup;
            public int DeinitializeCalls;
            public IViewModel ViewModel => null;
            public void Initialize(IViewModel viewModel) { }

            public void Deinitialize()
            {
                DeinitializeCalls++;
                if (FailCleanup)
                    throw new InvalidOperationException("Cleanup failed.");
            }
        }

        private static AsyncInstantiateOperation<Probe> Create(int overload, Probe original, IViewModel model)
        {
            var positions = new[] { Vector3.zero };
            var rotations = new[] { Quaternion.identity };
            return overload switch
            {
                0 => MonoView.InstantiateAsync(original, model),
                1 => MonoView.InstantiateAsync(original, null, model),
                2 => MonoView.InstantiateAsync(original, Vector3.zero, Quaternion.identity, model),
                3 => MonoView.InstantiateAsync(original, null, Vector3.zero, Quaternion.identity, model),
                4 => MonoView.InstantiateAsync(original, 3, model),
                5 => MonoView.InstantiateAsync(original, 3, null, model),
                6 => MonoView.InstantiateAsync(original, 3, Vector3.zero, Quaternion.identity, model),
                7 => MonoView.InstantiateAsync(original, 3, positions, rotations, model),
                8 => MonoView.InstantiateAsync(original, 3, null, Vector3.zero, Quaternion.identity, model),
                9 => MonoView.InstantiateAsync(original, 3, null, positions, rotations, model),
                10 => MonoView.InstantiateAsync(original, 3, null, Vector3.zero, Quaternion.identity, model, default),
                _ => MonoView.InstantiateAsync(original, 3, null, positions, rotations, model, default)
            };
        }

        private sealed class Probe : MonoBehaviour, IView, IDisposable
        {
            public static int InitializeCalls;
            public static int DeinitializeCalls;
            public static int FailAt;
            public bool FailCleanup;
            public IViewModel ViewModel { get; private set; }

            public void Initialize(IViewModel viewModel)
            {
                ViewModel = viewModel;
                if (++InitializeCalls == FailAt)
                    throw new InvalidOperationException("Initialization failed.");
            }

            public void Deinitialize()
            {
                DeinitializeCalls++;
                ViewModel = null;
            }

            public void Dispose()
            {
                Deinitialize();
                if (FailCleanup)
                    throw new InvalidOperationException("Cleanup failed.");
            }
        }

        private sealed class Model : IViewModel, IDisposable
        {
            public bool Disposed;
            public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters) => default;
            public void Dispose() => Disposed = true;
        }
    }
}
