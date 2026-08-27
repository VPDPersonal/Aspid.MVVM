using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="FloatMonoBinder"/> that binds <see cref="AudioListener.volume"/>.
    /// </summary>
    /// <remarks>
    /// The master volume of the whole game, and the one audio value that is not attached to anything: it is a static
    /// property, so this binder needs no target and works wherever it is dropped. A project without an
    /// <see cref="AudioMixer"/> has nothing else to bind a master slider to.
    /// <para/>
    /// Clamped to 0..1, the range Unity documents; a non-finite value lands on zero rather than silencing the game
    /// with nothing in the log.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioListener Binder – Volume")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Audio/AudioListener Binder – Volume")]
    public class AudioListenerVolumeMonoBinder : FloatMonoBinder
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => AudioListener.volume;
            set => AudioListener.volume = BinderMath.SafeClamp01(value);
        }
    }
}
