using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatMonoBinder{AudioSource}"/> that switches the <see cref="AudioSource.time"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Time Switcher")]
    public sealed class AudioSourceTimeSwitcherMonoBinder : SwitcherFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.time = value;
    }
}