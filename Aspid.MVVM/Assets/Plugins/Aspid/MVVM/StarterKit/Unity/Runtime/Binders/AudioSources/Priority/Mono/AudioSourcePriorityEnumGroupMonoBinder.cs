using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority EnumGroup")]
    public sealed class AudioSourcePriorityEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource>
    {
        [SerializeField] [Range(0, 256)] private int _defaultValue;
        [SerializeField] [Range(0, 256)] private int _selectedValue;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedConverter;

        protected override void SetDefaultValue(AudioSource element) =>
            element.priority = _defaultConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(AudioSource element) =>
            element.priority = _selectedConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}