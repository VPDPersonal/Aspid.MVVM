using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="AudioSource.reverbZoneMix"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..1.1.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – ReverbZoneMix Switcher")]
    public sealed class AudioSourceReverbZoneMixSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.reverbZoneMix = this.SafeClamp(value, 0f, 1.1f);
    }
}
