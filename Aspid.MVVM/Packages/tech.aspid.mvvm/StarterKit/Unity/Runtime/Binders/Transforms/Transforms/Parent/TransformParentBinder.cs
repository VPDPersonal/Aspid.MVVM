#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetObjectBinder{T1, T2}">TargetObjectBinder&lt;Transform, Transform&gt;</see> that binds
    /// <see cref="Transform.parent"/>.
    /// </summary>
    /// <remarks>
    /// Reparenting is how an item moves from the world into a slot, from one slot to another, or back out — and nothing
    /// in the package could express it.
    /// <para/>
    /// The transform keeps its local position and rotation, which is what a UI slot wants: the item lands where the
    /// slot is. A destroyed parent arrives as <see langword="null"/>, which detaches the object to the scene root
    /// rather than throwing.
    /// </remarks>
    [Serializable]
    public class TransformParentBinder : TargetObjectBinder<Transform, Transform>
    {
        /// <inheritdoc/>
        protected sealed override Transform? Property
        {
            get => Target.parent;
            set => Target.SetParent(value, worldPositionStays: false);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public TransformParentBinder(Transform target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
