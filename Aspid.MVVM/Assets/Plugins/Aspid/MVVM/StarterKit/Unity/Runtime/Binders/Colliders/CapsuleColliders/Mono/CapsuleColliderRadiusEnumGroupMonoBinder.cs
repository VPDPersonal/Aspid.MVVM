using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupFloatMonoBinder{CapsuleCollider}"/> that sets the <see cref="CapsuleCollider.radius"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Radius EnumGroup")]
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Radius", SubPath = "EnumGroup")]
    public sealed class CapsuleColliderRadiusEnumGroupMonoBinder : EnumGroupFloatMonoBinder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected override void SetValue(CapsuleCollider element, float value) =>
            element.radius = value;
    }
}