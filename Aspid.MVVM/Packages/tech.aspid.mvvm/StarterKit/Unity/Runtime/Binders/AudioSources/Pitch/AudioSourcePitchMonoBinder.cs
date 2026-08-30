using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{AudioSource}"/> that binds the <see cref="AudioSource.pitch"/> property.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [−3, 3] before being applied to <see cref="AudioSource.pitch"/>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Pitch")]
    public class AudioSourcePitchMonoBinder : ComponentFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.pitch;
            set => CachedComponent.pitch = value;
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.pitch"/> property.
        /// Clamps the converted value to the valid range of −3 to 3.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        /// <param name="value">The value to convert.</param>
        protected override float GetConvertedValue(float value) =>
            this.SafeClamp(base.GetConvertedValue(value), -3, 3);
    }
}