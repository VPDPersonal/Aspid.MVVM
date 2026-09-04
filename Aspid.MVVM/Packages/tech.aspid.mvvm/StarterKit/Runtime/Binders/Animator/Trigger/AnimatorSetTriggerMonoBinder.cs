using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorTriggerMonoBinder"/> that calls <see cref="Animator.SetTrigger(string)"/>.
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
