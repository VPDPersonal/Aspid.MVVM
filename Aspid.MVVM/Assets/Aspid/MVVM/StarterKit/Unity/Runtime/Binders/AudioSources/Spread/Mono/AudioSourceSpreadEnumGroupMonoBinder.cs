using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupFloatMonoBinder{AudioSource}"/> that sets the <see cref="AudioSource.spread"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 360] before being applied to <see cref="AudioSource.spread"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Spread EnumGroup")]
    public sealed class AudioSourceSpreadEnumGroupMonoBinder : EnumGroupFloatMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets <see cref="AudioSource.spread"/> clamped to the valid range of 0 to 360.
        /// </summary>
        protected override void SetValue(AudioSource element, float value) =>
            element.spread = Mathf.Clamp(value, min: 0, max: 360f);
    }
}