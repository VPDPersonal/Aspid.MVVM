using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="AudioSource.priority"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..256.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "Priority", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority Enum")]
    public sealed class AudioSourcePriorityEnumMonoBinder : EnumMonoBinder<AudioSource, int>
    {
        /// <inheritdoc/>
        protected override void SetValue(int value) =>
            CachedComponent.priority = Mathf.Clamp(value, 0, 256);
    }
}
