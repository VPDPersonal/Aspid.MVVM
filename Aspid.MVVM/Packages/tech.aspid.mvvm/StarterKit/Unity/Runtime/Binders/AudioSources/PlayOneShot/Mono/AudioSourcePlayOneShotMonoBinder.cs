using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{AudioSource}"/> implementing
    /// <see cref="IBinder{T}">IBinder&lt;AudioClip&gt;</see> that plays each clip the ViewModel publishes, once.
    /// </summary>
    /// <remarks>
    /// <see cref="AudioSource.PlayOneShot(AudioClip, float)"/> mixes over whatever the source is already playing
    /// instead of replacing it.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "m_audioClip")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource Binder – Play One Shot")]
    public partial class AudioSourcePlayOneShotMonoBinder : ComponentMonoBinder<AudioSource>, IBinder<AudioClip>
    {
        [Tooltip("Volume the clip is played at, as a fraction of the source's own volume.")]
        [SerializeField] [Range(0f, 1f)] private float _volumeScale = 1f;

        /// <summary>
        /// Plays <paramref name="value"/> once, mixed over whatever the source is already playing.
        /// </summary>
        /// <param name="value">The clip received from the ViewModel, or <see langword="null"/> to do nothing.</param>
        [BinderLog]
        public void SetValue(AudioClip value)
        {
            if (!value) return;
            CachedComponent.PlayOneShot(value, this.SafeClamp01(_volumeScale));
        }
    }
}
