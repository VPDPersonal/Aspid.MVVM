using System;
using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Object = UnityEngine.Object;
using System.Reflection;
using Aspid.MVVM.StarterKit;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Aspid.MVVM.Tests
{
    [Category("AuditP2")]
    public sealed class AddressableDestroyTests
    {
        [UnityTest]
        public IEnumerator DestroyComponent_DoesNotInstantiateDefault() => Verify(false);

        [UnityTest]
        public IEnumerator DestroyOwner_DoesNotInstantiateDefault() => Verify(true);

        [UnityTest]
        public IEnumerator DestroyDuringPendingLoad_ReleasesWithoutApplyingCompletion()
        {
            var host = new GameObject("Binder");
            var container = new GameObject("Container");
            var template = new GameObject("Template");
            template.AddComponent<AwakeCounter>();
            var binder = host.AddComponent<GameObjectInstantiateAddressableMonoBinder>();
            typeof(GameObjectInstantiateAddressableMonoBinder).GetField("_container", BindingFlags.Instance | BindingFlags.NonPublic)!.SetValue(binder, container.transform);
            var provider = new PendingProvider();
            Addressables.ResourceManager.ResourceProviders.Add(provider);
            var location = new ResourceLocationBase("pending", "pending", provider.ProviderId, typeof(GameObject));
            var handle = Addressables.ResourceManager.ProvideResource<GameObject>(location);
            var loader = typeof(AddressableMonoBinder<GameObject>).GetProperty("Loader", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(binder);
            var loaderType = loader.GetType();
            loaderType.GetField("_pending", BindingFlags.Instance | BindingFlags.NonPublic)!.SetValue(loader, handle);
            var callback = (Action<AsyncOperationHandle<GameObject>>)Delegate.CreateDelegate(typeof(Action<AsyncOperationHandle<GameObject>>), loader,
                loaderType.GetMethod("OnCompleted", BindingFlags.Instance | BindingFlags.NonPublic)!);
            handle.Completed += callback;
            try
            {
                var created = AwakeCounter.Count;
                Object.Destroy(host);
                yield return null;
                provider.Complete(template);
                yield return null;
                yield return null;
                Assert.AreEqual(created, AwakeCounter.Count);
                Assert.AreEqual(0, container.transform.childCount);
                Assert.AreEqual(1, provider.Released);
            }
            finally
            {
                if (handle.IsValid()) handle.Release();
                Addressables.ResourceManager.ResourceProviders.Remove(provider);
                Object.DestroyImmediate(host);
                Object.DestroyImmediate(container);
                Object.DestroyImmediate(template);
            }
        }

        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void ReleaseFailure_DoesNotSkipCleanup(bool componentBinder, bool releaseAllDirectly)
        {
            var host = new GameObject("Release failure");
            var binder = componentBinder
                ? (MonoBinder)host.AddComponent<ComponentDestroyProbe>()
                : host.AddComponent<DestroyProbe>();
            var probe = (IDestroyProbe)binder;
            var member = new OneWayBindableMember<string>(null);
            binder.Bind(new BindContext(member, binder));
            var baseType = componentBinder
                ? typeof(AddressableMonoBinder<GameObject, Transform>)
                : typeof(AddressableMonoBinder<GameObject>);
            var loader = baseType.GetProperty("Loader", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(binder);
            var loaderType = loader.GetType();
            var pendingField = loaderType.GetField("_pending", BindingFlags.Instance | BindingFlags.NonPublic)!;
            var currentField = loaderType.GetField("_current", BindingFlags.Instance | BindingFlags.NonPublic)!;
            var failure = new InvalidOperationException("Pending release failed.");
            var pending = new ReleaseProbeOperation { Failure = failure };
            var current = new ReleaseProbeOperation();
            pendingField.SetValue(loader, Addressables.ResourceManager.StartOperation(pending, default));
            currentField.SetValue(loader, Addressables.ResourceManager.StartOperation(current, default));
            try
            {
                if (releaseAllDirectly)
                {
                    var exception = Assert.Throws<TargetInvocationException>(() => loaderType.GetMethod("ReleaseAll")!.Invoke(loader, null));
                    Assert.AreSame(failure, exception!.InnerException);
                }
                else
                {
                    Assert.AreSame(failure, Assert.Throws<InvalidOperationException>(() => probe.DestroyForTest()));
                    Assert.IsFalse(binder.IsBound, "Base destruction must unbind after cancellation fails.");
                    Assert.AreEqual(1, probe.Released, "Owned instances must be released after cancellation fails.");
                }

                Assert.AreEqual(1, pending.Released, "A failed release must not be retried by ReleaseAll.");
                Assert.AreEqual(1, current.Released, "The current asset must be released after cancellation fails.");
                Assert.IsFalse(((AsyncOperationHandle<GameObject>)pendingField.GetValue(loader)).IsValid());
                Assert.IsFalse(((AsyncOperationHandle<GameObject>)currentField.GetValue(loader)).IsValid());
            }
            finally
            {
                pendingField.SetValue(loader, default(AsyncOperationHandle<GameObject>));
                currentField.SetValue(loader, default(AsyncOperationHandle<GameObject>));
                Object.DestroyImmediate(host);
            }
        }

        private sealed class ReleaseProbeOperation : AsyncOperationBase<GameObject>
        {
            internal Exception Failure;
            internal int Released;

            protected override void Execute() =>
                Complete(null, true, (Exception)null);

            protected override void Destroy()
            {
                Released++;
                if (Failure is not null)
                    throw Failure;
            }
        }

        private sealed class PendingProvider : ResourceProviderBase
        {
            private ProvideHandle _handle;
            internal int Released;
            public override void Provide(ProvideHandle handle) => _handle = handle;
            public override void Release(IResourceLocation location, object asset) => Released++;
            internal void Complete(GameObject asset) => _handle.Complete(asset, true, null);
        }

        private static IEnumerator Verify(bool owner)
        {
            var template = new GameObject("Default");
            template.AddComponent<AwakeCounter>();
            var container = new GameObject("External container");
            var host = new GameObject("Binder");
            var binder = host.AddComponent<GameObjectInstantiateAddressableMonoBinder>();
            typeof(GameObjectInstantiateAddressableMonoBinder).GetField("_container", BindingFlags.Instance | BindingFlags.NonPublic)!.SetValue(binder, container.transform);
            typeof(GameObjectInstantiateAddressableMonoBinder).GetField("_defaultPrefab", BindingFlags.Instance | BindingFlags.NonPublic)!.SetValue(binder, template);
            try
            {
                binder.Bind(new BindContext(new OneWayBindableMember<string>(null), binder));
                Assert.AreEqual(1, container.transform.childCount);
                var created = AwakeCounter.Count;
                Object.Destroy(owner ? host : binder);
                yield return null;
                yield return null;
                Assert.AreEqual(created, AwakeCounter.Count, "Destroy instantiated the default prefab.");
                Assert.AreEqual(0, container.transform.childCount, "An owned instance survived the binder.");
            }
            finally
            {
                Object.DestroyImmediate(host);
                Object.DestroyImmediate(container);
                Object.DestroyImmediate(template);
            }
        }
    }

    public interface IDestroyProbe
    {
        int Released { get; }
        void DestroyForTest();
    }

    public sealed class DestroyProbe : AddressableMonoBinder<GameObject>, IDestroyProbe
    {
        public int Released { get; private set; }

        public void DestroyForTest() =>
            OnDestroy();

        protected override void ReleaseAsset() =>
            Released++;

        protected override void SetAsset(GameObject asset) { }
    }

    public sealed class ComponentDestroyProbe : AddressableMonoBinder<GameObject, Transform>, IDestroyProbe
    {
        public int Released { get; private set; }

        public void DestroyForTest() =>
            OnDestroy();

        protected override void ReleaseAsset() =>
            Released++;

        protected override void SetAsset(GameObject asset) { }
    }

    public sealed class AwakeCounter : MonoBehaviour
    {
        internal static int Count;
        private void Awake() => Count++;
    }
}
