using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{AudioSource, AudioClip}"/> that switches the <see cref="AudioSource.clip"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Clip Switcher")]
    public sealed class AudioSourceClipSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, AudioClip>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioClip value) =>
            CachedComponent.clip = value;
    }
}