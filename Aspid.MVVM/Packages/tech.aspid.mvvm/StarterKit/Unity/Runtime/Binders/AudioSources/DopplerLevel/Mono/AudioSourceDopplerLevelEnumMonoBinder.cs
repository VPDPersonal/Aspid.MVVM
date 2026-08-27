using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumFloatMonoBinder{AudioSource}"/> that sets the <see cref="AudioSource.dopplerLevel"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – DopplerLevel Enum")]
    public sealed class AudioSourceDopplerLevelEnumMonoBinder : EnumFloatMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value.
        /// Sets <see cref="AudioSource.dopplerLevel"/> clamped to the valid range of 0 to 5.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value) =>
            CachedComponent.dopplerLevel = BinderMath.SafeClamp(value, 0, 5);
    }
}