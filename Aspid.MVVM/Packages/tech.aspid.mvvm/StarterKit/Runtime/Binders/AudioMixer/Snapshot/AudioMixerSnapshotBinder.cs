#nullable enable
using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> that transitions an <see cref="AudioMixer"/> to one of the listed snapshots, chosen by
    /// index or by name.
    /// </summary>
    /// <remarks>
    /// An index outside the list, an unknown name and an empty slot are reported.
    /// </remarks>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    public class AudioMixerSnapshotBinder : Binder, IBinder<int>, IBinder<string?>
    {
        [Tooltip("Snapshots selectable by index or name.")]
        [SerializeField] private AudioMixerSnapshot[] _snapshots;

        [Tooltip("Transition time in seconds; zero switches instantly.")]
        [SerializeField] [Min(0f)] private float _transitionSeconds;

        /// <param name="snapshots">The snapshots selectable by index or name.</param>
        /// <param name="transitionSeconds">The transition time in seconds; zero switches instantly.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentNullException"><paramref name="snapshots"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="transitionSeconds"/> is negative.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public AudioMixerSnapshotBinder(
            AudioMixerSnapshot[] snapshots,
            float transitionSeconds = 0.5f,
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfNotOne();

            _snapshots = snapshots ?? throw new ArgumentNullException(nameof(snapshots));
            _transitionSeconds = transitionSeconds >= 0f
                ? transitionSeconds
                : throw new ArgumentOutOfRangeException(nameof(transitionSeconds), transitionSeconds, null);
        }

        /// <inheritdoc/>
        public override bool CanBind => _snapshots is { Length: > 0 };

        /// <summary>
        /// Transitions to the snapshot at <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The index received from the ViewModel.</param>
        public void SetValue(int value)
        {
            if (value < 0 || value >= _snapshots.Length)
            {
                this.LogError(
                    problem: $"the index {value} is outside the list of {_snapshots.Length} snapshots",
                    consequence: "No transition is started.");

                return;
            }

            TransitionTo(_snapshots[value], value.ToString());
        }

        /// <summary>
        /// Transitions to the snapshot named <paramref name="value"/>; <see langword="null"/> does nothing.
        /// </summary>
        /// <param name="value">The snapshot name received from the ViewModel.</param>
        public void SetValue(string? value)
        {
            if (value is null) return;
            foreach (var snapshot in _snapshots)
            {
                if (!snapshot || snapshot.name != value) continue;

                TransitionTo(snapshot, value);
                return;
            }

            this.LogError(
                problem: $"the list holds no snapshot named {value.Describe()}",
                consequence: "No transition is started.");
        }

        private void TransitionTo(AudioMixerSnapshot snapshot, string requested)
        {
            if (snapshot)
            {
                snapshot.TransitionTo(_transitionSeconds);
                return;
            }

            this.LogError(
                problem: $"the snapshot {requested.Describe()} is an empty slot",
                consequence: "No transition is started.");
        }
    }
}
