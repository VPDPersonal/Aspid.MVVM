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
    /// Assigning a controller rebinds the animator and resets its state machine — parameters keep their values, the
    /// state does not. A destroyed controller arrives as <see langword="null"/>.
    /// </remarks>
    [Serializable]
    public class AnimatorControllerBinder : TargetObjectBinder<Animator, RuntimeAnimatorController>
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public AnimatorControllerBinder(
            Animator target,
            IConverter<RuntimeAnimatorController?, RuntimeAnimatorController?>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override RuntimeAnimatorController? Property
        {
            get => Target.runtimeAnimatorController;
            set => Target.runtimeAnimatorController = value;
        }
    }
}
