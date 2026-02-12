using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority Enum")]
    public sealed class AudioSourcePriorityEnumMonoBinder : EnumMonoBinder<AudioSource, int>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(int value) =>
            CachedComponent.priority = _converter?.Convert(value) ?? value;
    }
}