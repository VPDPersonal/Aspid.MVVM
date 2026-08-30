using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent,T}">SwitcherMonoBinder&lt;AudioSource, float&gt;</see> that switches the <see cref="AudioSource.dopplerLevel"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – DopplerLevel Switcher")]
    public sealed class AudioSourceDopplerLevelSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, float>
    {
        /// <summary>
        /// Called when applying the selected value to the <see cref="AudioSource.dopplerLevel"/> property.
        /// Clamps the value to the valid range of 0 to 5.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value) =>
            CachedComponent.dopplerLevel = this.SafeClamp(value, 0, 5);
    }
}