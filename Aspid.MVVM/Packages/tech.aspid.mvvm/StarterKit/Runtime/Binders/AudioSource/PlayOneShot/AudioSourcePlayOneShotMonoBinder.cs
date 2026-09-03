using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that plays each bound <see cref="AudioClip"/> once.
    /// </summary>
    /// <remarks>
    /// <see cref="AudioSource.PlayOneShot(AudioClip, float)"/> mixes over whatever the source is already playing.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Play One Shot")]
    public partial class AudioSourcePlayOneShotMonoBinder : ComponentMonoBinder<AudioSource>, IBinder<AudioClip>
    {
        [Tooltip("Volume scale of the clip, relative to the source volume.")]
        [SerializeField] [Range(0f, 1f)] private float _volumeScale = 1f;

        /// <summary>
        /// Plays <paramref name="value"/> once; <see langword="null"/> plays nothing.
        /// </summary>
        /// <param name="value">The clip received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(AudioClip value)
        {
            if (!value) return;
            CachedComponent.PlayOneShot(value, _volumeScale);
        }
    }
}
