using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="AudioSource.panStereo"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to -1..1.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "Pan2D", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – PanStereo Enum")]
    public sealed class AudioSourcePanStereoEnumMonoBinder : EnumMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.panStereo = this.SafeClamp(value, -1f, 1f);
    }
}
