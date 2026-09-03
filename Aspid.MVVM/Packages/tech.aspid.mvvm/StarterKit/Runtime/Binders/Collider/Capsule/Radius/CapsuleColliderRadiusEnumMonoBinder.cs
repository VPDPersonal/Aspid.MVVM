using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="CapsuleCollider.radius"/>.
    /// </summary>
    /// <remarks>
    /// A negative value is raised to zero.
    /// </remarks>
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Radius", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Radius Enum")]
    public sealed class CapsuleColliderRadiusEnumMonoBinder : EnumMonoBinder<CapsuleCollider, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.radius = this.NonNegative(value);
    }
}
