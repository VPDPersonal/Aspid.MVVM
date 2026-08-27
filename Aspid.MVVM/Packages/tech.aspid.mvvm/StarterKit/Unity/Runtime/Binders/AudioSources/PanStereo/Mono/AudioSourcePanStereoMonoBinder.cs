using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{AudioSource}"/> that binds the <see cref="AudioSource.panStereo"/> property.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [−1, 1] before being applied to <see cref="AudioSource.panStereo"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – PanStereo")]
    public class AudioSourcePanStereoMonoBinder : ComponentFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.panStereo;
            set => CachedComponent.panStereo = value;
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.panStereo"/> property.
        /// Clamps the converted value to the valid range of −1 to 1.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        /// <param name="value">The value to convert.</param>
        protected override float GetConvertedValue(float value) =>
            BinderMath.SafeClamp(base.GetConvertedValue(value), -1, 1);
    }
}