using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{AudioSource, AudioClip}"/> that sets the <see cref="AudioSource.clip"/>
    /// property on each <see cref="AudioSource"/> in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Clip EnumGroup")]
    public sealed class AudioSourceClipEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, AudioClip>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, AudioClip value) =>
            element.clip = value;
    }
}