using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.timeSamples"/> property on an <see cref="AudioSource"/>
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
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