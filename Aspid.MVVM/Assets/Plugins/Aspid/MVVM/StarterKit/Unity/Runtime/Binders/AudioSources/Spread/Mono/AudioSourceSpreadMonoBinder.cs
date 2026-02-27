using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Spread")]
    public class AudioSourceSpreadMonoBinder : ComponentFloatMonoBinder<AudioSource>
    {
        protected sealed override float Property
        {
            get => CachedComponent.spread;
            set => CachedComponent.spread = value;
        }
        
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 360);
    }
}