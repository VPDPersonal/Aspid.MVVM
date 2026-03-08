using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector2;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.minDistance"/> and <see cref="AudioSource.maxDistance"/> properties
    /// on a group of <see cref="AudioSource"/> components, applying the configured selected or default value to each entry
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – MinMaxDistance EnumGroup")]
    public sealed class AudioSourceMinMaxDistanceEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, Vector2, Converter>
    {
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        protected override void SetValue(AudioSource element, Vector2 value) =>
            element.SetMinMaxDistance(value, _distanceMode);
    }
}