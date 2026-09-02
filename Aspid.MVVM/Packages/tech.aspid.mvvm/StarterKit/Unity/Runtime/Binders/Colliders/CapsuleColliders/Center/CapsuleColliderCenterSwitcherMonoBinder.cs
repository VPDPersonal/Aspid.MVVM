using UnityEngine;
// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{CapsuleCollider, Vector3}"/> that switches the <see cref="CapsuleCollider.center"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Center Switcher")]
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Center", SubPath = "Switcher")]
    public sealed class CapsuleColliderCenterSwitcherMonoBinder : SwitcherMonoBinder<CapsuleCollider, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.center = value;
    }
}