using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.outputAudioMixerGroup"/> property on an <see cref="AudioSource"/>
    /// to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – OutputAudioMixerGroup Enum")]
    public sealed class AudioSourceOutputAudioMixerGroupEnumMonoBinder : EnumMonoBinder<AudioSource, AudioMixerGroup>
    {
        protected override void SetValue(AudioMixerGroup value) =>
            CachedComponent.outputAudioMixerGroup = value;
    }
}