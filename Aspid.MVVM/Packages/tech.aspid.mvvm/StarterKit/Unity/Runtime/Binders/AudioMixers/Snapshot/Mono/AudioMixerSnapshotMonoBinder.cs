using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
// ReSharper disable NotNullOrRequiredMemberIsNotInitialized
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IBinder{T}">IBinder&lt;int&gt;</see> and
    /// <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that transitions an <see cref="AudioMixer"/> to one of the
    /// snapshots listed in the Inspector.
    /// </summary>
    /// <remarks>
    /// The ViewModel may send either the index into the serialized list or the snapshot's name. An index outside the
    /// list, a name not found, and an empty slot are all logged rather than ignored.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioMixer Binder – Snapshot")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Audio/AudioMixer Binder – Snapshot")]
    public partial class AudioMixerSnapshotMonoBinder : MonoBinder, IBinder<int>, IBinder<string>
    {
        [Tooltip("Snapshots to select by index or name. Required — logs an error if empty.")]
        [SerializeField] private AudioMixerSnapshot[] _snapshots;

        [Tooltip("Seconds the mixer takes to reach the snapshot. Zero switches instantly.")]
        [SerializeField] [Min(0f)] private float _transitionSeconds = 0.5f;

        /// <summary>
        /// Transitions to the snapshot at <paramref name="value"/> in the serialized list.
        /// </summary>
        /// <param name="value">The index received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value)
        {
            if (!IsUsable()) return;

            if (value < 0 || value >= _snapshots.Length)
            {
                Debug.LogError($"[{nameof(AudioMixerSnapshotMonoBinder)}] Snapshot index {value} is outside the list of {_snapshots.Length}.", context: this);
                return;
            }

            TransitionTo(_snapshots[value], value.ToString());
        }

        /// <summary>
        /// Transitions to the snapshot named <paramref name="value"/> in the serialized list.
        /// </summary>
        /// <param name="value">The snapshot name received from the ViewModel, or <see langword="null"/> to do nothing.</param>
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

            Debug.LogError($"[{nameof(AudioMixerSnapshotMonoBinder)}] No snapshot named '{value}' in the list.", context: this);
        }

        private void TransitionTo(AudioMixerSnapshot snapshot, string requested)
        {
            if (!snapshot)
            {
                Debug.LogError($"[{nameof(AudioMixerSnapshotMonoBinder)}] Snapshot '{requested}' is an empty slot.", context: this);
                return;
            }

            snapshot.TransitionTo(BinderMath.SafeClamp(_transitionSeconds, 0f, float.MaxValue));
        }

        private bool IsUsable()
        {
            if (_snapshots is { Length: > 0 }) return true;

            Debug.LogError($"[{nameof(AudioMixerSnapshotMonoBinder)}] No snapshots assigned.", context: this);
            return false;
        }
    }
}
