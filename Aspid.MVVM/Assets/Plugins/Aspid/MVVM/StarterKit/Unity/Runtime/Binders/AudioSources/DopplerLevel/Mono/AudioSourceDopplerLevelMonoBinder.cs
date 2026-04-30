using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{AudioSource}"/> that binds the <see cref="AudioSource.dopplerLevel"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current dopplerLevel value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – DopplerLevel")]
    public class AudioSourceDopplerLevelMonoBinder : ComponentFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.dopplerLevel;
            set => CachedComponent.dopplerLevel = value;
        }
    }
}