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
    public sealed class AudioSourcePanStereoEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, float, Converter>
    {
        protected override void SetValue(AudioSource element, float value) =>
            element.panStereo = Mathf.Clamp(value, min: -1, max: 1);
    }
}