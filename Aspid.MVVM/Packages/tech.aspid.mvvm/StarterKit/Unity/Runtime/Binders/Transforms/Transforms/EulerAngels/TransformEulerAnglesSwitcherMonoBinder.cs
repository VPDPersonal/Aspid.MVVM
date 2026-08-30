using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{Transform, Vector3}"/> that switches the <see cref="Transform.eulerAngles"/> or
    /// <see cref="Transform.localEulerAngles"/> between two values based on the bound boolean ViewModel value.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – EulerAngles Switcher")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation", SubPath = "Switcher")]
    public sealed class TransformEulerAnglesSwitcherMonoBinder : SwitcherMonoBinder<Transform, Vector3>
    {
        [Tooltip("The coordinate space in which the euler angles are applied.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Called when applying the selected value to the euler angles of the <see cref="Transform"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetEulerAngles(value, _space);
    }
}