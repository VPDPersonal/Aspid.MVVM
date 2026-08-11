#nullable enable
using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}">IBinder&lt;int&gt;</see> and
    /// <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that transitions an <see cref="AudioMixer"/> to one of the
    /// snapshots it is given.
    /// </summary>
    /// <remarks>
    /// A snapshot is how a mixer expresses a whole audio state — paused, underwater, in a cutscene — and the state
    /// itself is a ViewModel value.
    /// <para/>
    /// The ViewModel may send either the index into the snapshot list, which is what an enum-driven state does, or the
    /// snapshot's name. An index outside the list, a name that is not in it, and an empty slot are all logged rather
    /// than ignored: a silent no-op leaves the game in the previous audio state with no clue why.
    /// </remarks>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    public class AudioMixerSnapshotBinder : Binder, IBinder<int>, IBinder<string>
    {
        [Tooltip("Snapshots the ViewModel can select, by index or by name.")]
        [SerializeField] private AudioMixerSnapshot[] _snapshots;

        [Tooltip("Seconds the mixer takes to reach the snapshot. Zero switches instantly.")]
        [SerializeField] [Min(0f)] private float _transitionSeconds;

        /// <summary>
        /// Indicates whether binding is allowed: <see langword="false"/> when no snapshots were given.
        /// </summary>
        public override bool IsBind => _snapshots is { Length: > 0 };

        /// <summary>
        /// Initializes a new instance of <see cref="AudioMixerSnapshotBinder"/>.
        /// </summary>
        /// <param name="snapshots">The snapshots the ViewModel can select, by index or by name.</param>
        /// <param name="transitionSeconds">Seconds the mixer takes to reach the snapshot. Zero switches instantly.</param>
        /// <param name="mode">The binding mode. Must be <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/> — a snapshot transition has no value to read back.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="snapshots"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is neither <see cref="BindMode.OneWay"/> nor <see cref="BindMode.OneTime"/>.</exception>
        public AudioMixerSnapshotBinder(
            AudioMixerSnapshot[] snapshots,
            float transitionSeconds = 0.5f,
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfNotOne();

            _snapshots = snapshots ?? throw new ArgumentNullException(nameof(snapshots));
            _transitionSeconds = transitionSeconds;
        }

        /// <summary>
        /// Transitions to the snapshot at <paramref name="value"/> in the list.
        /// </summary>
        /// <param name="value">The index received from the ViewModel.</param>
        public void SetValue(int value)
        {
            if (value < 0 || value >= _snapshots.Length)
            {
                Debug.LogError($"[{nameof(AudioMixerSnapshotBinder)}] Snapshot index {value} is outside the list of {_snapshots.Length}.");
                return;
            }

            TransitionTo(_snapshots[value], value.ToString());
        }

        /// <summary>
        /// Transitions to the snapshot named <paramref name="value"/> in the list.
        /// </summary>
        /// <param name="value">The snapshot name received from the ViewModel, or <see langword="null"/> to do nothing.</param>
        public void SetValue(string? value)
        {
            if (value is null) return;

            foreach (var snapshot in _snapshots)
            {
                if (!snapshot || snapshot.name != value) continue;

                TransitionTo(snapshot, value);
                return;
            }

            Debug.LogError($"[{nameof(AudioMixerSnapshotBinder)}] No snapshot named '{value}' in the list.");
        }

        private void TransitionTo(AudioMixerSnapshot snapshot, string requested)
        {
            if (!snapshot)
            {
                Debug.LogError($"[{nameof(AudioMixerSnapshotBinder)}] Snapshot '{requested}' is an empty slot.");
                return;
            }

            snapshot.TransitionTo(BinderMath.SafeClamp(_transitionSeconds, 0f, float.MaxValue));
        }
    }
}
