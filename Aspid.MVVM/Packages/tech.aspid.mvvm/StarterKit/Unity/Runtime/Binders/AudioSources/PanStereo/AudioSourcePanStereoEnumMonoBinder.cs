using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent,TValue}">EnumMonoBinder&lt;AudioSource, float&gt;</see> that sets the <see cref="AudioSource.panStereo"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [−1, 1] before being applied to <see cref="AudioSource.panStereo"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – PanStereo Enum")]
    public sealed class AudioSourcePanStereoEnumMonoBinder : EnumMonoBinder<AudioSource, float>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value.
        /// Sets <see cref="AudioSource.panStereo"/> clamped to the valid range of −1 to 1.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value) =>
            CachedComponent.panStereo = this.SafeClamp(value, -1, 1);
    }
}