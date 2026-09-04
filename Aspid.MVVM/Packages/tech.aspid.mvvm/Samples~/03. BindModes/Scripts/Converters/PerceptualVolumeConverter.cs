using System;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.BindModes
{
    // A project-specific converter. [Serializable] lets it sit in a binder's converter slot,
    // [TypeSelectorDisplay] adds it to the converter picker in the Inspector.
    // ITwoWayConverter is required for TwoWay binders: ConvertBack maps the slider position back to the ViewModel.
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Samples/Bind Modes",
        Name = "Perceptual Volume",
        Tooltip = "Maps linear volume to a perceptual slider position and back")]
    public sealed class PerceptualVolumeConverter : ITwoWayConverter<float, float>
    {
        [Tooltip("Curve exponent. 1 is linear; 2 gives finer control at low volume.")]
        [SerializeField] [Min(0.1f)] private float _exponent = 2f;

        // ViewModel -> View: linear volume to slider position.
        public float Convert(float value) =>
            Mathf.Pow(Mathf.Clamp01(value), 1f / _exponent);

        // View -> ViewModel: slider position to linear volume.
        public float ConvertBack(float value) =>
            Mathf.Pow(Mathf.Clamp01(value), _exponent);
    }
}
