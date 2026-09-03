using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TComponent}"/> that binds <see cref="AudioSource.priority"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..256.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "Priority")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority")]
    public class AudioSourcePriorityMonoBinder : ComponentIntMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.priority;
            set => CachedComponent.priority = value;
        }

        /// <inheritdoc/>
        protected override int GetConvertedValue(int value) =>
            Mathf.Clamp(base.GetConvertedValue(value), 0, 256);
    }
}
