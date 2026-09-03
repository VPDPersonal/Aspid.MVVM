using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="CapsuleCollider.center"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Center")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Center")]
    public class CapsuleColliderCenterMonoBinder : ComponentMonoBinder<CapsuleCollider, Vector3>, IVector3Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => CachedComponent.center;
            set
            {
                if (this.RequireFinite(value))
                    CachedComponent.center = value;
            }
        }
    }
}
