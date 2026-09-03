using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that reports <see cref="AudioSource.isPlaying"/> to the ViewModel.
    /// </summary>
    /// <remarks>
    /// <see cref="AudioSource"/> raises no event when playback ends, so the state is polled once per frame while the binder is enabled.
    /// </remarks>
    [BindModeOverride(BindMode.OneWayToSource)]
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Is Playing To Source")]
    public sealed class AudioSourceIsPlayingToSourceMonoBinder : ComponentMonoBinder<AudioSource>, IReverseBinder<bool>
    {
        [Tooltip("Optional converter applied to the reported value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

        private bool _wasPlaying;

        /// <inheritdoc/>
        public event Action<bool> ValueChanged;

        /// <inheritdoc/>
        protected override BindMode DefaultMode => BindMode.OneWayToSource;

        /// <inheritdoc/>
        protected override void OnBound()
        {
            _wasPlaying = CachedComponent.isPlaying;
            Raise(_wasPlaying);
        }

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
