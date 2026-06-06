using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{AudioSource, float, Converter}"/> that sets the <see cref="AudioSource.dopplerLevel"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – DopplerLevel Enum")]
    public sealed class AudioSourceDopplerLevelEnumMonoBinder : EnumFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.dopplerLevel = value;
    }
}