using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumVector3MonoBinder{CapsuleCollider}"/> that sets the <see cref="CapsuleCollider.center"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Center Enum")]
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Center", SubPath = "Enum")]
    public sealed class CapsuleColliderCenterEnumMonoBinder : EnumVector3MonoBinder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.center = value;
    }
}