using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent,TValue}">EnumMonoBinder&lt;AudioSource, float&gt;</see> that sets the <see cref="AudioSource.reverbZoneMix"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1.1] before being applied to <see cref="AudioSource.reverbZoneMix"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – ReverbZoneMix Enum")]
    public sealed class AudioSourceReverbZoneMixEnumMonoBinder : EnumMonoBinder<AudioSource, float>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value.
        /// Sets <see cref="AudioSource.reverbZoneMix"/> clamped to the valid range of 0 to 1.1.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value) =>
            CachedComponent.reverbZoneMix = this.SafeClamp(value, 0, 1.1f);
    }
}