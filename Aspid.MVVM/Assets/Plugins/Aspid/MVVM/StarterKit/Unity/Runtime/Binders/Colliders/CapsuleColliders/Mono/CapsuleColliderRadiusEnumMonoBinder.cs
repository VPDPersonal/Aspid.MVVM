using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumFloatMonoBinder{CapsuleCollider}"/> that sets the <see cref="CapsuleCollider.radius"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Radius Enum")]
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Radius", SubPath = "Enum")]
    public sealed class CapsuleColliderRadiusEnumMonoBinder : EnumFloatMonoBinder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.radius = value;
    }
}