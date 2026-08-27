using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupFloatMonoBinder{AudioSource}"/> that sets the <see cref="AudioSource.dopplerLevel"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – DopplerLevel EnumGroup")]
    public sealed class AudioSourceDopplerLevelEnumGroupMonoBinder : EnumGroupFloatMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets <see cref="AudioSource.dopplerLevel"/> clamped to the valid range of 0 to 5.
        /// </summary>
        /// <param name="element">The component this entry of the group writes to.</param>
        /// <param name="value">The value the bound enum resolved to for this element.</param>
        protected override void SetValue(AudioSource element, float value) =>
            element.dopplerLevel = BinderMath.SafeClamp(value, 0, 5);
    }
}