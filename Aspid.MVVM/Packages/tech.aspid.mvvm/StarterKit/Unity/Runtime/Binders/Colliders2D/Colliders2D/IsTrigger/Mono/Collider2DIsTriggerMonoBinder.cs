using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Collider2D}"/> that binds <see cref="Collider2D.isTrigger"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Collider2D), serializePropertyNames: "m_IsTrigger")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider2D/Collider2D Binder – Is Trigger")]
    public class Collider2DIsTriggerMonoBinder : ComponentBoolMonoBinder<Collider2D>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.isTrigger;
            set => CachedComponent.isTrigger = value;
        }
    }
}
