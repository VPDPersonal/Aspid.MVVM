using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{T1, T2, T3}">SwitcherMonoBinder&lt;AudioSource, Vector2, IConverter&lt;Vector2, Vector2&gt;&gt;</see> that switches the
    /// min/max distance of an <see cref="AudioSource"/> between two <see cref="Vector2"/> values
    /// based on the bound boolean ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – MinMaxDistance Switcher")]
    public sealed class AudioSourceMinMaxDistanceSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, Vector2, Converter>
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