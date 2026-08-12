#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{MaskableGraphic}"/> that sets the <see cref="MaskableGraphic.maskable"/> property.
    /// </summary>
    /// <remarks>
    /// Turning this off exempts the graphic from any enclosing mask, so it draws outside the masked area.
    /// </remarks>
    [Serializable]
    public class GraphicMaskableBinder : TargetBoolBinder<MaskableGraphic>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.maskable;
            set => Target.maskable = value;
        }

        /// <inheritdoc/>
        public GraphicMaskableBinder(MaskableGraphic target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
