using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.bypassListenerEffects"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "BypassListenerEffects", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – BypassListenerEffects EnumGroup")]
    public sealed class AudioSourceBypassListenerEffectsEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, bool value) =>
            element.bypassListenerEffects = value;
    }
}
