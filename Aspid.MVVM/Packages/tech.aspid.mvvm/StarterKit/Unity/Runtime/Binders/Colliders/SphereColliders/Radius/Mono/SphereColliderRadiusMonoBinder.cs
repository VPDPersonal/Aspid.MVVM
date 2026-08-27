using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{SphereCollider}"/> that binds the <see cref="SphereCollider.radius"/> property.
    /// </summary>
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Radius")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Radius")]
    public class SphereColliderRadiusMonoBinder : ComponentFloatMonoBinder<SphereCollider>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.radius;
            set => CachedComponent.radius = BinderMath.NonNegative(value);
        }
    }
}