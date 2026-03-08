#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
	/// <summary>
	/// Abstract base MonoBehaviour binder that loads an Addressable asset by key or <see cref="UnityEngine.AddressableAssets.IKeyEvaluator"/>
	/// and applies it when the asset is ready. Requires the Addressables integration.
	/// </summary>
	public abstract partial class AddressableMonoBinder<TAsset> : MonoBinder, IBinder<string>, IBinder<IKeyEvaluator>
	{
		private AsyncOperationHandle<TAsset> _lastHandle;

		protected virtual void OnDestroy() =>
			ReleaseCurrentHandle();

		protected override void OnUnbound() =>
			SetDefault();
		
		[BinderLog]
		public void SetValue(string value)
		{
			SetDefault();
			
			if (!string.IsNullOrWhiteSpace(value))
				Load(value);
		}

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

		protected abstract void SetAsset(TAsset asset);

		protected virtual TAsset GetDefaultAsset() => default;
	}

	/// <summary>
	/// Abstract base MonoBehaviour binder that loads an Addressable asset of type <typeparamref name="TAsset"/> by key
	/// and applies it to a specific <typeparamref name="TComponent"/> on the GameObject. Requires the Addressables integration.
	/// </summary>
	/// <typeparam name="TAsset">The type of Addressable asset to load.</typeparam>
	/// <typeparam name="TComponent">The Unity component to apply the loaded asset to.</typeparam>
	public abstract partial class AddressableMonoBinder<TAsset, TComponent> : ComponentMonoBinder<TComponent>,
		IBinder<string>,
		IBinder<IKeyEvaluator>
		where TComponent : Component
	{
		private AsyncOperationHandle<TAsset> _lastHandle;

		protected virtual void OnDestroy() =>
			ReleaseCurrentHandle();

		protected override void OnUnbound() =>
			SetDefault();

		[BinderLog]
		public void SetValue(string value)
		{
			SetDefault();

			if (!string.IsNullOrWhiteSpace(value)) Load(value);
		}

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

		protected abstract void SetAsset(TAsset asset);

		protected virtual TAsset GetDefaultAsset() => default;
		public void SetValue(AssetReference value)
		{
			throw new System.NotImplementedException();
		}
	}
}
#endif