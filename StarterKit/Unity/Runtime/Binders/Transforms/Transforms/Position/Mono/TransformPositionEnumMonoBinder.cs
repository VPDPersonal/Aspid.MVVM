using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumVector3MonoBinder{Transform}"/> that sets the <see cref="Transform.position"/> or
    /// <see cref="Transform.localPosition"/> property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Position Enum")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalPosition", SubPath = "Enum")]
    public sealed class TransformPositionEnumMonoBinder : EnumVector3MonoBinder<Transform>
    {
        [Tooltip("The coordinate space in which the position is applied.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets the position of the <see cref="Transform"/> in the configured <see cref="Space"/>.
        /// </summary>
        protected override void SetValue(Vector3 value) =>
            transform.SetPosition(value, _space);
    }
}