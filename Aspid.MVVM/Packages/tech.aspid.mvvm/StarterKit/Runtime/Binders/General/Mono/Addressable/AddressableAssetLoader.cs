#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Loads one Addressable asset at a time for a binder, releasing the previous handle when the next one completes.
    /// </summary>
    /// <typeparam name="TAsset">The type of asset to load.</typeparam>
    internal sealed class AddressableAssetLoader<TAsset>
    {
        private readonly IBinder _owner;
        private readonly Action<TAsset> _apply;

        private AsyncOperationHandle<TAsset> _current;
        private AsyncOperationHandle<TAsset> _pending;

        /// <param name="owner">The binder that owns the loader; names the source in diagnostics.</param>
        /// <param name="apply">Receives each successfully loaded asset.</param>
        public AddressableAssetLoader(IBinder owner, Action<TAsset> apply)
        {
            _owner = owner;
            _apply = apply;
        }

        /// <summary>
        /// Starts loading <paramref name="key"/>, cancelling any load still in progress.
        /// </summary>
        /// <param name="key">The Addressable key.</param>
        public void Load(object key)
        {
            CancelPending();

            _pending = Addressables.LoadAssetAsync<TAsset>(key);

            if (_pending.IsDone) OnCompleted(_pending);
            else _pending.Completed += OnCompleted;
        }

        /// <summary>
        /// Cancels and releases the load in progress, if any.
        /// </summary>
        public void CancelPending()
        {
            if (_pending.IsValid())
            {
                _pending.Completed -= OnCompleted;
                _pending.Release();
            }

            _pending = default;
        }

        /// <summary>
        /// Releases the loaded asset, if any.
        /// </summary>
        public void ReleaseCurrent()
        {
            if (_current.IsValid()) _current.Release();
            _current = default;
        }

        /// <summary>
        /// Releases both the loaded asset and the load in progress.
        /// </summary>
        public void ReleaseAll()
        {
            CancelPending();
            ReleaseCurrent();
        }

        private void OnCompleted(AsyncOperationHandle<TAsset> handle)
        {
            handle.Completed -= OnCompleted;
            _pending = default;

            if (handle.Status is not AsyncOperationStatus.Succeeded)
            {
                _owner.LogError(
                    problem: $"the Addressable load failed: {handle.OperationException?.Message ?? "unknown error"}",
                    consequence: "The asset is not applied.");

                handle.Release();
                return;
            }

            ReleaseCurrent();
            _current = handle;
            _apply(handle.Result);
        }
    }
}
#endif
