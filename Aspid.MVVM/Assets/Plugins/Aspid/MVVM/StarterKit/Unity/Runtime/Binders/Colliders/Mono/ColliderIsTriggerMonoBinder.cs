using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Collider}"/> that binds the <see cref="Collider.isTrigger"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current convex value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – IsTrigger")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_IsTrigger")]
    public class ColliderIsTriggerMonoBinder : ComponentBoolMonoBinder<Collider>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.isTrigger;
            set => CachedComponent.isTrigger = value;
        }
    }
}