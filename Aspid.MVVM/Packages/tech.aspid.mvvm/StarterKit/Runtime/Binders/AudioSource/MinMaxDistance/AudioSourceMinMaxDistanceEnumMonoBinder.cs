using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="AudioSource.minDistance"/> and
    /// <see cref="AudioSource.maxDistance"/> as a <see cref="Vector2"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), "MinDistance", "MaxDistance", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – MinMaxDistance Enum")]
    public sealed class AudioSourceMinMaxDistanceEnumMonoBinder : EnumMonoBinder<AudioSource, Vector2>
    {
        [Tooltip("Which distances the bound value writes.")]
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        /// <inheritdoc/>
        protected override void SetValue(Vector2 value) =>
            CachedComponent.SetMinMaxDistance(value, _distanceMode);
    }
}
