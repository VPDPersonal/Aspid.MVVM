using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Animator}"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that
    /// plays the animator state the ViewModel names.
    /// </summary>
    /// <remarks>
    /// The domain could set the parameters a controller reads and could not tell it to play something. Naming a state
    /// directly is how a cutscene, a reaction or a one-off flourish is triggered without inventing a parameter and a
    /// transition for it.
    /// <para/>
    /// A blank or <see langword="null"/> name does nothing, so a ViewModel field that starts empty does not make the
    /// animator jump. A name the controller does not have is Unity's to report — <see cref="Animator.Play(string)"/>
    /// logs it itself, and checking beforehand would mean walking every state of every layer on every value.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Play State")]
    public partial class AnimatorPlayStateMonoBinder : ComponentMonoBinder<Animator>, IBinder<string>
    {
        [Tooltip("Layer the state is played on. -1 plays it on the first layer that has a state of that name.")]
        [SerializeField] private int _layer = -1;

        [Tooltip("Where in the clip playback starts, as a fraction of its length. Leave at zero to play from the beginning.")]
        [SerializeField] private float _normalizedTime;

        /// <summary>
        /// Plays the state named <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The state name received from the ViewModel, or <see langword="null"/> to do nothing.</param>
        [BinderLog]
        public void SetValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            CachedComponent.Play(value, _layer, BinderMath.SafeClamp01(_normalizedTime));
        }
    }
}
