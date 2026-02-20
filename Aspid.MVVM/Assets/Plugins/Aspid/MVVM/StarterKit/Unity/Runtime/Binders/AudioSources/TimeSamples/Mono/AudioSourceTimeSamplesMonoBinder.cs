using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – TimeSamples")]
    public class AudioSourceTimeSamplesMonoBinder : ComponentIntMonoBinder<AudioSource>
    {
        protected sealed override int Property
        {
            get => CachedComponent.timeSamples;
            set => CachedComponent.timeSamples = value;
        }
    }
}