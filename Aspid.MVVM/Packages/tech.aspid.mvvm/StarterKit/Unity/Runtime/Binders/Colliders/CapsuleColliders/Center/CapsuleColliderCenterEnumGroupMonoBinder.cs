using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{CapsuleCollider, Vector3}"/> that sets the <see cref="CapsuleCollider.center"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Center EnumGroup")]
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Center", SubPath = "EnumGroup")]
    public sealed class CapsuleColliderCenterEnumGroupMonoBinder : EnumGroupMonoBinder<CapsuleCollider, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(CapsuleCollider element, Vector3 value) =>
            element.center = value;
    }
}