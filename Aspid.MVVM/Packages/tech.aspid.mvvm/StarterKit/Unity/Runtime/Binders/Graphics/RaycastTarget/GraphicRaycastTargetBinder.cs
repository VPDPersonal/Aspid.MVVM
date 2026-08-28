#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Graphic, bool}"/> that sets the <see cref="Graphic.raycastTarget"/> property.
    /// </summary>
    /// <remarks>
    /// Turning this off makes the graphic invisible to pointer input while it stays on screen — the usual way to let clicks pass through an overlay.
    /// </remarks>
    [Serializable]
    public class GraphicRaycastTargetBinder : TargetBinder<Graphic, bool>
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public GraphicRaycastTargetBinder(Graphic target, IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneTime)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.raycastTarget;
            set => Target.raycastTarget = value;
        }
    }
}
