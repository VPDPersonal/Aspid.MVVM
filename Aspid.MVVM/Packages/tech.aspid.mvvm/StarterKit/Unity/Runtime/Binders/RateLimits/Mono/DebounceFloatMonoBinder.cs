using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="DebounceMonoBinder{T}">DebounceMonoBinder&lt;float&gt;</see> that holds a value until the values stop for a number.
    /// </summary>
    /// <remarks>
    /// The case this closure exists for: a search field that queries once the user pauses.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Debounce Binder – Float")]
    [AddBinderContextMenuByType(typeof(float))]
    public sealed partial class DebounceFloatMonoBinder : DebounceMonoBinder<float>, INumberBinder
    {
        /// <summary>
        /// Casts the value to <see langword="float"/> and hands it to the policy.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value) => SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        [BinderLog]
        public void SetValue(long value) => SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        /// <remarks>
        /// Narrowed to <see langword="float"/> — precision may be lost.
        /// </remarks>
        [BinderLog]
        public void SetValue(double value) => SetValue((float)value);
    }
}
