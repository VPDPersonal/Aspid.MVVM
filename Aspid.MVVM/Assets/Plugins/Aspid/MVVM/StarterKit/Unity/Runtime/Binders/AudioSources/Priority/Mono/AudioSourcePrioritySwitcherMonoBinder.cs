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
    /// MonoBehaviour binder that switches the <see cref="AudioSource.priority"/> property on an <see cref="AudioSource"/>
    /// between two values based on a bound boolean ViewModel property. The value is clamped to the range [0, 256].
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority Switcher")]
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "m_Priority", SubPath = "Switcher")]
    public sealed class AudioSourcePrioritySwitcherMonoBinder : SwitcherMonoBinder<AudioSource, int, Converter>
    {
        protected override void SetValue(int value) =>
            CachedComponent.priority = Mathf.Clamp(value, min: 0, max: 256);
    }
}