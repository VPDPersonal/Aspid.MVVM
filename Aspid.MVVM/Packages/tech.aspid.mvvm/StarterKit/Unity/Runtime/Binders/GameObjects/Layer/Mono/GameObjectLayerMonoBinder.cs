using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IBinder{T}">IBinder&lt;int&gt;</see> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;int&gt;</see> that sets the
    /// <see cref="GameObject.layer"/> of the object this component is attached to.
    /// </summary>
    /// <remarks>
    /// The domain had a Tag binder and no Layer one, though the layer is the property gameplay actually reads: what a
    /// raycast hits, what a camera renders, what collides with what. Moving an object to a "ghost" or "no-collision"
    /// layer for the duration of a state is a ViewModel decision.
    /// <para/>
    /// Only the object itself changes layer, not its children — the same as assigning the property by hand. Values
    /// outside the 32 layers Unity has are refused with an error rather than written: Unity silently keeps the
    /// previous layer for them.
    /// <para/>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current layer is sent back to
    /// the ViewModel.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Layer")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Layer")]
    public sealed partial class GameObjectLayerMonoBinder : MonoBinder, IBinder<int>, IReverseBinder<int>
    {
        private const int MaxLayer = 31;

        /// <inheritdoc/>
        public event Action<int> ValueChanged;

        /// <summary>
        /// Sets <see cref="GameObject.layer"/> to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The layer index received from the ViewModel.</param>
        /// <remarks>
        /// Logs an error and writes nothing when the index names no layer.
        /// </remarks>
        [BinderLog]
        public void SetValue(int value)
        {
            if (value is < 0 or > MaxLayer)
            {
                Debug.LogError($"[{nameof(GameObjectLayerMonoBinder)}] Layer {value} does not exist; ignored.", context: this);
                return;
            }

            gameObject.layer = value;
        }

        /// <summary>
        /// Called when the binder is bound. Sends the current layer to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(gameObject.layer);
        }
    }
}
