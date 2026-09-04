#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorTriggerBinder"/> that calls <see cref="Animator.ResetTrigger(string)"/>.
    /// </summary>
    /// <remarks>
    /// A trigger that was set and never consumed stays armed until reset.
    /// </remarks>
    [Serializable]
    public class AnimatorResetTriggerBinder : AnimatorTriggerBinder
    {
        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected AnimatorResetTriggerBinder() { }

        /// <param name="target">The animator to bind.</param>
        /// <param name="triggerName">The trigger parameter.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="triggerName"/> is <see langword="null"/>.
        /// </exception>
        public AnimatorResetTriggerBinder(Animator target, string triggerName)
            : base(target, triggerName) { }

        /// <inheritdoc/>
        protected override void Apply(string triggerName) =>
            Target.ResetTrigger(triggerName);
    }
}
