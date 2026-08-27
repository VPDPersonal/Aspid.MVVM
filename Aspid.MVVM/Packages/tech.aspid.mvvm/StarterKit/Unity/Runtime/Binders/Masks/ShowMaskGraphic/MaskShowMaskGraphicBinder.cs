#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Mask}"/> that binds <see cref="Mask.showMaskGraphic"/>.
    /// </summary>
    [Serializable]
    public class MaskShowMaskGraphicBinder : TargetBoolBinder<Mask>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.showMaskGraphic;
            set => Target.showMaskGraphic = value;
        }

        /// <inheritdoc/>
        public MaskShowMaskGraphicBinder(
            Mask target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
