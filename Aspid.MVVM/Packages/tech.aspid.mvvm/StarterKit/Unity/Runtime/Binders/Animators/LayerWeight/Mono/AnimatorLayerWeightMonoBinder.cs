using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Animator}"/> implementing <see cref="INumberBinder"/> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;float&gt;</see> that binds the weight of one animator layer.
    /// </summary>
    /// <remarks>
    /// Clamped to 0..1; a non-finite value lands on zero.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Layer Weight")]
    public partial class AnimatorLayerWeightMonoBinder : ComponentMonoBinder<Animator>, INumberBinder, IReverseBinder<float>
    {
        /// <inheritdoc/>
        public event Action<float> ValueChanged;

        [Tooltip("Index of the animator layer to bind. Layer 0 is ignored by the animator.")]
        [SerializeField] [Min(0)] private int _layer = 1;

        /// <summary>
        /// Casts the value to <see langword="float"/> and sets the layer's weight.
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

        /// <summary>
        /// Sets the weight of the configured layer, clamped to 0..1.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value)
        {
            if (!HasLayer()) return;
            CachedComponent.SetLayerWeight(_layer, BinderMath.SafeClamp01(value));
        }

        /// <summary>
        /// Called when the binder is bound. Sends the layer's current weight to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>, and reports a layer index the controller does not have.
        /// </summary>
        protected override void OnBound()
        {
            if (!HasLayer()) return;
            if (Mode is BindMode.OneWayToSource) ValueChanged?.Invoke(CachedComponent.GetLayerWeight(_layer));
        }

        private bool HasLayer()
        {
            if (_layer >= 0 && _layer < CachedComponent.layerCount) return true;

            Debug.LogError($"[{nameof(AnimatorLayerWeightMonoBinder)}] Layer {_layer} does not exist on this controller.", context: this);
            return false;
        }
    }
}
