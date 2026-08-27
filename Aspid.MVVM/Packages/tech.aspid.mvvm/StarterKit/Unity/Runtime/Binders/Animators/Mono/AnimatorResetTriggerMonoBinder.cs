using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="AnimatorTriggerMonoBinder"/> that resets the trigger parameter.
    /// </summary>
    /// <remarks>
    /// A trigger that was set and never consumed stays armed, firing the moment its state becomes reachable —
    /// often much later, in a state nobody connected to it.
    /// </remarks>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Reset Trigger")]
    public class AnimatorResetTriggerMonoBinder : AnimatorTriggerMonoBinder
    {
        /// <inheritdoc/>
        protected override void Apply(string triggerName) =>
            CachedComponent.ResetTrigger(triggerName);
    }
}
