using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Transform.rotation"/> or
    /// <see cref="Transform.localRotation"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Rotation")]
    public class TransformRotationMonoBinder : ComponentMonoBinder<Transform, Quaternion>, IRotationBinder
    {
        [Tooltip("Coordinate space the value is applied in.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected sealed override Quaternion Property
        {
            get => CachedComponent.GetRotation(_space);
            set => CachedComponent.SetRotation(value, _space);
        }
    }
}
