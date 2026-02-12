using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector2;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – MinMaxDistance EnumGroup")]
    public sealed class AudioSourceMinMaxDistanceEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource>
    {
        [SerializeField] private Vector2 _defaultValue;
        [SerializeField] private Vector2 _selectedValue;
        
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedConverter;
        
        protected override void SetDefaultValue(AudioSource element)
        {
            var value = _defaultConverter?.Convert(_defaultValue) ?? _defaultValue;
            element.SetMinMaxDistance(value, _distanceMode);
        }

        protected override void SetSelectedValue(AudioSource element)
        {
            var value = _selectedConverter?.Convert(_selectedValue) ?? _selectedValue;
            element.SetMinMaxDistance(value, _distanceMode);
        }
    }
}