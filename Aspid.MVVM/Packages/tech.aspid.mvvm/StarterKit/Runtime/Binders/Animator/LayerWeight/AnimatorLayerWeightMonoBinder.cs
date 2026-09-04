using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that binds the weight of one animator layer.
    /// </summary>
    /// <remarks>
    /// The weight is clamped to [0, 1]. A layer the controller does not have is reported.
    /// </remarks>
    [GenerateSerializableBinder]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Layer Weight")]
    public partial class AnimatorLayerWeightMonoBinder : ComponentMonoBinder<Animator>,
        IFloatBinder,
        IReverseBinder<float>
    {
        [Tooltip("Animator layer to bind; layer 0 always has full weight.")]
        [SerializeField] [Min(0)] private int _layer = 1;

        /// <inheritdoc/>
        public event Action<float> ValueChanged;

        /// <summary>
        /// Sets the layer weight, clamped to [0, 1].
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value)
        {
            if (HasLayer())
                CachedComponent.SetLayerWeight(_layer, this.SafeClamp01(value));
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource && HasLayer())
                ValueChanged?.Invoke(CachedComponent.GetLayerWeight(_layer));
        }

        private bool HasLayer()
        {
            if (_layer < CachedComponent.layerCount) return true;

            this.LogError(
                problem: $"the controller has no layer {_layer}",
                consequence: "The weight is not applied.");

            return false;
        }
    }
}
