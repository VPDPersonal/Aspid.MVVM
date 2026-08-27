using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ComponentMonoBinderWithConverter{T1, T2}">ComponentMonoBinderWithConverter&lt;AudioSource, Vector2&gt;</see> that also implements <see cref="INumberBinder"/>,
    /// binding the <see cref="AudioSource.minDistance"/> and <see cref="AudioSource.maxDistance"/> as a <see cref="Vector2"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – MinMaxDistance")]
    public partial class AudioSourceMinMaxDistanceMonoBinder : ComponentMonoBinderWithConverter<AudioSource, Vector2>, INumberBinder
    {
        [Tooltip("Which distance component the bound value updates.")]
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => new(CachedComponent.minDistance, CachedComponent.maxDistance);
            set => CachedComponent.SetMinMaxDistance(value, _distanceMode);
        }

        /// <summary>
        /// Assigns <paramref name="value"/> to whichever of <see cref="AudioSource.minDistance"/> and
        /// <see cref="AudioSource.maxDistance"/> the configured <see cref="AudioSourceDistanceMode"/>
        /// selects — both of them only when it is <see cref="AudioSourceDistanceMode.Range"/>.
        /// </summary>
        /// <param name="value">The distance to assign to the selected endpoint or endpoints.</param>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(new Vector2(value, value));

        /// <summary>
        /// Converts <paramref name="value"/> to <see cref="float"/> and calls <see cref="SetValue(float)"/>.
        /// </summary>
        /// <param name="value">The value to convert and apply.</param>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts <paramref name="value"/> to <see cref="float"/> and calls <see cref="SetValue(float)"/>.
        /// </summary>
        /// <param name="value">The value to convert and apply.</param>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts <paramref name="value"/> to <see cref="float"/> and calls <see cref="SetValue(float)"/>.
        /// </summary>
        /// <param name="value">The value to convert and apply.</param>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}