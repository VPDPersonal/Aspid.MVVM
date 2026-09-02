using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{T1, T2}">EnumMonoBinder&lt;AudioSource, Vector2&gt;</see> that sets the
    /// min/max distance of an <see cref="AudioSource"/> to a <see cref="Vector2"/> resolved from the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – MinMaxDistance Enum")]
    public sealed class AudioSourceMinMaxDistanceEnumMonoBinder : EnumMonoBinder<AudioSource, Vector2>
    {
        [Tooltip("Which end of the distance range the bound value writes.")]
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        /// <summary>
        /// Called when the bound enum resolves to a value.
        /// Applies the <see cref="Vector2"/> to <see cref="AudioSource.minDistance"/>, <see cref="AudioSource.maxDistance"/>,
        /// or both according to the configured <see cref="AudioSourceDistanceMode"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(Vector2 value) =>
            CachedComponent.SetMinMaxDistance(value, _distanceMode);
    }
}