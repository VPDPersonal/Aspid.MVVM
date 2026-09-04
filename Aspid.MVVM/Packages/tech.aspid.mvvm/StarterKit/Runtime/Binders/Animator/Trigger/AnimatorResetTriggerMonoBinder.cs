using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorTriggerMonoBinder"/> that calls <see cref="Animator.ResetTrigger(string)"/>.
    /// </summary>
    /// <remarks>
    /// A trigger that was set and never consumed stays armed until reset.
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
