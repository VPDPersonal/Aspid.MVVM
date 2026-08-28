#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{GridLayoutGroup}"/> that binds <see cref="GridLayoutGroup.constraintCount"/>.
    /// </summary>
    /// <remarks>
    /// Meaningful only while <see cref="GridLayoutGroup.constraint"/> names an axis to count. Not clamped
    /// here — Unity itself raises anything below one to one.
    /// </remarks>
    [Serializable]
    public class GridLayoutGroupConstraintCountBinder : TargetIntBinder<GridLayoutGroup>
    {
        /// <inheritdoc/>
        public GridLayoutGroupConstraintCountBinder(
            GridLayoutGroup target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.constraintCount;
            set => Target.constraintCount = value;
        }
    }
}
