using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="CapsuleCollider.center"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Center", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Center EnumGroup")]
    public sealed class CapsuleColliderCenterEnumGroupMonoBinder : EnumGroupMonoBinder<CapsuleCollider, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(CapsuleCollider element, Vector3 value)
        {
            if (this.RequireFinite(value))
                element.center = value;
        }
    }
}
