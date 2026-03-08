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
    /// MonoBehaviour binder that sets the <see cref="AudioSource.priority"/> property on an <see cref="AudioSource"/>
    /// to a value resolved from an enum bound on the ViewModel. The value is clamped to the range [0, 256].
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority Enum")]
    public sealed class AudioSourcePriorityEnumMonoBinder : EnumMonoBinder<AudioSource, int, Converter>
    {
        protected override void SetValue(int value) =>
            CachedComponent.priority = Mathf.Clamp(value, 0, 256);
    }
}