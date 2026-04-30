using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherVector3MonoBinder{Transform}"/> that switches the <see cref="Transform.eulerAngles"/> or
    /// <see cref="Transform.localEulerAngles"/> between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – EulerAngles Switcher")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation", SubPath = "Switcher")]
    public sealed class TransformEulerAnglesSwitcherMonoBinder : SwitcherVector3MonoBinder<Transform>
    {
        [Tooltip("The coordinate space in which the euler angles are applied.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Called when applying the selected value to the euler angles of the <see cref="Transform"/>.
        /// </summary>
        protected override void SetValue(Vector3 value) =>
            transform.SetEulerAngles(value, _space);
    }
}