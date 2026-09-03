using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.bypassEffects"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "BypassEffects", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – BypassEffects EnumGroup")]
    public sealed class AudioSourceBypassEffectsEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, bool value) =>
            element.bypassEffects = value;
    }
}
