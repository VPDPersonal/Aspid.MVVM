using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="AnimatorTriggerMonoBinder"/> that sets the trigger parameter.
    /// </summary>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Set Trigger")]
    public class AnimatorSetTriggerMonoBinder : AnimatorTriggerMonoBinder
    {
        /// <inheritdoc/>
        protected override void Apply(string triggerName) =>
            CachedComponent.SetTrigger(triggerName);
    }
}
