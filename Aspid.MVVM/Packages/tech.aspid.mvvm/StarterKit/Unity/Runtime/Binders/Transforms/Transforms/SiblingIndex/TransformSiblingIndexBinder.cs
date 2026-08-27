#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Transform}"/> implementing <see cref="IBinder{T}">IBinder&lt;int&gt;</see> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;int&gt;</see> that binds
    /// <see cref="Transform.GetSiblingIndex"/> / <see cref="Transform.SetSiblingIndex"/>.
    /// </summary>
    /// <remarks>
    /// Sibling order is draw order in a UI: it is what brings a panel to the front and what reorders a list the player
    /// dragged. The index is clamped to the siblings that exist, so a ViewModel bound in
    /// <see cref="BindMode.OneWayToSource"/> is told where the object actually ended up.
    /// </remarks>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class TransformSiblingIndexBinder : TargetBinder<Transform>, IBinder<int>, IReverseBinder<int>
    {
        /// <inheritdoc/>
        public event Action<int>? ValueChanged;

        /// <param name="target">The <see cref="Transform"/> to reorder.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — sibling order raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public TransformSiblingIndexBinder(Transform target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Moves the transform to <paramref name="value"/> among its siblings.
        /// </summary>
        /// <param name="value">The index received from the ViewModel.</param>
        public void SetValue(int value)
        {
            var parent = Target.parent;
            var last = parent ? parent.childCount - 1 : 0;

            Target.SetSiblingIndex(Mathf.Clamp(value, 0, last));
        }

        /// <summary>
        /// Called when the binder is bound. Sends the current sibling index to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(Target.GetSiblingIndex());
        }
    }
}
