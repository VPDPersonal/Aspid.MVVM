#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{ScrollRect}"/> that binds <see cref="ScrollRect.horizontal"/>.
    /// </summary>
    /// <remarks>
    /// Whether the user may scroll sideways — the way a ViewModel locks an axis while something else is in progress.
    /// </remarks>
    [Serializable]
    public class ScrollRectHorizontalBinder : TargetBoolBinder<ScrollRect>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.horizontal;
            set => Target.horizontal = value;
        }

        /// <inheritdoc/>
        public ScrollRectHorizontalBinder(
            ScrollRect target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
