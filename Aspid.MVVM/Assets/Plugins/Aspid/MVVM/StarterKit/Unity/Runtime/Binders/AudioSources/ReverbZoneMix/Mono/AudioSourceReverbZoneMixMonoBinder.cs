using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – ReverbZoneMix")]
    public class AudioSourceReverbZoneMixMonoBinder : ComponentFloatMonoBinder<AudioSource>
    {
        protected sealed override float Property
        {
            get => CachedComponent.reverbZoneMix;
            set => CachedComponent.reverbZoneMix = value;
        }

        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 1.1f);
    }
}