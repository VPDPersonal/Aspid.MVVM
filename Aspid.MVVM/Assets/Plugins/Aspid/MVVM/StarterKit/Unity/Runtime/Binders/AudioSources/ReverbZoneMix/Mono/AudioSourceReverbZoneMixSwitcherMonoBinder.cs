using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – ReverbZoneMix Switcher")]
    public sealed class AudioSourceReverbZoneMixSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, float, Converter>
    {
        protected override void SetValue(float value) =>
            CachedComponent.reverbZoneMix = Mathf.Clamp(value, min: 0, max: 1.1f);
    }
}