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
    /// MonoBehaviour binder that sets the <see cref="AudioSource.reverbZoneMix"/> property on an <see cref="AudioSource"/>
    /// to a value resolved from an enum bound on the ViewModel. The value is clamped to the range [0, 1.1].
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – ReverbZoneMix Enum")]
    public sealed class AudioSourceReverbZoneMixEnumMonoBinder : EnumMonoBinder<AudioSource, float, Converter>
    {
        protected override void SetValue(float value) =>
            CachedComponent.reverbZoneMix = Mathf.Clamp(value, min: 0, max: 1.1f);
    }
}