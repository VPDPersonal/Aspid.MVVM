#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using UnityEngine;
using UnityEngine.AddressableAssets;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that loads an Addressable asset by key or <see cref="IKeyEvaluator"/>
    /// and applies it once loaded. An empty key applies <see cref="GetDefaultAsset"/>.
    /// </summary>
    /// <remarks>
    /// Available only with <c>ASPID_MVVM_ADDRESSABLES_INTEGRATION</c>.
    /// </remarks>
    /// <typeparam name="TAsset">The type of asset to load.</typeparam>
    public abstract partial class AddressableMonoBinder<TAsset> : MonoBinder,
        IBinder<string>,
        IBinder<IKeyEvaluator>
    {
        [Tooltip("Keep the previous asset until the new one is loaded.")]
        [SerializeField] private bool _seamlessSwap;

        private AddressableAssetLoader<TAsset> _loader;

        private AddressableAssetLoader<TAsset> Loader =>
            _loader ??= new AddressableAssetLoader<TAsset>(this, SetAsset);

        /// <inheritdoc/>
        protected override void OnDestroy()
        {
            base.OnDestroy();
            _loader?.ReleaseAll();
        }

        /// <inheritdoc/>
        protected override void OnUnbound() =>
            SetDefault();

        /// <summary>
        /// Loads the asset at <paramref name="value"/>, or applies the default asset when the key is empty.
        /// </summary>
        /// <param name="value">The Addressable address.</param>
        [BinderLog]
        public void SetValue(string value) =>
            Apply(string.IsNullOrWhiteSpace(value) ? null : value);

        /// <summary>
        /// Loads the asset behind <paramref name="value"/>, or applies the default asset when the key is empty.
        /// </summary>
        /// <param name="value">The evaluator providing the Addressable runtime key.</param>
        [BinderLog]
        public void SetValue(IKeyEvaluator value) =>
            Apply(IsEmptyKey(value?.RuntimeKey) ? null : value);

        private void Apply(object key)
        {
            if (key is null)
            {
                SetDefault();
                return;
            }

            if (!_seamlessSwap) ResetToDefault();
            Loader.Load(key);
        }

        private void SetDefault()
        {
            Loader.CancelPending();
            ResetToDefault();
        }

        private void ResetToDefault()
        {
            Loader.ReleaseCurrent();
            SetAsset(GetDefaultAsset());
        }

        private static bool IsEmptyKey(object key) =>
            key is null || key is string text && string.IsNullOrWhiteSpace(text);

        /// <summary>
        /// Applies <paramref name="asset"/> to the target.
        /// </summary>
        /// <param name="asset">The loaded asset, or the default one.</param>
        protected abstract void SetAsset(TAsset asset);

        /// <summary>
        /// Returns the asset applied when no key is bound. The default is <see langword="default"/>.
        /// </summary>
        /// <returns>The default asset.</returns>
        protected virtual TAsset GetDefaultAsset() => default;
    }

    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that loads an Addressable asset by key or <see cref="IKeyEvaluator"/>
    /// and applies it to the component once loaded. An empty key applies <see cref="GetDefaultAsset"/>.
    /// </summary>
    /// <remarks>
    /// Available only with <c>ASPID_MVVM_ADDRESSABLES_INTEGRATION</c>.
    /// </remarks>
    /// <typeparam name="TAsset">The type of asset to load.</typeparam>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that receives the asset.</typeparam>
    public abstract partial class AddressableMonoBinder<TAsset, TComponent> : ComponentMonoBinder<TComponent>,
        IBinder<string>,
        IBinder<IKeyEvaluator>
        where TComponent : Component
    {
        [Tooltip("Keep the previous asset until the new one is loaded.")]
        [SerializeField] private bool _seamlessSwap;

        private AddressableAssetLoader<TAsset> _loader;

        private AddressableAssetLoader<TAsset> Loader =>
            _loader ??= new AddressableAssetLoader<TAsset>(this, SetAsset);

        /// <inheritdoc/>
        protected override void OnDestroy()
        {
            base.OnDestroy();
            _loader?.ReleaseAll();
        }

        /// <inheritdoc/>
        protected override void OnUnbound() =>
            SetDefault();

        /// <summary>
        /// Loads the asset at <paramref name="value"/>, or applies the default asset when the key is empty.
        /// </summary>
        /// <param name="value">The Addressable address.</param>
        [BinderLog]
        public void SetValue(string value) =>
            Apply(string.IsNullOrWhiteSpace(value) ? null : value);

        /// <summary>
        /// Loads the asset behind <paramref name="value"/>, or applies the default asset when the key is empty.
        /// </summary>
        /// <param name="value">The evaluator providing the Addressable runtime key.</param>
        [BinderLog]
        public void SetValue(IKeyEvaluator value) =>
            Apply(IsEmptyKey(value?.RuntimeKey) ? null : value);

        private void Apply(object key)
        {
            if (key is null)
            {
                SetDefault();
                return;
            }

            if (!_seamlessSwap) ResetToDefault();
            Loader.Load(key);
        }

        private void SetDefault()
        {
            Loader.CancelPending();
            ResetToDefault();
        }

        private void ResetToDefault()
        {
            Loader.ReleaseCurrent();
            SetAsset(GetDefaultAsset());
        }

        private static bool IsEmptyKey(object key) =>
            key is null || key is string text && string.IsNullOrWhiteSpace(text);

        /// <summary>
        /// Applies <paramref name="asset"/> to the component.
        /// </summary>
        /// <param name="asset">The loaded asset, or the default one.</param>
        protected abstract void SetAsset(TAsset asset);

        /// <summary>
        /// Returns the asset applied when no key is bound. The default is <see langword="default"/>.
        /// </summary>
        /// <returns>The default asset.</returns>
        protected virtual TAsset GetDefaultAsset() => default;
    }
}
#endif
