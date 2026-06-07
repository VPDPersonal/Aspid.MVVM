using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{AudioSource}"/> that binds the <see cref="AudioSource.spatialBlend"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current spatialBlend value
    /// is sent back to the ViewModel.
    /// The bound value is clamped to [0, 1] before being applied to <see cref="AudioSource.spatialBlend"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – SpatialBlend")]
    public class AudioSourceSpatialBlendMonoBinder : ComponentFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.spatialBlend;
            set => CachedComponent.spatialBlend = value;
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.spatialBlend"/> property.
        /// Clamps the converted value to the valid range of 0 to 1.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 1);
    }
}