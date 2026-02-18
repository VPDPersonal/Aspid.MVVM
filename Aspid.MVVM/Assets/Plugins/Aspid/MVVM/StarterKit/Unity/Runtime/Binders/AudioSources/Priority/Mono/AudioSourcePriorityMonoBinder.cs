using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority")]
    public class AudioSourcePriorityMonoBinder : ComponentIntMonoBinder<AudioSource>
    {
        protected sealed override int Property
        {
            get => CachedComponent.priority;
            set => CachedComponent.priority = value;
        }
        
        protected override int GetConvertedValue(int value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 256);
    }
}