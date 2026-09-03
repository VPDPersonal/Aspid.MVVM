using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="SphereCollider.center"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Center", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Center Enum")]
    public sealed class SphereColliderCenterEnumMonoBinder : EnumMonoBinder<SphereCollider, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector3 value)
        {
            if (this.RequireFinite(value))
                CachedComponent.center = value;
        }
    }
}
