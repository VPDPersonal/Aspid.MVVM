using System;
using System.Collections;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEditor;
using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.TestTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class UnityViewModelLifecycleTests : SceneFixture
    {
        private bool _restorePlayModeSettings;
        private bool _previousOptionsEnabled;
        private EnterPlayModeOptions _previousOptions;

        [Test]
        public void LifecycleExtensionsHandleDestroyedViewModel()
        {
            var model = ScriptableObject.CreateInstance<DisposableScriptableProbe>();
            var view = ScriptableObject.CreateInstance<StubScriptableView>();
            try
            {
                UnityEngine.Object.DestroyImmediate(model);

                Assert.That(Assert.Throws<ArgumentNullException>(() => model.InitializeView(view)).ParamName, Is.EqualTo("viewModel"));
                Assert.That(Assert.Throws<ArgumentNullException>(() => model.DisposeViewModel()).ParamName, Is.EqualTo("viewModel"));
                Assert.That(Assert.Throws<ArgumentNullException>(() => ((IViewModel)model).DisposeViewModel()).ParamName, Is.EqualTo("viewModel"));
                model.InitializeViewSafely(view);
                model.InitializeViewsSafely(view);

                Assert.That(view.ViewModel, Is.Null);
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(view);
            }
        }

        [UnityTearDown]
        public IEnumerator RestorePlayModeSettings()
        {
            if (!_restorePlayModeSettings) yield break;

            try
            {
                if (EditorApplication.isPlaying)
                    yield return new ExitPlayMode();
            }
            finally
            {
                EditorSettings.enterPlayModeOptions = _previousOptions;
                EditorSettings.enterPlayModeOptionsEnabled = _previousOptionsEnabled;
                _restorePlayModeSettings = false;
            }
        }

        [UnityTest]
        public IEnumerator ValidationSurvivesRepeatedPlayModeTransitionsWithoutDomainReload()
        {
            _previousOptionsEnabled = EditorSettings.enterPlayModeOptionsEnabled;
            _previousOptions = EditorSettings.enterPlayModeOptions;
            _restorePlayModeSettings = true;
            EditorSettings.enterPlayModeOptionsEnabled = true;
            FlushValidation();
            for (var cycle = 0; cycle < 4; cycle++)
            {
                EditorSettings.enterPlayModeOptions = cycle < 2
                    ? EnterPlayModeOptions.DisableDomainReload
                    : EnterPlayModeOptions.DisableDomainReload | EnterPlayModeOptions.DisableSceneReload;
                var editQueue = GetPendingValidationQueue();
                yield return new EnterPlayMode(expectDomainReload: false);
                Assert.That(GetPendingValidationQueue(), Is.Not.SameAs(editQueue));
                var model = Track(ScriptableObject.CreateInstance<ScriptableProbe>());
                FlushValidation();
                model.Calls = 0;
                model.Validate();
                yield return WaitFor(() => model.Calls > 0);
                Assert.That(model.Calls, Is.EqualTo(1));

                var playQueue = GetPendingValidationQueue();
                yield return new ExitPlayMode();
                Assert.That(GetPendingValidationQueue(), Is.Not.SameAs(playQueue));
                model = Track(ScriptableObject.CreateInstance<ScriptableProbe>());
                FlushValidation();
                model.Calls = 0;
                model.Validate();
                yield return WaitFor(() => model.Calls > 0);
                Assert.That(model.Calls, Is.EqualTo(1));
            }
        }

        [Test]
        public void NullArgumentsAreRejectedByBothOverloads()
        {
            Assert.That(Assert.Throws<ArgumentNullException>(() =>
                MonoViewModelExtensions.DestroyViewModel<MonoProbe>(null)).ParamName, Is.EqualTo("viewModel"));
            Assert.That(Assert.Throws<ArgumentNullException>(() =>
                MonoViewModelExtensions.DestroyViewModel((IViewModel)null)).ParamName, Is.EqualTo("viewModel"));
        }

        [Test]
        public void BaseDisposePreservesTheComponentAndGameObjectInEditMode()
        {
            var model = Spawn<MonoProbe>();
            var owner = model.gameObject;
            model.Dispose();
            Assert.That(model != null, Is.True);
            Assert.That(owner != null, Is.True);
            Assert.DoesNotThrow(model.Dispose);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DestroyDisposesOnceAndReturnsTheSurvivingGameObject(bool throughInterface)
        {
            var model = Spawn<DisposableProbe>();
            var owner = model.gameObject;
            var result = DestroyModel(model, throughInterface);
            Assert.That(result, Is.SameAs(owner));
            Assert.That(model == null, Is.True);
            Assert.That(model.DisposeCalls, Is.EqualTo(1));
            Assert.That(DestroyModel(model, throughInterface), Is.Null);
            Assert.That(model.DisposeCalls, Is.EqualTo(1));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DisposalFailureStillDestroysTheComponentAndPreservesTheException(bool throughInterface)
        {
            var model = Spawn<DisposableProbe>();
            var owner = model.gameObject;
            var failure = new InvalidOperationException("Disposal failed.");
            model.Cleanup = () => throw failure;
            var actual = Assert.Throws<InvalidOperationException>(() => DestroyModel(model, throughInterface));
            Assert.That(actual, Is.SameAs(failure));
            Assert.That(model == null, Is.True);
            Assert.That(owner != null, Is.True);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DisposalMayDestroyTheGameObject(bool throughInterface)
        {
            var model = Spawn<DisposableProbe>();
            model.Cleanup = () => UnityEngine.Object.DestroyImmediate(model.gameObject);
            Assert.That(DestroyModel(model, throughInterface), Is.Null);
        }

        [Test]
        public void NonDisposableComponentIsDestroyed()
        {
            var model = Spawn<ComponentProbe>();
            var owner = model.gameObject;
            Assert.That(model.DestroyViewModel(), Is.SameAs(owner));
            Assert.That(model == null, Is.True);
        }

        [Test]
        public void ManagedViewModelIsDisposedAndScriptableViewModelIsDestroyed()
        {
            var managed = new ManagedProbe();
            Assert.That(((IViewModel)managed).DestroyViewModel(), Is.Null);
            Assert.That(managed.Disposed, Is.True);

            var scriptable = Track(ScriptableObject.CreateInstance<ScriptableProbe>());
            Assert.That(((IViewModel)scriptable).DestroyViewModel(), Is.Null);
            Assert.That(scriptable == null, Is.True);
        }

        [UnityTest]
        public IEnumerator MonoValidationFromWorkerThreadIsDeferredAndCoalesced()
        {
            var model = Spawn<MonoProbe>();
            yield return WaitFor(() => model.Calls > 0);
            model.Calls = 0;
            var mainThread = Thread.CurrentThread.ManagedThreadId;

            Task.Run(() =>
            {
                model.Validate();
                model.Validate();
            }).GetAwaiter().GetResult();

            Assert.That(model.Calls, Is.Zero);
            yield return WaitFor(() => model.Calls > 0);
            Assert.That(model.Calls, Is.EqualTo(1));
            Assert.That(model.NotifyThread, Is.EqualTo(mainThread));
        }

        [UnityTest]
        public IEnumerator ScriptableValidationFromWorkerThreadUsesTheMainThread()
        {
            var model = Track(ScriptableObject.CreateInstance<ScriptableProbe>());
            model.Validate();
            yield return WaitFor(() => model.Calls > 0);
            model.Calls = 0;
            var mainThread = Thread.CurrentThread.ManagedThreadId;

            Task.Run(model.Validate).GetAwaiter().GetResult();
            Assert.That(model.Calls, Is.Zero);
            yield return WaitFor(() => model.Calls > 0);
            Assert.That(model.Calls, Is.EqualTo(1));
            Assert.That(model.NotifyThread, Is.EqualTo(mainThread));
        }

        [UnityTest]
        public IEnumerator DestroyedViewModelDoesNotReceivePendingNotifications()
        {
            var model = Spawn<MonoProbe>();
            var survivor = Spawn<MonoProbe>();
            model.Validate();
            Destroy(model);
            yield return WaitFor(() => survivor.Calls > 0);
            Assert.That(model.Calls, Is.Zero);
        }

        [UnityTest]
        public IEnumerator NotificationFailureDoesNotBlockOtherViewModels()
        {
            var broken = Spawn<MonoProbe>();
            var survivor = Spawn<MonoProbe>();
            broken.OnNotify = () => throw new InvalidOperationException("Notification failed.");
            LogAssert.Expect(LogType.Exception, "InvalidOperationException: Notification failed.");
            yield return WaitFor(() => survivor.Calls > 0);
            Assert.That(broken.Calls, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator ReentrantValidationQueuesAnotherNotification()
        {
            var model = Spawn<MonoProbe>();
            model.OnNotify = () =>
            {
                model.OnNotify = null;
                model.Validate();
                Assert.That(model.Calls, Is.EqualTo(1));
            };
            yield return WaitFor(() => model.Calls == 2);
        }

        [Test]
        public void ReentrantDispatchLeavesNewRequestsForTheNextPass()
        {
            var model = Spawn<MonoProbe>();
            FlushValidation();
            model.Calls = 0;
            model.OnNotify = () =>
            {
                model.OnNotify = null;
                model.Validate();
                FlushValidation();
            };

            model.Validate();
            model.Validate();
            FlushValidation();
            Assert.That(model.Calls, Is.EqualTo(1));

            FlushValidation();
            Assert.That(model.Calls, Is.EqualTo(2));
            FlushValidation();
            Assert.That(model.Calls, Is.EqualTo(2));
        }

        [Test]
        public void ConcurrentProducersCoalesceEachTargetAndAllowLaterNotifications()
        {
            var first = Spawn<MonoProbe>();
            var second = Spawn<MonoProbe>();
            FlushValidation();
            first.Calls = second.Calls = 0;
            var mainThread = Thread.CurrentThread.ManagedThreadId;

            Parallel.For(0, 128, _ =>
            {
                first.Validate();
                second.Validate();
            });

            Assert.That(first.Calls, Is.Zero);
            Assert.That(second.Calls, Is.Zero);
            FlushValidation();
            Assert.That(first.Calls, Is.EqualTo(1));
            Assert.That(second.Calls, Is.EqualTo(1));
            Assert.That(first.NotifyThread, Is.EqualTo(mainThread));
            Assert.That(second.NotifyThread, Is.EqualTo(mainThread));

            first.Validate();
            second.Validate();
            FlushValidation();
            Assert.That(first.Calls, Is.EqualTo(2));
            Assert.That(second.Calls, Is.EqualTo(2));
        }

        [TestCase(PlayModeStateChange.ExitingEditMode, PlayModeStateChange.EnteredPlayMode)]
        [TestCase(PlayModeStateChange.ExitingPlayMode, PlayModeStateChange.EnteredEditMode)]
        public void ModeTransitionDiscardsOldRequestsAndPreservesNewRequests(
            PlayModeStateChange exiting, PlayModeStateChange entered)
        {
            var stale = Track(ScriptableObject.CreateInstance<ScriptableProbe>());
            var fresh = Track(ScriptableObject.CreateInstance<ScriptableProbe>());
            FlushValidation();
            stale.Calls = fresh.Calls = 0;
            stale.Validate();

            var retiredQueue = (ConcurrentQueue<(UnityEngine.Object Target, Action<UnityEngine.Object> Notify)>)
                GetPendingValidationQueue();
            try
            {
                ChangeValidationMode(exiting);
                Task.Run(() =>
                {
                    retiredQueue.Enqueue((stale, target => ((ScriptableProbe)target).NotifyAll()));
                    fresh.Validate();
                }).GetAwaiter().GetResult();
                FlushValidation();
                Assert.That(stale.Calls, Is.Zero);
                Assert.That(fresh.Calls, Is.Zero);

                ChangeValidationMode(entered);
                FlushValidation();
                Assert.That(stale.Calls, Is.Zero);
                Assert.That(fresh.Calls, Is.EqualTo(1));
                stale.Validate();
                FlushValidation();
                Assert.That(stale.Calls, Is.EqualTo(1));
            }
            finally
            {
                ChangeValidationMode(entered);
            }
        }

        [TestCase(PlayModeStateChange.ExitingEditMode, PlayModeStateChange.EnteredPlayMode)]
        [TestCase(PlayModeStateChange.ExitingPlayMode, PlayModeStateChange.EnteredEditMode)]
        public void ModeTransitionInsideNotificationAbandonsOldBatchWithoutReentering(
            PlayModeStateChange exiting, PlayModeStateChange entered)
        {
            var first = Spawn<MonoProbe>();
            var stale = Spawn<MonoProbe>();
            FlushValidation();
            first.Calls = stale.Calls = 0;
            first.OnNotify = () =>
            {
                first.OnNotify = null;
                ChangeValidationMode(exiting);
                first.Validate();
                ChangeValidationMode(entered);
                FlushValidation();
            };
            try
            {
                first.Validate();
                stale.Validate();
                FlushValidation();
                Assert.That(first.Calls, Is.EqualTo(1));
                Assert.That(stale.Calls, Is.Zero);
                FlushValidation();
                Assert.That(first.Calls, Is.EqualTo(2));
                Assert.That(stale.Calls, Is.Zero);
            }
            finally
            {
                ChangeValidationMode(entered);
            }
        }

        private static object GetPendingValidationQueue() =>
            typeof(MonoViewModel).Assembly.GetType("Aspid.MVVM.ViewModelValidationDispatcher", true)
                .GetField("_pending", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

        private static void ChangeValidationMode(PlayModeStateChange state) =>
            typeof(MonoViewModel).Assembly.GetType("Aspid.MVVM.ViewModelValidationDispatcher", true)
                .GetMethod("OnPlayModeStateChanged", BindingFlags.Static | BindingFlags.NonPublic)
                .Invoke(null, new object[] { state });

        private static void FlushValidation() =>
            typeof(MonoViewModel).Assembly.GetType("Aspid.MVVM.ViewModelValidationDispatcher", true)
                .GetMethod("NotifyPending", BindingFlags.Static | BindingFlags.NonPublic)
                .Invoke(null, null);

        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void WholeObjectDestructionDisposesOnceEvenOnFailure(bool throughInterface, bool failCleanup)
        {
            var model = Spawn<DisposableProbe>();
            var owner = model.gameObject;
            var failure = new InvalidOperationException("Cleanup failed.");
            if (failCleanup) model.Cleanup = () => throw failure;
            GameObject Destroy() => throughInterface
                ? ((IViewModel)model).DestroyViewModelAndGameObject()
                : model.DestroyViewModelAndGameObject();
            if (failCleanup)
                Assert.That(Assert.Throws<InvalidOperationException>(() => Destroy()), Is.SameAs(failure));
            else
                Assert.That(Destroy(), Is.Null);
            Assert.That(owner == null, Is.True);
            Assert.That(model == null, Is.True);
            Assert.That(model.DisposeCalls, Is.EqualTo(1));
        }

        [TestCase(false, false, false)]
        [TestCase(false, false, true)]
        [TestCase(false, true, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, false)]
        [TestCase(true, false, true)]
        [TestCase(true, true, false)]
        [TestCase(true, true, true)]
        public void ScriptableDestructionDisposesOnce(bool wholeObject, bool throughInterface, bool failCleanup)
        {
            var model = Track(ScriptableObject.CreateInstance<DisposableScriptableProbe>());
            var failure = new InvalidOperationException("Cleanup failed.");
            if (failCleanup) model.Cleanup = () => throw failure;
            GameObject Destroy() => wholeObject
                ? throughInterface ? ((IViewModel)model).DestroyViewModelAndGameObject() : model.DestroyViewModelAndGameObject()
                : throughInterface ? ((IViewModel)model).DestroyViewModel() : model.DestroyViewModel();
            if (failCleanup)
                Assert.That(Assert.Throws<InvalidOperationException>(() => Destroy()), Is.SameAs(failure));
            else
                Assert.That(Destroy(), Is.Null);
            Assert.That(model == null, Is.True);
            Assert.That(model.DisposeCalls, Is.EqualTo(1));
            Assert.That(Destroy(), Is.Null);
        }

        [Test]
        public void DisposalIsReentrantAndIsNotRepeatedByUnityDestruction()
        {
            var mono = Spawn<DisposableProbe>();
            var scriptable = Track(ScriptableObject.CreateInstance<DisposableScriptableProbe>());
            mono.Cleanup = mono.Dispose;
            scriptable.Cleanup = scriptable.Dispose;
            mono.Dispose();
            scriptable.Dispose();
            Assert.That(mono != null && scriptable != null, Is.True);
            mono.Dispose();
            scriptable.Dispose();
            UnityEngine.Object.DestroyImmediate(mono.gameObject);
            UnityEngine.Object.DestroyImmediate(scriptable);
            Assert.That(mono.DisposeCalls, Is.EqualTo(1));
            Assert.That(scriptable.DisposeCalls, Is.EqualTo(1));
        }

        [Test]
        public void DirectScriptableDestructionRunsCleanup()
        {
            var scriptable = Track(ScriptableObject.CreateInstance<DisposableScriptableProbe>());
            UnityEngine.Object.DestroyImmediate(scriptable);
            Assert.That(scriptable.DisposeCalls, Is.EqualTo(1));
        }

        [Test]
        public void WholeObjectHelperRejectsNullAndDisposesManagedModels()
        {
            Assert.Throws<ArgumentNullException>(() =>
                MonoViewModelExtensions.DestroyViewModelAndGameObject<MonoProbe>(null));
            Assert.Throws<ArgumentNullException>(() =>
                MonoViewModelExtensions.DestroyViewModelAndGameObject((IViewModel)null));
            var model = new ManagedProbe();
            Assert.That(((IViewModel)model).DestroyViewModelAndGameObject(), Is.Null);
            Assert.That(model.Disposed, Is.True);
        }

        [Test]
        public void InitializerDisposesButDoesNotDestroyAnExternalViewModel()
        {
            var model = Spawn<DisposableProbe>();
            var owner = Spawn();
            owner.SetActive(false);
            var initializer = owner.AddComponent<Aspid.MVVM.StarterKit.ViewInitializer>();
            var type = initializer.GetType();
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            type.GetField("_viewModel", flags).SetValue(initializer, model);
            type.GetField("_isDisposeViewModelOnDestroy", flags).SetValue(initializer, true);
            type.GetMethod("OnDestroy", flags).Invoke(initializer, null);
            Assert.That(model != null, Is.True);
            Assert.That(model.DisposeCalls, Is.EqualTo(1));
        }

        private sealed class DisposableScriptableProbe : ScriptableViewModel
        {
            public int DisposeCalls { get; private set; }
            public Action Cleanup { get; set; }

            protected override void OnDispose()
            {
                DisposeCalls++;
                Cleanup?.Invoke();
            }
        }

        private static GameObject DestroyModel(DisposableProbe model, bool throughInterface) => throughInterface
            ? ((IViewModel)model).DestroyViewModel()
            : model.DestroyViewModel();

        private static IEnumerator WaitFor(Func<bool> condition)
        {
            var deadline = Time.realtimeSinceStartup + 5f;
            while (!condition() && Time.realtimeSinceStartup < deadline)
                yield return null;

            Assert.That(condition(), Is.True, "Expected an Editor update to deliver the notification.");
        }

        private sealed class MonoProbe : MonoViewModel
        {
            public int Calls { get; set; }
            public int NotifyThread { get; private set; }
            public Action OnNotify { get; set; }

            public void Validate() =>
                base.OnValidate();

            public override void NotifyAll()
            {
                Calls++;
                NotifyThread = Thread.CurrentThread.ManagedThreadId;
                OnNotify?.Invoke();
            }
        }

        private sealed class ScriptableProbe : ScriptableViewModel
        {
            public int Calls { get; set; }
            public int NotifyThread { get; private set; }

            public void Validate() =>
                base.OnValidate();

            public override void NotifyAll()
            {
                Calls++;
                NotifyThread = Thread.CurrentThread.ManagedThreadId;
            }
        }

        private sealed class DisposableProbe : MonoViewModel
        {
            public int DisposeCalls { get; private set; }
            public Action Cleanup { get; set; }

            protected override void OnDispose()
            {
                DisposeCalls++;
                Cleanup?.Invoke();
            }
        }

        private sealed class ComponentProbe : MonoBehaviour, IViewModel
        {
            public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters) =>
                default;
        }

        private sealed class ManagedProbe : IViewModel, IDisposable
        {
            public bool Disposed { get; private set; }

            public void Dispose() =>
                Disposed = true;

            public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters) =>
                default;
        }
    }
}
