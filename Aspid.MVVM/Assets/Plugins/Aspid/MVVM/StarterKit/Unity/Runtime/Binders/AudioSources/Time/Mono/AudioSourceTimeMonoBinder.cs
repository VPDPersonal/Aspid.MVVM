using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Time")]
    public class AudioSourceTimeMonoBinder : ComponentFloatMonoBinder<AudioSource> 
    {
        protected sealed override float Property
        {
            get => CachedComponent.time;
            set => CachedComponent.time = value;
        }
    }
}