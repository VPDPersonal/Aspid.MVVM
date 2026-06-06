using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatMonoBinder{AudioSource}"/> that switches the <see cref="AudioSource.spatialBlend"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="AudioSource.spatialBlend"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – SpatialBlend Switcher")]
    public sealed class AudioSourceSpatialBlendSwitcherMonoBinder : SwitcherFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.spatialBlend = value;

        /// <summary>
        /// Called when converting the selected value before applying it to the <see cref="AudioSource.spatialBlend"/> property.
        /// Clamps the converted value to the valid range of 0 to 1.
        /// </summary>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 1);
    }
}