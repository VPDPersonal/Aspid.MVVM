using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatMonoBinder{AudioSource}"/> that switches the <see cref="AudioSource.volume"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="AudioSource.volume"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Volume Switcher")]
    public sealed class AudioSourceVolumeSwitcherMonoBinder : SwitcherFloatMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when applying the selected value to the <see cref="AudioSource.volume"/> property.
        /// Clamps the value to the valid range of 0 to 1.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.volume = Mathf.Clamp(value, min: 0, max: 1);
    }
}