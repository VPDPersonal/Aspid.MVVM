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
    /// MonoBehaviour binder that switches the <see cref="AudioSource.timeSamples"/> property on an <see cref="AudioSource"/>
    /// between two values based on a bound boolean ViewModel property.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – TimeSamples Switcher")]
    public sealed class AudioSourceTimeSamplesSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, int, Converter>
    {
        protected override void SetValue(int value) =>
            CachedComponent.timeSamples = value;
    }
}