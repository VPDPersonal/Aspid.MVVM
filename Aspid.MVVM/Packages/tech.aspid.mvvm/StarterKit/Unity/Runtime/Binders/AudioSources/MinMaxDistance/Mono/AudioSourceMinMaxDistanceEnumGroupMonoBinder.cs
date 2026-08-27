using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinderWithConverter{T1, T2}">EnumGroupMonoBinderWithConverter&lt;AudioSource, Vector2&gt;</see> that sets the
    /// min/max distance on each <see cref="AudioSource"/> element to a <see cref="Vector2"/> resolved from the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – MinMaxDistance EnumGroup")]
    public sealed class AudioSourceMinMaxDistanceEnumGroupMonoBinder : EnumGroupMonoBinderWithConverter<AudioSource, Vector2>
    {
        [Tooltip("Which end of the distance range the bound value writes.")]
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Applies the <see cref="Vector2"/> to <see cref="AudioSource.minDistance"/>, <see cref="AudioSource.maxDistance"/>,
        /// or both according to the configured <see cref="AudioSourceDistanceMode"/>.
        /// </summary>
        /// <param name="element">The component this entry of the group writes to.</param>
        /// <param name="value">The value the bound enum resolved to for this element.</param>
        protected override void SetValue(AudioSource element, Vector2 value) =>
            element.SetMinMaxDistance(value, _distanceMode);
    }
}