using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherIntMonoBinder{AudioSource}"/> that switches the <see cref="AudioSource.priority"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 256] before being applied to <see cref="AudioSource.priority"/>.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority Switcher")]
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "m_Priority", SubPath = "Switcher")]
    public sealed class AudioSourcePrioritySwitcherMonoBinder : SwitcherIntMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when applying the selected value to the <see cref="AudioSource.priority"/> property.
        /// Clamps the value to the valid range of 0 to 256.
        /// </summary>
        protected override void SetValue(int value) =>
            CachedComponent.priority = Mathf.Clamp(value, min: 0, max: 256);
    }
}