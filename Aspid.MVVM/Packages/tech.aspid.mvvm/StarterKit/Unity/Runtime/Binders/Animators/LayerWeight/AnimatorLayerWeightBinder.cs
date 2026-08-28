#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Animator}"/> implementing <see cref="IFloatBinder"/> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;float&gt;</see> that binds the weight of one animator layer.
    /// </summary>
    /// <remarks>
    /// Clamped to 0..1; a non-finite value lands on zero.
    /// </remarks>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class AnimatorLayerWeightBinder : TargetBinder<Animator>, 
        IFloatBinder,
        IReverseBinder<float>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("Index of the animator layer to bind. Layer 0 is ignored by the animator.")]
        [SerializeField] [Min(0)] private int _layer = 1;
        
        /// <inheritdoc/>
        public event Action<float>? ValueChanged;

        /// <param name="target">The <see cref="Animator"/> whose layer weight is bound.</param>
        /// <param name="layer">Index of the layer whose weight is bound. Layer 0 is ignored by the animator.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — a layer weight raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AnimatorLayerWeightBinder(
            Animator target,
            int layer = 1,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            _layer = layer;
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Sets the weight of the configured layer, clamped to 0..1.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(float value)
        {
            if (!HasLayer()) return;
            
            Target.SetLayerWeight(
                layerIndex: _layer,
                weight: this.SafeClamp01(value, Target));
        }

        /// <summary>
        /// Called when the binder is bound. Sends the layer's current weight to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>, and reports a layer index the controller does not have.
        /// </summary>
        protected override void OnBound()
        {
            if (!HasLayer()) return;
            
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(Target.GetLayerWeight(_layer));
        }

        private bool HasLayer()
        {
            if (_layer < Target.layerCount) return true;
            
            this.LogError(
                problem: $"the controller has no layer {_layer}", 
                consequence: "The weight is not applied.", context: Target);
            
            return false;
        }
    }
}
