using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent,T}">SwitcherMonoBinder&lt;AudioSource, float&gt;</see> that switches the <see cref="AudioSource.volume"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="AudioSource.volume"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Volume Switcher")]
    public sealed class AudioSourceVolumeSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, float>
    {
        /// <summary>
        /// Called when applying the selected value to the <see cref="AudioSource.volume"/> property.
        /// Clamps the value to the valid range of 0 to 1.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value) =>
            CachedComponent.volume = this.SafeClamp(value, 0, 1);
    }
}