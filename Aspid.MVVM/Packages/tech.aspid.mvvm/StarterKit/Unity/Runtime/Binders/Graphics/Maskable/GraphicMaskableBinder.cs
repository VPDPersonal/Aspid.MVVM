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
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public GraphicMaskableBinder(MaskableGraphic target, IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneTime)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
