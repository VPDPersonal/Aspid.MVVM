using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.panStereo"/> on each element.
    /// </summary>
    /// <remarks>
    /// The value is clamped to -1..1.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "Pan2D", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – PanStereo EnumGroup")]
    public sealed class AudioSourcePanStereoEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, float value) =>
            element.panStereo = this.SafeClamp(value, -1f, 1f);
    }
}
