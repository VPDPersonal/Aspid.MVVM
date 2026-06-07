using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupFloatMonoBinder{AudioSource}"/> that sets the <see cref="AudioSource.panStereo"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [−1, 1] before being applied to <see cref="AudioSource.panStereo"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – PanStereo EnumGroup")]
    public sealed class AudioSourcePanStereoEnumGroupMonoBinder : EnumGroupFloatMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets <see cref="AudioSource.panStereo"/> clamped to the valid range of −1 to 1.
        /// </summary>
        protected override void SetValue(AudioSource element, float value) =>
            element.panStereo = Mathf.Clamp(value, min: -1, max: 1);
    }
}