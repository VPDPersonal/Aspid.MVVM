using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="AudioSource.bypassReverbZones"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "BypassReverbZones", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – BypassReverbZones Enum")]
    public sealed class AudioSourceBypassReverbZonesEnumMonoBinder : EnumMonoBinder<AudioSource, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.bypassReverbZones = value;
    }
}
