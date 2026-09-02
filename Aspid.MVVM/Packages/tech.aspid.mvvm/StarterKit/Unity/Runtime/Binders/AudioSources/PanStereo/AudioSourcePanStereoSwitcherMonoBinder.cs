using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent,T}">SwitcherMonoBinder&lt;AudioSource, float&gt;</see> that switches the <see cref="AudioSource.panStereo"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [−1, 1] before being applied to <see cref="AudioSource.panStereo"/>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – PanStereo Switcher")]
    public sealed class AudioSourcePanStereoSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.panStereo = value;

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.panStereo"/> property.
        /// Clamps the converted value to the valid range of −1 to 1.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        protected override float GetConvertedValue(float value) =>
            this.SafeClamp(base.GetConvertedValue(value), -1, 1);
    }
}