#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorTriggerBinder"/> that calls <see cref="Animator.SetTrigger(string)"/>.
    /// </summary>
    [Serializable]
    public class AnimatorSetTriggerBinder : AnimatorTriggerBinder
    {
        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected AnimatorSetTriggerBinder() { }

        /// <param name="target">The animator to bind.</param>
        /// <param name="triggerName">The trigger parameter.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="triggerName"/> is <see langword="null"/>.
        /// </exception>
        public AnimatorSetTriggerBinder(Animator target, string triggerName)
            : base(target, triggerName) { }

        /// <inheritdoc/>
        protected override void Apply(string triggerName) =>
            Target.SetTrigger(triggerName);
    }
}
