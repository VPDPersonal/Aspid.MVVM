using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{AudioSource}"/> that binds the <see cref="AudioSource.volume"/> property.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="AudioSource.volume"/>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Volume")]
    public class AudioSourceVolumeMonoBinder : ComponentFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.volume;
            set => CachedComponent.volume = value;
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.volume"/> property.
        /// Clamps the converted value to the valid range of 0 to 1.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        /// <param name="value">The value to convert.</param>
        protected override float GetConvertedValue(float value) =>
            this.SafeClamp(base.GetConvertedValue(value), 0, 1);
    }
}