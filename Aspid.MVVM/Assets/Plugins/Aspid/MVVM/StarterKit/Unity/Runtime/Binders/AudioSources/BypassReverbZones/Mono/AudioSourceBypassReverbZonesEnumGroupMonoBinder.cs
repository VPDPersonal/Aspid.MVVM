using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – BypassReverbZones EnumGroup")]
    public sealed class AudioSourceBypassReverbZonesEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, bool>
    {
        protected override void SetValue(AudioSource element, bool value) =>
            element.bypassReverbZones = value;
    }
}