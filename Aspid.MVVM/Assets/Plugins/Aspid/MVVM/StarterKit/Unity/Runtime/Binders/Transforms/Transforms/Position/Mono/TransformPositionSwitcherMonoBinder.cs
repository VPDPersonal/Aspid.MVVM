using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherVector3MonoBinder{Transform}"/> that switches the <see cref="Transform.position"/> or
    /// <see cref="Transform.localPosition"/> between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Position Switcher")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalPosition", SubPath = "Switcher")]
    public sealed class TransformPositionSwitcherMonoBinder : SwitcherVector3MonoBinder<Transform>
    {
        [Tooltip("The coordinate space in which the position is applied.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Called when applying the selected value to the position of the <see cref="Transform"/>.
        /// </summary>
        protected override void SetValue(Vector3 value) =>
            transform.SetPosition(value, _space);
    }
}