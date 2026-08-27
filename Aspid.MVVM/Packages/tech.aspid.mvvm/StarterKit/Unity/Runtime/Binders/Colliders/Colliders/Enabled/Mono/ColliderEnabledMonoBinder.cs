using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Collider}"/> that binds the <see cref="Collider.enabled"/> property.
    /// </summary>
    [AddBinderContextMenu(typeof(Collider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Enabled")]
    public class ColliderEnabledMonoBinder : ComponentBoolMonoBinder<Collider>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.enabled;
            set => CachedComponent.enabled = value;
        }
    }
}