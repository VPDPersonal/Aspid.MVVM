using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.bypassReverbZones"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "BypassReverbZones", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – BypassReverbZones EnumGroup")]
    public sealed class AudioSourceBypassReverbZonesEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, bool value) =>
            element.bypassReverbZones = value;
    }
}
