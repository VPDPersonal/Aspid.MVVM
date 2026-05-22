using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumVector3MonoBinder{Transform}"/> that sets the <see cref="Transform.rotation"/> or
    /// <see cref="Transform.localRotation"/> property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Rotation Enum")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation", SubPath = "Enum")]
    public sealed class TransformRotationEnumMonoBinder : EnumVector3MonoBinder<Transform>
    {
        [Tooltip("The coordinate space in which the rotation is applied.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets the rotation of the <see cref="Transform"/> in the configured <see cref="Space"/>.
        /// </summary>
        protected override void SetValue(Vector3 value) =>
            transform.SetRotation(Quaternion.Euler(value), _space);
    }
}