using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupFloatMonoBinder{SphereCollider}"/> that sets the <see cref="SphereCollider.radius"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Radius EnumGroup")]
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Radius", SubPath = "EnumGroup")]
    public sealed class SphereColliderRadiusEnumGroupMonoBinder : EnumGroupFloatMonoBinder<SphereCollider>
    {
        /// <inheritdoc/>
        protected override void SetValue(SphereCollider element, float value) =>
            element.radius = value;
    }
}