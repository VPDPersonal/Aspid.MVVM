using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="AudioSource.minDistance"/> and
    /// <see cref="AudioSource.maxDistance"/> as a <see cref="Vector2"/>, or a single number written to both.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), "MinDistance", "MaxDistance")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – MinMaxDistance")]
    public partial class AudioSourceMinMaxDistanceMonoBinder : ComponentMonoBinder<AudioSource, Vector2>, IFloatBinder
    {
        [Tooltip("Which distances the bound value writes.")]
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => new(CachedComponent.minDistance, CachedComponent.maxDistance);
            set => CachedComponent.SetMinMaxDistance(value, _distanceMode);
        }

        /// <summary>
        /// Writes <paramref name="value"/> to the distances selected by the mode.
        /// </summary>
        /// <param name="value">The distance received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(new Vector2(value, value));
    }
}
