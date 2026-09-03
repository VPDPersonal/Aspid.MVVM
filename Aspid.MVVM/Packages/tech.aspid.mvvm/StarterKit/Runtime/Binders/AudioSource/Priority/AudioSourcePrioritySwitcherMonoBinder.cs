using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="AudioSource.priority"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..256.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "Priority", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority Switcher")]
    public sealed class AudioSourcePrioritySwitcherMonoBinder : SwitcherMonoBinder<AudioSource, int>
    {
        /// <inheritdoc/>
        protected override void SetValue(int value) =>
            CachedComponent.priority = Mathf.Clamp(value, 0, 256);
    }
}
