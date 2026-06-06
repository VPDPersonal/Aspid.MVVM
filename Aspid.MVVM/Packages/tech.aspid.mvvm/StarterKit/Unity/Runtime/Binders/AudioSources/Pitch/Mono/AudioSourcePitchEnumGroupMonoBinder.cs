using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupFloatMonoBinder{AudioSource}"/> that sets the <see cref="AudioSource.pitch"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [−3, 3] before being applied to <see cref="AudioSource.pitch"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Pitch EnumGroup")]
    public sealed class AudioSourcePitchEnumGroupMonoBinder : EnumGroupFloatMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets <see cref="AudioSource.pitch"/> clamped to the valid range of −3 to 3.
        /// </summary>
        protected override void SetValue(AudioSource element, float value) =>
            element.pitch = Mathf.Clamp(value, min: -3, max: 3);
    }
}