using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="TweenMonoBinder{T}">TweenMonoBinder&lt;float&gt;</see> that also implements
    /// <see cref="INumberBinder"/>, easing a number toward each value it receives.
    /// </summary>
    /// <remarks>
    /// The health bar case: the ViewModel publishes the number it holds and the bar catches up over time.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/Tween/Tween Binder – Float")]
    [AddBinderContextMenuByType(typeof(float))]
    public sealed partial class TweenFloatMonoBinder : TweenMonoBinder<float>, INumberBinder
    {
        /// <summary>
        /// Casts the value to <see langword="float"/> and eases toward it.
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

        /// <inheritdoc/>
        protected override float Interpolate(float from, float to, float progress) =>
            Mathf.LerpUnclamped(from, to, progress);
    }
}
