using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumVector3MonoBinder{SphereCollider}"/> that sets the <see cref="SphereCollider.center"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Center Enum")]
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Center", SubPath = "Enum")]
    public sealed class SphereColliderCenterEnumMonoBinder : EnumVector3MonoBinder<SphereCollider>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.center = value;
    }
}