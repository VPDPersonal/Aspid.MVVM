using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.priority"/> on each element.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..256.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "Priority", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority EnumGroup")]
    public sealed class AudioSourcePriorityEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, int>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, int value) =>
            element.priority = Mathf.Clamp(value, 0, 256);
    }
}
