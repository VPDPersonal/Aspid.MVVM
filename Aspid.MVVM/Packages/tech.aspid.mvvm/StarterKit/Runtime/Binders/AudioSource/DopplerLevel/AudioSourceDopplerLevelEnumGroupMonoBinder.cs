using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.dopplerLevel"/> on each element.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..5.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "DopplerLevel", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – DopplerLevel EnumGroup")]
    public sealed class AudioSourceDopplerLevelEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, float value) =>
            element.dopplerLevel = this.SafeClamp(value, 0f, 5f);
    }
}
