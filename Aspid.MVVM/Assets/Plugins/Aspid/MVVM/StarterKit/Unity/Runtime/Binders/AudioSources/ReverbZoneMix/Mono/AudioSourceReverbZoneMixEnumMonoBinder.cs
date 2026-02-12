using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – ReverbZoneMix Enum")]
    public sealed class AudioSourceReverbZoneMixEnumMonoBinder : EnumMonoBinder<AudioSource, float>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(float value) =>
            CachedComponent.reverbZoneMix = _converter?.Convert(value) ?? value;
    }
}