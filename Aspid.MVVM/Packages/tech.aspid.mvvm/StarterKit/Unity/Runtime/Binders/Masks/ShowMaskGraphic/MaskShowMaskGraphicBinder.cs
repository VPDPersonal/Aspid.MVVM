#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Mask, bool}"/> that binds <see cref="Mask.showMaskGraphic"/>.
    /// </summary>
    [Serializable]
    public class MaskShowMaskGraphicBinder : TargetBinder<Mask, bool>
    {
        /// <inheritdoc/>
        public MaskShowMaskGraphicBinder(
            Mask target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.showMaskGraphic;
            set => Target.showMaskGraphic = value;
        }
    }
}
