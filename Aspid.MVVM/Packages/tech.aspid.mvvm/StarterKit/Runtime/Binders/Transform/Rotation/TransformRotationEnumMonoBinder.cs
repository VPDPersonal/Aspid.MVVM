using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Transform.rotation"/> or
    /// <see cref="Transform.localRotation"/>.
    /// </summary>
    /// <remarks>
    /// Takes euler angles.
    /// </remarks>
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Rotation Enum")]
    public sealed class TransformRotationEnumMonoBinder : EnumMonoBinder<Transform, Vector3>
    {
        [Tooltip("Coordinate space the value is applied in.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetRotation(Quaternion.Euler(value), _space);
    }
}
