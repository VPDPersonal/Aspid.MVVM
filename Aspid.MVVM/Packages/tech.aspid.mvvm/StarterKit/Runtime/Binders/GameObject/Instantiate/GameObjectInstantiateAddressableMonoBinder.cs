#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AddressableMonoBinder{TAsset}"/> that instantiates the loaded prefab into a container, replacing
    /// the previous instance.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Instantiate Addressable")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Instantiate Addressable")]
    public sealed class GameObjectInstantiateAddressableMonoBinder : AddressableMonoBinder<GameObject>
    {
        [Tooltip("Parent of the instance; this transform when empty.")]
        [SerializeField] private Transform _container;

        [Tooltip("Instance position.")]
        [SerializeField] private Vector3 _position;

        [Tooltip("Space the position is applied in.")]
        [SerializeField] private Space _positionSpace = Space.Self;

        [Tooltip("Instance rotation as Euler angles.")]
        [SerializeField] private Vector3 _rotation;

        [Tooltip("Space the rotation is applied in.")]
        [SerializeField] private Space _rotationSpace = Space.Self;

        [Tooltip("Prefab shown while loading, when loading fails, or when no address is bound.")]
        [SerializeField] private GameObject _defaultPrefab;

        private GameObject _currentInstance;

        /// <inheritdoc/>
        protected override void Reset()
        {
            base.Reset();
            _container = transform;
        }

        /// <inheritdoc/>
        protected override void OnDestroy()
        {
            DestroyCurrentInstance();
            base.OnDestroy();
        }

        /// <inheritdoc/>
        protected override GameObject GetDefaultAsset() =>
            _defaultPrefab;

        /// <inheritdoc/>
        protected override void SetAsset(GameObject prefab)
        {
            DestroyCurrentInstance();
            if (!prefab) return;

            var parent = _container ? _container : transform;
            _currentInstance = Instantiate(prefab, parent);

            var instanceTransform = _currentInstance.transform;
            instanceTransform.SetPosition(_position, _positionSpace);
            instanceTransform.SetEulerAngles(_rotation, _rotationSpace);
        }

        private void DestroyCurrentInstance()
        {
            if (!_currentInstance) return;

            Destroy(_currentInstance);
            _currentInstance = null;
        }
    }
}
#endif
