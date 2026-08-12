#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Animator}"/> implementing <see cref="INumberBinder"/> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;float&gt;</see> that binds the weight of one animator layer.
    /// </summary>
    /// <remarks>
    /// Layer weight is how an additive layer is faded in: an injured limp over a walk, an aim pose over a stance.
    /// Clamped to 0..1; a non-finite value lands on zero. A layer index the controller does not have is reported once
    /// rather than on every value.
    /// </remarks>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class AnimatorLayerWeightBinder : TargetBinder<Animator>, INumberBinder, IReverseBinder<float>
    {
        /// <inheritdoc/>
        public event Action<float>? ValueChanged;

        [Tooltip("Index of the animator layer whose weight is bound. Layer 0 is the base layer, whose weight the animator ignores.")]
        [SerializeField] [Min(0)] private int _layer;

        /// <summary>
        /// Initializes a new instance of <see cref="AnimatorLayerWeightBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Animator"/> whose layer weight is bound.</param>
        /// <param name="layer">Index of the layer whose weight is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — a layer weight raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AnimatorLayerWeightBinder(Animator target, int layer = 1, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _layer = layer;
        }

        /// <summary>
        /// Casts the value to <see langword="float"/> and sets the layer's weight.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(int value) => SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        public void SetValue(long value) => SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        /// <remarks>
        /// Narrowed to <see langword="float"/> — precision may be lost.
        /// </remarks>
        public void SetValue(double value) => SetValue((float)value);

        /// <summary>
        /// Sets the weight of the configured layer, clamped to 0..1.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(float value)
        {
            if (!HasLayer()) return;
            Target.SetLayerWeight(_layer, BinderMath.SafeClamp01(value));
        }

        /// <summary>
        /// Called when the binder is bound. Sends the layer's current weight to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>, and reports a layer index the controller does not have.
        /// </summary>
        protected override void OnBound()
        {
            if (!HasLayer()) return;
            if (Mode is BindMode.OneWayToSource) ValueChanged?.Invoke(Target.GetLayerWeight(_layer));
        }

        private bool HasLayer()
        {
            if (_layer >= 0 && _layer < Target.layerCount) return true;

            Debug.LogError($"[{nameof(AnimatorLayerWeightBinder)}] Layer {_layer} does not exist on this controller.", Target);
            return false;
        }
    }
}
