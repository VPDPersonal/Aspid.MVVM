using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherIntMonoBinder{AudioSource}"/> that switches the <see cref="AudioSource.timeSamples"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – TimeSamples Switcher")]
    public sealed class AudioSourceTimeSamplesSwitcherMonoBinder : SwitcherIntMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected override void SetValue(int value) =>
            CachedComponent.timeSamples = value;
    }
}