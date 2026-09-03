using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="SphereCollider.center"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Center", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Center EnumGroup")]
    public sealed class SphereColliderCenterEnumGroupMonoBinder : EnumGroupMonoBinder<SphereCollider, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(SphereCollider element, Vector3 value)
        {
            if (this.RequireFinite(value))
                element.center = value;
        }
    }
}
