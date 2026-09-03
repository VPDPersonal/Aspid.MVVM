using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="AudioSource.panStereo"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to -1..1.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "Pan2D", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – PanStereo Switcher")]
    public sealed class AudioSourcePanStereoSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.panStereo = this.SafeClamp(value, -1f, 1f);
    }
}
