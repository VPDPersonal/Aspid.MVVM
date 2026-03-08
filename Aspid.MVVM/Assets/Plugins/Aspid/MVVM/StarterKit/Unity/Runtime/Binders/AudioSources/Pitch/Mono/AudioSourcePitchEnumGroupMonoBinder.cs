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
    /// MonoBehaviour binder that sets the <see cref="AudioSource.pitch"/> property on a group of <see cref="AudioSource"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value. The value is clamped to the range [-3, 3].
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Pitch EnumGroup")]
    public sealed class AudioSourcePitchEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, float, Converter>
    {
        protected override void SetValue(AudioSource element, float value) =>
            element.pitch = Mathf.Clamp(value, min: -3, max: 3);
    }
}