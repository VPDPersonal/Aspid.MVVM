using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ThrottleMonoBinder{T}">ThrottleMonoBinder&lt;float&gt;</see> that lets at most one value through per interval for a number.
    /// </summary>
    /// <remarks>
    /// The case this closure exists for: a position or a timer that publishes every frame.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Throttle Binder – Float")]
    [AddBinderContextMenuByType(typeof(float))]
    public sealed partial class ThrottleFloatMonoBinder : ThrottleMonoBinder<float>, INumberBinder
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
