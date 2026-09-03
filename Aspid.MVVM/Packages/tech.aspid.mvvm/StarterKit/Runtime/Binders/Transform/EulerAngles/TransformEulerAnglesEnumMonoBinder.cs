using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Transform.eulerAngles"/> or
    /// <see cref="Transform.localEulerAngles"/>.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – EulerAngles Enum")]
    public sealed class TransformEulerAnglesEnumMonoBinder : EnumMonoBinder<Transform, Vector3>
    {
        [Tooltip("Coordinate space the value is applied in.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetEulerAngles(value, _space);
    }
}
