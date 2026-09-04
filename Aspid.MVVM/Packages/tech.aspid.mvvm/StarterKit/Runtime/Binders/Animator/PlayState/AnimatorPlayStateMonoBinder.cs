using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that plays the animator state named by the bound string.
    /// </summary>
    /// <remarks>
    /// A blank name does nothing; an unknown name is reported by <see cref="Animator.Play(string)"/> itself.
    /// </remarks>
    [GenerateSerializableBinder]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Play State")]
    public partial class AnimatorPlayStateMonoBinder : ComponentMonoBinder<Animator>, IBinder<string>
    {
        [Tooltip("Layer the state plays on; -1 is the first layer with a matching state.")]
        [SerializeField] [Min(-1)] private int _layer = -1;

        [Tooltip("Where playback starts, as a fraction of the clip length.")]
        [SerializeField] [Range(0f, 1f)] private float _normalizedTime;

        /// <summary>
        /// Plays the state named <paramref name="value"/>; a blank name does nothing.
        /// </summary>
        /// <param name="value">The state name received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            CachedComponent.Play(value, _layer, _normalizedTime);
        }
    }
}
