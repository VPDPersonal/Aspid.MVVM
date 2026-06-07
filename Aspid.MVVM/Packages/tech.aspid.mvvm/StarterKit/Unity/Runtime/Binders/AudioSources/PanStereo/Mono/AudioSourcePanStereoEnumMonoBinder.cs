using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumFloatMonoBinder{AudioSource}"/> that sets the <see cref="AudioSource.panStereo"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [−1, 1] before being applied to <see cref="AudioSource.panStereo"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – PanStereo Enum")]
    public sealed class AudioSourcePanStereoEnumMonoBinder : EnumFloatMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value.
        /// Sets <see cref="AudioSource.panStereo"/> clamped to the valid range of −1 to 1.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.panStereo = Mathf.Clamp(value, min: -1, max: 1);
    }
}