using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="AudioSource.bypassEffects"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "BypassEffects", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – BypassEffects Enum")]
    public sealed class AudioSourceBypassEffectsEnumMonoBinder : EnumMonoBinder<AudioSource, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.bypassEffects = value;
    }
}
