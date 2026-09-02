using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent,TValue}">EnumMonoBinder&lt;AudioSource, int&gt;</see> that sets the <see cref="AudioSource.timeSamples"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – TimeSamples Enum")]
    public sealed class AudioSourceTimeSamplesEnumMonoBinder : EnumMonoBinder<AudioSource, int>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets <see cref="AudioSource.timeSamples"/> to the resolved value.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(int value) =>
            CachedComponent.SetTimeSamples(value);
    }
}