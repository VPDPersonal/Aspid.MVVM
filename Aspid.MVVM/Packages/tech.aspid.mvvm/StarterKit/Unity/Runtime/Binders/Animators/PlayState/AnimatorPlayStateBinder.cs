#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Animator}"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that plays
    /// the animator state the ViewModel names.
    /// </summary>
    /// <remarks>
    /// Naming a state directly is how a cutscene, a reaction or a one-off flourish is triggered without inventing a
    /// parameter and a transition for it.
    /// <para/>
    /// A blank or <see langword="null"/> name does nothing, so a ViewModel field that starts empty does not make the
    /// animator jump.
    /// </remarks>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    public class AnimatorPlayStateBinder : TargetBinder<Animator>, IBinder<string>
    {
        [Tooltip("Layer the state is played on. -1 plays it on the first layer that has a state of that name.")]
        [SerializeField] private int _layer;

        [Tooltip("Where in the clip playback starts, as a fraction of its length. Leave at zero to play from the beginning.")]
        [SerializeField] private float _normalizedTime;

        /// <summary>
        /// Initializes a new instance of <see cref="AnimatorPlayStateBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Animator"/> to play states on.</param>
        /// <param name="layer">The layer the state is played on, or <c>-1</c> for the first layer that has it.</param>
        /// <param name="normalizedTime">Where in the clip playback starts, as a fraction of its length.</param>
        /// <param name="mode">The binding mode. Must be <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/> — playing a state has nothing to read back.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is neither <see cref="BindMode.OneWay"/> nor <see cref="BindMode.OneTime"/>.</exception>
        public AnimatorPlayStateBinder(
            Animator target,
            int layer = -1,
            float normalizedTime = 0f,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();

            _layer = layer;
            _normalizedTime = normalizedTime;
        }

        /// <summary>
        /// Plays the state named <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The state name received from the ViewModel, or <see langword="null"/> to do nothing.</param>
        public void SetValue(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            Target.Play(value, _layer, BinderMath.SafeClamp01(_normalizedTime));
        }
    }
}
