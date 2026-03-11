using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Behaviour}"/> that binds the <see cref="Behaviour.enabled"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current enabled value
    /// is sent back to the ViewModel. Supports optional value inversion.
    /// </remarks>
    [AddBinderContextMenu(typeof(Behaviour))]
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour Binder – Enabled")]
    public class BehaviourEnabledMonoBinder : ComponentBoolMonoBinder<Behaviour>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.enabled;
            set => CachedComponent.enabled = value;
        }
    }
}