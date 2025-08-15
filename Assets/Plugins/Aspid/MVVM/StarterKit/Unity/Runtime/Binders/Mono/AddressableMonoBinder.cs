#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Aspid.MVVM.StarterKit.Unity
{
    public abstract partial class AddressableMonoBinder<TAsset> : MonoBinder, IBinder<string>, IBinder<IKeyEvaluator>
    {
        private AsyncOperationHandle<TAsset> _handle;
        
        protected virtual void OnDestroy() =>
            TryReleaseHandle();

        protected override void OnUnbound()
        {
            TryReleaseHandle();
            SetAsset(GetDefaultAsset());
        }

        [BinderLog]
        public void SetValue(IKeyEvaluator value)
        {
            TryReleaseHandle();
            SetAsset(GetDefaultAsset());
            
            Load(value);
        }
        
        [BinderLog]
        public void SetValue(string value)
        {
            TryReleaseHandle();
            SetAsset(GetDefaultAsset());
            
            if (!string.IsNullOrWhiteSpace(value))
            {
                Load(value);
            }
        }

        private void Load(object key)
        {
            _handle = Addressables.LoadAssetAsync<TAsset>(key);
            _handle.Completed += OnHandleCompleted;
        }

        private void OnHandleCompleted(AsyncOperationHandle<TAsset> handle)
        {
            if (_handle.Equals(handle)) SetAsset(_handle.Result);
            else Addressables.Release(handle);
        }
        
        private void TryReleaseHandle()
        {
            if (_handle.IsValid() && _handle.IsDone)
                Addressables.Release(_handle);
            
            _handle = default;
        }
        
        protected abstract void SetAsset(TAsset asset);
        
        protected virtual TAsset GetDefaultAsset() => default;
    }
    
    public abstract partial class AddressableMonoBinder<TAsset, TComponent> : ComponentMonoBinder<TComponent>, IBinder<string>, IBinder<IKeyEvaluator>
        where TComponent : Component
    {
        private AsyncOperationHandle<TAsset> _handle;

        protected virtual void OnDestroy() =>
            TryReleaseHandle();

        protected override void OnUnbound()
        {
            TryReleaseHandle();
            SetAsset(GetDefaultAsset());
        }

        [BinderLog]
        public void SetValue(IKeyEvaluator value)
        {
            TryReleaseHandle();
            SetAsset(GetDefaultAsset());
            
            Load(value);
        }
        
        [BinderLog]
        public void SetValue(string value)
        {
            TryReleaseHandle();
            SetAsset(GetDefaultAsset());
            
            if (!string.IsNullOrWhiteSpace(value))
            {
                Load(value);
            }
        }

        protected void Load(object key)
        {
            _handle = Addressables.LoadAssetAsync<TAsset>(key);
            _handle.Completed += OnHandleCompleted;
        }
        
        protected void TryReleaseHandle()
        {
            if (_handle.IsValid() && _handle.IsDone)
                Addressables.Release(_handle);
            
            _handle = default;
        }

        protected abstract void SetAsset(TAsset asset);
        
        protected virtual TAsset GetDefaultAsset() => default;
        
        private void OnHandleCompleted(AsyncOperationHandle<TAsset> handle)
        {
            if (_handle.Equals(handle)) SetAsset(_handle.Result);
            else Addressables.Release(handle);
        }
    }
}
#endif