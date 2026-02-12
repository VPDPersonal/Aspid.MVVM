using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – ReverbZoneMix EnumGroup")]
    public sealed class AudioSourceReverbZoneMixEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource>
    {
        [SerializeField] [Range(0, 1.1f)] private float _defaultValue;
        [SerializeField] [Range(0, 1.1f)] private float _selectedValue;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedConverter;
        
        protected override void SetDefaultValue(AudioSource element) =>
            element.reverbZoneMix = _defaultConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(AudioSource element) =>
            element.reverbZoneMix = _selectedConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}