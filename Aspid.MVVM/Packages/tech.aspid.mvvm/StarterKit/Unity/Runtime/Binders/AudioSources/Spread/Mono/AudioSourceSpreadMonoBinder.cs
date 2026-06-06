using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{AudioSource}"/> that binds the <see cref="AudioSource.spread"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current spread value
    /// is sent back to the ViewModel.
    /// The bound value is clamped to [0, 360] before being applied to <see cref="AudioSource.spread"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Spread")]
    public class AudioSourceSpreadMonoBinder : ComponentFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.spread;
            set => CachedComponent.spread = value;
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.spread"/> property.
        /// Clamps the converted value to the valid range of 0 to 360.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 360);
    }
}