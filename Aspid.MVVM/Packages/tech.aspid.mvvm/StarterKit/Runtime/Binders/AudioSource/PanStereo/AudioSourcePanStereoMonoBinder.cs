using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="AudioSource.panStereo"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to -1..1.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "Pan2D")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – PanStereo")]
    public class AudioSourcePanStereoMonoBinder : ComponentFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.panStereo;
            set => CachedComponent.panStereo = value;
        }

        /// <inheritdoc/>
        protected override float GetConvertedValue(float value) =>
            this.SafeClamp(base.GetConvertedValue(value), -1f, 1f);
    }
}
