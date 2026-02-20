#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
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
			
			if (value is not null)
				Load(value);
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

	public abstract partial class AddressableMonoBinder<TAsset, TComponent> : ComponentMonoBinder<TComponent>, IBinder<string>, IBinder<IKeyEvaluator>
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

			if (value is not null) Load(value);
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
}
#endif