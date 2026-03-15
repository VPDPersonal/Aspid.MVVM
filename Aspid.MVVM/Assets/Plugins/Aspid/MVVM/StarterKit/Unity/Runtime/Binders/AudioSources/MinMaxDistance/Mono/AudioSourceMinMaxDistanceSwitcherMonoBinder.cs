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
    /// <see cref="SwitcherMonoBinder{AudioSource, Vector2, IConverter{Vector2, Vector2}}"/> that switches the
    /// min/max distance of an <see cref="AudioSource"/> between two <see cref="Vector2"/> values
    /// based on the bound boolean ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – MinMaxDistance Switcher")]
    public sealed class AudioSourceMinMaxDistanceSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, Vector2, Converter>
    {
        [Tooltip("Determines which distance component (min, max, or both) is updated when the bound value changes.")]
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        /// <summary>
        /// Called when applying the selected <see cref="Vector2"/> to the <see cref="AudioSource"/> min/max distance.
        /// Dispatches to <see cref="AudioSource.minDistance"/>, <see cref="AudioSource.maxDistance"/>, or both
        /// according to the configured <see cref="AudioSourceDistanceMode"/>.
        /// </summary>
        protected override void SetValue(Vector2 value) =>
            CachedComponent.SetMinMaxDistance(value, _distanceMode);
    }
}