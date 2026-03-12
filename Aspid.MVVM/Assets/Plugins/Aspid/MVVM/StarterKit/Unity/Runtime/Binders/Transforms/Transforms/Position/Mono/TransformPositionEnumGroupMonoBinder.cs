using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupVector3MonoBinder{Transform}"/> that sets the <see cref="Transform.position"/> or
    /// <see cref="Transform.localPosition"/> on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Position EnumGroup")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalPosition", SubPath = "EnumGroup")]
    public sealed class TransformPositionEnumGroupMonoBinder : EnumGroupVector3MonoBinder<Transform>
    {
        [Tooltip("The coordinate space in which the position is applied.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets the position of the element in the configured <see cref="Space"/>.
        /// </summary>
        protected override void SetValue(Transform element, Vector3 value) =>
            element.SetPosition(value, _space);
    }
}