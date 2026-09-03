using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.outputAudioMixerGroup"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "OutputAudioMixerGroup", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – OutputAudioMixerGroup EnumGroup")]
    public sealed class AudioSourceOutputAudioMixerGroupEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, AudioMixerGroup>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, AudioMixerGroup value) =>
            element.outputAudioMixerGroup = value;
    }
}
