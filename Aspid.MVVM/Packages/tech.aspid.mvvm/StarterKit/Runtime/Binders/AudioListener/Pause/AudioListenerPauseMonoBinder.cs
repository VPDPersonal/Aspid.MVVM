using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder{TProperty}"/> that binds <see cref="AudioListener.pause"/>.
    /// </summary>
    /// <remarks>
    /// A static property, so the binder has no target.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Audio/AudioListener Binder – Pause")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioListener Binder – Pause")]
    public class AudioListenerPauseMonoBinder : MonoBinder<bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => AudioListener.pause;
            set => AudioListener.pause = value;
        }
    }
}
