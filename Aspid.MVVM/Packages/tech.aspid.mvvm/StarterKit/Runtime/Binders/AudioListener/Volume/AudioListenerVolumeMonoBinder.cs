using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="FloatMonoBinder"/> that binds <see cref="AudioListener.volume"/>.
    /// </summary>
    /// <remarks>
    /// A static property, so the binder has no target. The value is clamped to [0, 1].
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Audio/AudioListener Binder – Volume")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioListener Binder – Volume")]
    public class AudioListenerVolumeMonoBinder : FloatMonoBinder
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => AudioListener.volume;
            set => AudioListener.volume = this.SafeClamp01(value);
        }
    }
}
