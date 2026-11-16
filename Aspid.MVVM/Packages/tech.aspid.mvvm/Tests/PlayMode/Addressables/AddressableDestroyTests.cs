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

    public sealed class AwakeCounter : MonoBehaviour
    {
        internal static int Count;
        private void Awake() => Count++;
    }
}
