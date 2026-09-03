using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that binds the sibling index of a <see cref="Transform"/>.
    /// </summary>
    /// <remarks>
    /// The index is clamped to the existing siblings, so <see cref="BindMode.OneWayToSource"/> reports where
    /// the object actually is.
    /// </remarks>
    [GenerateSerializableBinder]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [AddBinderContextMenu(typeof(Transform))]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Sibling Index")]
    public partial class TransformSiblingIndexMonoBinder : ComponentMonoBinder<Transform>,
        IBinder<int>,
        IReverseBinder<int>
    {
        /// <inheritdoc/>
        public event Action<int> ValueChanged;

        /// <summary>
        /// Moves the transform to <paramref name="value"/> among its siblings.
        /// </summary>
        /// <param name="value">The index received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value)
        {
            var parent = CachedComponent.parent;
            var last = parent ? parent.childCount - 1 : 0;

            CachedComponent.SetSiblingIndex(Mathf.Clamp(value, 0, last));
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(CachedComponent.GetSiblingIndex());
        }
    }
}
