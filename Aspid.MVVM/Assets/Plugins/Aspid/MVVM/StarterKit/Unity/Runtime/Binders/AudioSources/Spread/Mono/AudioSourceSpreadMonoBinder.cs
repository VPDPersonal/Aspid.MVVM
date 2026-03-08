using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.spread"/> property on an <see cref="AudioSource"/>
    /// when the bound ViewModel value changes. The value is clamped to the range [0, 360].
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
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