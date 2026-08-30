using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Animator}"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that
    /// plays the animator state the ViewModel names.
    /// </summary>
    /// <remarks>
    /// A blank or <see langword="null"/> name does nothing. A name the controller does not have is Unity's own
    /// <see cref="Animator.Play(string)"/> to report; checking beforehand would mean walking every state of every
    /// layer on every value.
    /// </remarks>
    [GenerateSerializableBinder]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Play State")]
    public partial class AnimatorPlayStateMonoBinder : ComponentMonoBinder<Animator>, IBinder<string>
    {
        [Tooltip("Layer the state plays on. -1 = first layer with a matching state.")]
        [SerializeField] private int _layer = -1;

        [Tooltip("Where playback starts, as a fraction of the clip's length.")]
        [SerializeField] private float _normalizedTime;

        /// <summary>
        /// Plays the state named <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The state name received from the ViewModel, or <see langword="null"/> to do nothing.</param>
        [BinderLog]
        public void SetValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            CachedComponent.Play(value, _layer, this.SafeClamp01(_normalizedTime));
        }
    }
}
