using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="Transform.eulerAngles"/> or
    /// <see cref="Transform.localEulerAngles"/>.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – EulerAngles Switcher")]
    public sealed class TransformEulerAnglesSwitcherMonoBinder : SwitcherMonoBinder<Transform, Vector3>
    {
        [Tooltip("Coordinate space the value is applied in.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetEulerAngles(value, _space);
    }
}
