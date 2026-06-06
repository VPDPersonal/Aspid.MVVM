using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupVector3MonoBinder{Transform}"/> that sets the <see cref="Transform.rotation"/> or
    /// <see cref="Transform.localRotation"/> on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Rotation EnumGroup")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation", SubPath = "EnumGroup")]
    public sealed class TransformRotationEnumGroupMonoBinder : EnumGroupVector3MonoBinder<Transform>
    {
        [Tooltip("The coordinate space in which the rotation is applied.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets the rotation of the element in the configured <see cref="Space"/>.
        /// </summary>
        protected override void SetValue(Transform element, Vector3 value) => 
            element.SetRotation(Quaternion.Euler(value), _space);
    }
}