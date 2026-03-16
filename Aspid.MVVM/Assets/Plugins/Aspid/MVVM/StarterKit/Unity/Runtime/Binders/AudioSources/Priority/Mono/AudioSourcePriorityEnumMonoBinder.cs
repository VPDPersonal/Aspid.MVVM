using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumIntMonoBinder{AudioSource}"/> that sets the <see cref="AudioSource.priority"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 256] before being applied to <see cref="AudioSource.priority"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority Enum")]
    public sealed class AudioSourcePriorityEnumMonoBinder : EnumIntMonoBinder<AudioSource>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value.
        /// Sets <see cref="AudioSource.priority"/> clamped to the valid range of 0 to 256.
        /// </summary>
        protected override void SetValue(int value) =>
            CachedComponent.priority = Mathf.Clamp(value, min: 0, max: 256);
    }
}