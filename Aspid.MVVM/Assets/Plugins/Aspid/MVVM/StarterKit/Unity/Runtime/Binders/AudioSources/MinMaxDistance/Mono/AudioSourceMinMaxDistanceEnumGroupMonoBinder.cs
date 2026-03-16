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
    /// <see cref="EnumGroupMonoBinder{AudioSource, Vector2, IConverter{Vector2, Vector2}}"/> that sets the
    /// min/max distance on each <see cref="AudioSource"/> element to a <see cref="Vector2"/> resolved from the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – MinMaxDistance EnumGroup")]
    public sealed class AudioSourceMinMaxDistanceEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, Vector2, Converter>
    {
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Applies the <see cref="Vector2"/> to <see cref="AudioSource.minDistance"/>, <see cref="AudioSource.maxDistance"/>,
        /// or both according to the configured <see cref="AudioSourceDistanceMode"/>.
        /// </summary>
        protected override void SetValue(AudioSource element, Vector2 value) =>
            element.SetMinMaxDistance(value, _distanceMode);
    }
}