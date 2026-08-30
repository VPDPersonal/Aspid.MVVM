using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="FloatMonoBinder"/> that binds <see cref="AudioListener.volume"/>.
    /// </summary>
    /// <remarks>
    /// A static property, so this binder has no target.
    /// <para/>
    /// Clamped to 0..1, the range Unity documents; a non-finite value lands on zero rather than silencing the game
    /// with nothing in the log.
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
