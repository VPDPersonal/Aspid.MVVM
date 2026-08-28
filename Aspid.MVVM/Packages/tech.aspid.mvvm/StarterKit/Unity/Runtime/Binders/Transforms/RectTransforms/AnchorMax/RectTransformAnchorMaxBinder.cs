#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{RectTransform, Vector2}"/> that binds <see cref="RectTransform.anchorMax"/>.
    /// </summary>
    /// <remarks>
    /// Where the element's upper-right corner is pinned inside its parent, as a fraction. Together with the minimum
    /// anchor it decides whether the element keeps a size or stretches with its parent.
    /// <para/>
    /// Values outside 0..1 are legal — that is how an element is stretched past its parent — so only a
    /// non-finite one is refused: the rect is computed from these numbers and one <c>NaN</c> takes the element
    /// off the screen.
    /// </remarks>
    [Serializable]
    public class RectTransformAnchorMaxBinder : TargetBinder<RectTransform, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.anchorMax;
            set
            {
                if (!this.RequireFinite(value, Target)) return;
                Target.anchorMax = value;
            }
        }

        /// <inheritdoc/>
        public RectTransformAnchorMaxBinder(
            RectTransform target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
