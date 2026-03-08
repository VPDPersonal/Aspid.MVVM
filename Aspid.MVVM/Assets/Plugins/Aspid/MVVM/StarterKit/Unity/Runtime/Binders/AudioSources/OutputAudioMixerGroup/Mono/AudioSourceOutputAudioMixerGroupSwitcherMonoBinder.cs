using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that switches the <see cref="AudioSource.outputAudioMixerGroup"/> property on an <see cref="AudioSource"/>
    /// between two values based on a bound boolean ViewModel property.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – OutputAudioMixerGroup Switcher")]
    public sealed class AudioSourceOutputAudioMixerGroupSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, AudioMixerGroup>
    {
        protected override void SetValue(AudioMixerGroup value) =>
            CachedComponent.outputAudioMixerGroup = value;
    }
}