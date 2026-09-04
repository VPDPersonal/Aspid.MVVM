using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="Behaviour"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Behaviour))]
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour To Source Binder")]
    public sealed class BehaviourToSourceMonoBinder : ComponentToSourceMonoBinder<Behaviour>
    {
        /// <inheritdoc/>
        protected override Behaviour ResolveComponent() =>
            gameObject.GetFirstNonBinderBehaviour();
    }
}
