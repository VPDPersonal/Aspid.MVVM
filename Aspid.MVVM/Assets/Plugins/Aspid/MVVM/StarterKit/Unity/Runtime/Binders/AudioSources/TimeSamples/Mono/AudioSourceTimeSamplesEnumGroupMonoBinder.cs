using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.timeSamples"/> property on a group of <see cref="AudioSource"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – TimeSamples EnumGroup")]
    public sealed class AudioSourceTimeSamplesEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, int, Converter>
    {
        protected override void SetValue(AudioSource element, int value) =>
            element.timeSamples = value;
    }
}