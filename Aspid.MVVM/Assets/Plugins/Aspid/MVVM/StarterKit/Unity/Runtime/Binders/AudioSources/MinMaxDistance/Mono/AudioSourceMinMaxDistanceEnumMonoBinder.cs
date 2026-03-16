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
    /// <see cref="EnumMonoBinder{AudioSource, Vector2, IConverter{Vector2, Vector2}}"/> that sets the
    /// min/max distance of an <see cref="AudioSource"/> to a <see cref="Vector2"/> resolved from the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – MinMaxDistance Enum")]
    public sealed class AudioSourceMinMaxDistanceEnumMonoBinder : EnumMonoBinder<AudioSource, Vector2, Converter>
    {
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        /// <summary>
        /// Called when the bound enum resolves to a value.
        /// Applies the <see cref="Vector2"/> to <see cref="AudioSource.minDistance"/>, <see cref="AudioSource.maxDistance"/>,
        /// or both according to the configured <see cref="AudioSourceDistanceMode"/>.
        /// </summary>
        protected override void SetValue(Vector2 value) =>
            CachedComponent.SetMinMaxDistance(value, _distanceMode);
    }
}