using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.BindModes
{
    // One member per bind mode. The mode on the ViewModel side is the upper bound;
    // each binder in the scene picks its own mode from what the member allows.
    [ViewModel]
    [Serializable]
    public sealed partial class AudioSettingsViewModel
    {
        // OneTime: read once on bind, never updated. A readonly field is OneTime automatically.
        [OneTimeBind] private readonly string _version = "1.0.0";

        // TwoWay: the slider moves the value, and the value moves the slider.
        // BindAlso re-sends the computed VolumeLabel whenever Volume changes.
        [BindAlso(nameof(VolumeLabel))]
        [TwoWayBind]
        [SerializeField] [Range(0f, 1f)] private float _volume = 0.5f;

        // TwoWay: the toggle drives IsMuted; the ResetCommand drives the toggle.
        [TwoWayBind]
        [SerializeField] private bool _isMuted;

        // OneWayToSource: View -> ViewModel only. The ViewModel never pushes a value into the input field.
        [OneWayToSourceBind] private string _profileName;

        [RelayCommand]
        private void Reset()
        {
            Volume = 0.5f;
            IsMuted = false;
        }

        // OneWay: ViewModel -> View. A computed member, registered as bindable by the BindAlso above.
        private string VolumeLabel => Volume switch
        {
            0f => "Silent",
            < 0.34f => "Quiet",
            < 0.67f => "Normal",
            _ => "Loud",
        };

        partial void OnProfileNameChanged(string newValue) =>
            Debug.Log($"Profile name is now \"{newValue}\"");
    }
}
