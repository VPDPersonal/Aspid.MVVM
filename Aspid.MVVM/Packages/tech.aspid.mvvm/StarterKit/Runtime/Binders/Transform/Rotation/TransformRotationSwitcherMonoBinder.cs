using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="Transform.rotation"/> or
    /// <see cref="Transform.localRotation"/>.
    /// </summary>
    /// <remarks>
    /// Takes euler angles.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Rotation Switcher")]
    public sealed class TransformRotationSwitcherMonoBinder : SwitcherMonoBinder<Transform, Vector3>
    {
        [Tooltip("Coordinate space the value is applied in.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetRotation(Quaternion.Euler(value), _space);
    }
}
