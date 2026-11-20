using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class UnityViewModelLifetimeTests : SceneFixture
    {
        [UnityTest]
        public IEnumerator BaseDisposePreservesTheComponentAndGameObject()
        {
            var model = Spawn<MonoProbe>();
            var owner = model.gameObject;
            model.Dispose();
            Assert.That(model != null, Is.True);
            yield return null;
            Assert.That(model != null, Is.True);
            Assert.That(owner != null, Is.True);
        }

        [UnityTest]
        public IEnumerator GenericDestroyDefersDestruction() =>
            CheckDestruction(throughInterface: false, throwOnDispose: false);

        [UnityTest]
        public IEnumerator InterfaceDestroyDefersDestruction() =>
            CheckDestruction(throughInterface: true, throwOnDispose: false);

        [UnityTest]
        public IEnumerator GenericDestroyStillDestroysAfterDisposalFailure() =>
            CheckDestruction(throughInterface: false, throwOnDispose: true);

        [UnityTest]
        public IEnumerator InterfaceDestroyStillDestroysAfterDisposalFailure() =>
            CheckDestruction(throughInterface: true, throwOnDispose: true);

        private IEnumerator CheckDestruction(bool throughInterface, bool throwOnDispose)
        {
            var model = Spawn<DisposableProbe>();
            var owner = model.gameObject;
            var failure = new InvalidOperationException("Disposal failed.");
            if (throwOnDispose)
                model.Failure = failure;

            GameObject result = null;
            void DestroyModel() =>
                result = throughInterface ? ((IViewModel)model).DestroyViewModel() : model.DestroyViewModel();

            if (throwOnDispose)
                Assert.That(Assert.Throws<InvalidOperationException>(DestroyModel), Is.SameAs(failure));
            else
            {
                DestroyModel();
                Assert.That(result, Is.SameAs(owner));
            }

            Assert.That(model.DisposeCalls, Is.EqualTo(1));
            Assert.That(model != null, Is.True);
            yield return null;
            Assert.That(model == null, Is.True);
            Assert.That(owner != null, Is.True);
            Assert.That(model.DisposeCalls, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator WholeGameObjectDestructionCleansUpOnce() =>
            CheckWholeObjectDestruction(failCleanup: false);

        [UnityTest]
        public IEnumerator WholeGameObjectDestructionSurvivesCleanupFailure() =>
            CheckWholeObjectDestruction(failCleanup: true);

        private IEnumerator CheckWholeObjectDestruction(bool failCleanup)
        {
            var model = Spawn<DisposableProbe>();
            var owner = model.gameObject;
            if (failCleanup) model.Failure = new InvalidOperationException("Disposal failed.");
            if (failCleanup)
                Assert.Throws<InvalidOperationException>(() => model.DestroyViewModelAndGameObject());
            else
                model.DestroyViewModelAndGameObject();
            Assert.That(owner != null, Is.True);
            yield return null;
            Assert.That(owner == null, Is.True);
            Assert.That(model.DisposeCalls, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator ScriptableDestructionIsDeferredAndDisposesOnce()
        {
            var model = ScriptableObject.CreateInstance<ScriptableProbe>();
            try
            {
                model.DestroyViewModelAndGameObject();
                Assert.That(model != null, Is.True);
                yield return null;
                Assert.That(model == null, Is.True);
                Assert.That(model.DisposeCalls, Is.EqualTo(1));
            }
            finally
            {
                if (model) UnityEngine.Object.Destroy(model);
            }
        }

        [UnityTest]
        public IEnumerator ExternalUnityDestructionDisposesTheModel()
        {
            var model = Spawn<DisposableProbe>();
            UnityEngine.Object.Destroy(model.gameObject);
            yield return null;
            Assert.That(model.DisposeCalls, Is.EqualTo(1));
        }

        private sealed class ScriptableProbe : ScriptableViewModel
        {
            public int DisposeCalls { get; private set; }

            protected override void OnDispose() =>
                DisposeCalls++;
        }

        private sealed class MonoProbe : MonoViewModel { }

        private sealed class DisposableProbe : MonoViewModel
        {
            public int DisposeCalls { get; private set; }
            public Exception Failure { get; set; }

            protected override void OnDispose()
            {
                DisposeCalls++;
                if (Failure is not null)
                    throw Failure;
            }
        }
    }
}
