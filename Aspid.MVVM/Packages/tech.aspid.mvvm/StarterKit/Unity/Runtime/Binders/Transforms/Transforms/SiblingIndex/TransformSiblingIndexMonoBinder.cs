using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Transform}"/> implementing <see cref="IBinder{T}">IBinder&lt;int&gt;</see> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;int&gt;</see> that binds
    /// <see cref="Transform.GetSiblingIndex"/> / <see cref="Transform.SetSiblingIndex"/>.
    /// </summary>
    /// <remarks>
    /// The index is clamped to the siblings that exist. Unity clamps it too, so this is about the reverse channel: a
    /// ViewModel in <see cref="BindMode.OneWayToSource"/> is told where the object actually ended up.
    /// </remarks>
    [GenerateSerializableBinder]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [AddBinderContextMenu(typeof(Transform))]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Sibling Index")]
    public partial class TransformSiblingIndexMonoBinder : ComponentMonoBinder<Transform>, IBinder<int>, IReverseBinder<int>
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

        /// <summary>
        /// Called when the binder is bound. Sends the current sibling index to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(CachedComponent.GetSiblingIndex());
        }
    }
}
