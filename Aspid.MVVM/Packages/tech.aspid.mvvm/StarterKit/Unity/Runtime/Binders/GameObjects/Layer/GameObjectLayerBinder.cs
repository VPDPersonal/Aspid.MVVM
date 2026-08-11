#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{GameObject}"/> implementing <see cref="IBinder{T}">IBinder&lt;int&gt;</see> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;int&gt;</see> that sets <see cref="GameObject.layer"/>.
    /// </summary>
    /// <remarks>
    /// The domain had a Tag binder and no Layer one, though the layer is the property gameplay actually reads: what a
    /// raycast hits, what a camera renders, what collides with what.
    /// <para/>
    /// Only the object itself changes layer, not its children. Values outside the 32 layers Unity has are refused with
    /// an error rather than written: Unity silently keeps the previous layer for them.
    /// </remarks>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class GameObjectLayerBinder : TargetBinder<GameObject>, IBinder<int>, IReverseBinder<int>
    {
        private const int MaxLayer = 31;

        /// <inheritdoc/>
        public event Action<int>? ValueChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="GameObjectLayerBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="GameObject"/> whose layer is set.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the layer raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public GameObjectLayerBinder(GameObject target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Sets <see cref="GameObject.layer"/> to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The layer index received from the ViewModel.</param>
        /// <remarks>
        /// Logs an error and writes nothing when the index names no layer.
        /// </remarks>
        public void SetValue(int value)
        {
            if (value is < 0 or > MaxLayer)
            {
                Debug.LogError($"[{nameof(GameObjectLayerBinder)}] Layer {value} does not exist; ignored.", Target);
                return;
            }

            Target.layer = value;
        }

        /// <summary>
        /// Called when the binder is bound. Sends the current layer to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(Target.layer);
        }
    }
}
