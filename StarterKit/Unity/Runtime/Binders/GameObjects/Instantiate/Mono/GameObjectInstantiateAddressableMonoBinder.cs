#if ASPID_MVVM_ADDRESSABLES_INTEGRATION
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AddressableMonoBinder{GameObject}"/> that instantiates a <see cref="GameObject"/> prefab
    /// loaded from the Addressables system into the configured container with the specified position and rotation.
    /// Destroys the previously spawned instance before each new spawn.
    /// </summary>
    /// <remarks>
    /// Position and rotation are applied in the chosen <see cref="Space"/> after instantiation —
    /// <see cref="Space.Self"/> values are local to the container, <see cref="Space.World"/> values are absolute.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Instantiate Addressable")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Addressable")]
    public sealed class GameObjectInstantiateAddressableMonoBinder : AddressableMonoBinder<GameObject>
    {
        [Tooltip("Parent transform under which loaded prefabs are spawned. Defaults to this object on Reset.")]
        [SerializeField] private Transform _container;

        [Tooltip("Spawn position; interpreted in the chosen Space (Self = local to container, World = absolute).")]
        [SerializeField] private Vector3 _position;
        [SerializeField] private Space _positionSpace = Space.Self;

        [Tooltip("Spawn rotation as Euler angles; interpreted in the chosen Space.")]
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private Space _rotationSpace = Space.Self;

        [Tooltip("Prefab spawned when no address is bound or when the binder is unbound.")]
        [SerializeField] private GameObject _defaultPrefab;

        private GameObject _currentInstance;

        private void Reset() =>
            _container = transform;

        /// <summary>
        /// Called when the MonoBehaviour is destroyed. Destroys the spawned instance before releasing the prefab handle.
        /// </summary>
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
