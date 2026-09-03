using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="AudioSource.dopplerLevel"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..5.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "DopplerLevel", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – DopplerLevel Switcher")]
    public sealed class AudioSourceDopplerLevelSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.dopplerLevel = this.SafeClamp(value, 0f, 5f);
    }
}
