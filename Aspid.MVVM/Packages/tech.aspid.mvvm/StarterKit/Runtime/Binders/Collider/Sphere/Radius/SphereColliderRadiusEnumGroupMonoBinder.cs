using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="SphereCollider.radius"/> on each element.
    /// </summary>
    /// <remarks>
    /// A negative value is raised to zero.
    /// </remarks>
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Radius", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Radius EnumGroup")]
    public sealed class SphereColliderRadiusEnumGroupMonoBinder : EnumGroupMonoBinder<SphereCollider, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(SphereCollider element, float value) =>
            element.radius = this.NonNegative(value);
    }
}
