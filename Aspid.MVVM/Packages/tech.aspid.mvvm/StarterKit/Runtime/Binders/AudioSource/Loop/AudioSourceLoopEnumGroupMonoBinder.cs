using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.loop"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "Loop", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Loop EnumGroup")]
    public sealed class AudioSourceLoopEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, bool value) =>
            element.loop = value;
    }
}
