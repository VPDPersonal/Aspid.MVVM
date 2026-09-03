using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Transform.position"/> or
    /// <see cref="Transform.localPosition"/> on each element.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalPosition", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Position EnumGroup")]
    public sealed class TransformPositionEnumGroupMonoBinder : EnumGroupMonoBinder<Transform, Vector3>
    {
        [Tooltip("Coordinate space the value is applied in.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected override void SetValue(Transform element, Vector3 value) =>
            element.SetPosition(value, _space);
    }
}
