using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.BindModes
{
    [View]
    public sealed partial class AudioSettingsView : MonoView
    {
        [Header("OneTime")]
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _version;

        [Header("OneWay")]
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _volumeLabel;

        [Header("TwoWay")]
        [RequireBinder(typeof(float))]
        [SerializeField] private MonoBinder[] _volume;

        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _isMuted;

        [Header("OneWayToSource")]
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _profileName;

        [Header("Commands")]
        [RequireBinder(typeof(IRelayCommand))]
        [SerializeField] private MonoBinder[] _resetCommand;
    }
}
