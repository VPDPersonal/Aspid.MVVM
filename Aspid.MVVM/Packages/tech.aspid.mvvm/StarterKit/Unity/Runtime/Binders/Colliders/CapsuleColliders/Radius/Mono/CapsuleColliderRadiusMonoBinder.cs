using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{CapsuleCollider}"/> that binds the <see cref="CapsuleCollider.radius"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current radius value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Radius")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Radius")]
    public class CapsuleColliderRadiusMonoBinder : ComponentFloatMonoBinder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.radius;
            set => CachedComponent.radius = value;
        }
    }
}