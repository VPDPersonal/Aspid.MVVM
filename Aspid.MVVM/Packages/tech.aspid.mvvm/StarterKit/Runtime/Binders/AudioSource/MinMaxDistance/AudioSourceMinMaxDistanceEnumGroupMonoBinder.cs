using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.minDistance"/> and
    /// <see cref="AudioSource.maxDistance"/> on each element as a <see cref="Vector2"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), "MinDistance", "MaxDistance", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – MinMaxDistance EnumGroup")]
    public sealed class AudioSourceMinMaxDistanceEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, Vector2>
    {
        [Tooltip("Which distances the bound value writes.")]
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, Vector2 value) =>
            element.SetMinMaxDistance(value, _distanceMode);
    }
}
