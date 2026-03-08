using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.reverbZoneMix"/> property on a group of <see cref="AudioSource"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value. The value is clamped to the range [0, 1.1].
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – ReverbZoneMix EnumGroup")]
    public sealed class AudioSourceReverbZoneMixEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, float, Converter>
    {
        protected override void SetValue(AudioSource element, float value) =>
            element.reverbZoneMix = Mathf.Clamp(value, min: 0, max: 1.1f);
    }
}