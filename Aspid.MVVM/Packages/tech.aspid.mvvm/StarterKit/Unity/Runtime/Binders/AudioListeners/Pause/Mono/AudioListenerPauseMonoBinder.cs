using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="BoolMonoBinder"/> that binds <see cref="AudioListener.pause"/>.
    /// </summary>
    /// <remarks>
    /// Silences every source at once while keeping their playback positions, which is what a pause menu wants and
    /// what setting <see cref="Time.timeScale"/> to zero does not do — audio ignores the time scale. Like
    /// <see cref="AudioListener.volume"/> it is a static property, so the binder needs no target.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioListener Binder – Pause")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Audio/AudioListener Binder – Pause")]
    public class AudioListenerPauseMonoBinder : BoolMonoBinder
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => AudioListener.pause;
            set => AudioListener.pause = value;
        }
    }
}
