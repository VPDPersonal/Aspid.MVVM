using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{AudioSource}"/> that binds the <see cref="AudioSource.priority"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current priority value
    /// is sent back to the ViewModel.
    /// The bound value is clamped to [0, 256] before being applied to <see cref="AudioSource.priority"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority")]
    public class AudioSourcePriorityMonoBinder : ComponentIntMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.priority;
            set => CachedComponent.priority = value;
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.priority"/> property.
        /// Clamps the converted value to the valid range of 0 to 256.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override int GetConvertedValue(int value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 256);
    }
}