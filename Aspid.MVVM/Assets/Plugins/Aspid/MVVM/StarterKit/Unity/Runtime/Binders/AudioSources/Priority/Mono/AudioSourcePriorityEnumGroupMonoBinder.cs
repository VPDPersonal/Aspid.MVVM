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
    public sealed class AudioSourcePriorityEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, int, Converter>
    {
        protected override void SetValue(AudioSource element, int value) =>
            element.priority = Mathf.Clamp(value, min: 0, max: 256);
    }
}