using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumVector3MonoBinder{Transform}"/> that sets the <see cref="Transform.eulerAngles"/> or
    /// <see cref="Transform.localEulerAngles"/> property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – EulerAngles Enum")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation", SubPath = "Enum")]
    public sealed class TransformEulerAnglesEnumMonoBinder : EnumVector3MonoBinder<Transform>
    {
        [Tooltip("The coordinate space in which the euler angles are applied.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets the euler angles of the <see cref="Transform"/> in the configured <see cref="Space"/>.
        /// </summary>
        protected override void SetValue(Vector3 value) =>
            transform.SetEulerAngles(value, _space);
    }
}