using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Transform.rotation"/> or
    /// <see cref="Transform.localRotation"/> on each element.
    /// </summary>
    /// <remarks>
    /// Takes euler angles.
    /// </remarks>
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Rotation EnumGroup")]
    public sealed class TransformRotationEnumGroupMonoBinder : EnumGroupMonoBinder<Transform, Vector3>
    {
        [Tooltip("Coordinate space the value is applied in.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected override void SetValue(Transform element, Vector3 value) =>
            element.SetRotation(Quaternion.Euler(value), _space);
    }
}
