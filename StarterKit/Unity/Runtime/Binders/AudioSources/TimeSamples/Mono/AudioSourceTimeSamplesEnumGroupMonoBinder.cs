using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupIntMonoBinder{AudioSource}"/> that sets the <see cref="AudioSource.timeSamples"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – TimeSamples EnumGroup")]
    public sealed class AudioSourceTimeSamplesEnumGroupMonoBinder : EnumGroupIntMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets <see cref="AudioSource.timeSamples"/> to the resolved value.
        /// </summary>
        protected override void SetValue(AudioSource element, int value) =>
            element.timeSamples = value;
    }
}