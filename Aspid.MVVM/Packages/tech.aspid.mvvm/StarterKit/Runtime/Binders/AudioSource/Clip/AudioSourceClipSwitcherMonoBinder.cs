using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="AudioSource.clip"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "m_audioClip", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Clip Switcher")]
    public sealed class AudioSourceClipSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, AudioClip>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioClip value) =>
            CachedComponent.clip = value;
    }
}
