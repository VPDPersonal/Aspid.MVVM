using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{AudioSource}"/> that binds the <see cref="AudioSource.dopplerLevel"/> property.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – DopplerLevel")]
    public class AudioSourceDopplerLevelMonoBinder : ComponentFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.dopplerLevel;
            set => CachedComponent.dopplerLevel = value;
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.dopplerLevel"/> property.
        /// Replaces a non-finite converted value with <c>0</c>.
        /// </summary>
        /// <remarks>
        /// Unity clamps this property to its 0..5 range inside the setter, but lets <c>NaN</c> and infinities through.
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to keep that guard.
        /// </remarks>
        /// <param name="value">The value to convert.</param>
        protected override float GetConvertedValue(float value) =>
            this.SafeClamp(base.GetConvertedValue(value), 0, 5);
    }
}