using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumFloatMonoBinder{AudioSource}"/> that sets the <see cref="AudioSource.time"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Time Enum")]
    public sealed class AudioSourceTimeEnumMonoBinder : EnumFloatMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets <see cref="AudioSource.time"/> to the resolved value.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.time = value;
    }
}