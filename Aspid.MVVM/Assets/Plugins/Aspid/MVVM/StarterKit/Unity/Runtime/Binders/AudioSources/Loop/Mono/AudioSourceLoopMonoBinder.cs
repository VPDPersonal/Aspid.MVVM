using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Loop")]
    public class AudioSourceLoopMonoBinder : ComponentBoolMonoBinder<AudioSource>
    {
        protected sealed override bool Property
        {
            get => CachedComponent.loop;
            set => CachedComponent.loop = value;
        }
    }
}