using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority Switcher")]
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "m_Priority", SubPath = "Switcher")]
    public sealed class AudioSourcePrioritySwitcherMonoBinder : SwitcherMonoBinder<AudioSource, int>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(int value) =>
            CachedComponent.priority = value;
    }
}