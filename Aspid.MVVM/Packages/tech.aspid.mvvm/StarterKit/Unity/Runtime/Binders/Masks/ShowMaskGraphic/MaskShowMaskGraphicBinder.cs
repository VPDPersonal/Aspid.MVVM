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
    /// <remarks>
    /// Whether the graphic that defines the mask is drawn as well as used. Turning it on and off is the
    /// difference between a frame around an avatar and an invisible cut-out, and it was the one property of the
    /// component worth binding.
    /// </remarks>
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
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
