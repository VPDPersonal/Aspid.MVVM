using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatMonoBinder{AudioSource}"/> that switches the <see cref="AudioSource.pitch"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [−3, 3] before being applied to <see cref="AudioSource.pitch"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Pitch Switcher")]
    public sealed class AudioSourcePitchSwitcherMonoBinder : SwitcherFloatMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when applying the selected value to the <see cref="AudioSource.pitch"/> property.
        /// Clamps the value to the valid range of −3 to 3.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.pitch = Mathf.Clamp(value, min: -3, max: 3);
    }
}