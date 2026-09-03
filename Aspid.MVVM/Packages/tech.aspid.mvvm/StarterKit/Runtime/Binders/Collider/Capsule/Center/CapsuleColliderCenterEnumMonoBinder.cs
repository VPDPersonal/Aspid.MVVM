using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="CapsuleCollider.center"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Center", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Center Enum")]
    public sealed class CapsuleColliderCenterEnumMonoBinder : EnumMonoBinder<CapsuleCollider, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector3 value)
        {
            if (this.RequireFinite(value))
                CachedComponent.center = value;
        }
    }
}
