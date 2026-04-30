using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumFloatMonoBinder{AudioSource}"/> that sets the <see cref="AudioSource.pitch"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [−3, 3] before being applied to <see cref="AudioSource.pitch"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Pitch Enum")]
    public sealed class AudioSourcePitchEnumMonoBinder : EnumFloatMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value.
        /// Sets <see cref="AudioSource.pitch"/> clamped to the valid range of −3 to 3.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.pitch = Mathf.Clamp(value, min: -3, max: 3);
    }
}