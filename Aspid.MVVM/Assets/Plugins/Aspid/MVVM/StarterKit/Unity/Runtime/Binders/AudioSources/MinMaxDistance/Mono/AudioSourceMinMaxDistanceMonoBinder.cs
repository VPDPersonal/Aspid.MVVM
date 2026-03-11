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
    /// <see cref="ComponentMonoBinder{AudioSource, Vector2, IConverter{Vector2, Vector2}}"/> that binds the
    /// min/max distance of an <see cref="AudioSource"/> as a <see cref="Vector2"/>.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current min/max distance
    /// is sent back to the ViewModel.
    /// Also implements <see cref="INumberBinder"/>, allowing scalar numeric values to set both
    /// <see cref="AudioSource.minDistance"/> and <see cref="AudioSource.maxDistance"/> simultaneously.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – MinMaxDistance")]
    public partial class AudioSourceMinMaxDistanceMonoBinder : ComponentMonoBinder<AudioSource, Vector2, Converter>, INumberBinder
    {
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;

        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => new(CachedComponent.minDistance, CachedComponent.maxDistance);
            set => CachedComponent.SetMinMaxDistance(value, _distanceMode);
        }

        /// <summary>
        /// Sets both <see cref="AudioSource.minDistance"/> and <see cref="AudioSource.maxDistance"/>
        /// to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to assign to both distance properties.</param>
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