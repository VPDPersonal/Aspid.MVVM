#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
	/// <summary>
	/// Abstract base <see cref="MonoBinder"/> that loads an Addressable asset by key or <see cref="IKeyEvaluator"/>
	/// and applies it when the load completes.
	/// </summary>
	/// <remarks>
	/// Only available when <c>ASPID_MVVM_ADDRESSABLES_INTEGRATION</c> is defined.
	/// When <see cref="_seamlessSwap"/> is enabled, the currently displayed asset is kept on screen
	/// until the next load completes, providing a seamless swap. Otherwise the asset is immediately
	/// reset to the default value before the new load starts.
	/// </remarks>
	/// <typeparam name="TAsset">The type of Addressable asset to load and apply.</typeparam>
	public abstract partial class AddressableMonoBinder<TAsset> : MonoBinder, IBinder<string>, IBinder<IKeyEvaluator>
	{
		[SerializeField] private bool _seamlessSwap;

		private AsyncOperationHandle<TAsset> _currentHandle;
		private AsyncOperationHandle<TAsset> _pendingHandle;

		/// <summary>
		/// Called when the MonoBehaviour is destroyed. Releases both the current and any pending Addressable asset handles.
		/// </summary>
		protected virtual void OnDestroy()
		{
			ReleasePendingHandle();
			ReleaseCurrentHandle();
		}

		/// <summary>
		/// Called after unbinding is complete. Resets to the default asset and releases all Addressable asset handles.
		/// </summary>
		protected override void OnUnbound() =>
			SetDefault();

		/// <summary>
		/// Loads a new asset by the given address key. When seamless swap is enabled the currently displayed asset
		/// is kept on screen until the load completes; otherwise the asset is immediately reset to the default.
		/// Resets to the default asset if <paramref name="value"/> is <see langword="null"/> or whitespace.
		/// </summary>
		/// <param name="value">The Addressable address string.</param>
		[BinderLog]
		public void SetValue(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				SetDefault();
				return;
			}

			if (!_seamlessSwap) ResetToDefault();
			Load(value);
		}

		/// <summary>
		/// Loads a new asset using the runtime key from <paramref name="value"/>. When seamless swap is enabled the
		/// currently displayed asset is kept on screen until the load completes; otherwise the asset is immediately
		/// reset to the default. Resets to the default asset if <paramref name="value"/> is <see langword="null"/>
		/// or its key is <see langword="null"/> or invalid.
		/// </summary>
		/// <param name="value">The key evaluator providing the Addressable runtime key.</param>
		[BinderLog]
		public void SetValue(IKeyEvaluator value)
		{
			if (value is null)
			{
				SetDefault();
				return;
			}

			var key = value.RuntimeKey;

			switch (key)
			{
				case null:
				case string stringKey when string.IsNullOrWhiteSpace(stringKey):
					SetDefault();
					return;

				default:
					if (!_seamlessSwap) ResetToDefault();
					Load(value);
					break;
			}
		}

		private void SetDefault()
		{
			ReleasePendingHandle();
			ResetToDefault();
		}

		private void ResetToDefault()
		{
			ReleaseCurrentHandle();
			SetAsset(GetDefaultAsset());
		}

		private void Load(object key)
		{
			ReleasePendingHandle();

			_pendingHandle = Addressables.LoadAssetAsync<TAsset>(key);

			if (_pendingHandle.IsDone) OnPendingHandleCompleted(_pendingHandle);
			else _pendingHandle.Completed += OnPendingHandleCompleted;
		}

		private void OnPendingHandleCompleted(AsyncOperationHandle<TAsset> handle)
		{
			handle.Completed -= OnPendingHandleCompleted;

			if (this == null) return;

			ReleaseCurrentHandle();
			_currentHandle = handle;
			_pendingHandle = default;

			SetAsset(handle.Result);
		}

		private void ReleaseCurrentHandle()
		{
			if (_currentHandle.IsValid()) _currentHandle.Release();
			_currentHandle = default;
		}

		private void ReleasePendingHandle()
		{
			if (_pendingHandle.IsValid())
			{
				_pendingHandle.Completed -= OnPendingHandleCompleted;
				_pendingHandle.Release();
			}

			_pendingHandle = default;
		}

		/// <summary>
		/// Applies the loaded <paramref name="asset"/> to the target.
		/// Called when the Addressable load operation completes.
		/// </summary>
		/// <param name="asset">The loaded asset.</param>
		protected abstract void SetAsset(TAsset asset);

		/// <summary>
		/// Returns the default asset to display when no address is bound or when the binder is unbound.
		/// Returns <see langword="default"/> by default.
		/// </summary>
		/// <returns>The default asset value.</returns>
		protected virtual TAsset GetDefaultAsset() => default;
	}

	/// <summary>
	/// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that loads an Addressable <typeparamref name="TAsset"/> by key
	/// and applies it to the component.
	/// </summary>
	/// <remarks>
	/// Only available when <c>ASPID_MVVM_ADDRESSABLES_INTEGRATION</c> is defined.
	/// When <see cref="_seamlessSwap"/> is enabled, the currently displayed asset is kept on screen
	/// until the next load completes, providing a seamless swap. Otherwise the asset is immediately
	/// reset to the default value before the new load starts.
	/// </remarks>
	/// <typeparam name="TAsset">The type of Addressable asset to load.</typeparam>
	/// <typeparam name="TComponent">The type of <see cref="Component"/> to apply the loaded asset to.</typeparam>
	public abstract partial class AddressableMonoBinder<TAsset, TComponent> : ComponentMonoBinder<TComponent>,
		IBinder<string>,
		IBinder<IKeyEvaluator>
		where TComponent : Component
	{
		[SerializeField] private bool _seamlessSwap;

		private AsyncOperationHandle<TAsset> _currentHandle;
		private AsyncOperationHandle<TAsset> _pendingHandle;

		/// <summary>
		/// Called when the MonoBehaviour is destroyed. Releases both the current and any pending Addressable asset handles.
		/// </summary>
		protected virtual void OnDestroy()
		{
			ReleasePendingHandle();
			ReleaseCurrentHandle();
		}

		/// <summary>
		/// Called after unbinding is complete. Resets to the default asset and releases all Addressable asset handles.
		/// </summary>
		protected override void OnUnbound() =>
			SetDefault();

		/// <summary>
		/// Loads a new asset by the given address key. When seamless swap is enabled the currently displayed asset
		/// is kept on screen until the load completes; otherwise the asset is immediately reset to the default.
		/// Resets to the default asset if <paramref name="value"/> is <see langword="null"/> or whitespace.
		/// </summary>
		/// <param name="value">The Addressable address string.</param>
		[BinderLog]
		public void SetValue(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				SetDefault();
				return;
			}

			if (!_seamlessSwap) ResetToDefault();
			Load(value);
		}

		/// <summary>
		/// Loads a new asset using the runtime key from <paramref name="value"/>. When seamless swap is enabled the
		/// currently displayed asset is kept on screen until the load completes; otherwise the asset is immediately
		/// reset to the default. Resets to the default asset if <paramref name="value"/> is <see langword="null"/>
		/// or its key is <see langword="null"/> or invalid.
		/// </summary>
		/// <param name="value">The key evaluator providing the Addressable runtime key.</param>
		[BinderLog]
		public void SetValue(IKeyEvaluator value)
		{
			if (value is null)
			{
				SetDefault();
				return;
			}

			var key = value.RuntimeKey;

			switch (key)
			{
				case null:
				case string stringKey when string.IsNullOrWhiteSpace(stringKey):
					SetDefault();
					return;

				default:
					if (!_seamlessSwap) ResetToDefault();
					Load(value);
					break;
			}
		}

		private void SetDefault()
		{
			ReleasePendingHandle();
			ResetToDefault();
		}

		private void ResetToDefault()
		{
			ReleaseCurrentHandle();
			SetAsset(GetDefaultAsset());
		}

		private void Load(object key)
		{
			ReleasePendingHandle();

			_pendingHandle = Addressables.LoadAssetAsync<TAsset>(key);

			if (_pendingHandle.IsDone) OnPendingHandleCompleted(_pendingHandle);
			else _pendingHandle.Completed += OnPendingHandleCompleted;
		}

		private void OnPendingHandleCompleted(AsyncOperationHandle<TAsset> handle)
		{
			handle.Completed -= OnPendingHandleCompleted;

			if (this == null) return;

			ReleaseCurrentHandle();
			_currentHandle = handle;
			_pendingHandle = default;

			SetAsset(handle.Result);
		}

		private void ReleaseCurrentHandle()
		{
			if (_currentHandle.IsValid()) _currentHandle.Release();
			_currentHandle = default;
		}

		private void ReleasePendingHandle()
		{
			if (_pendingHandle.IsValid())
			{
				_pendingHandle.Completed -= OnPendingHandleCompleted;
				_pendingHandle.Release();
			}

			_pendingHandle = default;
		}

		/// <summary>
		/// Applies the loaded <paramref name="asset"/> to the component.
		/// Called when the Addressable load operation completes.
		/// </summary>
		/// <param name="asset">The loaded asset.</param>
		protected abstract void SetAsset(TAsset asset);

		/// <summary>
		/// Returns the default asset to display when no address is bound or when the binder is unbound.
		/// Returns <see langword="default"/> by default.
		/// </summary>
		/// <returns>The default asset value.</returns>
		protected virtual TAsset GetDefaultAsset() => default;
	}
}
#endif
