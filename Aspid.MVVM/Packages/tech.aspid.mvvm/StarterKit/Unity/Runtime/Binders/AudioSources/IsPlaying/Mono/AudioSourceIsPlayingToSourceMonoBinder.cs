using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{AudioSource}"/> implementing
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;bool&gt;</see> that reports whether the source is playing.
    /// </summary>
    /// <remarks>
    /// The ViewModel could start and stop a sound and could not learn that one had finished, so a button that should
    /// re-enable itself when a voice line ends had nothing to listen to.
    /// <para/>
    /// <see cref="AudioSource"/> raises no event for this, so the state is polled in <c>Update</c> and the ViewModel is
    /// told only when it changes. That is one boolean comparison per frame per binder — cheap, but not free, which is
    /// why this is a binder a project adds where it needs it rather than something the playback binders do on their own.
    /// While the component is disabled nothing is polled: a paused game does not report a sound as finished.
    /// </remarks>
    [BindModeOverride(modes: BindMode.OneWayToSource)]
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource Binder – Is Playing To Source")]
    public partial class AudioSourceIsPlayingToSourceMonoBinder : ComponentMonoBinder<AudioSource>, IReverseBinder<bool>
    {
        /// <inheritdoc/>
        public event Action<bool> ValueChanged;

        [Tooltip("When enabled, the reported value is inverted — bind an IsIdle flag to it directly.")]
        [SerializeField] private bool _isInvert;

        private bool _wasPlaying;

        /// <inheritdoc/>
        protected override BindMode DefaultMode => BindMode.OneWayToSource;

        /// <summary>
        /// Called when the binder is bound. Reports the current state so the ViewModel starts in step with the source.
        /// </summary>
        protected override void OnBound()
        {
            _wasPlaying = CachedComponent.isPlaying;
            Raise(_wasPlaying);
        }

        /// <summary>
        /// Polls <see cref="AudioSource.isPlaying"/> and reports a change.
        /// </summary>
        /// <remarks>
        /// Unity calls this only while the component is enabled, which is the behaviour that keeps a paused game from
        /// reporting a sound as finished.
        /// </remarks>
        private void Update()
        {
            if (!IsBound) return;

            var isPlaying = CachedComponent && CachedComponent.isPlaying;
            if (isPlaying == _wasPlaying) return;

            _wasPlaying = isPlaying;
            Raise(isPlaying);
        }

        private void Raise(bool isPlaying) =>
            ValueChanged?.Invoke(_isInvert ? !isPlaying : isPlaying);
    }
}
