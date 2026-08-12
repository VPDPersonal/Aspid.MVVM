using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentQuaternionMonoBinder{Transform}"/> that sets the <see cref="Transform.rotation"/> or
    /// <see cref="Transform.localRotation"/> property depending on the configured <see cref="Space"/>.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current rotation
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Rotation")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation")]
    public partial class TransformRotationMonoBinder : ComponentQuaternionMonoBinder<Transform>
    {
        [Tooltip("The coordinate space in which the rotation is applied.")]
        [SerializeField] private Space _space = Space.World;
        
        protected sealed override Quaternion Property
        {
            get => CachedComponent.GetRotation(_space);
            set => CachedComponent.SetRotation(value, _space);
        }
    }
}