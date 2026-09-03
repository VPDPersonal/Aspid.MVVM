using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="AudioSource.dopplerLevel"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..5.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "DopplerLevel", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – DopplerLevel Enum")]
    public sealed class AudioSourceDopplerLevelEnumMonoBinder : EnumMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.dopplerLevel = this.SafeClamp(value, 0f, 5f);
    }
}
