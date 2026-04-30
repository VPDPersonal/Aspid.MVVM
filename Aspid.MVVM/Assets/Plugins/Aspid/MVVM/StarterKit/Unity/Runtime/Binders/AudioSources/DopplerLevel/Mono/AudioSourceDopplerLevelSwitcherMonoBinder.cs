using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatMonoBinder{AudioSource}"/> that switches the <see cref="AudioSource.dopplerLevel"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – DopplerLevel Switcher")]
    public sealed class AudioSourceDopplerLevelSwitcherMonoBinder : SwitcherFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.dopplerLevel = value;
    }
}