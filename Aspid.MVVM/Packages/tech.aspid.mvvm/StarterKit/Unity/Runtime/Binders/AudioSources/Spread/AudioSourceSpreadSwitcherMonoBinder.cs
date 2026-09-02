using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent,T}">SwitcherMonoBinder&lt;AudioSource, float&gt;</see> that switches the <see cref="AudioSource.spread"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 360] before being applied to <see cref="AudioSource.spread"/>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Spread Switcher")]
    public sealed class AudioSourceSpreadSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.spread = value;

        /// <summary>
        /// Called when converting the selected value before applying it to the <see cref="AudioSource.spread"/> property.
        /// Clamps the converted value to the valid range of 0 to 360.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        protected override float GetConvertedValue(float value) =>
            this.SafeClamp(base.GetConvertedValue(value), 0, 360);
    }
}