using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="AudioSource.dopplerLevel"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..5.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "DopplerLevel")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – DopplerLevel")]
    public class AudioSourceDopplerLevelMonoBinder : ComponentFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.dopplerLevel;
            set => CachedComponent.dopplerLevel = value;
        }

        /// <inheritdoc/>
        protected override float GetConvertedValue(float value) =>
            this.SafeClamp(base.GetConvertedValue(value), 0f, 5f);
    }
}
