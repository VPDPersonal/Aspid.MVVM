using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{Transform, Vector3}"/> that switches the <see cref="Transform.rotation"/> or
    /// <see cref="Transform.localRotation"/> between two euler angle values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Rotation Switcher")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation", SubPath = "Switcher")]
    public sealed class TransformRotationSwitcherMonoBinder : SwitcherMonoBinder<Transform, Vector3>
    {
        [Tooltip("The coordinate space in which the rotation is applied.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Called when applying the selected euler angles as a <see cref="Quaternion"/> rotation to the <see cref="Transform"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(Vector3 value) => 
            CachedComponent.SetRotation(Quaternion.Euler(value), _space);
    }
}