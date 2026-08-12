using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{AudioSource}"/> implementing
    /// <see cref="IBinder{T}">IBinder&lt;AudioClip&gt;</see> that plays each clip the ViewModel publishes, once.
    /// </summary>
    /// <remarks>
    /// The playback binders start and stop the clip a source is configured with. This one takes the clip from the
    /// ViewModel instead, which is what a sound per event needs — a hit, a purchase, a level-up — and it does not
    /// interrupt whatever the source is already playing, because <see cref="AudioSource.PlayOneShot(AudioClip, float)"/>
    /// mixes rather than replaces.
    /// <para/>
    /// A <see langword="null"/> clip does nothing, so a ViewModel field that starts empty — and one that is cleared
    /// after a sound has been requested — is silent rather than an error. A destroyed clip is treated the same way.
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
            CachedComponent.PlayOneShot(value, BinderMath.SafeClamp01(_volumeScale));
        }
    }
}
