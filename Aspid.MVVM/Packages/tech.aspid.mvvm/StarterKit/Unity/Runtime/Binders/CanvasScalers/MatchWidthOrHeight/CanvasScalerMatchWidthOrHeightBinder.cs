#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{CanvasScaler}"/> that binds <see cref="CanvasScaler.matchWidthOrHeight"/>.
    /// </summary>
    /// <remarks>
    /// Whether the canvas follows the screen's width, its height, or something between — the value that
    /// decides how a layout behaves on an aspect ratio it was not designed for. Only read when
    /// <see cref="CanvasScaler.screenMatchMode"/> is
    /// <see cref="CanvasScaler.ScreenMatchMode.MatchWidthOrHeight"/>. Clamped to 0..1, the range Unity
    /// itself documents.
    /// </remarks>
    [Serializable]
    public class CanvasScalerMatchWidthOrHeightBinder : TargetFloatBinder<CanvasScaler>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.matchWidthOrHeight;
            set => Target.matchWidthOrHeight = BinderMath.SafeClamp01(value);
        }

        /// <inheritdoc/>
        public CanvasScalerMatchWidthOrHeightBinder(
            CanvasScaler target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
