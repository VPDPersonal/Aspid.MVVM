using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="AudioSource.timeSamples"/>.
    /// </summary>
    /// <remarks>
    /// The position is kept inside the current clip; without a clip the write is skipped.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – TimeSamples Switcher")]
    public sealed class AudioSourceTimeSamplesSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, int>
    {
        /// <inheritdoc/>
        protected override void SetValue(int value) =>
            CachedComponent.SetTimeSamples(value);
    }
}
