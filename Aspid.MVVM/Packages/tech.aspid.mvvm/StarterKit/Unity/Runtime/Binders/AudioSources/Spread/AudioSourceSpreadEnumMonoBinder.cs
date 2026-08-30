using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent,TValue}">EnumMonoBinder&lt;AudioSource, float&gt;</see> that sets the <see cref="AudioSource.spread"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 360] before being applied to <see cref="AudioSource.spread"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Spread Enum")]
    public sealed class AudioSourceSpreadEnumMonoBinder : EnumMonoBinder<AudioSource, float>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets <see cref="AudioSource.spread"/> clamped to the valid range of 0 to 360.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value) =>
            CachedComponent.spread = this.SafeClamp(value, 0, 360f);
    }
}