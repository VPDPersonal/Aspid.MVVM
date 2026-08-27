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
    /// <see cref="AudioSource"/> raises no event for playback finishing, so the state is polled once per frame instead.
    /// </remarks>
    [BindModeOverride(modes: BindMode.OneWayToSource)]
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource Binder – Is Playing To Source")]
    public partial class AudioSourceIsPlayingToSourceMonoBinder : ComponentMonoBinder<AudioSource>, IReverseBinder<bool>
    {
        /// <inheritdoc/>
        public event Action<bool> ValueChanged;

        [Tooltip("Optional converter applied to the reported value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

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
        /// Runs only while the component is enabled, so a disabled or paused source is not reported as finished.
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
            ValueChanged?.Invoke(_converter?.Convert(isPlaying) ?? isPlaying);
    }
}
