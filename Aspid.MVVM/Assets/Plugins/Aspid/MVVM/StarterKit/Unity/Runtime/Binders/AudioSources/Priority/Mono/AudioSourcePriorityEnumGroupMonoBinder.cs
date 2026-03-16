using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupIntMonoBinder{AudioSource}"/> that sets the <see cref="AudioSource.priority"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 256] before being applied to <see cref="AudioSource.priority"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority EnumGroup")]
    public sealed class AudioSourcePriorityEnumGroupMonoBinder : EnumGroupIntMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets <see cref="AudioSource.priority"/> clamped to the valid range of 0 to 256.
        /// </summary>
        protected override void SetValue(AudioSource element, int value) =>
            element.priority = Mathf.Clamp(value, min: 0, max: 256);
    }
}