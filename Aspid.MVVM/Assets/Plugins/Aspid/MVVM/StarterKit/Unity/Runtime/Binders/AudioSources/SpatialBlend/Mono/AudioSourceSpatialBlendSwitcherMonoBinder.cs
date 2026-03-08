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
    /// MonoBehaviour binder that switches the <see cref="AudioSource.spatialBlend"/> property on an <see cref="AudioSource"/>
    /// between two values based on a bound boolean ViewModel property. The value is clamped to the range [0, 1].
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – SpatialBlend Switcher")]
    public sealed class AudioSourceSpatialBlendSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, float, Converter>
    {
        protected override void SetValue(float value) =>
            CachedComponent.spatialBlend = value;

        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 1);
    }
}