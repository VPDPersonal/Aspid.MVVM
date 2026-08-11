using UnityEngine;
using UnityEngine.Video;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{T1, T2}">ComponentObjectMonoBinder&lt;VideoPlayer, VideoClip&gt;</see> that binds
    /// <see cref="VideoPlayer.clip"/>.
    /// </summary>
    /// <remarks>
    /// Which video plays: a cutscene chosen by progress, a tutorial chosen by the step the player is on, an ad chosen by
    /// what the server sent. It is the one property of the component a ViewModel really decides.
    /// <para/>
    /// Assigning a clip stops playback and rewinds — Unity's behaviour, not the binder's — so a project that swaps clips
    /// mid-scene plays the new one itself.
    /// <para/>
    /// A destroyed clip arrives as <see langword="null"/>, which leaves the player without a clip rather than with a
    /// reference that fails on the next prepare.
    /// </remarks>
    [AddBinderContextMenu(typeof(VideoPlayer), serializePropertyNames: "m_VideoClip")]
    [AddComponentMenu("Aspid/MVVM/Binders/Video/VideoPlayer Binder – Clip")]
    public class VideoPlayerClipMonoBinder : ComponentObjectMonoBinder<VideoPlayer, VideoClip>
    {
        /// <inheritdoc/>
        protected sealed override VideoClip Property
        {
            get => CachedComponent.clip;
            set => CachedComponent.clip = value;
        }
    }
}
