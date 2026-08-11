using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector2MonoBinder{CanvasScaler}"/> that binds <see cref="CanvasScaler.referenceResolution"/>.
    /// </summary>
    /// <remarks>
    /// The resolution the layout was designed against — what a game switches when it ships one UI for phones
    /// and another for tablets. Only read when <see cref="CanvasScaler.uiScaleMode"/> is
    /// <see cref="CanvasScaler.ScaleMode.ScaleWithScreenSize"/>. Each component is clamped to at least one:
    /// the scaler divides the screen size by this value, so a zero or a non-finite one would scale the whole
    /// canvas to infinity.
    /// </remarks>
    [AddBinderContextMenu(typeof(CanvasScaler), serializePropertyNames: "m_ReferenceResolution")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasScaler/CanvasScaler Binder – Reference Resolution")]
    public class CanvasScalerReferenceResolutionMonoBinder : ComponentVector2MonoBinder<CanvasScaler>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.referenceResolution;
            set => CachedComponent.referenceResolution = new Vector2(BinderMath.SafeClamp(value.x, 1f, float.MaxValue), BinderMath.SafeClamp(value.y, 1f, float.MaxValue));
        }
    }
}
