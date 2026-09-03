using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="AudioSource.outputAudioMixerGroup"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "OutputAudioMixerGroup", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – OutputAudioMixerGroup Enum")]
    public sealed class AudioSourceOutputAudioMixerGroupEnumMonoBinder : EnumMonoBinder<AudioSource, AudioMixerGroup>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioMixerGroup value) =>
            CachedComponent.outputAudioMixerGroup = value;
    }
}
