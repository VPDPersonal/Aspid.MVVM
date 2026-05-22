using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherVector3MonoBinder{Transform}"/> that switches the <see cref="Transform.rotation"/> or
    /// <see cref="Transform.localRotation"/> between two euler angle values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Rotation Switcher")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation", SubPath = "Switcher")]
    public sealed class TransformRotationSwitcherMonoBinder : SwitcherVector3MonoBinder<Transform>
    {
        [Tooltip("The coordinate space in which the rotation is applied.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Called when applying the selected euler angles as a <see cref="Quaternion"/> rotation to the <see cref="Transform"/>.
        /// </summary>
        protected override void SetValue(Vector3 value) => 
            transform.SetRotation(Quaternion.Euler(value), _space);
    }
}