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
	/// </remarks>
	/// <typeparam name="TAsset">The type of Addressable asset to load and apply.</typeparam>
	public abstract partial class AddressableMonoBinder<TAsset> : MonoBinder, IBinder<string>, IBinder<IKeyEvaluator>
	{
		private AsyncOperationHandle<TAsset> _lastHandle;

		/// <summary>
		/// Called when the MonoBehaviour is destroyed. Releases the current Addressable asset handle.
		/// </summary>
		protected virtual void OnDestroy() =>
			ReleaseCurrentHandle();

		/// <summary>
		/// Called after unbinding is complete. Resets to the default asset and releases the current Addressable asset handle.
		/// </summary>
		protected override void OnUnbound() =>
			SetDefault();

		/// <summary>
		/// Resets to the default asset and loads a new one by the given address key.
		/// Does nothing if <paramref name="value"/> is <see langword="null"/> or whitespace.
		/// </summary>
		/// <param name="value">The Addressable address string.</param>
		[BinderLog]
		public void SetValue(string value)
		{
			SetDefault();
			if (!string.IsNullOrWhiteSpace(value)) Load(value);
		}

		/// <summary>
		/// Resets to the default asset and loads a new one using the runtime key from <paramref name="value"/>.
		/// Does nothing if <paramref name="value"/> is <see langword="null"/> or its key is <see langword="null"/> or invalid.
		/// </summary>
		/// <param name="value">The key evaluator providing the Addressable runtime key.</param>
		[BinderLog]
		public void SetValue(IKeyEvaluator value)
		{
			SetDefault();

			if (value is null) return;
			var key = value.RuntimeKey;

			switch (key)
			{
				case null:
				case string stringKey when string.IsNullOrWhiteSpace(stringKey): return;

				default: Load(value); break;
			}
		}

		private void SetDefault()
		{
			ReleaseCurrentHandle();
			SetAsset(GetDefaultAsset());
		}

		private void Load(object key)
		{
			_lastHandle = Addressables.LoadAssetAsync<TAsset>(key);

			if (_lastHandle.IsDone) OnHandleCompleted(_lastHandle);
			else _lastHandle.Completed += OnHandleCompleted;
		}

		private void ReleaseCurrentHandle()
		{
			if (_lastHandle.IsValid())
			{
				_lastHandle.Completed -= OnHandleCompleted;
				_lastHandle.Release();
			}

			_lastHandle = default;
		}

		private void OnHandleCompleted(AsyncOperationHandle<TAsset> handle)
		{
			handle.Completed -= OnHandleCompleted;
			SetAsset(handle.Result);
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
	/// </remarks>
	/// <typeparam name="TAsset">The type of Addressable asset to load.</typeparam>
	/// <typeparam name="TComponent">The type of <see cref="Component"/> to apply the loaded asset to.</typeparam>
	public abstract partial class AddressableMonoBinder<TAsset, TComponent> : ComponentMonoBinder<TComponent>,
		IBinder<string>,
		IBinder<IKeyEvaluator>
		where TComponent : Component
	{
		private AsyncOperationHandle<TAsset> _lastHandle;

		/// <summary>
		/// Called when the MonoBehaviour is destroyed. Releases the current Addressable asset handle.
		/// </summary>
		protected virtual void OnDestroy() =>
			ReleaseCurrentHandle();

		/// <summary>
		/// Called after unbinding is complete. Resets to the default asset and releases the current Addressable asset handle.
		/// </summary>
		protected override void OnUnbound() =>
			SetDefault();

		/// <summary>
		/// Resets to the default asset and loads a new one by the given address key.
		/// Does nothing if <paramref name="value"/> is <see langword="null"/> or whitespace.
		/// </summary>
		/// <param name="value">The Addressable address string.</param>
		[BinderLog]
		public void SetValue(string value)
		{
			SetDefault();
			if (!string.IsNullOrWhiteSpace(value)) Load(value);
		}

		/// <summary>
		/// Resets to the default asset and loads a new one using the runtime key from <paramref name="value"/>.
		/// Does nothing if <paramref name="value"/> is <see langword="null"/> or its key is <see langword="null"/> or invalid.
		/// </summary>
		/// <param name="value">The key evaluator providing the Addressable runtime key.</param>
		[BinderLog]
		public void SetValue(IKeyEvaluator value)
		{
			SetDefault();

			if (value is null) return;
			var key = value.RuntimeKey;

			switch (key)
			{
				case null:
				case string stringKey when string.IsNullOrWhiteSpace(stringKey): return;

				default: Load(value); break;
			}
		}

		private void SetDefault()
		{
			ReleaseCurrentHandle();
			SetAsset(GetDefaultAsset());
		}

		private void Load(object key)
		{
			_lastHandle = Addressables.LoadAssetAsync<TAsset>(key);

			if (_lastHandle.IsDone) OnHandleCompleted(_lastHandle);
			else _lastHandle.Completed += OnHandleCompleted;
		}

		private void ReleaseCurrentHandle()
		{
			if (_lastHandle.IsValid())
			{
				_lastHandle.Completed -= OnHandleCompleted;
				_lastHandle.Release();
			}

			_lastHandle = default;
		}

		private void OnHandleCompleted(AsyncOperationHandle<TAsset> handle)
		{
			handle.Completed -= OnHandleCompleted;
			SetAsset(handle.Result);
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