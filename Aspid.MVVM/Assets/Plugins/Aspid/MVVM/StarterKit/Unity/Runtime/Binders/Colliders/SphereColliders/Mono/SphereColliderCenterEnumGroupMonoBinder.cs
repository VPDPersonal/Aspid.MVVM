using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupVector3MonoBinder{SphereCollider}"/> that sets the <see cref="SphereCollider.center"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Center EnumGroup")]
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Center", SubPath = "EnumGroup")]
    public sealed class SphereColliderCenterEnumGroupMonoBinder : EnumGroupVector3MonoBinder<SphereCollider>
    {
        /// <inheritdoc/>
        protected override void SetValue(SphereCollider element, Vector3 value) =>
            element.center = value;
    }
}