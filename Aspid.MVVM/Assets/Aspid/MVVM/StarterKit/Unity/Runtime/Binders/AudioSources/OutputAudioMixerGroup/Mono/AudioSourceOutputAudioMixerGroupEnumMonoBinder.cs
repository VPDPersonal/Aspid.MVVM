using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{AudioSource, AudioMixerGroup}"/> that sets the <see cref="AudioSource.outputAudioMixerGroup"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – OutputAudioMixerGroup Enum")]
    public sealed class AudioSourceOutputAudioMixerGroupEnumMonoBinder : EnumMonoBinder<AudioSource, AudioMixerGroup>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioMixerGroup value) =>
            CachedComponent.outputAudioMixerGroup = value;
    }
}