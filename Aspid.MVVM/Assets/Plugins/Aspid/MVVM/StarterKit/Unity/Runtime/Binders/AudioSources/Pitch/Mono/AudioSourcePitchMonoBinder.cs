using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.pitch"/> property on an <see cref="AudioSource"/>
    /// when the bound ViewModel value changes. The value is clamped to the range [-3, 3].
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Pitch")]
    public class AudioSourcePitchMonoBinder : ComponentFloatMonoBinder<AudioSource>
    {
        protected sealed override float Property
        {
            get => CachedComponent.pitch;
            set => CachedComponent.pitch = value;
        }

        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: -3, max: 3);
    }
}