#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetObjectBinder{T1, T2}">TargetObjectBinder&lt;Animator, RuntimeAnimatorController&gt;</see> that binds
    /// <see cref="Animator.runtimeAnimatorController"/>.
    /// </summary>
    /// <remarks>
    /// Swapping the whole controller is how one rig plays a different set of animations: a character that changes class
    /// or mount, a weapon that brings its own moves.
    /// <para/>
    /// Assigning a controller rebinds the animator and resets its state machine — parameters keep their values, the
    /// state does not. A destroyed controller arrives as <see langword="null"/>.
    /// </remarks>
    [Serializable]
    public class AnimatorControllerBinder : TargetObjectBinder<Animator, RuntimeAnimatorController>
    {
        /// <inheritdoc/>
        protected sealed override RuntimeAnimatorController? Property
        {
            get => Target.runtimeAnimatorController;
            set => Target.runtimeAnimatorController = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public AnimatorControllerBinder(Animator target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
