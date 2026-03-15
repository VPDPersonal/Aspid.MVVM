using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Collider}"/> that binds the <see cref="Collider.enabled"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current enabled value
    /// is sent back to the ViewModel.
    /// </remarks>
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