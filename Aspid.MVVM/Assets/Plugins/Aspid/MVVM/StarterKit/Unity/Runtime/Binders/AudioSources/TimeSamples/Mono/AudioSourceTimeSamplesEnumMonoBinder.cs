using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumIntMonoBinder{AudioSource}"/> that sets the <see cref="AudioSource.timeSamples"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – TimeSamples Enum")]
    public sealed class AudioSourceTimeSamplesEnumMonoBinder : EnumIntMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets <see cref="AudioSource.timeSamples"/> to the resolved value.
        /// </summary>
        protected override void SetValue(int value) =>
            CachedComponent.timeSamples = value;
    }
}