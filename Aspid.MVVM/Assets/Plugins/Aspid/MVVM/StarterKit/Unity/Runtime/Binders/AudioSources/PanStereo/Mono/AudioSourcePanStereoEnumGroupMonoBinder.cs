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
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – PanStereo EnumGroup")]
    public sealed class AudioSourcePanStereoEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource>
    {
        [SerializeField] [Range(-1, 1)] private float _defaultValue;
        [SerializeField] [Range(-1, 1)] private float _selectedValue;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedConverter;
        
        protected override void SetDefaultValue(AudioSource element) =>
            element.panStereo = _defaultConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(AudioSource element) =>
            element.panStereo = _selectedConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}