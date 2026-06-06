using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupFloatMonoBinder{AudioSource}"/> that sets the <see cref="AudioSource.reverbZoneMix"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1.1] before being applied to <see cref="AudioSource.reverbZoneMix"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – ReverbZoneMix EnumGroup")]
    public sealed class AudioSourceReverbZoneMixEnumGroupMonoBinder : EnumGroupFloatMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets <see cref="AudioSource.reverbZoneMix"/> clamped to the valid range of 0 to 1.1.
        /// </summary>
        protected override void SetValue(AudioSource element, float value) =>
            element.reverbZoneMix = Mathf.Clamp(value, min: 0, max: 1.1f);
    }
}