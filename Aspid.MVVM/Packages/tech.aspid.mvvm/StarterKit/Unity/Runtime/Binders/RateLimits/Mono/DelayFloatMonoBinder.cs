using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="DelayMonoBinder{T}">DelayMonoBinder&lt;float&gt;</see> that forwards every value, late for a number.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Delay Binder – Float")]
    [AddBinderContextMenuByType(typeof(float))]
    public sealed partial class DelayFloatMonoBinder : DelayMonoBinder<float>, INumberBinder
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
