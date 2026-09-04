using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that transitions an <see cref="AudioMixer"/> to one of the listed snapshots, chosen by
    /// index or by name.
    /// </summary>
    /// <remarks>
    /// An index outside the list, an unknown name and an empty slot are reported.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioMixer Binder – Snapshot")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Audio/AudioMixer Binder – Snapshot")]
    public partial class AudioMixerSnapshotMonoBinder : MonoBinder, IBinder<int>, IBinder<string>
    {
        [Tooltip("Snapshots selectable by index or name.")]
        [SerializeField] private AudioMixerSnapshot[] _snapshots;

        [Tooltip("Transition time in seconds; zero switches instantly.")]
        [SerializeField] [Min(0f)] private float _transitionSeconds = 0.5f;

        /// <summary>
        /// Transitions to the snapshot at <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The index received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value)
        {
            if (!IsUsable()) return;

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
        [BinderLog]
        public void SetValue(string value)
        {
            if (value is null) return;
            if (!IsUsable()) return;

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

        private bool IsUsable()
        {
            if (_snapshots is { Length: > 0 }) return true;

            this.LogError(
                problem: "no snapshots are assigned",
                consequence: "The binder does nothing.");

            return false;
        }
    }
}
