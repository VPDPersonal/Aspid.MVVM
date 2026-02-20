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
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – TimeSamples EnumGroup")]
    public sealed class AudioSourceTimeSamplesEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, int, Converter>
    {
        protected override void SetValue(AudioSource element, int value) =>
            element.timeSamples = value;
    }
}