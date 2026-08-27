#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;GridLayoutGroup, GridLayoutGroup.Constraint&gt;</see> that binds
    /// <see cref="GridLayoutGroup.constraint"/>.
    /// </summary>
    /// <remarks>
    /// Paired with <see cref="GridLayoutGroupConstraintCountBinder"/> — the count means nothing until this
    /// names which axis it counts.
    /// </remarks>
    [Serializable]
    public class GridLayoutGroupConstraintBinder : TargetBinder<GridLayoutGroup, GridLayoutGroup.Constraint>
    {
        /// <inheritdoc/>
        protected sealed override GridLayoutGroup.Constraint Property
        {
            get => Target.constraint;
            set => Target.constraint = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public GridLayoutGroupConstraintBinder(GridLayoutGroup target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
