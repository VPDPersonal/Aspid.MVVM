using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumFloatMonoBinder{SphereCollider}"/> that sets the <see cref="SphereCollider.radius"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Radius Enum")]
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Radius", SubPath = "Enum")]
    public sealed class SphereColliderRadiusEnumMonoBinder : EnumFloatMonoBinder<SphereCollider>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.radius = value;
    }
}