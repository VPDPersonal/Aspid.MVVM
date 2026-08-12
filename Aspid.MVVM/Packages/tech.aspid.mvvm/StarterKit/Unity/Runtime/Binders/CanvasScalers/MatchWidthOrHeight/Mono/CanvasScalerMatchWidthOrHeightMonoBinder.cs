using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{CanvasScaler}"/> that binds <see cref="CanvasScaler.matchWidthOrHeight"/>.
    /// </summary>
    /// <remarks>
    /// Whether the canvas follows the screen's width, its height, or something between — the value that
    /// decides how a layout behaves on an aspect ratio it was not designed for. Only read when
    /// <see cref="CanvasScaler.screenMatchMode"/> is
    /// <see cref="CanvasScaler.ScreenMatchMode.MatchWidthOrHeight"/>. Clamped to 0..1, the range Unity
    /// itself documents.
    /// </remarks>
    [AddBinderContextMenu(typeof(CanvasScaler), serializePropertyNames: "m_MatchWidthOrHeight")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasScaler/CanvasScaler Binder – Match Width Or Height")]
    public class CanvasScalerMatchWidthOrHeightMonoBinder : ComponentFloatMonoBinder<CanvasScaler>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.matchWidthOrHeight;
            set => CachedComponent.matchWidthOrHeight = BinderMath.SafeClamp01(value);
        }
    }
}
