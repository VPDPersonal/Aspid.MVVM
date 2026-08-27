#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector2Binder{CanvasScaler}"/> that binds <see cref="CanvasScaler.referenceResolution"/>.
    /// </summary>
    /// <remarks>
    /// Only read when <see cref="CanvasScaler.uiScaleMode"/> is
    /// <see cref="CanvasScaler.ScaleMode.ScaleWithScreenSize"/>. Each component is clamped to at least <c>1</c>,
    /// since the scaler divides the screen size by this value.
    /// </remarks>
    [Serializable]
    public class CanvasScalerReferenceResolutionBinder : TargetVector2Binder<CanvasScaler>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.referenceResolution;
            set => Target.referenceResolution = new Vector2(BinderMath.SafeClamp(value.x, 1f, float.MaxValue), BinderMath.SafeClamp(value.y, 1f, float.MaxValue));
        }

        /// <inheritdoc/>
        public CanvasScalerReferenceResolutionBinder(
            CanvasScaler target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
