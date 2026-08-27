using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinderWithConverter{T1, T2}">SwitcherMonoBinderWithConverter&lt;AudioSource, Vector2&gt;</see> that switches the
    /// min/max distance of an <see cref="AudioSource"/> between two <see cref="Vector2"/> values
    /// based on the bound boolean ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – MinMaxDistance Switcher")]
    public sealed class AudioSourceMinMaxDistanceSwitcherMonoBinder : SwitcherMonoBinderWithConverter<AudioSource, Vector2>
    {
        [Tooltip("Which distance component the bound value updates.")]
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        /// <summary>
        /// Called when applying the selected <see cref="Vector2"/> to the <see cref="AudioSource"/> min/max distance.
        /// Dispatches to <see cref="AudioSource.minDistance"/>, <see cref="AudioSource.maxDistance"/>, or both
        /// according to the configured <see cref="AudioSourceDistanceMode"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(Vector2 value) =>
            CachedComponent.SetMinMaxDistance(value, _distanceMode);
    }
}