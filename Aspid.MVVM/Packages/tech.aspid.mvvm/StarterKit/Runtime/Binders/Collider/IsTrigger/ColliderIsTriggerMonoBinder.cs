using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Collider.isTrigger"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_IsTrigger")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – IsTrigger")]
    public class ColliderIsTriggerMonoBinder : ComponentMonoBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.isTrigger;
            set => CachedComponent.isTrigger = value;
        }
    }
}
