using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.spread"/> on each element.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..360.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Spread EnumGroup")]
    public sealed class AudioSourceSpreadEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, float value) =>
            element.spread = this.SafeClamp(value, 0f, 360f);
    }
}
