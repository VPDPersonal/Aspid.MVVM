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
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Pitch EnumGroup")]
    public sealed class AudioSourcePitchEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource>
    {
        [SerializeField] [Range(-3f, 3)] private float _defaultValue;
        [SerializeField] [Range(-3f, 3)] private float _selectedValue;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedConverter;

        protected override void SetDefaultValue(AudioSource element) =>
            element.pitch = _defaultConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(AudioSource element) =>
            element.pitch = _selectedConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}