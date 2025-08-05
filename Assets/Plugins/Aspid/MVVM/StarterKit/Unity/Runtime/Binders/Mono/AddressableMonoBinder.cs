#if ASPID_MVVM_ADDRESSABLES_INTEGRATION && ASPID_MVVM_UNITASK_INTEGRATION
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Aspid.MVVM.StarterKit.Unity
{
    public abstract partial class AddressableMonoBinder<TAsset, TComponent> : ComponentMonoBinder<TComponent>, IBinder<string>, IBinder<object>
        where TComponent : Component
    {
        private CancellationTokenSource _cts;
        private AsyncOperationHandle<TAsset> _handle;
        protected CancellationToken[] AdditionalCancellationTokensField;

        protected bool IsLoading { get; private set; }
        
        protected virtual CancellationToken[] AdditionalCancellationTokens =>
            AdditionalCancellationTokensField ??= new[] { destroyCancellationToken };

        protected virtual void OnDestroy()
        {
            CancelLoadAsset();
            TryReleaseHandle();
        }

        protected override void OnUnbound()
        {
            CancelLoadAsset();
            TryReleaseHandle();
        }

        [BinderLog]
        public void SetValue(object value)
        {
            TryReleaseHandle();
            SetAsset(GetDefaultAsset());
            
            LoadAssetAsync(value, GetCancellationToken()).Forget();
        }
        
        [BinderLog]
        public void SetValue(string value)
        {
            TryReleaseHandle();
            SetAsset(GetDefaultAsset());
            
            if (!string.IsNullOrWhiteSpace(value))
            {
                LoadAssetAsync(value, GetCancellationToken()).Forget();
            }
            else if (IsLoading)
            {
                CancelLoadAsset();
                IsLoading = false;
            }
        }

        protected virtual TAsset GetDefaultAsset() => default;
        
        protected abstract void SetAsset(TAsset asset);

        private async UniTaskVoid LoadAssetAsync(object value, CancellationToken cancellationToken)
        {
            IsLoading = true;
            _handle = Addressables.LoadAssetAsync<TAsset>(value);
            await _handle;

            if (!cancellationToken.IsCancellationRequested)
            {
                SetAsset(_handle.Result);
                IsLoading = false;
            }
        }

        private CancellationToken GetCancellationToken()
        {
            if (IsLoading)
            {
                CancelLoadAsset();
                _cts = CancellationTokenSource.CreateLinkedTokenSource(AdditionalCancellationTokens);
            }
            else _cts ??= CancellationTokenSource.CreateLinkedTokenSource(AdditionalCancellationTokens);
            
            return _cts.Token;
        }

        private void CancelLoadAsset()
        {
            if (_cts is null) return;
            
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
        
        private void TryReleaseHandle()
        {
            if (!_handle.IsValid()) return;
            
            if (_handle.IsDone)
            {
                Addressables.Release(_handle);
            }
            else if (_handle.Status != AsyncOperationStatus.Failed)
            {
                  _handle.Completed += Addressables.Release;  
            }
        }
    }
}
#endif