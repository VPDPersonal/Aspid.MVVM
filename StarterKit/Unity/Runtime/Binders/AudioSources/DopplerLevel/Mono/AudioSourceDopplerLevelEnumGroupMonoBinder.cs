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
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, float value) =>
            element.dopplerLevel = value;
    }
}